namespace phoenix
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.IO.Compression;

    class RsyncClient : IDisposable
    {
        private static readonly string s_ClientDirectory = Path.Combine(Path.GetTempPath(), "phoenix");
        private static readonly string s_PrivateKeyPath  = Path.Combine(s_ClientDirectory, MachineIdentity);
        private static readonly string s_PublicKeyPath   = Path.Combine(s_ClientDirectory, string.Format("{0}.pub",MachineIdentity));
        private static readonly string s_ResourceUri     = "phoenix.Resources.rsync.zip";
        private Process                m_RsyncProcess    = null;

        public static string PathToCygwinPath(string path)
        {
            if (String.IsNullOrWhiteSpace(path) || path.StartsWith("/cygdrive/"))
                return path;

            if (Path.IsPathRooted(path))
            {
                path = path.Replace("\\", "/");
                path = path.Replace(":", string.Empty);
                path = string.Format("/cygdrive/{0}", path);
            }

            return path.Trim();
        }

        public static string PrivateKey
        {
            get
            {
                if (!KeysExist)
                    GenerateKeys();

                try { return File.ReadAllText(s_PrivateKeyPath); }
                catch { return "Private Key cannot be read."; }
            }

            set
            {
                try { File.WriteAllText(s_PrivateKeyPath, value); }
                catch { Logger.Error("Unable to write private key."); }
            }
        }
        public static string PublicKey
        {
            get
            {
                if (!KeysExist)
                    GenerateKeys();

                try { return File.ReadAllText(s_PublicKeyPath); }
                catch { return "Public Key cannot be read."; }
            }

            set
            {
                try { File.WriteAllText(s_PublicKeyPath, value); }
                catch { Logger.Error("Unable to write public key."); }
            }
        }
        private static bool ClientExtracted
        {
            get
            {
                return
                    File.Exists(Path.Combine(s_ClientDirectory, "ssh.exe")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "rsync.exe")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "ssh-keygen.exe")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cygz.dll")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cygwin1.dll")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cygssp-0.dll")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cygiconv-2.dll")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cyggcc_s-1.dll")) &&
                    File.Exists(Path.Combine(s_ClientDirectory, "cygcrypto-1.0.0.dll"));
            }
        }
        private static bool KeysExist
        {
            get
            {
                return
                    File.Exists(s_PublicKeyPath) &&
                    File.Exists(s_PrivateKeyPath);
            }
        }
        public static string MachineIdentity
        {
            get
            {
                string machine_name = Environment.MachineName;

                foreach (var c in Path.GetInvalidPathChars())
                    machine_name = machine_name.Replace(c.ToString(), string.Empty);

                foreach (var c in Path.GetInvalidFileNameChars())
                    machine_name = machine_name.Replace(c.ToString(), string.Empty);

                machine_name = machine_name.Replace(" ", "_");

                return machine_name.Trim();
            }
        }
        private static bool ExtractClient()
        {
            try
            {
                if (!Directory.Exists(s_ClientDirectory))
                    Directory.CreateDirectory(s_ClientDirectory);

                if (ClientExtracted)
                    return true;

                using (ZipArchive archive = new ZipArchive(
                    Assembly.
                    GetExecutingAssembly().
                    GetManifestResourceStream(s_ResourceUri)))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string extract_path = Path.Combine(s_ClientDirectory, entry.Name);

                        if (!File.Exists(extract_path))
                        {
                            entry.ExtractToFile(extract_path, true);
                        }
                    }
                }

                // this checks if everything is extracted
                return ClientExtracted;
            }
            catch
            {
                Logger.Error("[RSYNC] Unable to extract rsync from resources.");
                return false;
            }
        }

        private static bool GenerateKeys()
        {
            if (!ClientExtracted && !ExtractClient())
            {
                Logger.Error("[RSYNC] Cannot generate keys because client does not exist.");
                return false;
            }

            try
            {
                ProcessStartInfo process_info = new ProcessStartInfo();
                process_info.Arguments = string.Format("-q -t rsa -f '{0}' -N ''", MachineIdentity);
                process_info.FileName = Path.Combine(s_ClientDirectory, "ssh-keygen.exe");
                process_info.WorkingDirectory = s_ClientDirectory;
                process_info.UseShellExecute = false;
                process_info.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = process_info;
                    process.Start();
                    process.WaitForExit();
                }

                // double checks both keys exist
                return KeysExist;
            }
            catch
            {
                Logger.Error("[RSYNC] Unable to generate key pairs.");
                return false;
            }
        }

        public static void RegenerateKeys()
        {
            try
            {
                if (KeysExist)
                {
                    File.Delete(s_PublicKeyPath);
                    File.Delete(s_PrivateKeyPath);
                }
            }
            catch
            {
                Logger.Error("[RSYNC] Unable to remove old key pairs.");
                return;
            }

            if (!GenerateKeys())
            {
                Logger.Error("[RSYNC] Unable to re-generate key pairs.");
            }
        }

        public void Execute(
            string remote,
            string local,
            string username,
            string address,
            ushort port,
            Action pre_update,
            Action post_update)
        {
            if (String.IsNullOrWhiteSpace(remote) ||
                String.IsNullOrWhiteSpace(local) ||
                String.IsNullOrWhiteSpace(username) ||
                String.IsNullOrWhiteSpace(address))
                return;

            if (!ClientExtracted && !ExtractClient())
            {
                Logger.Error("[RSYNC] Cannot execute because client does not exist.");
                return;
            }

            if (!KeysExist && !GenerateKeys())
            {
                Logger.Error("[RSYNC] Cannot execute because keys do not exist.");
                return;
            }

            // Make sure we have an established home directory for SSH
            string home_directory = Path.Combine(s_ClientDirectory, "home");

            if (!Directory.Exists(home_directory))
                Directory.CreateDirectory(home_directory);

            ProcessStartInfo start_info = new ProcessStartInfo();
            start_info.FileName = Path.Combine(s_ClientDirectory, "rsync.exe");
            start_info.EnvironmentVariables["HOME"] = home_directory;
            start_info.EnvironmentVariables["PATH"] = s_ClientDirectory;
            start_info.WorkingDirectory = s_ClientDirectory;
            start_info.UseShellExecute = false;
            start_info.Arguments = string.Format(
                "-ravz -e \"ssh -p {0} -i '{1}' -o StrictHostKeyChecking=no\" {2}@{3}:{4} {5}",
                port,
                MachineIdentity,
                username,
                address,
                remote,
                local);
            start_info.RedirectStandardError = true;
            start_info.RedirectStandardOutput = true;
            start_info.CreateNoWindow = true;

            try
            {
                if (m_RsyncProcess != null)
                {
                    m_RsyncProcess.Refresh();
                    if (!m_RsyncProcess.HasExited)
                    {
                        m_RsyncProcess.CloseMainWindow();
                        m_RsyncProcess.Close();
                    }
                }
            }
            catch
            {
                Logger.Warn("[RSYNC] Unable to shutdown previous rsync session.");
            }

            m_RsyncProcess = new Process();
            m_RsyncProcess.StartInfo = start_info;
            m_RsyncProcess.EnableRaisingEvents = true;

            m_RsyncProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Logger.Info(string.Format("[RSYNC] {0}", e.Data));
                }
            });

            m_RsyncProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Logger.Error(string.Format("[RSYNC] {0}", e.Data));
                }
            });

            if (pre_update != null)
                pre_update();

            m_RsyncProcess.Start();
            m_RsyncProcess.BeginOutputReadLine();
            m_RsyncProcess.BeginErrorReadLine();

            if (post_update != null)
            {
                int id = m_RsyncProcess.Id;
                m_RsyncProcess.Exited += new EventHandler((s, e) =>
                {
                    // make sure we are the same one, discard ow.
                    if (m_RsyncProcess.Id == id)
                    {
                        if (!m_RsyncProcess.HasExited)
                        {
                            m_RsyncProcess.Close();
                            m_RsyncProcess.WaitForExit();
                        }

                        Logger.Info("[RSYNC] session finished.");
                        post_update();
                    }
                    else
                        Logger.Error("[RSYNC] stalled a session. Did you issue an update in the middle of another one?");
                });
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && m_RsyncProcess != null)
                {
                    m_RsyncProcess.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}

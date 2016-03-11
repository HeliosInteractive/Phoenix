namespace phoenix
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.IO.Compression;

    /// <summary>
    /// Class responsible for executing RSync and handling remote
    /// updates and pulling down new versions of the application
    /// </summary>
    class RsyncClient : IDisposable
    {
        /// <summary>
        /// Directory where RSync binaries are located
        /// </summary>
        private static readonly string s_ClientDirectory = Path.Combine(Path.GetTempPath(), "phoenix");
        /// <summary>
        /// Physical location of the RSA private key
        /// </summary>
        private static readonly string s_PrivateKeyPath  = Path.Combine(s_ClientDirectory, MachineIdentity);
        /// <summary>
        /// Physical location of the RSA public key
        /// </summary>
        private static readonly string s_PublicKeyPath   = Path.Combine(s_ClientDirectory, string.Format("{0}.pub",MachineIdentity));
        /// <summary>
        /// Currently executing RSync process
        /// </summary>
        private Process                m_RsyncProcess    = null;

        public RsyncClient()
        {
            Logger.RsyncClient.InfoFormat("RSync client directory: {0}",
                s_ClientDirectory);
        }

        /// <summary>
        /// RSA Private key string, reflects the actual physical file
        /// </summary>
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
                catch { Logger.RsyncClient.Error("Unable to write private key."); }
            }
        }

        /// <summary>
        /// RSA Public key string, reflects the actual physical file
        /// </summary>
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
                catch { Logger.RsyncClient.Error("Unable to write public key."); }
            }
        }

        /// <summary>
        /// Returns true if ALL necessary files for the RSync client
        /// are extracted from its Zip archive
        /// </summary>
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

        /// <summary>
        /// Answers true if both RSA key files exist (does not validate them)
        /// </summary>
        private static bool KeysExist
        {
            get
            {
                return
                    File.Exists(s_PublicKeyPath) &&
                    File.Exists(s_PrivateKeyPath);
            }
        }

        /// <summary>
        /// Returns a string semi-unique per machine, constructed from
        /// Machine name (computer name / host name)
        /// </summary>
        public static string MachineIdentity
        {
            get
            {
                return Environment
                    .MachineName
                    .AsPath(Extensions.PathType.FileName)
                    .Replace(" ", "_")
                    .Replace("-", "_");
            }
        }

        /// <summary>
        /// Extracts the RSync client from its embedded resource
        /// </summary>
        /// <returns>true on successful extraction</returns>
        private static bool ExtractClient()
        {
            try
            {
                if (!Directory.Exists(s_ClientDirectory))
                    Directory.CreateDirectory(s_ClientDirectory);

                if (ClientExtracted)
                    return true;

                using (ZipArchive archive = new ZipArchive(
                    new MemoryStream(Properties.Resources.rsync)))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        string extract_path = Path.Combine(s_ClientDirectory, entry.Name);

                        if (!File.Exists(extract_path))
                        {
                            try
                            {
                                entry.ExtractToFile(extract_path, true);
                            }
                            catch(Exception ex)
                            {
                                Logger.RsyncClient.ErrorFormat("unable to extract {0}: {1}"
                                    , extract_path, ex.Message);
                                return false;
                            }
                        }
                    }
                }

                // this checks if everything is extracted
                return ClientExtracted;
            }
            catch
            {
                Logger.RsyncClient.Error("Unable to extract rsync from resources.");
                return false;
            }
        }

        /// <summary>
        /// Generates a new set of RSA key pairs
        /// </summary>
        /// <returns>true on successful extraction</returns>
        public static bool GenerateKeys()
        {
            if (!ClientExtracted && !ExtractClient())
            {
                Logger.RsyncClient.Error("Cannot generate keys because client does not exist.");
                return false;
            }

            if (KeysExist)
            {
                try
                {
                    File.Delete(s_PublicKeyPath);
                    File.Delete(s_PrivateKeyPath);
                }
                catch (Exception ex)
                {
                    Logger.RsyncClient.ErrorFormat("Unable to remove old key pairs: {0}", ex.Message);
                    return false;
                }
            }

            try
            {
                using (Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments           = string.Format("-q -t rsa -f '{0}' -N ''", MachineIdentity),
                        FileName            = Path.Combine(s_ClientDirectory, "ssh-keygen.exe"),
                        WorkingDirectory    = s_ClientDirectory,
                        UseShellExecute     = false,
                        CreateNoWindow      = true,
                    }
                })
                {
                    process.Start();
                    process.WaitForExit(5000);
                }

                // double checks both keys exist
                return KeysExist;
            }
            catch
            {
                Logger.RsyncClient.Error("Unable to generate key pairs.");
                return false;
            }
        }

        /// <summary>
        /// Execute and spawn an RSync session. Stops/Stalls previous calls.
        /// </summary>
        /// <param name="remote">remote directory</param>
        /// <param name="local">Local directory</param>
        /// <param name="username">SSH username</param>
        /// <param name="address">SSH address</param>
        /// <param name="port">SSH port</param>
        /// <param name="pre_update">action to be called before update begines</param>
        /// <param name="post_update">action to be called after update finishes</param>
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
            {
                Logger.RsyncClient.Error("Invalid parameters supplied.");
                return;
            }

            if (!ClientExtracted && !ExtractClient())
            {
                Logger.RsyncClient.Error("Cannot execute because client does not exist.");
                return;
            }

            if (!KeysExist && !GenerateKeys())
            {
                Logger.RsyncClient.Error("Cannot execute because keys do not exist.");
                return;
            }

            // Make sure we have an established home directory for SSH
            string home_directory = Path.Combine(s_ClientDirectory, "home");

            if (!Directory.Exists(home_directory))
            {
                try
                {
                    Directory.CreateDirectory(home_directory);
                }
                catch(Exception ex)
                {
                    Logger.RsyncClient.ErrorFormat("Unable to create the home directory: {0}", ex.Message);
                    return;
                }
            }

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
                Logger.RsyncClient.Warn("Unable to shutdown previous rsync session.");
            }

            m_RsyncProcess = new Process();
            m_RsyncProcess.StartInfo = start_info;
            m_RsyncProcess.EnableRaisingEvents = true;

            m_RsyncProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Logger.RsyncClient.Info(e.Data);
                }
            });

            m_RsyncProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    Logger.RsyncClient.Error(e.Data);
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

                        Logger.RsyncClient.Info("session finished.");
                        post_update();
                    }
                    else
                        Logger.RsyncClient.Error("stalled a session. Did you issue an update in the middle of another one?");
                });
            }
        }

        //! @cond
        #region IDisposable Support
        private bool m_Disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing && m_RsyncProcess != null)
                {
                    m_RsyncProcess.Dispose();
                }

                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
        //! @endcond
    }
}

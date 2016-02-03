namespace phoenix
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.IO.Compression;

    class RsyncClient
    {
        private static readonly string s_ClientDirectory = Path.Combine(Path.GetTempPath(), "phoenix");
        private static readonly string s_PrivateKeyPath  = Path.Combine(s_ClientDirectory, MachineIdentity);
        private static readonly string s_PublicKeyPath   = Path.Combine(s_ClientDirectory, string.Format("{0}.pub",MachineIdentity));
        private static readonly string s_ResourceUri     = "phoenix.Resources.rsync.zip";

        public static string PathToCygwinPath(string path)
        {
            if (String.IsNullOrWhiteSpace(path) || path.StartsWith("cygdrive/"))
                return path;

            if (Path.IsPathRooted(path))
            {
                path = path.Replace("\\", "/");
                path = path.Replace(":", string.Empty);
                path = string.Format("cygdrive/{0}", path);
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
                    File.Exists(Path.Combine(s_ClientDirectory, "ssh-kegen.exe")) &&
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
        private static void ExtractClient()
        {
            if (!Directory.Exists(s_ClientDirectory))
                Directory.CreateDirectory(s_ClientDirectory);

            if (ClientExtracted)
                return;

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
        }

        private static void GenerateKeys()
        {
            if (!ClientExtracted)
                ExtractClient();

            ProcessStartInfo process_info   = new ProcessStartInfo();
            process_info.Arguments          = string.Format("-q -t rsa -f '{0}' -N ''", MachineIdentity);
            process_info.FileName           = Path.Combine(s_ClientDirectory, "ssh-keygen.exe");
            process_info.WorkingDirectory   = s_ClientDirectory;
            process_info.UseShellExecute    = false;
            process_info.CreateNoWindow     = true;

            using (Process process = new Process())
            {
                process.StartInfo = process_info;
                process.Start();
                process.WaitForExit();
            }
        }

        public static void RegenerateKeys()
        {
            if (KeysExist)
            {
                File.Delete(s_PublicKeyPath);
                File.Delete(s_PrivateKeyPath);
            }

            GenerateKeys();
        }

        public static void Execute(string remote, string local, string username, string address, ushort port)
        {
            if (String.IsNullOrWhiteSpace(remote) ||
                String.IsNullOrWhiteSpace(local) ||
                String.IsNullOrWhiteSpace(username) ||
                String.IsNullOrWhiteSpace(address))
                return;

            if (!ClientExtracted)
                ExtractClient();

            if (!KeysExist)
                GenerateKeys();

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

            using (Process rsync_process = new Process())
            {
                rsync_process.StartInfo = start_info;

                rsync_process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        Logger.Info(string.Format("[RSYNC] {0}", e.Data));
                    }
                });

                rsync_process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        Logger.Error(string.Format("[RSYNC] {0}", e.Data));
                    }
                });

                rsync_process.Start();
                rsync_process.BeginOutputReadLine();
                rsync_process.BeginErrorReadLine();
            }
        }
    }
}

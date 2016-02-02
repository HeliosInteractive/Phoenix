namespace phoenix
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Diagnostics;
    using System.IO.Compression;

    class RsyncClient
    {
        private static string s_ClientDirectory = string.Format("{0}phoenix{1}", Path.GetTempPath(), Path.DirectorySeparatorChar);
        private static string s_PrivateKeyPath  = string.Format("{0}{1}", s_ClientDirectory, MachineIdentity);
        private static string s_PublicKeyPath   = string.Format("{0}{1}.pub", s_ClientDirectory, MachineIdentity);
        private static string s_ResourceUri     = "phoenix.Resources.rsync.zip";

        public static string PathToCygwinPath(string path)
        {
            if (path == string.Empty || path.StartsWith("cygdrive/")) return path;
            if (Directory.Exists(path) && Path.IsPathRooted(path))
            {
                path = path.Replace("\\", "/");
                path = path.Replace(":", string.Empty);
                path = string.Format("cygdrive/{0}", path);
            }
            return path;
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
                catch { /* no-op */ }
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
                catch { /* no-op */ }
            }
        }
        private static bool ClientExtracted
        {
            get
            {
                return
                    File.Exists(string.Format("{0}ssh.exe", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}rsync.exe", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}ssh-keygen.exe", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygz.dll", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygwin1.dll", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygssp-0.dll", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygiconv-2.dll", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cyggcc_s-1.dll", s_ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygcrypto-1.0.0.dll", s_ClientDirectory));
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
                    var extract_path = string.Format("{0}{1}", s_ClientDirectory, entry.Name);
                    
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

            ProcessStartInfo process_info = new ProcessStartInfo();
            process_info.Arguments = string.Format("-q -t rsa -f '{0}' -N ''", MachineIdentity);
            process_info.FileName = string.Format("{0}ssh-keygen.exe", s_ClientDirectory);
            process_info.WorkingDirectory = s_ClientDirectory;
            process_info.UseShellExecute = false;
            process_info.CreateNoWindow = true;

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

        private static void Execute(string remote, string local, string username, string address, ushort port)
        {
            if (!ClientExtracted)
                ExtractClient();

            if (!KeysExist)
                GenerateKeys();

            // Make sure we have an established home directory for SSH
            string home_directory = string.Format("{0}home", s_ClientDirectory);

            if (!Directory.Exists(home_directory))
                Directory.CreateDirectory(home_directory);

            ProcessStartInfo start_info = new ProcessStartInfo();
            start_info.FileName = string.Format("{0}rsync.exe", s_ClientDirectory);
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
                        Debug.WriteLine(e.Data);
                    }
                });
                rsync_process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        Debug.WriteLine(e.Data);
                    }
                });
                rsync_process.Start();
                rsync_process.BeginOutputReadLine();
                rsync_process.BeginErrorReadLine();
            }
        }
    }
}

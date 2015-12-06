using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace phoenix
{
    class RsyncClient
    {
        private static string s_ClientDirectory = string.Format("{0}phoenix{1}", Path.GetTempPath(), Path.DirectorySeparatorChar);
        private static string s_PackagePath     = string.Format("{0}rsync.zip", ClientDirectory);

        public static string ClientDirectory
        {
            get { return s_ClientDirectory; }
        }
        public static string PackagePath
        {
            get { return s_PackagePath; }
        }
        public static bool ClientExtracted
        {
            get
            {
                bool files_exist =
                    File.Exists(string.Format("{0}ssh.exe", ClientDirectory)) &&
                    File.Exists(string.Format("{0}rsync.exe", ClientDirectory)) &&
                    File.Exists(string.Format("{0}ssh-keygen.exe", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygz.dll", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygwin1.dll", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygssp-0.dll", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygiconv-2.dll", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cyggcc_s-1.dll", ClientDirectory)) &&
                    File.Exists(string.Format("{0}cygcrypto-1.0.0.dll", ClientDirectory));

                return files_exist;
            }
        }
        public static bool KeysExist
        {
            get
            {
                bool keys_exist =
                    File.Exists(string.Format("{0}{1}", ClientDirectory, Environment.MachineName)) &&
                    File.Exists(string.Format("{0}{1}.pub", ClientDirectory, Environment.MachineName));

                return keys_exist;
            }
        }
        public static void Extract()
        {
            if (!Directory.Exists(ClientDirectory))
                Directory.CreateDirectory(ClientDirectory);

            if (!File.Exists(PackagePath))
                File.WriteAllBytes(PackagePath, Properties.Resources.rsync);

            if (ClientExtracted)
                return;

            using (ZipArchive archive = ZipFile.OpenRead(PackagePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    var extract_path = string.Format("{0}{1}", ClientDirectory, entry.Name);
                    
                    if (!File.Exists(extract_path))
                    {
                        entry.ExtractToFile(extract_path, true);
                    }
                }
            }
        }

        public static void GenerateKey()
        {
            if (!ClientExtracted)
                Extract();

            ProcessStartInfo process_info = new ProcessStartInfo();
            process_info.Arguments = string.Format("-q -t rsa -f '{0}' -N ''", Environment.MachineName);
            process_info.FileName = string.Format("{0}ssh-keygen.exe", ClientDirectory);
            process_info.WorkingDirectory = ClientDirectory;
            process_info.UseShellExecute = false;
            process_info.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = process_info;
                process.Start();
                process.WaitForExit();
            }
        }

        public static void Execute(string remote, string local, string username, string address, short port)
        {
            if (!ClientExtracted)
                Extract();

            if (!KeysExist)
                GenerateKey();

            // Make sure we have an established home directory for SSH
            string home_directory = string.Format("{0}home", ClientDirectory);

            if (!Directory.Exists(home_directory))
                Directory.CreateDirectory(home_directory);

            ProcessStartInfo start_info = new ProcessStartInfo();
            start_info.FileName = string.Format("{0}rsync.exe", ClientDirectory);
            start_info.EnvironmentVariables["HOME"] = home_directory;
            start_info.EnvironmentVariables["PATH"] = ClientDirectory;
            start_info.WorkingDirectory = ClientDirectory;
            start_info.UseShellExecute = false;
            start_info.Arguments = string.Format(
                "-ravz -e \"ssh -p {0} -i '{1}' -o StrictHostKeyChecking=no\" {2}@{3}:{4} {5}",
                port,
                Environment.MachineName,
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
                    // Prepend line numbers to each line of the output.
                    if (!String.IsNullOrEmpty(e.Data))
                    {
                        Debug.WriteLine(e.Data);
                    }
                });
                rsync_process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    // Prepend line numbers to each line of the output.
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

using System.IO;
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
    }
}

using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Reflection;

namespace phoenix
{
    class UpdateManager
    {
        Version mCurrentVersion;
        Version mUpdateVersion;
        Uri     mUpdateAddress;
        string  mFeedAddress;

        public UpdateManager(string feed_address)
        {
            mCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            mUpdateVersion  = Version.Parse("0.0.0.0");
            mFeedAddress    = feed_address;
        }

        public void Check()
        {
            XmlDocument feed_xml = new XmlDocument();
            feed_xml.Load(mFeedAddress);

            mUpdateVersion = Version.Parse(feed_xml["phoenix"]["version"].InnerText);
            mUpdateAddress = new Uri(feed_xml["phoenix"]["address"].InnerText);

            if (mUpdateVersion > mCurrentVersion)
            {
                string temp_loc = Path.Combine(
                    Path.GetTempPath(),
                    Guid.NewGuid().ToString(),
                    Path.GetFileName(mUpdateAddress.ToString()));

                using (WebClient client = new WebClient())
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(temp_loc));
                    client.DownloadFile(mUpdateAddress, temp_loc);

                    string oldapp_location = Assembly.GetExecutingAssembly().Location;
                    string backup_location = Path.Combine(Path.GetDirectoryName(oldapp_location), "backup.dat");

                    if (File.Exists(backup_location))
                        File.Delete(backup_location);

                    File.Move(oldapp_location, backup_location);
                    File.Move(temp_loc, oldapp_location);
                }
            }
        }
    }
}

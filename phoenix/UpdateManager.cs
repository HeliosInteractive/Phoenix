using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Reflection;

namespace phoenix
{
    class UpdateManager
    {
        Version m_CurrentVersion;
        Version m_UpdateVersion;
        Uri     m_UpdateAddress;
        Uri     m_FeedAddress;
        string  m_FeedChannel;

        public UpdateManager(string feed_address)
        {
            m_CurrentVersion = Assembly.GetExecutingAssembly().GetName().Version;
            m_UpdateVersion  = Version.Parse("0.0.0.0");
            m_FeedAddress    = new Uri(feed_address);
            m_FeedChannel    = "alpha"; // change this if you are working on a different branch.

            Logger.Info(string.Format("Update Manager is up with feed address: {0} on channel: {1}",
                m_FeedAddress,
                m_FeedChannel));

            Logger.Info(string.Format("Update Manager reports current version is: {0}",
                m_CurrentVersion));

            Check();
        }

        public void Check()
        {
            Logger.Info("Checking for updates...");
            XmlDocument feed_xml = new XmlDocument();

            using (WebClient wc = new WebClient())
            {
                wc.DownloadStringCompleted += (object sender, DownloadStringCompletedEventArgs e) => {
                    try
                    {
                        feed_xml.LoadXml(e.Result);
                    }
                    catch (XmlException)
                    {
                        Logger.Error("Unable to load the feed xml due to malformation.");
                        feed_xml = null;
                    }
                    catch
                    {
                        Logger.Error("Unable to load the feed xml.");
                        feed_xml = null;
                    }

                    if (feed_xml == null || !feed_xml.HasChildNodes)
                        return;

                    try
                    {
                        if (feed_xml["phoenix"]["channel"].InnerText == m_FeedChannel)
                        {
                            Logger.Info(string.Format("Found a matching update channel: {0}", m_FeedChannel));

                            m_UpdateVersion = Version.Parse(feed_xml["phoenix"]["version"].InnerText);
                            Logger.Info(string.Format("Update version is parsed to be: {0}", m_UpdateVersion));

                            m_UpdateAddress = new Uri(feed_xml["phoenix"]["address"].InnerText);
                            Logger.Info(string.Format("Update address is loaded to be: {0}", m_UpdateAddress));
                        }
                        else
                        {
                            Logger.Warn(string.Format("Update channels do not match: {0} vs {1}"
                                , feed_xml["phoenix"]["channel"].InnerText
                                , m_FeedChannel));
                        }
                    }
                    catch
                    {
                        Logger.Error("Update feed has missing components.");
                        m_UpdateAddress = null;
                    }

                    if (m_UpdateAddress == null)
                        return;

                    if (m_UpdateVersion > m_CurrentVersion)
                    {
                        string temp_loc = Path.Combine(
                            Path.GetTempPath(),
                            Guid.NewGuid().ToString(),
                            Path.GetFileName(m_UpdateAddress.ToString()));

                        Logger.Info(string.Format("Downloading new phoenix to: {0}.",
                            temp_loc));

                        using (WebClient client = new WebClient())
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(temp_loc));
                            client.DownloadFileCompleted += (sender2, e2) =>
                            {
                                string oldapp_location = Assembly.GetExecutingAssembly().Location;
                                string backup_location = Path.Combine(Path.GetDirectoryName(oldapp_location), "backup.dat");

                                Logger.Info(string.Format("Old location is: {0}. Backup location is: {1}"
                                    , oldapp_location
                                    , backup_location));

                                if (File.Exists(backup_location))
                                {
                                    try
                                    {
                                        File.Delete(backup_location);
                                        Logger.Info("Old backup executable removed.");
                                    }
                                    catch
                                    {
                                        Logger.Error("Unable to remove old backup.");
                                        return;
                                    }
                                }

                                try
                                {
                                    File.Move(oldapp_location, backup_location);
                                    Logger.Info("Backup executable created.");
                                }
                                catch
                                {
                                    Logger.Error("Backup creation failed.");
                                    return;
                                }

                                try
                                {
                                    File.Move(temp_loc, oldapp_location);
                                    Logger.Info("Updates successfully applied");
                                }
                                catch
                                {
                                    Logger.Error("Applying updates failed, rolling back.");
                                    File.Move(backup_location, oldapp_location);
                                }
                            };

                            try
                            {
                                client.DownloadFileAsync(m_UpdateAddress, temp_loc);
                            }
                            catch (WebException)
                            {
                                Logger.Error("Phoenix update download failed due to WebException.");
                                return;
                            }
                            catch
                            {
                                Logger.Error("Phoenix update download failed due to exception.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Logger.Info("Phoenix is already a newer version.");
                    }
                };

                try
                {
                    wc.DownloadStringAsync(m_FeedAddress);
                }
                catch (WebException)
                {
                    Logger.Error("Phoenix feed download failed due to WebException.");
                    return;
                }
                catch
                {
                    Logger.Error("Phoenix feed download failed due to exception.");
                    return;
                }
            }
        }
    }
}

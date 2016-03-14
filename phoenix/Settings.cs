namespace phoenix
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    /// A persistent settings class
    /// </summary>
    class Settings
    {
        private XElement m_PhoenixRoot;
        private XElement m_OptionsRoot;

        public Settings()
        {
            FileInfo config_file = new FileInfo(
                Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
                File.WriteAllText(config_file.FullName, Properties.Resources.phoenix_base_settings);

            string phoenix_root_name = "phoenix";
            string options_node_name = "options";

            m_PhoenixRoot = XElement.Load(config_file.FullName);

            if (m_PhoenixRoot == null || m_PhoenixRoot.Name != phoenix_root_name)
            {
                Logger.Settings.Warn("Phoenix node not found. It will be created.");
                m_PhoenixRoot = new XElement(phoenix_root_name);
            }

            m_OptionsRoot = m_PhoenixRoot.Element(options_node_name);

            if (m_OptionsRoot == null)
            {
                Logger.Settings.Error("Options node not found. It will be created.");
                m_OptionsRoot = new XElement(options_node_name);
                m_PhoenixRoot.Add(m_OptionsRoot);
            }

            Logger.Settings.InfoFormat("Path to settings files is: {0}", config_file.FullName);
        }

        ~Settings()
        {
            if (m_PhoenixRoot == null)
            {
                Logger.Settings.Warn("Options node is empty.");
                return;
            }

            m_PhoenixRoot.Save(Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));
        }

        /// <summary>
        /// Writes a string entry to XML tree
        /// </summary>
        /// <typeparam name="T">Type of the value to be written</typeparam>
        /// <param name="Section">section name</param>
        /// <param name="Key">key name</param>
        /// <param name="Value">value parameter</param>
        public void Write<T>(string Section, string Key, T Value)
        {
            if (m_OptionsRoot == null)
            {
                Logger.Settings.Error("Options node is null. Discarding write operation.");
                return;
            }

            XElement section = m_OptionsRoot.Element(Section);

            if (section == null)
            {
                section = new XElement(Section);
                m_OptionsRoot.Add(section);
            }

            XElement entry = section.Element(Key);

            if (entry == null)
                section.Add(new XElement(Key, Value.ToString()));
            else
                entry.Value = Value.ToString();

            Logger.Settings.InfoFormat("Wrote {0} in {1} with value {2}",
                Section, Key, Value.ToString());
        }

        /// <summary>
        /// Reads an entry in settings file, returns DefaultValue if not found
        /// </summary>
        /// <typeparam name="T">Type of the value to be stored</typeparam>
        /// <param name="Section">settings section name</param>
        /// <param name="Key">settings key name</param>
        /// <param name="DefaultValue">default value (on failure to lookup)</param>
        /// <returns>read value or default of its type if failed to be read</returns>
        public T Read<T>(string Section, string Key, T DefaultValue)
        {
            if (m_OptionsRoot == null)
            {
                Logger.Settings.Error("Options node is null. Discarding write operation.");
                return DefaultValue;
            }

            XElement section = m_OptionsRoot.Element(Section);

            if (section == null)
            {
                Write(Section, Key, DefaultValue);
                return DefaultValue;
            }

            XElement entry = section.Element(Key);

            if (entry == null)
            {
                Write(Section, Key, DefaultValue);
                return DefaultValue;
            }

            string value = entry.Value;

            if (!String.IsNullOrWhiteSpace(value) &&
                typeof(T).CanBeCastedFrom(value))
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    Logger.Settings.WarnFormat(
                        "Failed to entry type in section {0} and key {1}. Default value: {2}",
                        Section,
                        Key,
                        DefaultValue);

                    Write(Section, Key, DefaultValue);
                    return DefaultValue;
                }
            }
            else
            {
                Logger.Settings.InfoFormat("Failed to Read {0} in {1}",
                    Key, Section);

                Write(Section, Key, DefaultValue);
                return DefaultValue;
            }
        }
    }
}

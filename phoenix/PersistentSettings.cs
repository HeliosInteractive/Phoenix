namespace phoenix
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    /// <summary>
    /// A persistent settings class
    /// </summary>
    class PersistentSettings
    {
        private XElement m_SettingsRoot;
        private XElement m_PhoenixRoot;

        public PersistentSettings()
        {
            FileInfo config_file = new FileInfo(
                Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));

            if (!config_file.Exists)
                File.WriteAllText(config_file.FullName, Properties.Resources.logger);

            string phoenix_root_name = "phoenix";
            m_SettingsRoot = XElement.Load(Properties.Resources.SettingsFileName);
            
            if (m_SettingsRoot.Element(phoenix_root_name) == null)
                m_SettingsRoot.Add(new XElement(phoenix_root_name));

            m_PhoenixRoot = m_SettingsRoot.Element(phoenix_root_name);

            Logger.Settings.InfoFormat("Path to settings files is: {0}", config_file.FullName);
        }

        ~PersistentSettings()
        {
            m_SettingsRoot.Save(Path.Combine(Program.Directory, Properties.Resources.SettingsFileName));

            Logger.Settings.Info("Saved settings entries back to file.");
        }

        /// <summary>
        /// Writes a string entry to XML tree
        /// </summary>
        /// <param name="Section">section name</param>
        /// <param name="Key">key name</param>
        /// <param name="Value">value parameter</param>
        public void Write(string Section, string Key, string Value)
        {
            if (m_PhoenixRoot.Element(Section) == null)
                m_PhoenixRoot.Add(new XElement(Section));

            XElement section = m_PhoenixRoot.Element(Section);

            if (section.Element(Key) == null)
                section.Add(new XElement(Key, Value));
            else
                section.Element(Key).Value = Value;

            Logger.Settings.InfoFormat("Wrote {0} in {1} with value {2}",
                Section, Key, Value);
        }

        /// <summary>
        /// Writes a string entry to XML tree
        /// </summary>
        /// <typeparam name="T">Type of the value to be written</typeparam>
        /// <param name="Section">section name</param>
        /// <param name="Key">key name</param>
        /// <param name="Value">value parameter. New lines will be replaced with a br tag</param>
        public void Write<T>(string Section, string Key, T Value)
        {
            string to_be_stored = Value.ToString();

            // http://www.askingbox.com/question/windows-api-getprofilestring-and-getprivateprofilestring-clears-quotes
            if (to_be_stored.StartsWith("\"") && to_be_stored.EndsWith("\""))
                to_be_stored = string.Format("\"{0}\"", to_be_stored);

            if (to_be_stored.Contains("\r\n"))
                to_be_stored = to_be_stored.Replace("\r\n", "<br>");

            if (to_be_stored.Contains("\n"))
                to_be_stored = to_be_stored.Replace("\n", "<br>");

            if (to_be_stored.Contains("\r"))
                to_be_stored = to_be_stored.Replace("\r", "<br>");

            Write(Section, Key, to_be_stored);
        }

        /// <summary>
        /// Reads an entry in INI file, returns DefaultValue if not found
        /// </summary>
        /// <typeparam name="T">Type of the value to be stored</typeparam>
        /// <param name="Section">INI section name</param>
        /// <param name="Key">INI key name</param>
        /// <param name="DefaultValue">default value (on failure to lookup)</param>
        /// <returns>read value or default of its type if failed to be read</returns>
        public T Read<T>(string Section, string Key, T DefaultValue)
        {
            if (m_PhoenixRoot.Element(Section) == null)
            {
                Write(Section, Key, DefaultValue);
                return DefaultValue;
            }

            if (m_PhoenixRoot.Element(Section).Element(Key) == null)
            {
                Write(Section, Key, DefaultValue);
                return DefaultValue;
            }

            string value = m_PhoenixRoot.Element(Section).Element(Key).Value;

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
                        "Failed to comvert INI entry type in section {0} and key {1}. Default value: {2}",
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

        /// <summary>
        /// string overload of Read<T>. Takes special care of returned values,
        /// replacing br tags with Windows new lines (\\r\\n)
        /// </summary>
        public string Read(string Section, string Key, string DefaultValue)
        {
            return Read<string>(Section, Key, DefaultValue).Replace("<br>", "\r\n");
        }
    }
}

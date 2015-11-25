using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace phoenix
{
    /// <summary>
    /// A class that can read and write to INI files using Kernel32 API
    /// It can auto save the read entries automatically to an INI file.
    /// </summary>
    class IniSettings
    {
        private Dictionary<string, Dictionary<string, string>>
                        m_Settings  = new Dictionary<string, Dictionary<string, string>>();
        private string  m_Path      = string.Empty;

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms725501(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms724353(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// Constructor. Creates an empty file if "path" does not exist.
        /// </summary>
        /// <param name="path"></param>
        public IniSettings(string path)
        {
            m_Path = path;

            // Convert relative to absolute path. Kernel32 API
            // does not like relative paths apparently.
            if (!Path.IsPathRooted(m_Path))
                m_Path = Path.GetFullPath(m_Path);

            // Create an empty INI file if it does not exist.
            if (!File.Exists(m_Path))
                File.Create(m_Path).Dispose();
        }

        ~IniSettings()
        {
            // Save read values on destruction
            SaveReadEntries();
        }

        /// <summary>
        /// Writes a string entry to INI file, you'd hardly ever need this
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Write(string Section, string Key, string Value)
        {
            if (Value == string.Empty) return;
            WritePrivateProfileString(Section, Key, Value, this.m_Path);
        }

        /// <summary>
        /// Store a value to be saved later via SaveReadEntries()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        public void Store<T>(string Section, string Key, T value)
        {
            if (!m_Settings.ContainsKey(Section))
                m_Settings[Section] = new Dictionary<string, string>();

            m_Settings[Section][Key] = value.ToString();
        }

        /// <summary>
        /// Reads an entry in INI file, returns DefaultValue if not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public T Read<T>(string Section, string Key, T DefaultValue)
        {
            StringBuilder strb = new StringBuilder(2048);

            if (GetPrivateProfileString(Section, Key, "", strb, strb.Capacity, this.m_Path) > 0
                && CanChangeType(strb.ToString(), typeof(T)))
            {
                try
                {
                    T temp_val = (T)Convert.ChangeType(strb.ToString(), typeof(T));
                    Store(Section, Key, temp_val);
                    return temp_val;
                }
                catch
                { /* no-op */ }
            }

            Store(Section, Key, DefaultValue);
            return DefaultValue;
        }

        /// <summary>
        /// Save everything read through Read() API so far back to INI file
        /// </summary>
        public void SaveReadEntries()
        {
            foreach (var keys in m_Settings)
            {
                foreach (var sections in keys.Value)
                {
                    Write(keys.Key, sections.Key, sections.Value);
                }
            }
        }

        private static bool CanChangeType(object value, Type conversionType)
        {
            if (conversionType == null || value == null || (value as IConvertible) == null)
            {
                return false;
            }

            return true;
        }
    }
}

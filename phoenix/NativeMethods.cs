using System.Runtime.InteropServices;

namespace phoenix
{
    internal static class NativeMethods
    {
        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(System.IntPtr hWnd);

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505(v=vs.85).aspx"/>
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern System.IntPtr GetForegroundWindow();
        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms725501(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);

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
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);
    }
}

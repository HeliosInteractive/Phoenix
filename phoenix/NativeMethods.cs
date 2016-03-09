namespace phoenix
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeMethods
    {
        /// <summary>
        /// <see cref="http://www.pinvoke.net/default.aspx/user32.setwindowpos"/>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_NOMOVE      = 0x0002,
            SWP_NOSIZE      = 0x0001,
            SWP_SHOWWINDOW  = 0x0040
        }

        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

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

        [Flags]
        public enum RegisterHotKeyModifiers : uint
        {
            MOD_NOREPEAT    = 0x4000,
            MOD_ALT         = 0x0001
        }

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646309(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <param name="fsModifiers"></param>
        /// <param name="vk"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(System.IntPtr hWnd, int id, RegisterHotKeyModifiers fsModifiers, uint vk);

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646309(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633553(v=vs.85).aspx"/>
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="fAltTab"></param>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
    }
}

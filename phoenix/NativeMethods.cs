namespace phoenix
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// DllImported functions from native DLLs to be used by Phoenix.
    /// It is mostly Win32 APIs and enumerations.
    /// </summary>
    internal static class NativeMethods
    {
        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx"/>
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_NOMOVE      = 0x0002,
            SWP_NOSIZE      = 0x0001,
            SWP_SHOWWINDOW  = 0x0040
        }

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646309(v=vs.85).aspx"/>
        [Flags]
        public enum RegisterHotKeyModifiers : uint
        {
            MOD_NOREPEAT    = 0x4000,
            MOD_ALT         = 0x0001
        }

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx"/>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633545(v=vs.85).aspx"/>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539(v=vs.85).aspx"/>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(System.IntPtr hWnd);

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505(v=vs.85).aspx"/>
        [DllImport("user32.dll")]
        public static extern System.IntPtr GetForegroundWindow();

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646309(v=vs.85).aspx"/>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(System.IntPtr hWnd, int id, RegisterHotKeyModifiers fsModifiers, uint vk);

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms646309(v=vs.85).aspx"/>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/ms633553(v=vs.85).aspx"/>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
    }
}

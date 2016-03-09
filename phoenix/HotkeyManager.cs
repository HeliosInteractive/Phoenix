namespace phoenix
{
    using System;

    /// <summary>
    /// Class responsible for (un)registering hot-keys
    /// It automatically unregisters hot-keys on destruction
    /// </summary>
    class HotkeyManager : IDisposable
    {
        /// <summary>
        /// Window handle we want to register hot-keys for
        /// </summary>
        readonly IntPtr m_Hwnd      = IntPtr.Zero;
        /// <summary>
        /// Flag to check redundant Dispose calls
        /// </summary>
        private bool    m_Disposed  = false;

        /// <summary>
        /// IDs of registered hot-keys
        /// </summary>
        public enum ID : int
        {
            ForceAlwaysOnTop    = 1,
            ControlPanelUi      = 2,
            Monitoring          = 3,
            Screenshot          = 4,
        }

        /// <summary>
        /// Attempts to register hot-keys on construction
        /// </summary>
        /// <param name="hwnd">window handles of hot-keys</param>
        public HotkeyManager(IntPtr hwnd)
        {
            m_Hwnd = hwnd;
            Register();
        }

        /// <summary>
        /// Registers hot-keys for m_Hwnd
        /// </summary>
        private void Register()
        {
            if (m_Hwnd == IntPtr.Zero)
            {
                Logger.MainDialog.Warn("HotkeyManager received a null window handle");
                return;
            }

            NativeMethods.RegisterHotKey(m_Hwnd,
                (int)ID.ForceAlwaysOnTop,
                NativeMethods.RegisterHotKeyModifiers.MOD_NOREPEAT |
                NativeMethods.RegisterHotKeyModifiers.MOD_ALT,
                (int)System.Windows.Forms.Keys.F12);

            NativeMethods.RegisterHotKey(m_Hwnd,
                (int)ID.ControlPanelUi,
                NativeMethods.RegisterHotKeyModifiers.MOD_NOREPEAT |
                NativeMethods.RegisterHotKeyModifiers.MOD_ALT,
                (int)System.Windows.Forms.Keys.F11);

            NativeMethods.RegisterHotKey(m_Hwnd,
                (int)ID.Monitoring,
                NativeMethods.RegisterHotKeyModifiers.MOD_NOREPEAT |
                NativeMethods.RegisterHotKeyModifiers.MOD_ALT,
                (int)System.Windows.Forms.Keys.F10);

            NativeMethods.RegisterHotKey(m_Hwnd,
                (int)ID.Screenshot,
                NativeMethods.RegisterHotKeyModifiers.MOD_NOREPEAT |
                NativeMethods.RegisterHotKeyModifiers.MOD_ALT,
                (int)System.Windows.Forms.Keys.F9);
        }

        //! @cond

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    // no managed objects to dispose.
                }

                if (m_Hwnd != IntPtr.Zero)
                {
                    NativeMethods.UnregisterHotKey(m_Hwnd, (int)ID.ForceAlwaysOnTop);
                    NativeMethods.UnregisterHotKey(m_Hwnd, (int)ID.ControlPanelUi);
                    NativeMethods.UnregisterHotKey(m_Hwnd, (int)ID.Monitoring);
                    NativeMethods.UnregisterHotKey(m_Hwnd, (int)ID.Screenshot);
                }

                m_Disposed = true;
            }
        }

        ~HotkeyManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        //! @endcond
    }
}

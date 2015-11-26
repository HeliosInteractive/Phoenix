using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace phoenix
{
    /// <summary>
    /// A class that runs and monitors a given process
    /// </summary>
    class ProcessRunner
    {
        int     m_DelaySeconds  = 0;
        int     m_Attempts      = 10;
        int     m_CurrAttempt   = 0;
        string  m_CrashScript   = string.Empty;
        string  m_ProcessPath   = string.Empty;
        string  m_CommandLine   = string.Empty;
        bool    m_Validated     = false;
        bool    m_Monitoring    = false;
        Process m_Process       = null;

        /// <summary>
        /// An event that is fired when the process is exited
        /// Process exit code will be passed to the called handler
        /// </summary>
        public Action<int> ProcessExited;

        /// <summary>
        /// A script to be called in case of process exit/crash.
        /// Process exit code will be passed to the called script
        /// </summary>
        public string CrashScript
        {
            get { return m_CrashScript; }
            set { m_CrashScript = value; Validate(); }
        }

        /// <summary>
        /// Maximum attempts the process will be restarted before we
        /// give up restarting it. 0 means infinite attempts.
        /// </summary>
        public int Attempts
        {
            get { return m_Attempts; }
            set { m_Attempts = value; Validate(); }
        }

        /// <summary>
        /// The time delay before re-launching the process (seconds)
        /// </summary>
        public int DelaySeconds
        {
            get { return m_DelaySeconds; }
            set { m_DelaySeconds = value; Validate(); }
        }
        
        /// <summary>
        /// Process path (could be an executable or a bat file for example)
        /// </summary>
        public string ProcessPath
        {
            get { return m_ProcessPath; }
            set { m_ProcessPath = value; Validate(); }
        }
        
        /// <summary>
        /// Command line arguments to be passed to the process
        /// </summary>
        public string CommandLine
        {
            get { return m_CommandLine; }
            set { m_CommandLine = value; Validate(); }
        }

        /// <summary>
        /// Call this method to reset the counter and monitor the process
        /// </summary>
        public void Run()
        {
            if (!m_Validated) return;

            m_CurrAttempt = 0;
            ExecuteProcess();
            ExecuteMonitor();
        }

        void ExecuteMonitor()
        {
            if (m_Monitoring)
                return;

            m_Monitoring = true;
            MonitorAction();
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        void MonitorAction()
        {
            if (m_Process == null || m_Process.HasExited)
                return;

            m_Process.Refresh();

            if (!m_Process.Responding)
            {
                try { m_Process.Kill(); } catch { /* no-op */ }
                HandleProcessExit(m_Process, null);
            }
            else if (m_Process.MainWindowHandle != IntPtr.Zero)
            {
                SetForegroundWindow(m_Process.MainWindowHandle);
            }

            Task.Delay(1000)
                .ContinueWith(fn => MonitorAction());
        }

        /// <summary>
        /// Virtual method, gets called on process exit every time it exits
        /// </summary>
        /// <param name="exit_code">Process' exit code</param>
        protected virtual void OnProcessExit(int exit_code)
        {
            if (ProcessExited != null)
                ProcessExited(exit_code);
        }

        /// <summary>
        /// Validate members. Sets a flag that prevents Run() from operating
        /// if members are incorrectly set.
        /// </summary>
        void Validate()
        {
            m_Validated = false;

            m_DelaySeconds = Math.Abs(m_DelaySeconds);
            m_Attempts = Math.Abs(m_Attempts);

            if (m_ProcessPath != string.Empty && !Path.IsPathRooted(m_ProcessPath))
                m_ProcessPath = Path.GetFullPath(m_ProcessPath);

            if (m_CrashScript != string.Empty && !Path.IsPathRooted(m_CrashScript))
                m_CrashScript = Path.GetFullPath(m_CrashScript);

            if (!File.Exists(m_ProcessPath))
            {
                m_ProcessPath = string.Empty;
                return;
            }

            if (!File.Exists(m_CrashScript))
            {
                m_CrashScript = string.Empty;
            }

            m_Validated = true;
        }

        /// <summary>
        /// Executes the given process with set command line arguments
        /// </summary>
        void ExecuteProcess()
        {
            if (!m_Validated) return;

            ProcessStartInfo process_info = new ProcessStartInfo();
            process_info.FileName = ProcessPath;

            if (CommandLine != string.Empty)
                process_info.Arguments = CommandLine;

            m_Process = new Process();
            m_Process.StartInfo = process_info;
            m_Process.EnableRaisingEvents = true;
            m_Process.Exited += new EventHandler(HandleProcessExit);
            m_Process.Start();
        }

        /// <summary>
        /// Method that is called on Process exit event on .NET framework's
        /// Process.Exited event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleProcessExit(object sender, EventArgs e)
        {
            if (!m_Validated || (m_Attempts > 0 &&  m_CurrAttempt >= m_Attempts))
                return;

            if (m_CrashScript != string.Empty)
            {
                ProcessStartInfo process_info = new ProcessStartInfo();
                process_info.FileName = CrashScript;

                if (sender as Process != null)
                {
                    Process dead_process = (sender as Process);
                    process_info.Arguments = dead_process.ExitCode.ToString();
                    OnProcessExit(dead_process.ExitCode);
                }

                m_Process = new Process();
                m_Process.StartInfo = process_info;
                m_Process.Start();
            }

            Task.Delay(1000 * DelaySeconds)
                .ContinueWith( fn => ExecuteProcess() );

            m_CurrAttempt++;
        }
    }
}

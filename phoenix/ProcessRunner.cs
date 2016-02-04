namespace phoenix
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// A class that runs and monitors a given process
    /// </summary>
    class ProcessRunner : IDisposable
    {
        #region Private Members

        PerformanceCounter
                    m_PerformanceCounter;
        int         m_DelaySeconds  = 0;
        int         m_Attempts      = 10;
        int         m_CurrAttempt   = 0;
        string      m_WorkingDir    = string.Empty;
        string      m_StartScript   = string.Empty;
        string      m_CrashScript   = string.Empty;
        string      m_ProcessPath   = string.Empty;
        string      m_CommandLine   = string.Empty;
        bool        m_Validated     = false;
        bool        m_PauseMonitor  = false;
        Process     m_Process       = null;
        bool        m_AlwaysOnTop   = false;
        bool        m_CrashIfUnresp = false;
        bool        m_Monitoring    = false;
        const int   m_NumSamples    = 100;
        double[]    m_MemoryUsage   = new double[m_NumSamples];
        double[]    m_CpuUsage      = new double[m_NumSamples];
        double[]    m_UsageIndices  = new double[m_NumSamples];

        #endregion

        #region Events

        /// <summary>
        /// event triggered when monitoring starts
        /// </summary>
        public Action MonitorStarted;
        /// <summary>
        /// event triggered when monitoring stops
        /// </summary>
        public Action MonitorStopped;
        /// <summary>
        /// Event triggered when we spawn the process
        /// </summary>
        public Action ProcessStarted;
        /// <summary>
        /// Event triggered when process stopped
        /// </summary>
        public Action ProcessStopped;

        protected virtual void OnMonitorStarted()
        {
            m_Monitoring = true;

            if (MonitorStarted != null)
                MonitorStarted();
        }
        protected virtual void OnMonitorStopped()
        {
            m_Monitoring = false;

            if (MonitorStopped != null)
                MonitorStopped();
        }
        protected virtual void OnProcessStarted()
        {
            if (ProcessStarted != null)
                ProcessStarted();
        }
        protected virtual void OnProcessStopped()
        {
            if (ProcessStopped != null)
                ProcessStopped();
        }

        #endregion

        public ProcessRunner()
        {
            for (int index = 0; index < m_NumSamples; ++index)
            {
                m_UsageIndices[index] = index;
                m_MemoryUsage[index] = 0;
                m_CpuUsage[index] = 0;
            }
        }

        #region Property Indexers

        /// <summary>
        /// Returns true if this instance is actively monitoring
        /// </summary>
        public bool Monitoring
        {
            get { return m_Monitoring; }
        }
        /// <summary>
        /// An array filled with [0,NumSamples] for charts
        /// </summary>
        public double[] UsageIndices
        {
            get { return m_UsageIndices; }
        }
        /// <summary>
        /// % memory usage (CurrentWorkingSet / PeakWorkingSet)
        /// </summary>
        public double[] MemoryUsage
        {
            get { return m_MemoryUsage; }
        }
        /// <summary>
        /// % CPU usage (queried from PerfMon)
        /// </summary>
        public double[] CpuUsage
        {
            get { return m_CpuUsage; }
        }
        /// <summary>
        /// Number of memory / CPU usage samples to be taken
        /// </summary>
        public int NumSamples
        {
            get { return m_NumSamples; }
        }
        /// <summary>
        /// Assume crashed if process' main window is not responding
        /// </summary>
        public bool AssumeCrashIfNotResponsive
        {
            get { return m_CrashIfUnresp; }
            set { m_CrashIfUnresp = value; }
        }
        /// <summary>
        /// Force main window of the process to be always on top
        /// </summary>
        public bool ForceAlwaysOnTop
        {
            get { return m_AlwaysOnTop; }
            set { m_AlwaysOnTop = value; }
        }
        /// <summary>
        /// A script to be called in case of process exit/crash
        /// </summary>
        public string CrashScript
        {
            get { return m_CrashScript; }
            set { m_CrashScript = value; Validate(); }
        }
        /// <summary>
        /// A script to be called in case of process start
        /// </summary>
        public string StartScript
        {
            get { return m_StartScript; }
            set { m_StartScript = value; Validate(); }
        }
        /// <summary>
        /// Process' working directory.
        /// </summary>
        public string WorkingDirectory
        {
            get { return m_WorkingDir; }
            set { m_WorkingDir = value; Validate(); }
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

        #endregion

        /// <summary>
        /// Call this method to reset the counter and monitor the process
        /// </summary>
        public void Start()
        {
            Validate();

            if (!m_Validated) return;

            m_CurrAttempt = 0;
            ExecuteProcess();

            OnMonitorStarted();
        }

        /// <summary>
        /// Stops monitoring the Process
        /// </summary>
        public void Stop(bool raise_events = false)
        {
            if (m_Process != null)
            {
                m_Process.EnableRaisingEvents = raise_events;

                try
                {
                    if (HasMainWindow())
                        m_Process.CloseMainWindow(); // gently ask to close

                    if (!m_Process.HasExited)
                    {
                        m_Process.Kill();
                        m_Process.WaitForExit();
                    }
                }
                catch
                {
                    Logger.Error("An error occurred while trying to shutdown the process.");
                }

                m_Process.Dispose();
            }

            m_Process = null;
            m_CurrAttempt = 0;
            m_PauseMonitor = true;

            OnMonitorStopped();
        }

        /// <summary>
        /// Monitor the running process (should be called frequently)
        /// </summary>
        public void Monitor()
        {
            if (!Monitorable())
                return;

            m_Process.Refresh();

            if (AssumeCrashIfNotResponsive && !m_Process.Responding)
            {
                // This will raise the Exited event.
                Stop(true);
            }

            if (ForceAlwaysOnTop && HasMainWindow())
            {
                if (NativeMethods.GetForegroundWindow() != m_Process.MainWindowHandle)
                    NativeMethods.SetForegroundWindow(m_Process.MainWindowHandle);
            }
        }

        /// <summary>
        /// Update the metrics data (should be called frequently)
        /// </summary>
        public void UpdateMetrics()
        {
            if (!Monitorable())
                return;

            for (int index = 1; index < m_NumSamples; ++index)
            {
                m_MemoryUsage[index - 1] = m_MemoryUsage[index];
                m_CpuUsage[index - 1] = m_CpuUsage[index];
            }

            m_MemoryUsage[m_NumSamples - 1] = m_Process.WorkingSet64 / (double)m_Process.PeakWorkingSet64;
            try { m_CpuUsage[m_NumSamples - 1] = m_PerformanceCounter.NextValue() / 100d; }
            catch { m_CpuUsage[m_NumSamples - 1] = 0; }
        }

        /// <summary>
        /// Resets the performance counter that queries CPU usage
        /// </summary>
        void ResetPerformanceCounter()
        {
            if (!Monitorable())
                return;

            if (m_PerformanceCounter != null)
                m_PerformanceCounter.Close();

            m_PerformanceCounter = new PerformanceCounter(
                "Process",
                "% Processor Time",
                m_Process.ProcessName,
                m_Process.MachineName);
        }

        /// <summary>
        /// Return true if the Process underneath is in a situation where it can be monitored
        /// </summary>
        /// <returns></returns>
        bool Monitorable()
        {
            try { return !(m_PauseMonitor || m_Process == null || m_Process.HasExited); }
            catch { return false; }
        }

        bool HasMainWindow()
        {
            try { return (m_Process != null && !m_Process.HasExited && m_Process.MainWindowHandle != IntPtr.Zero); }
            catch { return false; }
        }

        /// <summary>
        /// Validate members. Sets a flag that prevents monitor from operating
        /// if members are incorrectly set.
        /// </summary>
        void Validate()
        {
            m_Validated = false;

            m_DelaySeconds  = Math.Abs(m_DelaySeconds);
            m_Attempts      = Math.Abs(m_Attempts);

            m_ProcessPath   = CleanStringAsPath(m_ProcessPath);
            m_CrashScript   = CleanStringAsPath(m_CrashScript);
            m_StartScript   = CleanStringAsPath(m_StartScript);
            m_WorkingDir    = CleanStringAsPath(m_WorkingDir);

            if (m_ProcessPath != string.Empty && !Path.IsPathRooted(m_ProcessPath))
                m_ProcessPath = Path.GetFullPath(m_ProcessPath);

            if (m_CrashScript != string.Empty && !Path.IsPathRooted(m_CrashScript))
                m_CrashScript = Path.GetFullPath(m_CrashScript);

            if (m_StartScript != string.Empty && !Path.IsPathRooted(m_StartScript))
                m_StartScript = Path.GetFullPath(m_StartScript);

            if (m_WorkingDir != string.Empty && !Path.IsPathRooted(m_WorkingDir))
                m_WorkingDir = Path.GetFullPath(m_WorkingDir);

            if (!File.Exists(m_ProcessPath) || Path.GetExtension(m_ProcessPath) != ".exe")
            {
                m_ProcessPath = string.Empty;
                return;
            }

            if (!File.Exists(m_CrashScript))
            {
                m_CrashScript = string.Empty;
            }

            if (!File.Exists(m_StartScript))
            {
                m_StartScript = string.Empty;
            }

            if (!Directory.Exists(m_WorkingDir))
            {
                m_WorkingDir = string.Empty;
            }

            m_Validated = true;
        }

        /// <summary>
        /// Executes the given process with set command line arguments
        /// </summary>
        void ExecuteProcess()
        {
            if (!m_Validated)
                return;

            CallScript(m_StartScript);

            ProcessStartInfo process_info = new ProcessStartInfo();
            process_info.FileName = ProcessPath;
            process_info.WorkingDirectory = WorkingDirectory;
            process_info.UseShellExecute = false;
            process_info.Arguments = CommandLine;

            if (m_Process != null)
                m_Process.Close();

            m_Process = new Process();
            m_Process.StartInfo = process_info;
            m_Process.EnableRaisingEvents = true;
            m_Process.Exited += new EventHandler(HandleProcessExit);
            m_Process.Start();

            OnProcessStarted();

            // resume process monitoring
            m_PauseMonitor = false;

            ResetPerformanceCounter();
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
            {
                if (m_CurrAttempt == m_Attempts)
                    OnMonitorStopped();

                return;
            }

            // Don't monitor till we restart
            m_PauseMonitor = true;

            OnProcessStopped();
            CallScript(m_CrashScript);

            Task.Delay(1000 * DelaySeconds)
                .ContinueWith( fn => ExecuteProcess() );

            m_CurrAttempt++;
        }

        /// <summary>
        /// Calls a script and waits for its exit. Used to call start/stop scripts
        /// </summary>
        /// <param name="path"></param>
        static void CallScript(string path)
        {
            if (path == string.Empty) return;

            ProcessStartInfo process_info = new ProcessStartInfo();
            process_info.FileName = path;

            using(Process process = new Process())
            {
                process.StartInfo = process_info;
                process.Start();
                process.WaitForExit();
            }
        }
        
        /// <summary>
        /// Clean a string and returns it as a clean file/folder path
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CleanStringAsPath(string input)
        {
            foreach (var c in Path.GetInvalidPathChars())
            {
                input = input.Replace(c.ToString(), string.Empty);
            }

            return input.Trim();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (m_PerformanceCounter != null)
                        m_PerformanceCounter.Close();

                    if (m_Process != null)
                        m_Process.Close();
                }
                // free native resources
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Stop();
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}

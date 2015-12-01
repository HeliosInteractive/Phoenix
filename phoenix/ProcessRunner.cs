using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace phoenix
{
    /// <summary>
    /// A class that runs and monitors a given process
    /// </summary>
    class ProcessRunner : IDisposable
    {
        PerformanceCounter
                    m_PerformanceCounter;
        int         m_DelaySeconds  = 0;
        int         m_Attempts      = 10;
        int         m_CurrAttempt   = 0;
        string      m_CrashScript   = string.Empty;
        string      m_ProcessPath   = string.Empty;
        string      m_CommandLine   = string.Empty;
        bool        m_Validated     = false;
        bool        m_PauseMonitor  = false;
        Process     m_Process       = null;
        bool        m_AlwaysOnTop   = false;
        bool        m_CrashIfUnresp = false;
        bool        m_CrashScrshot  = false;
        bool        m_Monitoring    = false;
        const int   m_NumSamples    = 100;
        double[]    m_MemoryUsage   = new double[m_NumSamples];
        double[]    m_CpuUsage      = new double[m_NumSamples];
        double[]    m_UsageIndices  = new double[m_NumSamples];

        /// <summary>
        /// event triggered when monitoring starts
        /// </summary>
        public Action MonitorStarted;
        /// <summary>
        /// event triggered when monitoring stops
        /// </summary>
        public Action MonitorStopped;

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

        public bool Monitoring
        {
            get { return m_Monitoring; }
        }

        public ProcessRunner()
        {
            for (int index = 0; index < m_NumSamples; ++index)
            {
                m_UsageIndices[index] = index;
                m_MemoryUsage[index] = 0;
                m_CpuUsage[index] = 0;
            }
        }

        public double[] UsageIndices
        {
            get { return m_UsageIndices; }
        }
        public double[] MemoryUsage
        {
            get { return m_MemoryUsage; }
        }
        public double[] CpuUsage
        {
            get { return m_CpuUsage; }
        }
        public int NumSamples
        {
            get { return m_NumSamples; }
        }
        public bool AssumeCrashIfNotResponsive
        {
            get { return m_CrashIfUnresp; }
            set { m_CrashIfUnresp = value; }
        }
        public bool ScreenShotOnCrash
        {
            get { return m_CrashScrshot; }
            set { m_CrashScrshot = value; }
        }
        public bool ForceAlwaysOnTop
        {
            get { return m_AlwaysOnTop; }
            set { m_AlwaysOnTop = value; }
        }

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
        public void Start()
        {
            if (!m_Validated) return;

            m_CurrAttempt = 0;
            ExecuteProcess();

            OnMonitorStarted();
        }

        /// <summary>
        /// Stops monitoring the Process
        /// </summary>
        public void Stop()
        {
            if (m_Process != null)
            {
                m_Process.EnableRaisingEvents = false;
                m_Process.CloseMainWindow(); // gently ask to close
                m_Process.Dispose();
            }

            m_Process = null;
            m_CurrAttempt = 0;
            m_PauseMonitor = true;

            OnMonitorStopped();
        }

        /// <summary>
        /// Monitor the running process
        /// </summary>
        public void Monitor()
        {
            if (!Monitorable())
                return;

            m_Process.Refresh();

            if (AssumeCrashIfNotResponsive && !m_Process.Responding)
            {
                try { m_Process.Kill(); } catch { /* no-op */ }
            }

            if (ForceAlwaysOnTop && m_Process.MainWindowHandle != IntPtr.Zero)
            {
                if (NativeMethods.GetForegroundWindow() != m_Process.MainWindowHandle)
                    NativeMethods.SetForegroundWindow(m_Process.MainWindowHandle);
            }
        }

        /// <summary>
        /// Update the metrics data.
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
            return !(m_PauseMonitor || m_Process == null || m_Process.HasExited);
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

            if (m_ProcessPath.Trim() != string.Empty && !Path.IsPathRooted(m_ProcessPath.Trim()))
                m_ProcessPath = Path.GetFullPath(m_ProcessPath.Trim());

            if (m_CrashScript.Trim() != string.Empty && !Path.IsPathRooted(m_CrashScript.Trim()))
                m_CrashScript = Path.GetFullPath(m_CrashScript.Trim());

            if (!File.Exists(m_ProcessPath) || Path.GetExtension(m_ProcessPath) != ".exe")
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
            process_info.WorkingDirectory = Path.GetDirectoryName(ProcessPath);
            process_info.UseShellExecute = false;

            if (CommandLine != string.Empty)
                process_info.Arguments = CommandLine;

            m_Process = new Process();
            m_Process.StartInfo = process_info;
            m_Process.EnableRaisingEvents = true;
            m_Process.Exited += new EventHandler(HandleProcessExit);
            m_Process.Start();

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

            if (m_CrashScript != string.Empty)
            {
                ProcessStartInfo process_info = new ProcessStartInfo();
                process_info.FileName = CrashScript;

                if (sender as Process != null)
                {
                    Process dead_process = (sender as Process);
                    process_info.Arguments = dead_process.ExitTime.Ticks.ToString();
                }

                m_Process = new Process();
                m_Process.StartInfo = process_info;
                m_Process.Start();
            }

            if (ScreenShotOnCrash)
            {
                ScreenCapture.TakeScreenShot();
            }

            Task.Delay(1000 * DelaySeconds)
                .ContinueWith( fn => ExecuteProcess() );

            m_CurrAttempt++;
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

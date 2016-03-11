namespace phoenix
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic.Devices;

    /// <summary>
    /// The central nerve system of Phoenix! Handles restarting,
    /// closing, monitoring, and etc. of a running Process.
    /// </summary>
    class ProcessRunner : IDisposable
    {
        #region Private Members

        /// <summary>Performance counter used to collect CPU usage</summary>
        PerformanceCounter      m_CpuCounter    = null;
        /// <summary>Process instance currently being monitored</summary>
        Process                 m_Process       = null;
        /// <summary>Working directory of m_Process</summary>
        string                  m_WorkingDir    = string.Empty;
        /// <summary>Absolute path to a script launched upon (re)start of m_Process</summary>
        string                  m_StartScript   = string.Empty;
        /// <summary>Absolute path to a script launched upon crash/exit of m_Process</summary>
        string                  m_CrashScript   = string.Empty;
        /// <summary>Absolute path to the executable of m_Process</summary>
        string                  m_ProcessPath   = string.Empty;
        /// <summary>Command line passed to m_Process upon its launch</summary>
        string                  m_CommandLine   = string.Empty;
        /// <summary>Cached name of m_Process (to be used for force killing m_Process)</summary>
        string                  m_CachedName    = string.Empty;
        /// <summary>Environment variables to be merged with system's variables for m_Process</summary>
        string                  m_Environment   = string.Empty;
        /// <summary>Memory usage samples</summary>
        double[]                m_MemoryUsage   = new double[m_NumSamples];
        /// <summary>CPU usage samples</summary>
        double[]                m_CpuUsage      = new double[m_NumSamples];
        /// <summary>Maximum memory available on this machine (bytes)</summary>
        double                  m_MaxMemory     = -1d;
        /// <summary>Number of Metrics samples collected in m_MemoryUsage and m_CpuUsage</summary>
        const int               m_NumSamples    = 100;
        /// <summary>The time delay (s) to wait between successive (re)starts</summary>
        int                     m_DelaySeconds  = 0;
        /// <summary>The time delay (s) to wait before assuming unresponsive m_Process is crashed</summary>
        int                     m_WaitTime      = 0;
        /// <summary>Keeps m_Process always on top (force-fully, works with Full-screen windows as well)</summary>
        bool                    m_AlwaysOnTop   = false;
        /// <summary>Assume m_Process is crashed if its main window is unresponsive</summary>
        bool                    m_CrashIfUnresp = false;
        /// <summary>Sets to be true if m_Process is being monitored or ProcessRunner is idle</summary>
        bool                    m_Monitoring    = false;
        /// <summary>If true, stdout and stderr of m_Process is captured into Phoenix' logger</summary>
        bool                    m_CaptureOutput = false;

        #endregion

        //! @cond
        #region Property Indexers

        public double[] MemoryUsage     { get { return m_MemoryUsage; } }
        public double[] CpuUsage        { get { return m_CpuUsage; } }
        public int      NumSamples      { get { return m_NumSamples; } }
        public bool     Monitoring      { get { return m_Monitoring; } }
        public string   Environment     { get; set; }
        public bool CaptureConsoleOutput
        {
            get { return m_CaptureOutput; }
            set { m_CaptureOutput = value; }
        }
        public bool AssumeCrashIfNotResponsive
        {
            get { return m_CrashIfUnresp; }
            set { m_CrashIfUnresp = value; }
        }
        public bool ForceAlwaysOnTop
        {
            get { return m_AlwaysOnTop; }
            set { m_AlwaysOnTop = value; }
        }
        public string CrashScript
        {
            get { return m_CrashScript; }
            set { m_CrashScript = value; Validate(); }
        }
        public string StartScript
        {
            get { return m_StartScript; }
            set { m_StartScript = value; Validate(); }
        }
        public string WorkingDirectory
        {
            get { return m_WorkingDir; }
            set { m_WorkingDir = value; Validate(); }
        }
        public int DelaySeconds
        {
            get { return m_DelaySeconds; }
            set { m_DelaySeconds = value; Validate(); }
        }
        public int WaitTime
        {
            get { return m_WaitTime; }
            set { m_WaitTime = value; Validate(); }
        }
        public string ProcessPath
        {
            get { return m_ProcessPath; }
            set { m_ProcessPath = value; Validate(); }
        }
        public string CommandLine
        {
            get { return m_CommandLine; }
            set { m_CommandLine = value; Validate(); }
        }
        public string CachedTitle
        {
            get { return m_CachedName; }
            set { m_CachedName = value; }
        }

        #endregion
        //! @endcond

        #region Events
        
        /// <summary>
        /// Event which is fired upon m_Process' (re)start
        /// </summary>
        public Action<ExecType> ProcessStarted;

        /// <summary>
        /// Event which is fired upon m_Process' crash/exit
        /// </summary>
        public Action<ExecType> ProcessStopped;

        //! @cond
        void OnProcessStarted(ExecType type)
        {
            ResetPerformanceCounter();

            m_Monitoring = true;
            m_CachedName = m_Process.ProcessName;

            if (ProcessStarted != null)
                ProcessStarted(type);
        }

        void OnProcessStopped(ExecType type)
        {
            m_Monitoring = false;

            if (type == ExecType.Crashed)
            {
                ExecuteScript(m_CrashScript, string.Empty, false);

                Task.Delay(new TimeSpan(0, 0, DelaySeconds))
                    .ContinueWith((fn) => {
                        if (!m_Monitoring)
                            Start(ExecType.Crashed);
                    });
            }

            if (ProcessStopped != null)
                ProcessStopped(type);
        }

        // This is here solely because I need to remove this
        // subscriber in case of a NORMAL Stop() request.
        void OnProcessCrashed(object sender, EventArgs e)
        {
            OnProcessStopped(ExecType.Crashed);
        }

        //! @endcond

        #endregion

        /// <summary>
        /// Enum, determining the type of start/stop of m_Process. In case of Crashed
        /// then all subsequent callbacks are called. In case of Normal, operations
        /// are performed silently.
        /// </summary>
        public enum ExecType { Crashed, Normal }

        /// <summary>
        /// Initializes metric sample arrays and tries to obtain available system memory
        /// </summary>
        public ProcessRunner()
        {
            m_MemoryUsage = Enumerable.Repeat(0d, NumSamples).ToArray();
            m_CpuUsage = Enumerable.Repeat(0d, NumSamples).ToArray();

            try
            {
                m_MaxMemory = new ComputerInfo().AvailablePhysicalMemory;
            }
            catch (Exception ex)
            {
                Logger.ProcessRunner.ErrorFormat("Unable to obtain available memory: {0}", ex.Message);
                m_MaxMemory = -1d;
            }
        }

        /// <summary>
        /// Stops m_Process from running. No-op if already stopped.
        /// </summary>
        /// <param name="type">stop type (crashed / normal close)</param>
        public void Stop(ExecType type)
        {
            if (m_Process != null)
            {
                // do not raise events if we are stopping
                m_Process.EnableRaisingEvents = false;
                m_Process.Exited -= OnProcessCrashed;

                try
                {
                    // Step 1. Ask window to close by sending WM_CLOSE
                    if (HasMainWindow())
                    {
                        m_Process.CloseMainWindow();
                        m_Process.WaitForExit(1000);
                    }

                    // Step 2. Ask C# Kill API to handle it
                    if (!m_Process.HasExited)
                    {
                        Logger.ProcessRunner.Warn("Failed to close with WM_CLOSE");
                        m_Process.Kill();
                        m_Process.WaitForExit(1000);
                    }

                    // Step 3. task kill that mo-fo, fo-sho
                    if (!m_Process.HasExited)
                    {
                        Logger.ProcessRunner.Warn("Failed to close with Kill API");
                        ExecuteScript("taskkill",
                            string.Format("/F /T /IM {0}.exe", m_Process.ProcessName), true);
                    }

                    m_Process.Dispose();
                    m_Process = null;
                }
                catch(Exception ex)
                {
                    Logger.ProcessRunner.ErrorFormat("Error shutting down the process: {0}",
                        ex.Message);
                }
            }

            if (!String.IsNullOrWhiteSpace(m_CachedName))
            {
                if (Process.GetProcessesByName(m_CachedName).Length > 0)
                {
                    Logger.ProcessRunner.Warn("A leftover process found with the cached name.");

                    ExecuteScript("taskkill",
                        string.Format("/F /T /IM {0}.exe", m_CachedName), true);
                }
            }

            OnProcessStopped(type);
        }

        /// <summary>
        /// Starts m_Process from running. No-op if already running.
        /// </summary>
        /// <param name="type">start type (after crash / normal start)</param>
        public void Start(ExecType type)
        {
            if (!Validate() || m_Disposed)
                return;

            Stop(ExecType.Normal);
            ExecuteScript(m_StartScript, string.Empty, false);

            m_Process = new Process {
                StartInfo = new ProcessStartInfo
                {
                    WorkingDirectory        = WorkingDirectory,
                    UseShellExecute         = false,
                    Arguments               = CommandLine,
                    FileName                = ProcessPath,
                    RedirectStandardOutput  = CaptureConsoleOutput,
                    RedirectStandardError   = CaptureConsoleOutput,
                },
                EnableRaisingEvents = true,
            };

            if (!String.IsNullOrWhiteSpace(Environment))
            {
                foreach (string variable_expr in Environment
                    .Split(new[] { "\r\n" },
                        StringSplitOptions.RemoveEmptyEntries))
                {
                    string key = string.Empty;
                    string val = string.Empty;

                    string[] split_expr = variable_expr.Split('=');
                    if (split_expr.Length >= 1)
                    {
                        key = split_expr[0].Trim();
                        if (split_expr.Length > 1)
                        {
                            val = String.Join("", split_expr.Skip(1)).Trim();
                            if (!String.IsNullOrWhiteSpace(val))
                                val = System.Environment.ExpandEnvironmentVariables(val);
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(key))
                        m_Process.StartInfo.EnvironmentVariables[key] = val;
                }
            }

            if (CaptureConsoleOutput)
            {
                m_Process.OutputDataReceived += (s, e) => {
                    if (!String.IsNullOrEmpty(e.Data))
                        Logger.ProcessRunner.InfoFormat("stdout: {0}", e.Data);
                };
                m_Process.ErrorDataReceived += (s, e) => {
                    if (!String.IsNullOrEmpty(e.Data))
                        Logger.ProcessRunner.ErrorFormat("stderr: {0}", e.Data);
                };
            }

            try
            {
                m_Process.Exited += OnProcessCrashed;
                m_Process.Start();

                if (HasMainWindow())
                    m_Process.WaitForInputIdle(5000);

                if (CaptureConsoleOutput)
                {
                    m_Process.BeginOutputReadLine();
                    m_Process.BeginErrorReadLine();
                }

                if (m_Process.Responding)
                    OnProcessStarted(type);
            }
            catch(Exception ex)
            {
                Logger.ProcessRunner.ErrorFormat("Unable to start the process: {0}"
                    , ex.Message);
                Stop(ExecType.Normal);
            }
        }

        /// <summary>
        /// Performs monitoring routines (check for crash and etc.)
        /// </summary>
        public void Monitor()
        {
            if (!Monitorable())
                return;

            m_Process.Refresh();

            if (AssumeCrashIfNotResponsive && !m_Process.Responding)
                Task.Delay(new TimeSpan(0, 0, WaitTime))
                    .ContinueWith((fn) => {
                        if (!m_Process.Responding)
                            Stop(ExecType.Crashed);
                    });

            if (ForceAlwaysOnTop && HasMainWindow())
            {
                if (NativeMethods.GetForegroundWindow() !=
                    m_Process.MainWindowHandle)
                {
                    NativeMethods.SwitchToThisWindow(m_Process.MainWindowHandle, true);
                    NativeMethods.SetForegroundWindow(m_Process.MainWindowHandle);
                    NativeMethods.SetWindowPos(
                        m_Process.MainWindowHandle,
                        NativeMethods.HWND_TOPMOST,
                        0, 0, 0, 0,
                        NativeMethods.SetWindowPosFlags.SWP_NOSIZE |
                        NativeMethods.SetWindowPosFlags.SWP_NOMOVE |
                        NativeMethods.SetWindowPosFlags.SWP_SHOWWINDOW);
                }
            }
        }

        /// <summary>
        /// Collects new metric samples of m_Process
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

            int sample_index = m_NumSamples - 1;
            m_MemoryUsage[sample_index] = m_Process.WorkingSet64 / m_MaxMemory;
            try { m_CpuUsage[sample_index] = m_CpuCounter.NextValue() / (System.Environment.ProcessorCount * 100d); }
            catch { m_CpuUsage[sample_index] = 0; }
        }

        /// <summary>
        /// Resets performance counters used
        /// </summary>
        void ResetPerformanceCounter()
        {
            if (!Monitorable())
                return;

            if (m_CpuCounter != null)
                m_CpuCounter.Close();

            m_CpuCounter = new PerformanceCounter(
                "Process",
                "% Processor Time",
                m_Process.ProcessName,
                m_Process.MachineName);
        }

        /// <summary>
        /// Returns true if m_Process is monitor able
        /// </summary>
        bool Monitorable()
        {
            try { return m_Process != null && !m_Process.HasExited; }
            catch { return false; }
        }

        /// <summary>
        /// Returns true if process has main window (false for Console processes)
        /// </summary>
        bool HasMainWindow()
        {
            try { return Monitorable() && m_Process.MainWindowHandle != IntPtr.Zero; }
            catch { return false; }
        }

        /// <summary>
        /// Validates members before launching m_Process
        /// </summary>
        /// <returns>true on success</returns>
        bool Validate()
        {
            m_WaitTime      = Math.Abs(m_WaitTime);
            m_DelaySeconds  = Math.Abs(m_DelaySeconds);
            m_ProcessPath   = m_ProcessPath.AsPath(Extensions.PathType.FilePath);
            m_CrashScript   = m_CrashScript.AsPath(Extensions.PathType.FilePath);
            m_StartScript   = m_StartScript.AsPath(Extensions.PathType.FilePath);
            m_WorkingDir    = m_WorkingDir.AsPath(Extensions.PathType.FilePath);

            if (m_ProcessPath != string.Empty && !Path.IsPathRooted(m_ProcessPath))
                m_ProcessPath = Path.GetFullPath(m_ProcessPath);

            if (m_CrashScript != string.Empty && !Path.IsPathRooted(m_CrashScript))
                m_CrashScript = Path.GetFullPath(m_CrashScript);

            if (m_StartScript != string.Empty && !Path.IsPathRooted(m_StartScript))
                m_StartScript = Path.GetFullPath(m_StartScript);

            if (m_WorkingDir != string.Empty && !Path.IsPathRooted(m_WorkingDir))
                m_WorkingDir = Path.GetFullPath(m_WorkingDir);

            if (!File.Exists(m_ProcessPath) || Path.GetExtension(m_ProcessPath) != ".exe") {
                m_ProcessPath = string.Empty;
                return false;
            }

            if (!File.Exists(m_CrashScript)) {
                m_CrashScript = string.Empty;
            }

            if (!File.Exists(m_StartScript)) {
                m_StartScript = string.Empty;
            }

            if (!Directory.Exists(m_WorkingDir)) {
                m_WorkingDir = string.Empty;
            }

            return true;
        }

        /// <summary>
        /// Executes a script
        /// </summary>
        /// <param name="script">path to the script</param>
        /// <param name="cmd">command line arguments</param>
        /// <param name="headless">launch headless?</param>
        static void ExecuteScript(string script, string cmd, bool headless)
        {
            if (String.IsNullOrWhiteSpace(script))
                return;

            using (Process process = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = script,
                    Arguments = cmd,
                    CreateNoWindow = headless,
                    UseShellExecute = headless,
                }
            })
            {
                process.Start();
                process.WaitForExit();
            }
        }

        //! @cond
        #region IDisposable Support
        private bool m_Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (m_CpuCounter != null)
                        m_CpuCounter.Close();

                    if (m_Process != null)
                        m_Process.Close();
                }
                // free native resources
                m_Disposed = true;
            }
        }

        public void Dispose()
        {
            Stop(ExecType.Normal);
            Dispose(true);
        }
        #endregion
        //! @endcond
    }
}

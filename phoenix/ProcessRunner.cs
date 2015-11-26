using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace phoenix
{
    class ProcessRunner
    {
        int     m_DelaySeconds  = 0;
        int     m_Attempts      = 10;
        int     m_CurrAttempt   = 0;
        string  m_ProcessPath   = string.Empty;
        string  m_CommandLine   = string.Empty;
        bool    m_Validated     = false;

        public int Attempts
        {
            get { return m_Attempts; }
            set { m_Attempts = value; Validate(); }
        }

        public int DelaySeconds
        {
            get { return m_DelaySeconds; }
            set { m_DelaySeconds = value; Validate(); }
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

        public void Run()
        {
            if (!m_Validated) return;

            m_CurrAttempt = 0;
            ExecuteProcess();
        }

        void Validate()
        {
            m_Validated = false;

            m_DelaySeconds = Math.Abs(m_DelaySeconds);
            m_Attempts = Math.Abs(m_Attempts);

            if (m_ProcessPath == string.Empty)
                return;

            if (!Path.IsPathRooted(m_ProcessPath))
                m_ProcessPath = Path.GetFullPath(m_ProcessPath);

            if (!File.Exists(m_ProcessPath))
            {
                m_ProcessPath = string.Empty;
                return;
            }

            m_Validated = true;
        }

        void ExecuteProcess()
        {
            if (!m_Validated) return;

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = ProcessPath;

            if (CommandLine != string.Empty)
                psi.Arguments = CommandLine;

            Process process = new Process();
            process.StartInfo = psi;
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ExecuteProcessAgain);

            process.Start();
        }

        void ExecuteProcessAgain(object sender, EventArgs e)
        {
            if (!m_Validated || (m_Attempts > 0 &&  m_CurrAttempt >= m_Attempts))
                return;

            Task.Delay(1000 * DelaySeconds)
                .ContinueWith(fn => ExecuteProcess());

            m_CurrAttempt++;
        }
    }
}

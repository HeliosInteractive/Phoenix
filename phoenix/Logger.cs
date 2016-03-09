namespace phoenix
{
    using System;
    using log4net;
    using System.IO;
    using log4net.Core;
    using log4net.Config;
    using log4net.Appender;
    using System.Windows.Forms;

    /// <summary>
    /// Phoenix wide logger class. Just a shallow wrapper
    /// for Log4Net package to organize things a bit.
    /// </summary>
    class Logger
    {
        /// <summary>Logger for Defaults class</summary>
        public static readonly ILog Defaults        = LogManager.GetLogger(typeof(phoenix.Defaults));
        /// <summary>Logger for MainDialog class</summary>
        public static readonly ILog MainDialog      = LogManager.GetLogger(typeof(phoenix.MainDialog));
        /// <summary>Logger for IniSettings class</summary>
        public static readonly ILog IniSettings     = LogManager.GetLogger(typeof(phoenix.IniSettings));
        /// <summary>Logger for RsyncClient class</summary>
        public static readonly ILog RsyncClient     = LogManager.GetLogger(typeof(phoenix.RsyncClient));
        /// <summary>Logger for ProcessRunner class</summary>
        public static readonly ILog ProcessRunner   = LogManager.GetLogger(typeof(phoenix.ProcessRunner));
        /// <summary>Logger for RemoteManager class</summary>
        public static readonly ILog RemoteManager   = LogManager.GetLogger(typeof(phoenix.RemoteManager));
        /// <summary>Logger for ReportManager class</summary>
        public static readonly ILog ReportManager   = LogManager.GetLogger(typeof(phoenix.ReportManager));
        /// <summary>Logger for ScreenCapture class</summary>
        public static readonly ILog ScreenCapture   = LogManager.GetLogger(typeof(phoenix.ScreenCapture));
        /// <summary>Logger for UpdateManager class</summary>
        public static readonly ILog UpdateManager   = LogManager.GetLogger(typeof(phoenix.UpdateManager));

        //! @cond
        public static void Configure(Form owner, RichTextBox log_box)
        {
            FileInfo logger_cfg = new FileInfo(
                Path.Combine(Program.Directory, "logger.xml"));

            if (!logger_cfg.Exists)
            {
                // configure from the configuration
                File.WriteAllText(logger_cfg.FullName, Properties.Resources.logger);
            }

            XmlConfigurator.Configure(logger_cfg);
            BasicConfigurator.Configure(new TextBoxAppender(log_box, owner));
            LogManager.GetLogger(typeof(Logger)).Info(Properties.Resources.LoggerHeader);
        }

        internal class TextBoxAppender : AppenderSkeleton
        {
            private RichTextBox m_TextBox;

            public TextBoxAppender(RichTextBox box, Form box_owner)
            {
                m_TextBox = box;
                Threshold = Level.All;
                box_owner.FormClosing += (s, e) => m_TextBox = null;
            }

            protected override void Append(LoggingEvent loggingEvent)
            {
                if (m_TextBox == null || m_TextBox.Disposing || m_TextBox.IsDisposed)
                    return;

                m_TextBox.BeginInvoke((MethodInvoker)delegate
                {
                    m_TextBox.AppendText(loggingEvent.RenderedMessage + Environment.NewLine);
                    
                    if (!m_TextBox.Visible)
                    {
                        m_TextBox.SelectionStart = m_TextBox.TextLength;
                        m_TextBox.ScrollToCaret();
                    }
                });
            }
        }
        //! endcond
    }
}

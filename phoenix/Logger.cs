namespace phoenix
{
    using System;
    using log4net;
    using System.IO;
    using log4net.Core;
    using log4net.Config;
    using log4net.Appender;
    using System.Windows.Forms;

    class Logger
    {
        public static readonly ILog Defaults        = LogManager.GetLogger(typeof(phoenix.Defaults));
        public static readonly ILog MainDialog      = LogManager.GetLogger(typeof(phoenix.MainDialog));
        public static readonly ILog IniSettings     = LogManager.GetLogger(typeof(phoenix.IniSettings));
        public static readonly ILog RsyncClient     = LogManager.GetLogger(typeof(phoenix.RsyncClient));
        public static readonly ILog ProcessRunner   = LogManager.GetLogger(typeof(phoenix.ProcessRunner));
        public static readonly ILog RemoteManager   = LogManager.GetLogger(typeof(phoenix.RemoteManager));
        public static readonly ILog ReportManager   = LogManager.GetLogger(typeof(phoenix.ReportManager));
        public static readonly ILog ScreenCapture   = LogManager.GetLogger(typeof(phoenix.ScreenCapture));
        public static readonly ILog UpdateManager   = LogManager.GetLogger(typeof(phoenix.UpdateManager));

        public static void Configure(Form owner, RichTextBox log_box)
        {
            FileInfo logger_cfg = new FileInfo(
                Path.Combine(phoenix.Program.Directory, "logger.xml"));

            if (logger_cfg.Exists)
            {
                // configure from the configuration
                XmlConfigurator.Configure(logger_cfg);
            }

            BasicConfigurator.Configure(new TextBoxAppender(log_box, owner));
        }

        public class TextBoxAppender : AppenderSkeleton
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

                m_TextBox.BeginInvoke((MethodInvoker)delegate {
                    m_TextBox.AppendText(loggingEvent.RenderedMessage + Environment.NewLine);
                });
            }
        }
    }
}

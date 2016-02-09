namespace phoenix
{
    using log4net;
    using System.IO;
    using log4net.Config;

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

        public static void Configure()
        {
            FileInfo logger_cfg = new FileInfo(
                Path.Combine(phoenix.Program.Directory, "logger.xml"));

            if (logger_cfg.Exists)
            {
                // configure from the configuration
                XmlConfigurator.Configure(logger_cfg);
            }
            else
            {
                // logger.xml is not found, log to std out
                BasicConfigurator.Configure();
            }
        }
    }
}

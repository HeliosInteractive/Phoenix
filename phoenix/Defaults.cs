namespace phoenix
{
    class Defaults
    {
        public class Local
        {
            public static string ApplicationToWtach = "";
            public static string CommandLineArguments = "";
            public static string ScriptToExecuteOnCrash = "";
            public static int TimeDelayBeforeLaunch = 10;
            public static bool ForceAlwaysOnTop = false;
            public static bool ForceMaximize = false;
            public static bool EnableMetrics = true;
            public static bool AssumeCrashIfNotResponsive = true;
            public static bool StartWithWindows = true;
            public static bool EnableVerboseLogging = true;
            public static bool EnableScreenshotOnCrash = true;
        }

        public class Remote
        {
            public static string UpdateServerAddress = "";
            public static string UpdateChannel = "#app";
            public static bool ReceiveAnonymousUpdates = false;
            public static string UpdateHash = "-- no hash --";
            public static string Username = "";
        }
    }
}

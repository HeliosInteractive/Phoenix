namespace phoenix
{
    class Defaults
    {
        public class Local
        {
            public static string ApplicationToWtach = "";
            public static string CommandLineArguments = "";
            public static string ScriptToExecuteOnCrash = "";
            public static string ScriptToExecuteOnStart = "";
            public static string WorkingDirectory = "";
            public static int TimeDelayBeforeLaunch = 10;
            public static int MaximumRetries = 10;
            public static bool ForceAlwaysOnTop = false;
            public static bool StartMinimized = false;
            public static bool AssumeCrashIfNotResponsive = true;
            public static bool EnableScreenshotOnCrash = true;
        }

        public class Remote
        {
            public static string MqttServerAddress = "test.mosquitto.org";
            public static string RSyncServerAddress = "";
            public static string RSyncServerUsername = "";
            public static string RSyncServerPassword = "";
            public static string RemoteDirectory = "";
            public static string LocalDirectory = "";
        }
    }
}

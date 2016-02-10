namespace phoenix
{
    using System;
    using System.Reflection;

    // NOTE: Keep section names and key names unique.
    // NOTE: Keep controls' names the same as keys but underscored
    class Defaults
    {
        public class Local
        {
            public static string ApplicationToWatch = "";
            public static string CommandLineArguments = "";
            public static string ScriptToExecuteOnCrash = "";
            public static string ScriptToExecuteOnStart = "";
            public static string WorkingDirectory = "";
            public static int TimeDelayBeforeLaunch = 10;
            public static int MaximumRetries = 10;
            public static bool ForceAlwaysOnTop = false;
            public static bool StartMinimized = false;
            public static bool AssumeCrashIfNotResponsive = true;
            public static bool EnableReportOnCrash = true;
        }

        public class Remote
        {
            public static string MqttServerAddress = "test.mosquitto.org";
            public static string RsyncServerAddress = "";
            public static ushort RsyncServerPort = 22;
            public static string RsyncServerUsername = "";
            public static string RemoteDirectory = "";
            public static string LocalDirectory = "";
        }
        public class Report
        {
            public static string FromEmailAddress = "phoenix-crash-report@heliosinteractive.com";
            public static string FromEmailPassword = "";
            public static string ToEmailAddress = "";
            public static string EmailSubject = "(#MACHINE_IDENTITY#) Helios Phoenix crash report";
            public static string EmailBody = "Helios Phoenix crash report,<br><br>Instance of #MACHINE_IDENTITY# has encountered a crash. Attached are a screen shot of this machine at the time of crash and optionally a log file (if specified before).<br>This machine is set to fetch updates from #RSYNC_ADDRESS# and obtain commands from #MQTT_ADDRESS#.<br><br>This is an automated message, please do not respond.";
            public static string EmailAttachment = "";
        }

        public class About
        {
            public static string UpdateFeedAddress = "http://localhost/helios/feed.xml";
        }

        public class Log
        {
            public static bool CaptureConsole = false;
        }

        public static string GetSectionByKey(string key)
        {
            foreach (Type nested_type in typeof(Defaults).GetNestedTypes())
            {
                foreach (FieldInfo field in nested_type.GetFields())
                {
                    if (field.Name == key)
                        return nested_type.Name;
                }
            }

            Logger.Defaults.ErrorFormat("Key {0} not found in default settings.", key);
            return string.Empty;
        }
    }
}

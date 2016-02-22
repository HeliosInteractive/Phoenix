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
            public static readonly string   ApplicationToWatch = "";
            public static readonly string   CommandLineArguments = "";
            public static readonly string   ScriptToExecuteOnCrash = "";
            public static readonly string   ScriptToExecuteOnStart = "";
            public static readonly string   WorkingDirectory = "";
            public static readonly string   Environment = "";
            public static readonly int      TimeDelayBeforeLaunch = 10;
            public static readonly int      WaitTime = 0;
            public static readonly bool     ForceAlwaysOnTop = false;
            public static readonly bool     StartMinimized = false;
            public static readonly bool     AssumeCrashIfNotResponsive = true;
            public static readonly bool     EnableReportOnCrash = true;
        }

        public class Remote
        {
            public static readonly string   MqttServerAddress = "";
            public static readonly string   RsyncServerAddress = "";
            public static readonly ushort   RsyncServerPort = 22;
            public static readonly string   RsyncServerUsername = "";
            public static readonly string   RemoteDirectory = "";
            public static readonly string   LocalDirectory = "";
        }

        public class Report
        {
            public static readonly string   FromEmailAddress = "";
            public static readonly string   FromEmailPassword = "";
            public static readonly string   ToEmailAddress = "";
            public static readonly string   EmailSubject = "(#MACHINE_IDENTITY#) Helios Phoenix crash report";
            public static readonly string   EmailBody = "Helios Phoenix crash report,<br><br>Instance of #MACHINE_IDENTITY# has encountered a crash. Attached are a screen shot of this machine at the time of crash and optionally a log file (if specified before).<br>This machine is set to fetch updates from #RSYNC_ADDRESS# and obtain commands from #MQTT_ADDRESS#.<br><br>This is an automated message, please do not respond.";
            public static readonly string   EmailAttachment = "";
        }

        public class About
        {
            public static readonly string   UpdateFeedAddress = "http://localhost/helios/feed.xml";
        }

        public class Log
        {
            public static readonly bool     CaptureConsole = false;
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

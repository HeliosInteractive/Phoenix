namespace phoenix
{
    using System;
    using System.Reflection;

    /// <summary>
    /// A container that holds default values of GUI parameters.
    /// If an option is not found in phoenix.ini, it will be replaced
    /// by its correspondent default entry here.
    /// <remarks>
    /// Keep section names and key names unique.
    /// Keep control names the same as keys but underscored, so:
    /// control_name --> ControlName
    /// </remarks>
    /// </summary>
    class Defaults
    {
        /// <summary>
        /// Local section holds entries related to this local instance of
        /// Phoenix. It controls mostly options about the application executable
        /// which is being monitored.
        /// </summary>
        public class Local
        {
            /// <summary>
            /// Absolute path to the monitored executable.
            /// </summary>
            public static readonly string   ApplicationToWatch = "";
            /// <summary>
            /// Command line arguments passed to ApplicationToWatch.
            /// </summary>
            public static readonly string   CommandLineArguments = "";
            /// <summary>
            /// Absolute path to a script to be executed on crash of
            /// ApplicationToWatch.
            /// </summary>
            public static readonly string   ScriptToExecuteOnCrash = "";
            /// <summary>
            /// Absolute path to a script to be executed on start of
            /// ApplicationToWatch.
            /// </summary>
            public static readonly string   ScriptToExecuteOnStart = "";
            /// <summary>
            /// Working directory of ApplicationToWatch.
            /// </summary>
            public static readonly string   WorkingDirectory = "";
            /// <summary>
            /// Environment variables to be merged with System variables
            /// to be visible to ApplicationToWatch during runtime.
            /// </summary>
            public static readonly string   Environment = "";
            /// <summary>
            /// Delay before each attempt to launch ApplicationToWatch.
            /// </summary>
            public static readonly int      TimeDelayBeforeLaunch = 10;
            /// <summary>
            /// Time Phoenix waits after ApplicationToWatch stops being
            /// responsive and assumed to be crashed.
            /// </summary>
            public static readonly int      WaitTime = 0;
            /// <summary>
            /// Forces ApplicationToWatch to stay on top all the time.
            /// </summary>
            public static readonly bool     ForceAlwaysOnTop = false;
            /// <summary>
            /// Starts Phoenix in task-bar next time it launches
            /// </summary>
            public static readonly bool     StartMinimized = false;
            /// <summary>
            /// Assume ApplicationToWatch is crashed if its main window
            /// is not responsive. <see cref="WaitTime"/>
            /// </summary>
            public static readonly bool     AssumeCrashIfNotResponsive = true;
            /// <summary>
            /// Send an email report on crash of ApplicationToWatch.
            /// </summary>
            public static readonly bool     EnableReportOnCrash = true;
        }

        /// <summary>
        /// Remote section holds options about the remote SSH update server
        /// and the MQTT command server. MQTT can be used to execute limited
        /// functionality in Phoenix instances remotely.
        /// </summary>
        public class Remote
        {
            /// <summary>
            /// Network accessible MQTT server address.
            /// </summary>
            public static readonly string   MqttServerAddress = "";
            /// <summary>
            /// Network accessible SSH server address.
            /// </summary>
            public static readonly string   RsyncServerAddress = "";
            /// <summary>
            /// Network accessible SSH server port.
            /// </summary>
            public static readonly ushort   RsyncServerPort = 22;
            /// <summary>
            /// Network accessible SSH server user name.
            /// </summary>
            public static readonly string   RsyncServerUsername = "";
            /// <summary>
            /// Where updates are located on the SSH server.
            /// </summary>
            public static readonly string   RemoteDirectory = "";
            /// <summary>
            /// Where updates should be downloaded to locally.
            /// </summary>
            public static readonly string   LocalDirectory = "";
        }

        /// <summary>
        /// Report section holds information about the email which gets
        /// sent out upon crash of Local.ApplicationToWatch
        /// </summary>
        public class Report
        {
            /// <summary>
            /// Email's "from" address.
            /// </summary>
            public static readonly string   FromEmailAddress = "";
            /// <summary>
            /// password of FromEmailAddress
            /// </summary>
            public static readonly string   FromEmailPassword = "";
            /// <summary>
            /// Email's "to" address.
            /// </summary>
            public static readonly string   ToEmailAddress = "";
            /// <summary>
            /// Email's subject.
            /// </summary>
            public static readonly string   EmailSubject = Properties.Resources.DefaultEmailTitle;
            /// <summary>
            /// Email's body.
            /// </summary>
            public static readonly string   EmailBody = Properties.Resources.DefaultEmailBody;
            /// <summary>
            /// An optional attachment to crash email.
            /// </summary>
            public static readonly string   EmailAttachment = "";
        }

        /// <summary>
        /// About section holds mostly private stuff to Phoenix. Currently
        /// it holds update server's address (A network accessible XML URL).
        /// </summary>
        public class About
        {
            /// <summary>
            /// Update feed XML address.
            /// </summary>
            public static readonly string   UpdateFeedAddress = Properties.Resources.UpdateFeedAddress;
        }

        /// <summary>
        /// Holds options for Log tab.
        /// </summary>
        public class Log
        {
            /// <summary>
            /// Enable stdout and stderr capture. This might prevent
            /// ApplicationToWatch to log to its own console but it
            /// can be useful if you want to stream logs to Papertrail
            /// for example.
            /// </summary>
            public static readonly bool     CaptureConsole = false;
        }

        /// <summary>
        /// helper that returns the section key by its member name.
        /// For example it returns "Remote" for MqttServerAddress.
        /// </summary>
        /// <param name="key">member name
        /// <example>MqttServerAddress</example>
        /// <remarks>needs to CamelCased</remarks>
        /// </param>
        /// <returns>section name or empty string if member name not found</returns>
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

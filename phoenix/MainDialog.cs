using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phoenix
{
    public partial class MainDialog : Form
    {
        private IniSettings m_AppSettings;
        public MainDialog()
        {
            m_AppSettings = new IniSettings("phoenix.ini");

            InitializeComponent();
            ApplySettings();
        }


        private void ApplySettings()
        {
            string section = "Local";

            application_to_watch.Text               = m_AppSettings.Read(section, "ApplicationToWtach",         Defaults.ApplicationToWtach);
            command_line_arguments.Text             = m_AppSettings.Read(section, "CommandLineArguments",       Defaults.CommandLineArguments);
            time_delay_before_launch.Text           = m_AppSettings.Read(section, "TimeDelayBeforeLaunch",      Defaults.TimeDelayBeforeLaunch).ToString();
            script_to_execute_on_crash.Text         = m_AppSettings.Read(section, "ScriptToExecuteOnCrash",     Defaults.ScriptToExecuteOnCrash);
            force_always_on_top.Checked             = m_AppSettings.Read(section, "ForceAlwaysOnTop",           Defaults.ForceAlwaysOnTop);
            force_maximize.Checked                  = m_AppSettings.Read(section, "ForceMaximize",              Defaults.ForceMaximize);
            enable_metrics.Checked                  = m_AppSettings.Read(section, "EnableMetrics",              Defaults.EnableMetrics);
            assume_crash_if_not_responsive.Checked  = m_AppSettings.Read(section, "AssumeCrashIfNotResponsive", Defaults.AssumeCrashIfNotResponsive);
            start_with_windows.Checked              = m_AppSettings.Read(section, "StartWithWindows",           Defaults.StartWithWindows);
            enable_verbose_logging.Checked          = m_AppSettings.Read(section, "EnableVerboseLogging",       Defaults.EnableVerboseLogging);
            enable_screenshot_on_crash.Checked      = m_AppSettings.Read(section, "EnableScreenshotOnCrash",    Defaults.EnableScreenshotOnCrash);

            section = "Remote";

            update_server_address.Text              = m_AppSettings.Read(section, "UpdateServerAddress",        Defaults.UpdateServerAddress);
            update_channel.Text                     = m_AppSettings.Read(section, "UpdateChannel",              Defaults.UpdateChannel);
            receive_anonymous_updates.Checked       = m_AppSettings.Read(section, "ReceiveAnonymousUpdates",    Defaults.ReceiveAnonymousUpdates);
            update_hash.Text                        = m_AppSettings.Read(section, "UpdateHash",                 Defaults.UpdateHash);
            username.Text                           = m_AppSettings.Read(section, "Username",                   Defaults.Username);
        }

        private void time_delay_before_launch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }
    }
}

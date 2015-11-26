using System;
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

            application_to_watch.Text               = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ApplicationToWtach),         Defaults.Local.ApplicationToWtach);
            command_line_arguments.Text             = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.CommandLineArguments),       Defaults.Local.CommandLineArguments);
            time_delay_before_launch.Text           = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.TimeDelayBeforeLaunch),      Defaults.Local.TimeDelayBeforeLaunch).ToString();
            script_to_execute_on_crash.Text         = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnCrash),     Defaults.Local.ScriptToExecuteOnCrash);
            force_always_on_top.Checked             = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ForceAlwaysOnTop),           Defaults.Local.ForceAlwaysOnTop);
            force_maximize.Checked                  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ForceMaximize),              Defaults.Local.ForceMaximize);
            enable_metrics.Checked                  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.EnableMetrics),              Defaults.Local.EnableMetrics);
            assume_crash_if_not_responsive.Checked  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.AssumeCrashIfNotResponsive), Defaults.Local.AssumeCrashIfNotResponsive);
            start_with_windows.Checked              = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.StartWithWindows),           Defaults.Local.StartWithWindows);
            enable_verbose_logging.Checked          = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.EnableVerboseLogging),       Defaults.Local.EnableVerboseLogging);
            enable_screenshot_on_crash.Checked      = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.EnableScreenshotOnCrash),    Defaults.Local.EnableScreenshotOnCrash);

            section = "Remote";

            update_server_address.Text              = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.UpdateServerAddress),        Defaults.Remote.UpdateServerAddress);
            update_channel.Text                     = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.UpdateChannel),              Defaults.Remote.UpdateChannel);
            receive_anonymous_updates.Checked       = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.ReceiveAnonymousUpdates),    Defaults.Remote.ReceiveAnonymousUpdates);
            update_hash.Text                        = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.UpdateHash),                 Defaults.Remote.UpdateHash);
            username.Text                           = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.Username),                   Defaults.Remote.Username);
        }

        private void time_delay_before_launch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void application_to_watch_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.ApplicationToWtach),
                Helpers.GetPropertyName(() => Defaults.Local.ApplicationToWtach),
                (sender as TextBox).Text);
        }
    }
}

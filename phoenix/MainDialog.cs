using System;
using System.Windows.Forms;

namespace phoenix
{
    public partial class MainDialog : Form
    {
        private IniSettings     m_AppSettings;
        private ProcessRunner   m_ProcessRunner;
        private OpenFileDialog  m_FileDialog;
        private bool            m_PauseMonitor = false;

        public MainDialog()
        {
            m_AppSettings = new IniSettings("phoenix.ini");
            m_ProcessRunner = new ProcessRunner();
            m_FileDialog = new OpenFileDialog();

            InitializeComponent();
            ApplySettings();

            process_monitor_timer.Start();
        }

        private void ApplySettings()
        {
            string section = "Local";

            application_to_watch.Text               = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ApplicationToWtach),         Defaults.Local.ApplicationToWtach);
            command_line_arguments.Text             = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.CommandLineArguments),       Defaults.Local.CommandLineArguments);
            time_delay_before_launch.Text           = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.TimeDelayBeforeLaunch),      Defaults.Local.TimeDelayBeforeLaunch).ToString();
            maximum_retries.Text                    = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.MaximumRetries),             Defaults.Local.MaximumRetries).ToString();
            script_to_execute_on_crash.Text         = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnCrash),     Defaults.Local.ScriptToExecuteOnCrash);
            force_always_on_top.Checked             = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ForceAlwaysOnTop),           Defaults.Local.ForceAlwaysOnTop);
            enable_metrics.Checked                  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.EnableMetrics),              Defaults.Local.EnableMetrics);
            assume_crash_if_not_responsive.Checked  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.AssumeCrashIfNotResponsive), Defaults.Local.AssumeCrashIfNotResponsive);
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

            m_ProcessRunner.ProcessPath = application_to_watch.Text;
        }

        private void command_line_arguments_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.CommandLineArguments),
                Helpers.GetPropertyName(() => Defaults.Local.CommandLineArguments),
                (sender as TextBox).Text);

            m_ProcessRunner.CommandLine = command_line_arguments.Text;
        }

        private void time_delay_before_launch_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.TimeDelayBeforeLaunch),
                Helpers.GetPropertyName(() => Defaults.Local.TimeDelayBeforeLaunch),
                (sender as TextBox).Text);

            m_ProcessRunner.DelaySeconds = Int32.Parse(time_delay_before_launch.Text);
        }

        private void force_always_on_top_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.ForceAlwaysOnTop),
                Helpers.GetPropertyName(() => Defaults.Local.ForceAlwaysOnTop),
                (sender as CheckBox).Checked);
        }

        private void enable_metrics_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.EnableMetrics),
                Helpers.GetPropertyName(() => Defaults.Local.EnableMetrics),
                (sender as CheckBox).Checked);
        }

        private void assume_crash_if_not_responsive_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.AssumeCrashIfNotResponsive),
                Helpers.GetPropertyName(() => Defaults.Local.AssumeCrashIfNotResponsive),
                (sender as CheckBox).Checked);
        }

        private void enable_screenshot_on_crash_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.EnableScreenshotOnCrash),
                Helpers.GetPropertyName(() => Defaults.Local.EnableScreenshotOnCrash),
                (sender as CheckBox).Checked);
        }

        private void script_to_execute_on_crash_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.ScriptToExecuteOnCrash),
                Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnCrash),
                (sender as TextBox).Text);

            m_ProcessRunner.CrashScript = script_to_execute_on_crash.Text;
        }

        private void update_server_address_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.UpdateServerAddress),
                Helpers.GetPropertyName(() => Defaults.Remote.UpdateServerAddress),
                (sender as TextBox).Text);
        }

        private void update_channel_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.UpdateChannel),
                Helpers.GetPropertyName(() => Defaults.Remote.UpdateChannel),
                (sender as TextBox).Text);
        }

        private void receive_anonymous_updates_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.ReceiveAnonymousUpdates),
                Helpers.GetPropertyName(() => Defaults.Remote.ReceiveAnonymousUpdates),
                (sender as CheckBox).Checked);
        }

        private void update_hash_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.UpdateHash),
                Helpers.GetPropertyName(() => Defaults.Remote.UpdateHash),
                (sender as TextBox).Text);
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.Username),
                Helpers.GetPropertyName(() => Defaults.Remote.Username),
                (sender as TextBox).Text);
        }

        private void maximum_retries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void maximum_retries_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.MaximumRetries),
                Helpers.GetPropertyName(() => Defaults.Local.MaximumRetries),
                (sender as TextBox).Text);

            m_ProcessRunner.Attempts = Int32.Parse(maximum_retries.Text);
        }

        private void watch_button_Click(object sender, EventArgs e)
        {
            if (watch_button.Text.StartsWith("Start"))
            {
                watch_button.Text = "Stop Watching";

                m_ProcessRunner.DelaySeconds = Int32.Parse(time_delay_before_launch.Text);
                m_ProcessRunner.ProcessPath = application_to_watch.Text;
                m_ProcessRunner.CommandLine = command_line_arguments.Text;
                m_ProcessRunner.Attempts = Int32.Parse(maximum_retries.Text);
                m_ProcessRunner.CrashScript = script_to_execute_on_crash.Text;
                m_ProcessRunner.Start();
            }
            else if(watch_button.Text.StartsWith("Stop"))
            {
                watch_button.Text = "Start Watching";

                m_ProcessRunner.Stop();
            }
        }

        private void app_path_button_Click(object sender, EventArgs e)
        {
            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                application_to_watch.Text = m_FileDialog.FileName;
            }
        }

        private void crash_script_button_Click(object sender, EventArgs e)
        {
            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                script_to_execute_on_crash.Text = m_FileDialog.FileName;
            }
        }

        private void process_monitor_timer_Tick(object sender, EventArgs e)
        {
            if (m_PauseMonitor) return;
            m_ProcessRunner.Monitor();
        }

        private void MainDialog_Activated(object sender, EventArgs e)
        {
            m_PauseMonitor = true;
        }

        private void MainDialog_Deactivate(object sender, EventArgs e)
        {
            m_PauseMonitor = false;
        }
    }
}

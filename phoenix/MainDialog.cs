using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Windows.Forms.DataVisualization.Charting;

namespace phoenix
{
    public partial class MainDialog : Form
    {
        private IniSettings         m_AppSettings;
        private ProcessRunner       m_ProcessRunner;
        private OpenFileDialog      m_FileDialog;
        private FolderBrowserDialog m_FolderDialog;
        private bool                m_PauseMonitor = false;
        private bool                m_FirstVisibleCall = true;
        private bool                m_ReportOnCrash = true;
        private Series              m_CpuUsageSeries;
        private Series              m_MemoryUsageSeries;
        static Mutex                m_SingleInstanceMutex;
        private UpdateManager       m_UpdateManager;
        private RemoteManager       m_RemoteManager;

        public MainDialog()
        {
            InitializeComponent();
            SetupTracer();

            HandleCreated += MainDialog_HandleCreated;
        }

        private void MainDialog_HandleCreated(object sender, EventArgs e)
        {
            m_AppSettings   = new IniSettings("phoenix.ini");
            m_ProcessRunner = new ProcessRunner();
            m_FileDialog    = new OpenFileDialog();
            m_FolderDialog  = new FolderBrowserDialog();
            m_UpdateManager = new UpdateManager("http://localhost/helios/feed.xml");
            m_RemoteManager = new RemoteManager();

            ApplySettings();

            m_MemoryUsageSeries = memory_chart.Series[0];
            m_CpuUsageSeries    = cpu_chart.Series[0];

            notify_icon.Icon = Icon;

            process_monitor_timer.Start();

            // These are executed in a separate thread
            m_ProcessRunner.MonitorStarted += ResetWatchButtonLabel;
            m_ProcessRunner.MonitorStopped += ResetWatchButtonLabel;
            m_ProcessRunner.ProcessStarted += ResetWatchButtonLabel;
            m_ProcessRunner.ProcessStopped += ResetWatchButtonLabel;
            m_ProcessRunner.ProcessStopped += SendCrashEmail;
            m_RemoteManager.OnConnectionOpened += ResetMqttConnectionLabel;
            m_RemoteManager.OnConnectionClosed += ResetMqttConnectionLabel;

            HotkeyManager.Register(Handle);

            ValidateAndStartMonitoring();
            ResetMqttConnectionLabel();
            ResetWatchButtonLabel();

            Logger.Info("Phoenix is up and running.");
        }

        private void SendCrashEmail()
        {
            ReportManager.Send(
                gmail_address.Text,
                gmail_password.Text,
                email_address.Text,
                email_subject.Text
                    .Replace("#MACHINE_IDENTITY#", RsyncClient.MachineIdentity),
                email_body.Text
                    .Replace("#MACHINE_IDENTITY#", RsyncClient.MachineIdentity)
                    .Replace("#RSYNC_ADDRESS#", rsync_server_address.Text)
                    .Replace("#MQTT_ADDRESS#", mqtt_server_address.Text),
                attachment.Text);
        }

        private void SetupTracer()
        {
            Trace.Listeners.Add(new TextboxWriterTraceListener(log_box, "PhoenixTextboxLog"));
            Trace.Listeners.Add(new TextWriterTraceListener("phoenix.log", "PhoneixFileLog"));
            Trace.Listeners["PhoneixFileLog"].TraceOutputOptions |= TraceOptions.DateTime;
        }

        private void ResetMqttConnectionLabel()
        {
            if (mqtt_connection_status == null ||
                mqtt_connection_status.IsDisposed ||
                m_RemoteManager == null)
                return;

            try {
                mqtt_connection_status.Invoke((MethodInvoker)(() =>
                {
                    if (m_RemoteManager.Connected)
                        mqtt_connection_status.Text = "CONNECTED";
                    else
                        mqtt_connection_status.Text = "DISCONNECTED";
                }));
            } catch {
                /* shrug */
                return;
            }
        }

        private void ResetWatchButtonLabel()
        {
            watch_button.Invoke((MethodInvoker)(() =>
            {
                if (m_ProcessRunner.Monitoring)
                    watch_button.Text = "Stop Watching ( ALT+F10 )";
                else
                    watch_button.Text = "Start Watching ( ALT+F10 )";
            }));
        }

        private bool EnsureSingleInstanceMode()
        {
            if (application_to_watch.Text == string.Empty)
                return false;

            try
            {
                if (m_SingleInstanceMutex != null)
                {
                    m_SingleInstanceMutex.ReleaseMutex();
                    m_SingleInstanceMutex.Dispose();
                    m_SingleInstanceMutex = null;
                }

                using (var md5 = MD5.Create())
                {
                    string app_hash =
                        BitConverter.ToString(md5.ComputeHash(
                            System.Text.Encoding.UTF8.GetBytes(
                                application_to_watch.Text)))
                                .Replace("-", string.Empty)
                                .ToLower();

                    bool result;
                    m_SingleInstanceMutex = new Mutex(true, "phoenix-" + app_hash, out result);

                    if (!result)
                    {
                        MessageBox.Show(string.Format(
                            "Another instance is already watching {0}.", application_to_watch.Text),
                            "Multiple instances detected!");

                        m_SingleInstanceMutex.Dispose();
                        m_SingleInstanceMutex = null;
                        return false;
                    }
                }
            }
            catch
            {
                MessageBox.Show(
                    "Could not properly ensure single instance mode.\n" +
                    "This can cause multi-instance issues.\n" +
                    "Try running as Administrator.",
                    "Failed to ensure single instance mode");
            }

            return true;
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
            start_minimized.Checked                 = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.StartMinimized),             Defaults.Local.StartMinimized);
            assume_crash_if_not_responsive.Checked  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.AssumeCrashIfNotResponsive), Defaults.Local.AssumeCrashIfNotResponsive);
            enable_email_report_on_crash.Checked    = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.EnableReportOnCrash),        Defaults.Local.EnableReportOnCrash);
            start_script.Text                       = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnStart),     Defaults.Local.ScriptToExecuteOnStart);
            working_directory.Text                  = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Local.WorkingDirectory),           Defaults.Local.WorkingDirectory);

            section = "Remote";

            mqtt_server_address.Text                = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.MqttServerAddress),         Defaults.Remote.MqttServerAddress);
            rsync_server_address.Text               = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerAddress),        Defaults.Remote.RSyncServerAddress);
            rsync_server_username.Text              = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerUsername),       Defaults.Remote.RSyncServerUsername);
            rsync_server_password.Text              = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerPassword),       Defaults.Remote.RSyncServerPassword);
            remote_directory.Text                   = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.RemoteDirectory),           Defaults.Remote.RemoteDirectory);
            local_directory.Text                    = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Remote.LocalDirectory),            Defaults.Remote.LocalDirectory);

            section = "Report";

            gmail_address.Text                      = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.FromEmailAddress),          Defaults.Report.FromEmailAddress);
            gmail_password.Text                     = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.FromEmailPassword),         Defaults.Report.FromEmailPassword);
            email_address.Text                      = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.ToEmailAddress),            Defaults.Report.ToEmailAddress);
            email_subject.Text                      = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.EmailSubject),              Defaults.Report.EmailSubject);
            email_body.Text                         = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.EmailBody),                 Defaults.Report.EmailBody).Replace("<br>", "\n");
            attachment.Text                         = m_AppSettings.Read(section, Helpers.GetPropertyName(() => Defaults.Report.EmailAttachment),           Defaults.Report.EmailAttachment);

            UpdateKeyPair();
        }

        private void UpdateKeyPair()
        {
            public_key.Text = RsyncClient.PublicKey;
            private_key.Text = RsyncClient.PrivateKey;
        }

        private void time_delay_before_launch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void application_to_watch_TextChanged(object sender, EventArgs e)
        {
            application_to_watch.Text = 
                ProcessRunner.CleanStringAsPath(application_to_watch.Text);

            if (application_to_watch.Text != string.Empty &&
                File.Exists(application_to_watch.Text) &&
                !m_FirstVisibleCall)
            {
                working_directory.Text =
                    Path.GetDirectoryName(application_to_watch.Text);
            }

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

            m_ProcessRunner.ForceAlwaysOnTop = force_always_on_top.Checked;
        }

        private void assume_crash_if_not_responsive_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.AssumeCrashIfNotResponsive),
                Helpers.GetPropertyName(() => Defaults.Local.AssumeCrashIfNotResponsive),
                (sender as CheckBox).Checked);

            m_ProcessRunner.AssumeCrashIfNotResponsive = assume_crash_if_not_responsive.Checked;
        }

        private void enable_report_on_crash_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.EnableReportOnCrash),
                Helpers.GetPropertyName(() => Defaults.Local.EnableReportOnCrash),
                (sender as CheckBox).Checked);

            m_ReportOnCrash = enable_email_report_on_crash.Checked;
        }

        private void script_to_execute_on_crash_TextChanged(object sender, EventArgs e)
        {
            script_to_execute_on_crash.Text =
                ProcessRunner.CleanStringAsPath(script_to_execute_on_crash.Text);

            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.ScriptToExecuteOnCrash),
                Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnCrash),
                (sender as TextBox).Text);

            m_ProcessRunner.CrashScript = script_to_execute_on_crash.Text;
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
            ValidateAndStartMonitoring();
        }

        private void ValidateAndStartMonitoring()
        {
            if (!m_ProcessRunner.Monitoring)
            {
                if (!EnsureSingleInstanceMode())
                    return; // silently

                bool validated = true;

                m_ProcessRunner.DelaySeconds = Int32.Parse(time_delay_before_launch.Text);
                m_ProcessRunner.ProcessPath = application_to_watch.Text;

                if (m_ProcessRunner.ProcessPath == string.Empty)
                    validated = false; // silently

                if (!File.Exists(m_ProcessRunner.ProcessPath))
                {
                    MessageBox.Show("The path you specified to watch does not exist.", "Invalid Monitor parameters");
                    validated = false;
                }

                m_ProcessRunner.CommandLine = command_line_arguments.Text;
                m_ProcessRunner.Attempts = Int32.Parse(maximum_retries.Text);
                m_ProcessRunner.CrashScript = script_to_execute_on_crash.Text;

                if (m_ProcessRunner.CrashScript != string.Empty && !File.Exists(m_ProcessRunner.CrashScript))
                {
                    MessageBox.Show("The path you specified as crash script does not exist.", "Invalid Monitor parameters");
                    validated = false;
                }

                if (validated)
                    m_ProcessRunner.Start();
            }
            else
            {
                m_ProcessRunner.Stop();
            }
        }

        private void process_monitor_timer_Tick(object sender, EventArgs e)
        {
            if (!m_PauseMonitor)
                m_ProcessRunner.Monitor();

            m_ProcessRunner.UpdateMetrics();

            m_MemoryUsageSeries.Points.Clear();
            m_CpuUsageSeries.Points.Clear();

            m_MemoryUsageSeries.Points.DataBindXY(m_ProcessRunner.UsageIndices, m_ProcessRunner.MemoryUsage);
            m_CpuUsageSeries.Points.DataBindXY(m_ProcessRunner.UsageIndices, m_ProcessRunner.CpuUsage);

            memory_chart.Invalidate();
            cpu_chart.Invalidate();
        }

        private void MainDialog_Activated(object sender, EventArgs e)
        {
            m_PauseMonitor = true;
        }

        private void MainDialog_Deactivate(object sender, EventArgs e)
        {
            m_PauseMonitor = false;
        }

        private void screenshot_button_Click(object sender, EventArgs e)
        {
            ScreenCapture.TakeScreenShot();
        }

        private void start_minimized_CheckedChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.StartMinimized),
                Helpers.GetPropertyName(() => Defaults.Local.StartMinimized),
                (sender as CheckBox).Checked);
        }

        private void toggleUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visible = !Visible;
        }

        private void exitPhoenixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (m_FirstVisibleCall)
            {
                base.SetVisibleCore(!start_minimized.Checked);
                m_FirstVisibleCall = false;
            }
            else
            {
                base.SetVisibleCore(value);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312 /* WM_HOTKEY */)
            {
                int hotkey_id = m.WParam.ToInt32();

                if (hotkey_id == HotkeyManager.TOGGLE_FORCE_ALWAYS_ON_TOP_ID)
                    force_always_on_top.Checked = !force_always_on_top.Checked;
                else if (hotkey_id == HotkeyManager.TOGGLE_CONTROL_PANEL_UI_ID)
                    Visible = !Visible;
                else if (hotkey_id == HotkeyManager.TOGGLE_MONITORING_ID)
                    if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(); else m_ProcessRunner.Start();
                else if (hotkey_id == HotkeyManager.TAKE_SCREENSHOT_ID)
                    ScreenCapture.TakeScreenShot();
            }

            base.WndProc(ref m);
        }

        private void working_directory_TextChanged(object sender, EventArgs e)
        {
            working_directory.Text =
                ProcessRunner.CleanStringAsPath(working_directory.Text);

            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.WorkingDirectory),
                Helpers.GetPropertyName(() => Defaults.Local.WorkingDirectory),
                (sender as TextBox).Text);

            m_ProcessRunner.WorkingDirectory = working_directory.Text;
        }

        private void start_script_TextChanged(object sender, EventArgs e)
        {
            start_script.Text =
                ProcessRunner.CleanStringAsPath(start_script.Text);

            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Local.ScriptToExecuteOnStart),
                Helpers.GetPropertyName(() => Defaults.Local.ScriptToExecuteOnStart),
                (sender as TextBox).Text);

            m_ProcessRunner.StartScript = start_script.Text;
        }

        private void application_to_watch_DoubleClick(object sender, EventArgs e)
        {
            m_FileDialog.Filter = "Windows Executable (*.exe)|*.exe";
            m_FileDialog.Title = "Select application to watch";

            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                application_to_watch.Text = m_FileDialog.FileName;
            }
        }

        private void working_directory_DoubleClick(object sender, EventArgs e)
        {
            m_FolderDialog.Description = "Select working directory";

            if (m_FolderDialog.ShowDialog() == DialogResult.OK)
            {
                working_directory.Text = m_FolderDialog.SelectedPath;
            }
        }

        private void script_to_execute_on_crash_DoubleClick(object sender, EventArgs e)
        {
            m_FileDialog.Filter = string.Empty;
            m_FileDialog.Title = "Select crash script";

            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                script_to_execute_on_crash.Text = m_FileDialog.FileName;
            }
        }

        private void start_script_DoubleClick(object sender, EventArgs e)
        {
            m_FileDialog.Filter = string.Empty;
            m_FileDialog.Title = "Select start script";

            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                start_script.Text = m_FileDialog.FileName;
            }
        }

        private void generate_new_keys_Click(object sender, EventArgs e)
        {
            RsyncClient.RegenerateKeys();
            UpdateKeyPair();
        }

        private void mqtt_server_address_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.MqttServerAddress),
                Helpers.GetPropertyName(() => Defaults.Remote.MqttServerAddress),
                (sender as TextBox).Text);

            m_RemoteManager.Connect((sender as TextBox).Text, "/helios/phoenix");
        }

        private void rsync_server_address_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.RSyncServerAddress),
                Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerAddress),
                (sender as TextBox).Text);

            ValidateAsServerAddress(sender);
        }

        private void rsync_server_username_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.RSyncServerUsername),
                Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerUsername),
                (sender as TextBox).Text);
        }

        private void rsync_server_password_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.RSyncServerPassword),
                Helpers.GetPropertyName(() => Defaults.Remote.RSyncServerPassword),
                (sender as TextBox).Text);
        }

        private void remote_directory_TextChanged(object sender, EventArgs e)
        {
            remote_directory.Text =
                ProcessRunner.CleanStringAsPath(remote_directory.Text);

            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.RemoteDirectory),
                Helpers.GetPropertyName(() => Defaults.Remote.RemoteDirectory),
                (sender as TextBox).Text);
        }

        private void local_directory_TextChanged(object sender, EventArgs e)
        {
            local_directory.Text =
                ProcessRunner.CleanStringAsPath(local_directory.Text);

            local_directory.Text = 
                RsyncClient.PathToCygwinPath(local_directory.Text);

            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Remote.LocalDirectory),
                Helpers.GetPropertyName(() => Defaults.Remote.LocalDirectory),
                (sender as TextBox).Text);
        }

        private bool ServerAdressValid(string address)
        {
            if (address == string.Empty) return false;
            try
            {
                Uri uri = new Uri("dummy://" + address);
                return uri.DnsSafeHost != string.Empty && uri.Port > 0;
            }
            catch { /* no-op */ }
            return false;
        }

        private void ValidateAsServerAddress(object sender)
        {
            TextBox tb = (sender as TextBox);
            if (tb == null) return;

            if (!ServerAdressValid(tb.Text))
                tb.BackColor = System.Drawing.Color.Salmon;
            else
                tb.BackColor = System.Drawing.Color.LightGreen;
        }

        private void public_key_TextChanged(object sender, EventArgs e)
        {
            if (public_key.Text != RsyncClient.PublicKey)
                RsyncClient.PublicKey = public_key.Text;
        }

        private void private_key_TextChanged(object sender, EventArgs e)
        {
            if (private_key.Text != RsyncClient.PrivateKey)
                RsyncClient.PrivateKey = private_key.Text;
        }

        private void local_directory_DoubleClick(object sender, EventArgs e)
        {
            m_FolderDialog.Description = "Select local directory to synchronize";

            if (m_FolderDialog.ShowDialog() == DialogResult.OK)
            {
                local_directory.Text = m_FolderDialog.SelectedPath;
            }
        }

        private void gmail_address_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.FromEmailAddress),
                Helpers.GetPropertyName(() => Defaults.Report.FromEmailAddress),
                (sender as TextBox).Text);
        }

        private void email_address_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.ToEmailAddress),
                Helpers.GetPropertyName(() => Defaults.Report.ToEmailAddress),
                (sender as TextBox).Text);
        }

        private void gmail_password_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.FromEmailPassword),
                Helpers.GetPropertyName(() => Defaults.Report.FromEmailPassword),
                (sender as TextBox).Text);
        }

        private void email_subject_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.EmailSubject),
                Helpers.GetPropertyName(() => Defaults.Report.EmailSubject),
                (sender as TextBox).Text);
        }

        private void attachment_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.EmailAttachment),
                Helpers.GetPropertyName(() => Defaults.Report.EmailAttachment),
                (sender as TextBox).Text);
        }

        private void email_body_TextChanged(object sender, EventArgs e)
        {
            m_AppSettings.Store(
                Helpers.GetClassName(() => Defaults.Report.EmailBody),
                Helpers.GetPropertyName(() => Defaults.Report.EmailBody),
                (sender as TextBox).Text.Replace("\n", "<br>"));
        }
    }
}

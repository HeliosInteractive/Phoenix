namespace phoenix
{
    using System;
    using System.IO;
    using Properties;
    using System.Threading;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Security.Cryptography;
    using System.Windows.Forms.DataVisualization.Charting;

    public partial class MainDialog : Form
    {
        private HotkeyManager       m_HotkeyManager;
        private PersistentSettings         m_AppSettings;
        private ProcessRunner       m_ProcessRunner;
        private OpenFileDialog      m_FileDialog;
        private FolderBrowserDialog m_FolderDialog;
        private Series              m_CpuUsageSeries;
        private Series              m_GpuUsageSeries;
        private Series              m_RamUsageSeries;
        private UpdateManager       m_UpdateManager;
        private RemoteManager       m_RemoteManager;
        private ReportManager       m_ReportManager;
        private RsyncClient         m_RsyncClient;
        private Metrics.Manager     m_MetricsManager;
        private static Mutex        m_SingleInstanceMutex;
        private bool                m_PhoenixReady = false;


        public MainDialog()
        {
            InitializeComponent();
            HandleCreated += OnHandleCreated;
        }

        private void OnHandleCreated(object sender, EventArgs e)
        {
            Logger.Configure(this, log_box);

            about_browser.DocumentStream = Assembly.
                GetExecutingAssembly().
                GetManifestResourceStream("phoenix.Resources.about.html");

            m_MetricsManager= new Metrics.Manager();
            m_HotkeyManager = new HotkeyManager(Handle);
            m_AppSettings   = new PersistentSettings();
            m_ProcessRunner = new ProcessRunner();
            m_FileDialog    = new OpenFileDialog();
            m_FolderDialog  = new FolderBrowserDialog();
            m_UpdateManager = new UpdateManager();
            m_RemoteManager = new RemoteManager();
            m_ReportManager = new ReportManager();
            m_RsyncClient   = new RsyncClient();

            m_RemoteManager.OnConnectionOpened += () => { ExecuteOnUiThread(OnMqttConnectionOpen); };
            m_RemoteManager.OnConnectionClosed += () => { ExecuteOnUiThread(OnMqttConnectionClose); };
            m_RemoteManager.OnMessage += (m, t) => { ExecuteOnUiThread(() => { OnMqttMessage(m, t); }); };

            ApplySettings();

            m_RamUsageSeries = metrics_chart.Series["ram_usage_series"];
            m_CpuUsageSeries = metrics_chart.Series["cpu_usage_series"];
            m_GpuUsageSeries = metrics_chart.Series["gpu_usage_series"];

            notify_icon.Icon = Icon;
            process_monitor_timer.Start();

            // These are executed in a separate thread
            m_ProcessRunner.ProcessStarted += (type) => { ExecuteOnUiThread(() => { OnProcessStart(type); }); };
            m_ProcessRunner.ProcessStopped += (type) => { ExecuteOnUiThread(() => { OnProcessStop(type); }); };

            ToggleMonitoring();
            ResetMqttConnectionLabel();
            ResetWatchButtonLabel();
            ResetReportTabStatus();

            Logger.MainDialog.InfoFormat("Phoenix is up and running on [{0}]."
                , RsyncClient.MachineIdentity);

            m_UpdateManager.Check();
            m_PhoenixReady = true;
        }

        private bool EnsureSingleInstanceMode()
        {
            if (application_to_watch.Text == string.Empty ||
                !File.Exists(application_to_watch.Text))
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
                        Logger.MainDialog.ErrorFormat("Another instance is already watching {0}."
                            , application_to_watch.Text);

                        m_SingleInstanceMutex.Dispose();
                        m_SingleInstanceMutex = null;
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.MainDialog.ErrorFormat("{0} {1} {2} : {3}",
                    "Could not properly ensure single instance mode.",
                    "This can cause multi-instance issues.",
                    "Try running as Administrator.",
                    ex.Message);
            }

            return true;
        }

        private void ApplySettings()
        {
            foreach (Type nested_type in typeof(Defaults).GetNestedTypes())
            {
                foreach (FieldInfo field in nested_type.GetFields())
                {
                    ApplyControlSetting(nested_type.Name, field.Name);
                }
            }

            m_ProcessRunner.CachedTitle = m_AppSettings.Read("Internal", "CachedName", m_ProcessRunner.CachedTitle);
            wait_time.Enabled = assume_crash_if_not_responsive.Checked;
            UpdateKeyPair();
        }

        void ApplyControlSetting(string section, string name)
        {
            object value    = typeof(Defaults).GetNestedType(section).GetField(name).GetValue(null);
            string element  = name.ToUnderScore();
            Control control = null;

            FieldInfo field = typeof(MainDialog).GetField(element, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null) {
                control = field.GetValue(this) as Control;
            }

            if (control == null) {
                Logger.MainDialog.ErrorFormat("Could not lookup control element of entry {0} in {1}", name, section);
                return;
            }

            if (control as TextBoxBase != null)
                ((TextBoxBase)control).Text = m_AppSettings.Read(section, name, value.ToString());
            else if (control as CheckBox != null)
                ((CheckBox)control).Checked = m_AppSettings.Read(section, name, (bool)value);
            else
                Logger.MainDialog.Error("Invalid control type is detected.");
        }

        private void UpdateKeyPair()
        {
            public_key.Text = RsyncClient.PublicKey;
            private_key.Text = RsyncClient.PrivateKey;
        }

        private void FilterDigitKeys(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Delete)))
                e.Handled = true;
        }

        private void StoreControlValue(object sender, EventArgs e)
        {
            if ((sender as Control) == null)
                return;

            Control control = (Control)sender;
            string control_name_cc = control.Name.ToCamelCase();

            OnControlValidate(control);

            if (control as TextBoxBase != null)
                m_AppSettings.Write(Defaults.GetSectionByKey(control_name_cc), control_name_cc, (sender as TextBoxBase).Text);
            else if (control as CheckBox != null)
                m_AppSettings.Write(Defaults.GetSectionByKey(control_name_cc), control_name_cc, (sender as CheckBox).Checked);
            else
                Logger.MainDialog.Error("Control type is not supported.");

            OnControlValueChanged(control);
        }

        private void OnControlValidate(Control control)
        {
            if (control == application_to_watch) {
                application_to_watch.Text = application_to_watch.Text.AsPath(Extensions.PathType.FilePath);

                if (application_to_watch.Text != string.Empty &&
                    File.Exists(application_to_watch.Text) &&
                    m_PhoenixReady)
                {
                    working_directory.Text =
                        Path.GetDirectoryName(application_to_watch.Text);
                    // chance that we want this to be the same is high
                    local_directory.Text = working_directory.Text;
                }
            } else if (control == script_to_execute_on_crash ||
                control == script_to_execute_on_start ||
                control == remote_directory ||
                control == working_directory) {
                (control as TextBoxBase).Text = ((control as TextBoxBase).Text).AsPath(Extensions.PathType.FilePath);
            } else if (control == local_directory) {
                local_directory.Text = local_directory.Text.AsCygwinPath();
            }
        }

        private void OnControlValueChanged(Control control)
        {
            if (control == command_line_arguments) {
                m_ProcessRunner.CommandLine = command_line_arguments.Text;
            } else if (control == force_always_on_top) {
                m_ProcessRunner.ForceAlwaysOnTop = force_always_on_top.Checked;
            } else if (control == time_delay_before_launch) {
                if (String.IsNullOrEmpty(time_delay_before_launch.Text.Trim())) {
                    m_ProcessRunner.DelaySeconds = 0;
                } else {
                    m_ProcessRunner.DelaySeconds = Int32.Parse(time_delay_before_launch.Text);
                }
            } else if (control == assume_crash_if_not_responsive) {
                m_ProcessRunner.AssumeCrashIfNotResponsive = assume_crash_if_not_responsive.Checked;
                wait_time.Enabled = assume_crash_if_not_responsive.Checked;
            } else if (control == enable_report_on_crash) {
                ResetReportTabStatus();
            } else if (control == mqtt_server_address) {
                m_RemoteManager.Connect(mqtt_server_address.Text, Resources.MqttTopic);
            } else if (control == application_to_watch) {
                m_ProcessRunner.ProcessPath = application_to_watch.Text;
                if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            } else if (control == script_to_execute_on_crash) {
                m_ProcessRunner.CrashScript = script_to_execute_on_crash.Text;
                if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            } else if (control == working_directory) {
                m_ProcessRunner.WorkingDirectory = working_directory.Text;
                if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            } else if (control == script_to_execute_on_start) {
                m_ProcessRunner.StartScript = script_to_execute_on_start.Text;
                if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            } else if (control == environment) {
                m_ProcessRunner.Environment = environment.Text;
                if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            } else if (control == update_feed_address) {
                m_UpdateManager.FeedAddress = update_feed_address.Text;
            } else if (control == capture_console) {
                m_ProcessRunner.CaptureConsoleOutput = capture_console.Checked;
                if (m_PhoenixReady &&
                    m_ProcessRunner.Monitoring)
                {
                    m_ProcessRunner.Start(ProcessRunner.ExecType.Normal);
                }
            }
        }

        private void OnWatchButtonClick(object sender, EventArgs e)
        {
            ToggleMonitoring();
        }

        private void ToggleMonitoring()
        {
            if (!m_ProcessRunner.Monitoring)
            {
                if (!EnsureSingleInstanceMode())
                {
                    Logger.MainDialog.Error("Could not guarantee single instance mode. Aborting.");
                    return;
                }

                m_ProcessRunner.DelaySeconds                = String.IsNullOrWhiteSpace(time_delay_before_launch.Text) ? 0 : Int32.Parse(time_delay_before_launch.Text);
                m_ProcessRunner.ResponseTime                = String.IsNullOrWhiteSpace(wait_time.Text) ? 0 : Int32.Parse(wait_time.Text);
                m_ProcessRunner.ProcessPath                 = application_to_watch.Text.AsPath(Extensions.PathType.FilePath);
                m_ProcessRunner.CommandLine                 = command_line_arguments.Text;
                m_ProcessRunner.StartScript                 = script_to_execute_on_start.Text.AsPath(Extensions.PathType.FilePath);
                m_ProcessRunner.CrashScript                 = script_to_execute_on_crash.Text.AsPath(Extensions.PathType.FilePath);
                m_ProcessRunner.WorkingDirectory            = working_directory.Text.AsPath(Extensions.PathType.FilePath);
                m_ProcessRunner.ForceAlwaysOnTop            = force_always_on_top.Checked;
                m_ProcessRunner.AssumeCrashIfNotResponsive  = assume_crash_if_not_responsive.Checked;

                m_ProcessRunner.Start(ProcessRunner.ExecType.Normal);
            }
            else
            {
                m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
            }
        }

        private void OnMonitorTimerTick(object sender, EventArgs e)
        {
            m_ProcessRunner.Monitor();
        }

        private void OnGraphTimerTick(object sender, EventArgs e)
        {
            m_MetricsManager.Update();

            if (enable_ram_graph.Checked)
                m_RamUsageSeries.Points.DataBindY(m_MetricsManager.RamSamples);

            if (enable_cpu_graph.Checked)
                m_CpuUsageSeries.Points.DataBindY(m_MetricsManager.CpuSamples);

            if (enable_gpu_graph.Checked)
                m_GpuUsageSeries.Points.DataBindY(m_MetricsManager.GpuSamples);
        }

        private void OnScreenshotButtonClick(object sender, EventArgs e) { ScreenCapture.TakeScreenShot(); }
        private void OnToggleUIToolStripMenuItemClick(object sender, EventArgs e) { Visible = !Visible; }
        private void OnExitPhoenixToolStripMenuItemClick(object sender, EventArgs e) { Application.Exit(); }

        protected override void SetVisibleCore(bool value)
        {
            if (!m_PhoenixReady)
            {
                if (!IsHandleCreated) CreateHandle();
                base.SetVisibleCore(!start_minimized.Checked);
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

                if (hotkey_id == (int)HotkeyManager.ID.ForceAlwaysOnTop)
                    force_always_on_top.Checked = !force_always_on_top.Checked;
                else if (hotkey_id == (int)HotkeyManager.ID.ControlPanelUi)
                    Visible = !Visible;
                else if (hotkey_id == (int)HotkeyManager.ID.Monitoring)
                    if (m_ProcessRunner.Monitoring) m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal);
                    else m_ProcessRunner.Start(ProcessRunner.ExecType.Normal);
                else if (hotkey_id == (int)HotkeyManager.ID.Screenshot)
                    ScreenCapture.TakeScreenShot();
            }

            base.WndProc(ref m);
        }

        private void SelectFileDialog(object sender, EventArgs e)
        {
            if (sender == application_to_watch)
                m_FileDialog.Filter = "Windows Executable (*.exe)|*.exe";
            else
                m_FileDialog.Filter = string.Empty;

            m_FileDialog.Title = "Please select a file to continue";

            if (m_FileDialog.ShowDialog() == DialogResult.OK && (sender as TextBoxBase) != null)
            {
                (sender as TextBoxBase).Text = m_FileDialog.FileName;
            }
        }
        private void SelectFolderDialog(object sender, EventArgs e)
        {
            m_FolderDialog.Description = "Please select a folder to continue";

            if (m_FolderDialog.ShowDialog() == DialogResult.OK && (sender as TextBoxBase) != null)
            {
                (sender as TextBoxBase).Text = m_FolderDialog.SelectedPath;
            }
        }

        private void OnGenerateNewKeysClick(object sender, EventArgs e)
        {
            RsyncClient.GenerateKeys();
            UpdateKeyPair();
        }

        private void OnPublicKeyTextChanged(object sender, EventArgs e)
        {
            if (public_key.Text != RsyncClient.PublicKey)
                RsyncClient.PublicKey = public_key.Text;
        }

        private void OnPrivateKeyTextChanged(object sender, EventArgs e)
        {
            if (private_key.Text != RsyncClient.PrivateKey)
                RsyncClient.PrivateKey = private_key.Text;
        }

        private void DragDropAcceptFirstFile(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                if (sender as TextBox != null)
                    (sender as TextBox).Text = files[0];
            }
        }

        private void DragEnterEffectChange(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void OnPullUpdateClick(object sender, EventArgs e)
        {
            try
            {
                main_tab.SelectedTab = log;
                m_RsyncClient.Execute(
                    remote_directory.Text,
                    local_directory.Text,
                    rsync_server_username.Text,
                    rsync_server_address.Text,
                    ushort.Parse(rsync_server_port.Text),
                    StopProcessRunner,
                    StartProcessRunner);
            }
            catch
            {
                Logger.MainDialog.Error("Unable to execute rsync.");
            }
        }

        private void ExecuteOnUiThread(Action action)
        {
            if (action == null)
                return;

            try
            {
                main_tab.Invoke((MethodInvoker)(action.Invoke));
            }
            catch
            {
                Logger.MainDialog.ErrorFormat("{0} {1}",
                    "Method execution on UI thread failed.",
                    "Perhaps you are calling something after exit?");
            }
        }

        private void StopProcessRunner()
        {
            ExecuteOnUiThread(()=> { m_ProcessRunner.Stop(ProcessRunner.ExecType.Normal); });
        }

        private void StartProcessRunner()
        {
            ExecuteOnUiThread(ToggleMonitoring);
        }

        private void ResetMqttConnectionLabel()
        {
            if (mqtt_connection_status == null ||
                mqtt_connection_status.IsDisposed ||
                m_RemoteManager == null)
                return;

            if (m_RemoteManager.Connected)
                mqtt_connection_status.Text = "CONNECTED";
            else
                mqtt_connection_status.Text = "DISCONNECTED";
        }

        private void ResetWatchButtonLabel()
        {
            if (m_ProcessRunner.Monitoring)
                watch_button.Text = "Stop Watching ( ALT+F10 )";
            else
                watch_button.Text = "Start Watching ( ALT+F10 )";
        }

        private void ResetReportTabStatus()
        {
            (report_tab as Control).Enabled
                = enable_report_on_crash.Checked;
        }

        private void SendCrashEmail()
        {
            if (!enable_report_on_crash.Checked)
                return;

            m_ReportManager.SendEmail(
                from_email_address.Text,
                from_email_password.Text,
                to_email_address.Text,
                email_subject.Text
                    .Replace("#MACHINE_IDENTITY#", RsyncClient.MachineIdentity),
                email_body.Text
                    .Replace("#MACHINE_IDENTITY#", RsyncClient.MachineIdentity)
                    .Replace("#RSYNC_ADDRESS#", rsync_server_address.Text)
                    .Replace("#MQTT_ADDRESS#", mqtt_server_address.Text),
                email_attachment.Text);
        }

        private void OnGraphCheckedChanged(object sender, EventArgs e)
        {
            if (!enable_cpu_graph.Checked)
                m_CpuUsageSeries.Points.Clear();

            if (!enable_gpu_graph.Checked)
                m_GpuUsageSeries.Points.Clear();

            if (!enable_ram_graph.Checked)
                m_RamUsageSeries.Points.Clear();
        }
    }
}

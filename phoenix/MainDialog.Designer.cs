namespace phoenix
{
    partial class MainDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                m_ProcessRunner.Dispose();
                m_RemoteManager.Dispose();
                m_ReportManager.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.main_tab = new System.Windows.Forms.TabControl();
            this.tab_local = new System.Windows.Forms.TabPage();
            this.script_to_execute_on_start = new System.Windows.Forms.TextBox();
            this.start_script_label = new System.Windows.Forms.Label();
            this.working_directory = new System.Windows.Forms.TextBox();
            this.working_directory_label = new System.Windows.Forms.Label();
            this.memory_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cpu_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.maximum_retries = new System.Windows.Forms.TextBox();
            this.time_delay_before_launch = new System.Windows.Forms.TextBox();
            this.assume_crash_if_not_responsive = new System.Windows.Forms.CheckBox();
            this.screenshot_button = new System.Windows.Forms.Button();
            this.enable_report_on_crash = new System.Windows.Forms.CheckBox();
            this.watch_button = new System.Windows.Forms.Button();
            this.script_to_execute_on_crash = new System.Windows.Forms.TextBox();
            this.crash_script_label = new System.Windows.Forms.Label();
            this.start_minimized = new System.Windows.Forms.CheckBox();
            this.force_always_on_top = new System.Windows.Forms.CheckBox();
            this.time_delay_label = new System.Windows.Forms.Label();
            this.command_line_arguments = new System.Windows.Forms.TextBox();
            this.cmd_line_label = new System.Windows.Forms.Label();
            this.app_path_label = new System.Windows.Forms.Label();
            this.application_to_watch = new System.Windows.Forms.TextBox();
            this.remote_tab = new System.Windows.Forms.TabPage();
            this.mqtt_connection_status = new System.Windows.Forms.Label();
            this.mqtt_connection_status_label = new System.Windows.Forms.Label();
            this.local_directory = new System.Windows.Forms.TextBox();
            this.remote_directory = new System.Windows.Forms.TextBox();
            this.local_directory_label = new System.Windows.Forms.Label();
            this.remote_directory_label = new System.Windows.Forms.Label();
            this.pull_update = new System.Windows.Forms.Button();
            this.generate_new_keys = new System.Windows.Forms.Button();
            this.private_key_label = new System.Windows.Forms.Label();
            this.public_key_label = new System.Windows.Forms.Label();
            this.private_key = new System.Windows.Forms.RichTextBox();
            this.public_key = new System.Windows.Forms.RichTextBox();
            this.rsync_server_password_label = new System.Windows.Forms.Label();
            this.rsync_server_username_label = new System.Windows.Forms.Label();
            this.rsync_server_port = new System.Windows.Forms.TextBox();
            this.rsync_server_username = new System.Windows.Forms.TextBox();
            this.rsync_server_address = new System.Windows.Forms.TextBox();
            this.rsync_server_address_label = new System.Windows.Forms.Label();
            this.mqtt_server_address = new System.Windows.Forms.TextBox();
            this.rabbitmq_server_address_label = new System.Windows.Forms.Label();
            this.report_tab = new System.Windows.Forms.TabPage();
            this.attachment_label = new System.Windows.Forms.Label();
            this.attachment = new System.Windows.Forms.TextBox();
            this.email_body = new System.Windows.Forms.RichTextBox();
            this.email_body_label = new System.Windows.Forms.Label();
            this.email_subject_label = new System.Windows.Forms.Label();
            this.email_subject = new System.Windows.Forms.TextBox();
            this.gmail_password = new System.Windows.Forms.TextBox();
            this.gmail_password_label = new System.Windows.Forms.Label();
            this.email_address = new System.Windows.Forms.TextBox();
            this.email_address_label = new System.Windows.Forms.Label();
            this.gmail_address = new System.Windows.Forms.TextBox();
            this.gmail_address_label = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TabPage();
            this.log_box = new System.Windows.Forms.RichTextBox();
            this.about_tab = new System.Windows.Forms.TabPage();
            this.update_feed_address_label = new System.Windows.Forms.Label();
            this.update_feed_address = new System.Windows.Forms.TextBox();
            this.about_browser = new System.Windows.Forms.WebBrowser();
            this.process_monitor_timer = new System.Windows.Forms.Timer(this.components);
            this.notify_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.context_menu_strip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitPhoenixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.main_tab.SuspendLayout();
            this.tab_local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memory_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpu_chart)).BeginInit();
            this.remote_tab.SuspendLayout();
            this.report_tab.SuspendLayout();
            this.log.SuspendLayout();
            this.about_tab.SuspendLayout();
            this.context_menu_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_tab
            // 
            this.main_tab.Controls.Add(this.tab_local);
            this.main_tab.Controls.Add(this.remote_tab);
            this.main_tab.Controls.Add(this.report_tab);
            this.main_tab.Controls.Add(this.log);
            this.main_tab.Controls.Add(this.about_tab);
            this.main_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main_tab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.main_tab.Location = new System.Drawing.Point(0, 0);
            this.main_tab.Multiline = true;
            this.main_tab.Name = "main_tab";
            this.main_tab.SelectedIndex = 0;
            this.main_tab.Size = new System.Drawing.Size(384, 338);
            this.main_tab.TabIndex = 0;
            // 
            // tab_local
            // 
            this.tab_local.Controls.Add(this.script_to_execute_on_start);
            this.tab_local.Controls.Add(this.start_script_label);
            this.tab_local.Controls.Add(this.working_directory);
            this.tab_local.Controls.Add(this.working_directory_label);
            this.tab_local.Controls.Add(this.memory_chart);
            this.tab_local.Controls.Add(this.cpu_chart);
            this.tab_local.Controls.Add(this.label2);
            this.tab_local.Controls.Add(this.maximum_retries);
            this.tab_local.Controls.Add(this.time_delay_before_launch);
            this.tab_local.Controls.Add(this.assume_crash_if_not_responsive);
            this.tab_local.Controls.Add(this.screenshot_button);
            this.tab_local.Controls.Add(this.enable_report_on_crash);
            this.tab_local.Controls.Add(this.watch_button);
            this.tab_local.Controls.Add(this.script_to_execute_on_crash);
            this.tab_local.Controls.Add(this.crash_script_label);
            this.tab_local.Controls.Add(this.start_minimized);
            this.tab_local.Controls.Add(this.force_always_on_top);
            this.tab_local.Controls.Add(this.time_delay_label);
            this.tab_local.Controls.Add(this.command_line_arguments);
            this.tab_local.Controls.Add(this.cmd_line_label);
            this.tab_local.Controls.Add(this.app_path_label);
            this.tab_local.Controls.Add(this.application_to_watch);
            this.tab_local.Location = new System.Drawing.Point(4, 22);
            this.tab_local.Name = "tab_local";
            this.tab_local.Padding = new System.Windows.Forms.Padding(3);
            this.tab_local.Size = new System.Drawing.Size(376, 312);
            this.tab_local.TabIndex = 0;
            this.tab_local.Text = "Local";
            this.tab_local.UseVisualStyleBackColor = true;
            // 
            // script_to_execute_on_start
            // 
            this.script_to_execute_on_start.AllowDrop = true;
            this.script_to_execute_on_start.Location = new System.Drawing.Point(193, 82);
            this.script_to_execute_on_start.Name = "script_to_execute_on_start";
            this.script_to_execute_on_start.Size = new System.Drawing.Size(173, 20);
            this.script_to_execute_on_start.TabIndex = 7;
            this.script_to_execute_on_start.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.script_to_execute_on_start.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropAcceptFirstFile);
            this.script_to_execute_on_start.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEffectChange);
            this.script_to_execute_on_start.DoubleClick += new System.EventHandler(this.SelectFileDialog);
            // 
            // start_script_label
            // 
            this.start_script_label.AutoSize = true;
            this.start_script_label.Location = new System.Drawing.Point(8, 85);
            this.start_script_label.Name = "start_script_label";
            this.start_script_label.Size = new System.Drawing.Size(130, 13);
            this.start_script_label.TabIndex = 22;
            this.start_script_label.Text = "Execute script on (re)start:";
            // 
            // working_directory
            // 
            this.working_directory.Location = new System.Drawing.Point(193, 32);
            this.working_directory.Name = "working_directory";
            this.working_directory.Size = new System.Drawing.Size(173, 20);
            this.working_directory.TabIndex = 3;
            this.working_directory.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.working_directory.DoubleClick += new System.EventHandler(this.SelectFolderDialog);
            // 
            // working_directory_label
            // 
            this.working_directory_label.AutoSize = true;
            this.working_directory_label.Location = new System.Drawing.Point(8, 35);
            this.working_directory_label.Name = "working_directory_label";
            this.working_directory_label.Size = new System.Drawing.Size(131, 13);
            this.working_directory_label.TabIndex = 17;
            this.working_directory_label.Text = "Process working directory:";
            // 
            // memory_chart
            // 
            this.memory_chart.BorderlineWidth = 0;
            chartArea7.AxisX.IsMarginVisible = false;
            chartArea7.AxisX.LabelStyle.Enabled = false;
            chartArea7.AxisX.LineWidth = 0;
            chartArea7.AxisX.MajorGrid.Enabled = false;
            chartArea7.AxisX.MajorTickMark.Enabled = false;
            chartArea7.AxisY.IsMarginVisible = false;
            chartArea7.AxisY.LabelStyle.Enabled = false;
            chartArea7.AxisY.LineWidth = 0;
            chartArea7.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea7.AxisY.MajorTickMark.Enabled = false;
            chartArea7.BackColor = System.Drawing.Color.Snow;
            chartArea7.BorderWidth = 0;
            chartArea7.Name = "metrics_chart_area";
            chartArea7.Position.Auto = false;
            chartArea7.Position.Height = 100F;
            chartArea7.Position.Width = 100F;
            this.memory_chart.ChartAreas.Add(chartArea7);
            legend7.Alignment = System.Drawing.StringAlignment.Far;
            legend7.BackColor = System.Drawing.Color.Transparent;
            legend7.DockedToChartArea = "metrics_chart_area";
            legend7.Name = "mem_legend";
            this.memory_chart.Legends.Add(legend7);
            this.memory_chart.Location = new System.Drawing.Point(193, 158);
            this.memory_chart.Name = "memory_chart";
            this.memory_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series7.ChartArea = "metrics_chart_area";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.IsXValueIndexed = true;
            series7.Legend = "mem_legend";
            series7.LegendText = "% Memory Usage";
            series7.Name = "memory_usage_series";
            this.memory_chart.Series.Add(series7);
            this.memory_chart.Size = new System.Drawing.Size(173, 76);
            this.memory_chart.TabIndex = 0;
            this.memory_chart.TabStop = false;
            // 
            // cpu_chart
            // 
            this.cpu_chart.BorderlineWidth = 0;
            chartArea8.AxisX.IsMarginVisible = false;
            chartArea8.AxisX.LabelStyle.Enabled = false;
            chartArea8.AxisX.LineWidth = 0;
            chartArea8.AxisX.MajorGrid.Enabled = false;
            chartArea8.AxisX.MajorTickMark.Enabled = false;
            chartArea8.AxisY.IsMarginVisible = false;
            chartArea8.AxisY.LabelStyle.Enabled = false;
            chartArea8.AxisY.LineWidth = 0;
            chartArea8.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea8.AxisY.MajorTickMark.Enabled = false;
            chartArea8.BackColor = System.Drawing.Color.Snow;
            chartArea8.BorderWidth = 0;
            chartArea8.Name = "metrics_chart_area";
            chartArea8.Position.Auto = false;
            chartArea8.Position.Height = 100F;
            chartArea8.Position.Width = 100F;
            this.cpu_chart.ChartAreas.Add(chartArea8);
            legend8.Alignment = System.Drawing.StringAlignment.Far;
            legend8.BackColor = System.Drawing.Color.Transparent;
            legend8.DockedToChartArea = "metrics_chart_area";
            legend8.IsTextAutoFit = false;
            legend8.Name = "cpu_legend";
            this.cpu_chart.Legends.Add(legend8);
            this.cpu_chart.Location = new System.Drawing.Point(8, 158);
            this.cpu_chart.Name = "cpu_chart";
            this.cpu_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series8.ChartArea = "metrics_chart_area";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.IsXValueIndexed = true;
            series8.Legend = "cpu_legend";
            series8.LegendText = "% CPU Usage";
            series8.Name = "cpu_usage_series";
            this.cpu_chart.Series.Add(series8);
            this.cpu_chart.Size = new System.Drawing.Size(173, 76);
            this.cpu_chart.TabIndex = 0;
            this.cpu_chart.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(285, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Retries (0 = inf):";
            // 
            // maximum_retries
            // 
            this.maximum_retries.Location = new System.Drawing.Point(285, 130);
            this.maximum_retries.Name = "maximum_retries";
            this.maximum_retries.Size = new System.Drawing.Size(81, 20);
            this.maximum_retries.TabIndex = 11;
            this.maximum_retries.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.maximum_retries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterDigitKeys);
            // 
            // time_delay_before_launch
            // 
            this.time_delay_before_launch.Location = new System.Drawing.Point(193, 130);
            this.time_delay_before_launch.Name = "time_delay_before_launch";
            this.time_delay_before_launch.Size = new System.Drawing.Size(81, 20);
            this.time_delay_before_launch.TabIndex = 10;
            this.time_delay_before_launch.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.time_delay_before_launch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterDigitKeys);
            // 
            // assume_crash_if_not_responsive
            // 
            this.assume_crash_if_not_responsive.AutoSize = true;
            this.assume_crash_if_not_responsive.Location = new System.Drawing.Point(8, 258);
            this.assume_crash_if_not_responsive.Name = "assume_crash_if_not_responsive";
            this.assume_crash_if_not_responsive.Size = new System.Drawing.Size(172, 17);
            this.assume_crash_if_not_responsive.TabIndex = 14;
            this.assume_crash_if_not_responsive.Text = "Assume crash if not responsive";
            this.assume_crash_if_not_responsive.UseVisualStyleBackColor = true;
            this.assume_crash_if_not_responsive.CheckedChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // screenshot_button
            // 
            this.screenshot_button.Location = new System.Drawing.Point(8, 280);
            this.screenshot_button.Name = "screenshot_button";
            this.screenshot_button.Size = new System.Drawing.Size(173, 24);
            this.screenshot_button.TabIndex = 16;
            this.screenshot_button.Text = "Take a screenshot ( ALT+F9 )";
            this.screenshot_button.UseVisualStyleBackColor = true;
            this.screenshot_button.Click += new System.EventHandler(this.OnScreenshotButtonClick);
            // 
            // enable_report_on_crash
            // 
            this.enable_report_on_crash.AutoSize = true;
            this.enable_report_on_crash.Location = new System.Drawing.Point(193, 258);
            this.enable_report_on_crash.Name = "enable_report_on_crash";
            this.enable_report_on_crash.Size = new System.Drawing.Size(160, 17);
            this.enable_report_on_crash.TabIndex = 15;
            this.enable_report_on_crash.Text = "Enable email report on crash";
            this.enable_report_on_crash.UseVisualStyleBackColor = true;
            this.enable_report_on_crash.CheckedChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // watch_button
            // 
            this.watch_button.Location = new System.Drawing.Point(192, 280);
            this.watch_button.Name = "watch_button";
            this.watch_button.Size = new System.Drawing.Size(173, 24);
            this.watch_button.TabIndex = 17;
            this.watch_button.Text = "Start Watching ( ALT+F10 )";
            this.watch_button.UseVisualStyleBackColor = true;
            this.watch_button.Click += new System.EventHandler(this.OnWatchButtonClick);
            // 
            // script_to_execute_on_crash
            // 
            this.script_to_execute_on_crash.AllowDrop = true;
            this.script_to_execute_on_crash.Location = new System.Drawing.Point(193, 57);
            this.script_to_execute_on_crash.Name = "script_to_execute_on_crash";
            this.script_to_execute_on_crash.Size = new System.Drawing.Size(173, 20);
            this.script_to_execute_on_crash.TabIndex = 5;
            this.script_to_execute_on_crash.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.script_to_execute_on_crash.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropAcceptFirstFile);
            this.script_to_execute_on_crash.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEffectChange);
            this.script_to_execute_on_crash.DoubleClick += new System.EventHandler(this.SelectFileDialog);
            // 
            // crash_script_label
            // 
            this.crash_script_label.AutoSize = true;
            this.crash_script_label.Location = new System.Drawing.Point(8, 60);
            this.crash_script_label.Name = "crash_script_label";
            this.crash_script_label.Size = new System.Drawing.Size(121, 13);
            this.crash_script_label.TabIndex = 10;
            this.crash_script_label.Text = "Execute script on crash:";
            // 
            // start_minimized
            // 
            this.start_minimized.AutoSize = true;
            this.start_minimized.Location = new System.Drawing.Point(193, 238);
            this.start_minimized.Name = "start_minimized";
            this.start_minimized.Size = new System.Drawing.Size(137, 17);
            this.start_minimized.TabIndex = 13;
            this.start_minimized.Text = "Start Phoenix minimized";
            this.start_minimized.UseVisualStyleBackColor = true;
            this.start_minimized.CheckedChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // force_always_on_top
            // 
            this.force_always_on_top.AutoSize = true;
            this.force_always_on_top.Location = new System.Drawing.Point(8, 240);
            this.force_always_on_top.Name = "force_always_on_top";
            this.force_always_on_top.Size = new System.Drawing.Size(183, 17);
            this.force_always_on_top.TabIndex = 12;
            this.force_always_on_top.Text = "Keep process on top ( ALT+F12 )";
            this.force_always_on_top.UseVisualStyleBackColor = true;
            this.force_always_on_top.CheckedChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // time_delay_label
            // 
            this.time_delay_label.AutoSize = true;
            this.time_delay_label.Location = new System.Drawing.Point(193, 110);
            this.time_delay_label.Name = "time_delay_label";
            this.time_delay_label.Size = new System.Drawing.Size(85, 13);
            this.time_delay_label.TabIndex = 4;
            this.time_delay_label.Text = "Spawn delay (s):";
            // 
            // command_line_arguments
            // 
            this.command_line_arguments.Location = new System.Drawing.Point(8, 130);
            this.command_line_arguments.Name = "command_line_arguments";
            this.command_line_arguments.Size = new System.Drawing.Size(173, 20);
            this.command_line_arguments.TabIndex = 9;
            this.command_line_arguments.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // cmd_line_label
            // 
            this.cmd_line_label.AutoSize = true;
            this.cmd_line_label.Location = new System.Drawing.Point(8, 110);
            this.cmd_line_label.Name = "cmd_line_label";
            this.cmd_line_label.Size = new System.Drawing.Size(128, 13);
            this.cmd_line_label.TabIndex = 2;
            this.cmd_line_label.Text = "Command line arguments:";
            // 
            // app_path_label
            // 
            this.app_path_label.AutoSize = true;
            this.app_path_label.Location = new System.Drawing.Point(8, 10);
            this.app_path_label.Name = "app_path_label";
            this.app_path_label.Size = new System.Drawing.Size(170, 13);
            this.app_path_label.TabIndex = 1;
            this.app_path_label.Text = "Process to watch ( only .exe files ):";
            // 
            // application_to_watch
            // 
            this.application_to_watch.AllowDrop = true;
            this.application_to_watch.Location = new System.Drawing.Point(193, 7);
            this.application_to_watch.Name = "application_to_watch";
            this.application_to_watch.Size = new System.Drawing.Size(173, 20);
            this.application_to_watch.TabIndex = 1;
            this.application_to_watch.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.application_to_watch.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropAcceptFirstFile);
            this.application_to_watch.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEffectChange);
            this.application_to_watch.DoubleClick += new System.EventHandler(this.SelectFileDialog);
            // 
            // remote_tab
            // 
            this.remote_tab.Controls.Add(this.mqtt_connection_status);
            this.remote_tab.Controls.Add(this.mqtt_connection_status_label);
            this.remote_tab.Controls.Add(this.local_directory);
            this.remote_tab.Controls.Add(this.remote_directory);
            this.remote_tab.Controls.Add(this.local_directory_label);
            this.remote_tab.Controls.Add(this.remote_directory_label);
            this.remote_tab.Controls.Add(this.pull_update);
            this.remote_tab.Controls.Add(this.generate_new_keys);
            this.remote_tab.Controls.Add(this.private_key_label);
            this.remote_tab.Controls.Add(this.public_key_label);
            this.remote_tab.Controls.Add(this.private_key);
            this.remote_tab.Controls.Add(this.public_key);
            this.remote_tab.Controls.Add(this.rsync_server_password_label);
            this.remote_tab.Controls.Add(this.rsync_server_username_label);
            this.remote_tab.Controls.Add(this.rsync_server_port);
            this.remote_tab.Controls.Add(this.rsync_server_username);
            this.remote_tab.Controls.Add(this.rsync_server_address);
            this.remote_tab.Controls.Add(this.rsync_server_address_label);
            this.remote_tab.Controls.Add(this.mqtt_server_address);
            this.remote_tab.Controls.Add(this.rabbitmq_server_address_label);
            this.remote_tab.Location = new System.Drawing.Point(4, 22);
            this.remote_tab.Name = "remote_tab";
            this.remote_tab.Padding = new System.Windows.Forms.Padding(3);
            this.remote_tab.Size = new System.Drawing.Size(376, 312);
            this.remote_tab.TabIndex = 1;
            this.remote_tab.Text = "Remote";
            this.remote_tab.UseVisualStyleBackColor = true;
            // 
            // mqtt_connection_status
            // 
            this.mqtt_connection_status.AutoSize = true;
            this.mqtt_connection_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mqtt_connection_status.Location = new System.Drawing.Point(193, 33);
            this.mqtt_connection_status.Name = "mqtt_connection_status";
            this.mqtt_connection_status.Size = new System.Drawing.Size(104, 13);
            this.mqtt_connection_status.TabIndex = 34;
            this.mqtt_connection_status.Text = "DISCONNECTED";
            // 
            // mqtt_connection_status_label
            // 
            this.mqtt_connection_status_label.AutoSize = true;
            this.mqtt_connection_status_label.Location = new System.Drawing.Point(193, 10);
            this.mqtt_connection_status_label.Name = "mqtt_connection_status_label";
            this.mqtt_connection_status_label.Size = new System.Drawing.Size(128, 13);
            this.mqtt_connection_status_label.TabIndex = 13;
            this.mqtt_connection_status_label.Text = "MQTT connection status:";
            // 
            // local_directory
            // 
            this.local_directory.Location = new System.Drawing.Point(193, 130);
            this.local_directory.Name = "local_directory";
            this.local_directory.Size = new System.Drawing.Size(173, 20);
            this.local_directory.TabIndex = 8;
            this.local_directory.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.local_directory.DoubleClick += new System.EventHandler(this.SelectFolderDialog);
            // 
            // remote_directory
            // 
            this.remote_directory.Location = new System.Drawing.Point(8, 130);
            this.remote_directory.Name = "remote_directory";
            this.remote_directory.Size = new System.Drawing.Size(173, 20);
            this.remote_directory.TabIndex = 7;
            this.remote_directory.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // local_directory_label
            // 
            this.local_directory_label.AutoSize = true;
            this.local_directory_label.Location = new System.Drawing.Point(193, 110);
            this.local_directory_label.Name = "local_directory_label";
            this.local_directory_label.Size = new System.Drawing.Size(79, 13);
            this.local_directory_label.TabIndex = 33;
            this.local_directory_label.Text = "Local directory:";
            // 
            // remote_directory_label
            // 
            this.remote_directory_label.AutoSize = true;
            this.remote_directory_label.Location = new System.Drawing.Point(8, 110);
            this.remote_directory_label.Name = "remote_directory_label";
            this.remote_directory_label.Size = new System.Drawing.Size(90, 13);
            this.remote_directory_label.TabIndex = 32;
            this.remote_directory_label.Text = "Remote directory:";
            // 
            // pull_update
            // 
            this.pull_update.Location = new System.Drawing.Point(192, 280);
            this.pull_update.Name = "pull_update";
            this.pull_update.Size = new System.Drawing.Size(173, 24);
            this.pull_update.TabIndex = 12;
            this.pull_update.Text = "Pull updates manually";
            this.pull_update.UseVisualStyleBackColor = true;
            this.pull_update.Click += new System.EventHandler(this.OnPullUpdateClick);
            // 
            // generate_new_keys
            // 
            this.generate_new_keys.Location = new System.Drawing.Point(8, 280);
            this.generate_new_keys.Name = "generate_new_keys";
            this.generate_new_keys.Size = new System.Drawing.Size(173, 24);
            this.generate_new_keys.TabIndex = 11;
            this.generate_new_keys.Text = "Generate new key pair";
            this.generate_new_keys.UseVisualStyleBackColor = true;
            this.generate_new_keys.Click += new System.EventHandler(this.OnGenerateNewKeysClick);
            // 
            // private_key_label
            // 
            this.private_key_label.AutoSize = true;
            this.private_key_label.Location = new System.Drawing.Point(193, 160);
            this.private_key_label.Name = "private_key_label";
            this.private_key_label.Size = new System.Drawing.Size(63, 13);
            this.private_key_label.TabIndex = 29;
            this.private_key_label.Text = "Private key:";
            // 
            // public_key_label
            // 
            this.public_key_label.AutoSize = true;
            this.public_key_label.Location = new System.Drawing.Point(8, 160);
            this.public_key_label.Name = "public_key_label";
            this.public_key_label.Size = new System.Drawing.Size(59, 13);
            this.public_key_label.TabIndex = 28;
            this.public_key_label.Text = "Public key:";
            // 
            // private_key
            // 
            this.private_key.DetectUrls = false;
            this.private_key.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.private_key.Location = new System.Drawing.Point(193, 180);
            this.private_key.Name = "private_key";
            this.private_key.Size = new System.Drawing.Size(173, 90);
            this.private_key.TabIndex = 10;
            this.private_key.Text = "";
            this.private_key.TextChanged += new System.EventHandler(this.OnPrivateKeyTextChanged);
            // 
            // public_key
            // 
            this.public_key.DetectUrls = false;
            this.public_key.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.public_key.Location = new System.Drawing.Point(8, 180);
            this.public_key.Name = "public_key";
            this.public_key.Size = new System.Drawing.Size(173, 90);
            this.public_key.TabIndex = 9;
            this.public_key.Text = "";
            this.public_key.TextChanged += new System.EventHandler(this.OnPublicKeyTextChanged);
            // 
            // rsync_server_password_label
            // 
            this.rsync_server_password_label.AutoSize = true;
            this.rsync_server_password_label.Location = new System.Drawing.Point(285, 60);
            this.rsync_server_password_label.Name = "rsync_server_password_label";
            this.rsync_server_password_label.Size = new System.Drawing.Size(53, 13);
            this.rsync_server_password_label.TabIndex = 25;
            this.rsync_server_password_label.Text = "SSH port:";
            // 
            // rsync_server_username_label
            // 
            this.rsync_server_username_label.AutoSize = true;
            this.rsync_server_username_label.Location = new System.Drawing.Point(193, 60);
            this.rsync_server_username_label.Name = "rsync_server_username_label";
            this.rsync_server_username_label.Size = new System.Drawing.Size(81, 13);
            this.rsync_server_username_label.TabIndex = 24;
            this.rsync_server_username_label.Text = "SSH username:";
            // 
            // rsync_server_port
            // 
            this.rsync_server_port.Location = new System.Drawing.Point(285, 80);
            this.rsync_server_port.Name = "rsync_server_port";
            this.rsync_server_port.Size = new System.Drawing.Size(81, 20);
            this.rsync_server_port.TabIndex = 6;
            this.rsync_server_port.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.rsync_server_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FilterDigitKeys);
            // 
            // rsync_server_username
            // 
            this.rsync_server_username.Location = new System.Drawing.Point(193, 80);
            this.rsync_server_username.Name = "rsync_server_username";
            this.rsync_server_username.Size = new System.Drawing.Size(81, 20);
            this.rsync_server_username.TabIndex = 5;
            this.rsync_server_username.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // rsync_server_address
            // 
            this.rsync_server_address.Location = new System.Drawing.Point(8, 80);
            this.rsync_server_address.Name = "rsync_server_address";
            this.rsync_server_address.Size = new System.Drawing.Size(173, 20);
            this.rsync_server_address.TabIndex = 4;
            this.rsync_server_address.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // rsync_server_address_label
            // 
            this.rsync_server_address_label.AutoSize = true;
            this.rsync_server_address_label.Location = new System.Drawing.Point(8, 60);
            this.rsync_server_address_label.Name = "rsync_server_address_label";
            this.rsync_server_address_label.Size = new System.Drawing.Size(104, 13);
            this.rsync_server_address_label.TabIndex = 4;
            this.rsync_server_address_label.Text = "SSH server address:";
            // 
            // mqtt_server_address
            // 
            this.mqtt_server_address.BackColor = System.Drawing.Color.White;
            this.mqtt_server_address.Location = new System.Drawing.Point(8, 30);
            this.mqtt_server_address.Name = "mqtt_server_address";
            this.mqtt_server_address.Size = new System.Drawing.Size(173, 20);
            this.mqtt_server_address.TabIndex = 1;
            this.mqtt_server_address.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // rabbitmq_server_address_label
            // 
            this.rabbitmq_server_address_label.AutoSize = true;
            this.rabbitmq_server_address_label.Location = new System.Drawing.Point(8, 10);
            this.rabbitmq_server_address_label.Name = "rabbitmq_server_address_label";
            this.rabbitmq_server_address_label.Size = new System.Drawing.Size(176, 13);
            this.rabbitmq_server_address_label.TabIndex = 2;
            this.rabbitmq_server_address_label.Text = "MQTT server address (port = 1883):";
            // 
            // report_tab
            // 
            this.report_tab.Controls.Add(this.attachment_label);
            this.report_tab.Controls.Add(this.attachment);
            this.report_tab.Controls.Add(this.email_body);
            this.report_tab.Controls.Add(this.email_body_label);
            this.report_tab.Controls.Add(this.email_subject_label);
            this.report_tab.Controls.Add(this.email_subject);
            this.report_tab.Controls.Add(this.gmail_password);
            this.report_tab.Controls.Add(this.gmail_password_label);
            this.report_tab.Controls.Add(this.email_address);
            this.report_tab.Controls.Add(this.email_address_label);
            this.report_tab.Controls.Add(this.gmail_address);
            this.report_tab.Controls.Add(this.gmail_address_label);
            this.report_tab.Location = new System.Drawing.Point(4, 22);
            this.report_tab.Name = "report_tab";
            this.report_tab.Padding = new System.Windows.Forms.Padding(3);
            this.report_tab.Size = new System.Drawing.Size(376, 312);
            this.report_tab.TabIndex = 5;
            this.report_tab.Text = "Report";
            this.report_tab.UseVisualStyleBackColor = true;
            // 
            // attachment_label
            // 
            this.attachment_label.AutoSize = true;
            this.attachment_label.Location = new System.Drawing.Point(8, 110);
            this.attachment_label.Name = "attachment_label";
            this.attachment_label.Size = new System.Drawing.Size(326, 13);
            this.attachment_label.TabIndex = 9;
            this.attachment_label.Text = "File to attach: (NOTE: use crash script to upload it if file is too large!)";
            // 
            // attachment
            // 
            this.attachment.AllowDrop = true;
            this.attachment.BackColor = System.Drawing.Color.White;
            this.attachment.Location = new System.Drawing.Point(8, 130);
            this.attachment.Name = "attachment";
            this.attachment.Size = new System.Drawing.Size(358, 20);
            this.attachment.TabIndex = 10;
            this.attachment.TextChanged += new System.EventHandler(this.StoreControlValue);
            this.attachment.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropAcceptFirstFile);
            this.attachment.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnterEffectChange);
            this.attachment.DoubleClick += new System.EventHandler(this.SelectFileDialog);
            // 
            // email_body
            // 
            this.email_body.Location = new System.Drawing.Point(8, 178);
            this.email_body.Name = "email_body";
            this.email_body.Size = new System.Drawing.Size(358, 125);
            this.email_body.TabIndex = 12;
            this.email_body.Text = resources.GetString("email_body.Text");
            this.email_body.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // email_body_label
            // 
            this.email_body_label.AutoSize = true;
            this.email_body_label.Location = new System.Drawing.Point(8, 158);
            this.email_body_label.Name = "email_body_label";
            this.email_body_label.Size = new System.Drawing.Size(61, 13);
            this.email_body_label.TabIndex = 11;
            this.email_body_label.Text = "Email body:";
            // 
            // email_subject_label
            // 
            this.email_subject_label.AutoSize = true;
            this.email_subject_label.Location = new System.Drawing.Point(193, 60);
            this.email_subject_label.Name = "email_subject_label";
            this.email_subject_label.Size = new System.Drawing.Size(72, 13);
            this.email_subject_label.TabIndex = 7;
            this.email_subject_label.Text = "Email subject:";
            // 
            // email_subject
            // 
            this.email_subject.BackColor = System.Drawing.Color.White;
            this.email_subject.Location = new System.Drawing.Point(193, 80);
            this.email_subject.Name = "email_subject";
            this.email_subject.Size = new System.Drawing.Size(173, 20);
            this.email_subject.TabIndex = 8;
            this.email_subject.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // gmail_password
            // 
            this.gmail_password.BackColor = System.Drawing.Color.White;
            this.gmail_password.Location = new System.Drawing.Point(8, 80);
            this.gmail_password.Name = "gmail_password";
            this.gmail_password.PasswordChar = '*';
            this.gmail_password.Size = new System.Drawing.Size(173, 20);
            this.gmail_password.TabIndex = 6;
            this.gmail_password.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // gmail_password_label
            // 
            this.gmail_password_label.AutoSize = true;
            this.gmail_password_label.Location = new System.Drawing.Point(8, 60);
            this.gmail_password_label.Name = "gmail_password_label";
            this.gmail_password_label.Size = new System.Drawing.Size(131, 13);
            this.gmail_password_label.TabIndex = 5;
            this.gmail_password_label.Text = "GMAIL password [FROM]:";
            // 
            // email_address
            // 
            this.email_address.BackColor = System.Drawing.Color.White;
            this.email_address.Location = new System.Drawing.Point(193, 30);
            this.email_address.Name = "email_address";
            this.email_address.Size = new System.Drawing.Size(173, 20);
            this.email_address.TabIndex = 4;
            this.email_address.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // email_address_label
            // 
            this.email_address_label.AutoSize = true;
            this.email_address_label.Location = new System.Drawing.Point(193, 10);
            this.email_address_label.Name = "email_address_label";
            this.email_address_label.Size = new System.Drawing.Size(134, 13);
            this.email_address_label.TabIndex = 3;
            this.email_address_label.Text = "GMAIL email address [TO]:";
            // 
            // gmail_address
            // 
            this.gmail_address.BackColor = System.Drawing.Color.White;
            this.gmail_address.Location = new System.Drawing.Point(8, 30);
            this.gmail_address.Name = "gmail_address";
            this.gmail_address.Size = new System.Drawing.Size(173, 20);
            this.gmail_address.TabIndex = 2;
            this.gmail_address.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // gmail_address_label
            // 
            this.gmail_address_label.AutoSize = true;
            this.gmail_address_label.Location = new System.Drawing.Point(8, 10);
            this.gmail_address_label.Name = "gmail_address_label";
            this.gmail_address_label.Size = new System.Drawing.Size(150, 13);
            this.gmail_address_label.TabIndex = 1;
            this.gmail_address_label.Text = "GMAIL email address [FROM]:";
            // 
            // log
            // 
            this.log.Controls.Add(this.log_box);
            this.log.Location = new System.Drawing.Point(4, 22);
            this.log.Name = "log";
            this.log.Padding = new System.Windows.Forms.Padding(3);
            this.log.Size = new System.Drawing.Size(376, 312);
            this.log.TabIndex = 4;
            this.log.Text = "Log";
            this.log.UseVisualStyleBackColor = true;
            // 
            // log_box
            // 
            this.log_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log_box.Location = new System.Drawing.Point(3, 3);
            this.log_box.Name = "log_box";
            this.log_box.ReadOnly = true;
            this.log_box.Size = new System.Drawing.Size(370, 306);
            this.log_box.TabIndex = 0;
            this.log_box.Text = "";
            this.log_box.WordWrap = false;
            // 
            // about_tab
            // 
            this.about_tab.Controls.Add(this.update_feed_address_label);
            this.about_tab.Controls.Add(this.update_feed_address);
            this.about_tab.Controls.Add(this.about_browser);
            this.about_tab.Location = new System.Drawing.Point(4, 22);
            this.about_tab.Name = "about_tab";
            this.about_tab.Padding = new System.Windows.Forms.Padding(3);
            this.about_tab.Size = new System.Drawing.Size(376, 312);
            this.about_tab.TabIndex = 6;
            this.about_tab.Text = "About";
            this.about_tab.UseVisualStyleBackColor = true;
            // 
            // update_feed_address_label
            // 
            this.update_feed_address_label.AutoSize = true;
            this.update_feed_address_label.Location = new System.Drawing.Point(8, 283);
            this.update_feed_address_label.Name = "update_feed_address_label";
            this.update_feed_address_label.Size = new System.Drawing.Size(148, 13);
            this.update_feed_address_label.TabIndex = 10;
            this.update_feed_address_label.Text = "Phoenix update feed address:";
            // 
            // update_feed_address
            // 
            this.update_feed_address.Location = new System.Drawing.Point(192, 280);
            this.update_feed_address.Name = "update_feed_address";
            this.update_feed_address.Size = new System.Drawing.Size(173, 20);
            this.update_feed_address.TabIndex = 2;
            this.update_feed_address.TextChanged += new System.EventHandler(this.StoreControlValue);
            // 
            // about_browser
            // 
            this.about_browser.AllowNavigation = false;
            this.about_browser.AllowWebBrowserDrop = false;
            this.about_browser.Dock = System.Windows.Forms.DockStyle.Top;
            this.about_browser.IsWebBrowserContextMenuEnabled = false;
            this.about_browser.Location = new System.Drawing.Point(3, 3);
            this.about_browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.about_browser.Name = "about_browser";
            this.about_browser.ScriptErrorsSuppressed = true;
            this.about_browser.Size = new System.Drawing.Size(370, 263);
            this.about_browser.TabIndex = 1;
            this.about_browser.WebBrowserShortcutsEnabled = false;
            // 
            // process_monitor_timer
            // 
            this.process_monitor_timer.Interval = 60;
            this.process_monitor_timer.Tick += new System.EventHandler(this.OnApplicationTick);
            // 
            // notify_icon
            // 
            this.notify_icon.ContextMenuStrip = this.context_menu_strip;
            this.notify_icon.Visible = true;
            // 
            // context_menu_strip
            // 
            this.context_menu_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleUIToolStripMenuItem,
            this.exitPhoenixToolStripMenuItem});
            this.context_menu_strip.Name = "context_menu_strip";
            this.context_menu_strip.ShowImageMargin = false;
            this.context_menu_strip.Size = new System.Drawing.Size(159, 48);
            // 
            // toggleUIToolStripMenuItem
            // 
            this.toggleUIToolStripMenuItem.Name = "toggleUIToolStripMenuItem";
            this.toggleUIToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.toggleUIToolStripMenuItem.Text = "Toggle UI (ALT+F11)";
            this.toggleUIToolStripMenuItem.Click += new System.EventHandler(this.OnToggleUIToolStripMenuItemClick);
            // 
            // exitPhoenixToolStripMenuItem
            // 
            this.exitPhoenixToolStripMenuItem.Name = "exitPhoenixToolStripMenuItem";
            this.exitPhoenixToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitPhoenixToolStripMenuItem.Text = "Exit Phoenix";
            this.exitPhoenixToolStripMenuItem.Click += new System.EventHandler(this.OnExitPhoenixToolStripMenuItemClick);
            // 
            // MainDialog
            // 
            this.AccessibleDescription = "Monitors and restarts crashed applications.";
            this.AccessibleName = "Phoenix";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 338);
            this.Controls.Add(this.main_tab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phoenix Control Panel";
            this.TopMost = true;
            this.main_tab.ResumeLayout(false);
            this.tab_local.ResumeLayout(false);
            this.tab_local.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memory_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpu_chart)).EndInit();
            this.remote_tab.ResumeLayout(false);
            this.remote_tab.PerformLayout();
            this.report_tab.ResumeLayout(false);
            this.report_tab.PerformLayout();
            this.log.ResumeLayout(false);
            this.about_tab.ResumeLayout(false);
            this.about_tab.PerformLayout();
            this.context_menu_strip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl main_tab;
        private System.Windows.Forms.TabPage tab_local;
        private System.Windows.Forms.TabPage remote_tab;
        private System.Windows.Forms.TextBox application_to_watch;
        private System.Windows.Forms.Label app_path_label;
        private System.Windows.Forms.Label cmd_line_label;
        private System.Windows.Forms.TextBox command_line_arguments;
        private System.Windows.Forms.Label time_delay_label;
        private System.Windows.Forms.CheckBox force_always_on_top;
        private System.Windows.Forms.CheckBox start_minimized;
        private System.Windows.Forms.TextBox script_to_execute_on_crash;
        private System.Windows.Forms.Label crash_script_label;
        private System.Windows.Forms.Label rabbitmq_server_address_label;
        private System.Windows.Forms.TextBox mqtt_server_address;
        private System.Windows.Forms.Label rsync_server_address_label;
        private System.Windows.Forms.TextBox rsync_server_address;
        private System.Windows.Forms.Button watch_button;
        private System.Windows.Forms.CheckBox enable_report_on_crash;
        private System.Windows.Forms.TabPage log;
        private System.Windows.Forms.RichTextBox log_box;
        private System.Windows.Forms.Button screenshot_button;
        private System.Windows.Forms.CheckBox assume_crash_if_not_responsive;
        private System.Windows.Forms.TextBox time_delay_before_launch;
        private System.Windows.Forms.TextBox maximum_retries;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer process_monitor_timer;
        private System.Windows.Forms.DataVisualization.Charting.Chart cpu_chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart memory_chart;
        private System.Windows.Forms.NotifyIcon notify_icon;
        private System.Windows.Forms.ContextMenuStrip context_menu_strip;
        private System.Windows.Forms.ToolStripMenuItem toggleUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitPhoenixToolStripMenuItem;
        private System.Windows.Forms.Label working_directory_label;
        private System.Windows.Forms.TextBox working_directory;
        private System.Windows.Forms.TextBox script_to_execute_on_start;
        private System.Windows.Forms.Label start_script_label;
        private System.Windows.Forms.TextBox rsync_server_port;
        private System.Windows.Forms.TextBox rsync_server_username;
        private System.Windows.Forms.Label rsync_server_password_label;
        private System.Windows.Forms.Label rsync_server_username_label;
        private System.Windows.Forms.RichTextBox public_key;
        private System.Windows.Forms.RichTextBox private_key;
        private System.Windows.Forms.Label public_key_label;
        private System.Windows.Forms.Label private_key_label;
        private System.Windows.Forms.Button generate_new_keys;
        private System.Windows.Forms.Button pull_update;
        private System.Windows.Forms.Label remote_directory_label;
        private System.Windows.Forms.Label local_directory_label;
        private System.Windows.Forms.TextBox remote_directory;
        private System.Windows.Forms.TextBox local_directory;
        private System.Windows.Forms.Label mqtt_connection_status_label;
        private System.Windows.Forms.Label mqtt_connection_status;
        private System.Windows.Forms.TabPage report_tab;
        private System.Windows.Forms.Label gmail_address_label;
        private System.Windows.Forms.TextBox gmail_address;
        private System.Windows.Forms.Label email_address_label;
        private System.Windows.Forms.TextBox email_address;
        private System.Windows.Forms.Label gmail_password_label;
        private System.Windows.Forms.TextBox gmail_password;
        private System.Windows.Forms.TextBox email_subject;
        private System.Windows.Forms.Label email_subject_label;
        private System.Windows.Forms.Label email_body_label;
        private System.Windows.Forms.RichTextBox email_body;
        private System.Windows.Forms.TextBox attachment;
        private System.Windows.Forms.Label attachment_label;
        private System.Windows.Forms.TabPage about_tab;
        private System.Windows.Forms.WebBrowser about_browser;
        private System.Windows.Forms.TextBox update_feed_address;
        private System.Windows.Forms.Label update_feed_address_label;
    }
}


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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.main_tab = new System.Windows.Forms.TabControl();
            this.tab_local = new System.Windows.Forms.TabPage();
            this.memory_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cpu_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.maximum_retries = new System.Windows.Forms.TextBox();
            this.time_delay_before_launch = new System.Windows.Forms.TextBox();
            this.assume_crash_if_not_responsive = new System.Windows.Forms.CheckBox();
            this.screenshot_button = new System.Windows.Forms.Button();
            this.enable_screenshot_on_crash = new System.Windows.Forms.CheckBox();
            this.watch_button = new System.Windows.Forms.Button();
            this.crash_script_button = new System.Windows.Forms.Button();
            this.app_path_button = new System.Windows.Forms.Button();
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
            this.receive_anonymous_updates = new System.Windows.Forms.CheckBox();
            this.release_button = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.password_label = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.username_label = new System.Windows.Forms.Label();
            this.update_location_button = new System.Windows.Forms.Button();
            this.update_package_location = new System.Windows.Forms.TextBox();
            this.update_location_label = new System.Windows.Forms.Label();
            this.update_hash = new System.Windows.Forms.TextBox();
            this.update_hash_label = new System.Windows.Forms.Label();
            this.update_save_button = new System.Windows.Forms.Button();
            this.update_password = new System.Windows.Forms.TextBox();
            this.update_password_label = new System.Windows.Forms.Label();
            this.update_channel = new System.Windows.Forms.TextBox();
            this.update_channel_label = new System.Windows.Forms.Label();
            this.update_server_address = new System.Windows.Forms.TextBox();
            this.update_server_label = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TabPage();
            this.log_box = new System.Windows.Forms.RichTextBox();
            this.about_tab = new System.Windows.Forms.TabPage();
            this.about_text_box = new System.Windows.Forms.RichTextBox();
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
            this.log.SuspendLayout();
            this.about_tab.SuspendLayout();
            this.context_menu_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_tab
            // 
            this.main_tab.Controls.Add(this.tab_local);
            this.main_tab.Controls.Add(this.remote_tab);
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
            this.tab_local.Controls.Add(this.memory_chart);
            this.tab_local.Controls.Add(this.cpu_chart);
            this.tab_local.Controls.Add(this.label2);
            this.tab_local.Controls.Add(this.maximum_retries);
            this.tab_local.Controls.Add(this.time_delay_before_launch);
            this.tab_local.Controls.Add(this.assume_crash_if_not_responsive);
            this.tab_local.Controls.Add(this.screenshot_button);
            this.tab_local.Controls.Add(this.enable_screenshot_on_crash);
            this.tab_local.Controls.Add(this.watch_button);
            this.tab_local.Controls.Add(this.crash_script_button);
            this.tab_local.Controls.Add(this.app_path_button);
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
            // memory_chart
            // 
            this.memory_chart.BorderlineWidth = 0;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.IsMarginVisible = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.Maximum = 1D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.BackColor = System.Drawing.Color.Snow;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "metrics_chart_area";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.memory_chart.ChartAreas.Add(chartArea1);
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.DockedToChartArea = "metrics_chart_area";
            legend1.Name = "mem_legend";
            this.memory_chart.Legends.Add(legend1);
            this.memory_chart.Location = new System.Drawing.Point(193, 157);
            this.memory_chart.Name = "memory_chart";
            this.memory_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "metrics_chart_area";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.IsXValueIndexed = true;
            series1.Legend = "mem_legend";
            series1.LegendText = "% Memory Usage";
            series1.Name = "memory_usage_series";
            this.memory_chart.Series.Add(series1);
            this.memory_chart.Size = new System.Drawing.Size(173, 77);
            this.memory_chart.TabIndex = 16;
            // 
            // cpu_chart
            // 
            this.cpu_chart.BorderlineWidth = 0;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LineWidth = 0;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisY.IsMarginVisible = false;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LineWidth = 0;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.Maximum = 1D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.Snow;
            chartArea2.BorderWidth = 0;
            chartArea2.Name = "metrics_chart_area";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 100F;
            chartArea2.Position.Width = 100F;
            this.cpu_chart.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.DockedToChartArea = "metrics_chart_area";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.IsTextAutoFit = false;
            legend2.Name = "cpu_legend";
            this.cpu_chart.Legends.Add(legend2);
            this.cpu_chart.Location = new System.Drawing.Point(8, 157);
            this.cpu_chart.Name = "cpu_chart";
            this.cpu_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series2.ChartArea = "metrics_chart_area";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.IsXValueIndexed = true;
            series2.Legend = "cpu_legend";
            series2.LegendText = "% CPU Usage";
            series2.Name = "cpu_usage_series";
            this.cpu_chart.Series.Add(series2);
            this.cpu_chart.Size = new System.Drawing.Size(173, 77);
            this.cpu_chart.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Max Retries:";
            // 
            // maximum_retries
            // 
            this.maximum_retries.Location = new System.Drawing.Point(254, 80);
            this.maximum_retries.Name = "maximum_retries";
            this.maximum_retries.Size = new System.Drawing.Size(112, 20);
            this.maximum_retries.TabIndex = 4;
            this.maximum_retries.TextChanged += new System.EventHandler(this.maximum_retries_TextChanged);
            this.maximum_retries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maximum_retries_KeyPress);
            // 
            // time_delay_before_launch
            // 
            this.time_delay_before_launch.Location = new System.Drawing.Point(131, 80);
            this.time_delay_before_launch.Name = "time_delay_before_launch";
            this.time_delay_before_launch.Size = new System.Drawing.Size(112, 20);
            this.time_delay_before_launch.TabIndex = 3;
            this.time_delay_before_launch.TextChanged += new System.EventHandler(this.time_delay_before_launch_TextChanged);
            this.time_delay_before_launch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.time_delay_before_launch_KeyPress);
            // 
            // assume_crash_if_not_responsive
            // 
            this.assume_crash_if_not_responsive.AutoSize = true;
            this.assume_crash_if_not_responsive.Location = new System.Drawing.Point(8, 258);
            this.assume_crash_if_not_responsive.Name = "assume_crash_if_not_responsive";
            this.assume_crash_if_not_responsive.Size = new System.Drawing.Size(172, 17);
            this.assume_crash_if_not_responsive.TabIndex = 8;
            this.assume_crash_if_not_responsive.Text = "Assume crash if not responsive";
            this.assume_crash_if_not_responsive.UseVisualStyleBackColor = true;
            this.assume_crash_if_not_responsive.CheckedChanged += new System.EventHandler(this.assume_crash_if_not_responsive_CheckedChanged);
            // 
            // screenshot_button
            // 
            this.screenshot_button.Location = new System.Drawing.Point(8, 280);
            this.screenshot_button.Name = "screenshot_button";
            this.screenshot_button.Size = new System.Drawing.Size(173, 24);
            this.screenshot_button.TabIndex = 11;
            this.screenshot_button.Text = "Take a screenshot";
            this.screenshot_button.UseVisualStyleBackColor = true;
            this.screenshot_button.Click += new System.EventHandler(this.screenshot_button_Click);
            // 
            // enable_screenshot_on_crash
            // 
            this.enable_screenshot_on_crash.AutoSize = true;
            this.enable_screenshot_on_crash.Location = new System.Drawing.Point(193, 258);
            this.enable_screenshot_on_crash.Name = "enable_screenshot_on_crash";
            this.enable_screenshot_on_crash.Size = new System.Drawing.Size(158, 17);
            this.enable_screenshot_on_crash.TabIndex = 10;
            this.enable_screenshot_on_crash.Text = "Enable screenshot on crash";
            this.enable_screenshot_on_crash.UseVisualStyleBackColor = true;
            this.enable_screenshot_on_crash.CheckedChanged += new System.EventHandler(this.enable_screenshot_on_crash_CheckedChanged);
            // 
            // watch_button
            // 
            this.watch_button.Location = new System.Drawing.Point(192, 280);
            this.watch_button.Name = "watch_button";
            this.watch_button.Size = new System.Drawing.Size(173, 24);
            this.watch_button.TabIndex = 12;
            this.watch_button.Text = "Start Watching";
            this.watch_button.UseVisualStyleBackColor = true;
            this.watch_button.Click += new System.EventHandler(this.watch_button_Click);
            // 
            // crash_script_button
            // 
            this.crash_script_button.Location = new System.Drawing.Point(337, 128);
            this.crash_script_button.Name = "crash_script_button";
            this.crash_script_button.Size = new System.Drawing.Size(30, 24);
            this.crash_script_button.TabIndex = 6;
            this.crash_script_button.Text = "...";
            this.crash_script_button.UseVisualStyleBackColor = true;
            this.crash_script_button.Click += new System.EventHandler(this.crash_script_button_Click);
            // 
            // app_path_button
            // 
            this.app_path_button.Location = new System.Drawing.Point(337, 28);
            this.app_path_button.Name = "app_path_button";
            this.app_path_button.Size = new System.Drawing.Size(30, 24);
            this.app_path_button.TabIndex = 1;
            this.app_path_button.Text = "...";
            this.app_path_button.UseVisualStyleBackColor = true;
            this.app_path_button.Click += new System.EventHandler(this.app_path_button_Click);
            // 
            // script_to_execute_on_crash
            // 
            this.script_to_execute_on_crash.Location = new System.Drawing.Point(8, 130);
            this.script_to_execute_on_crash.Name = "script_to_execute_on_crash";
            this.script_to_execute_on_crash.Size = new System.Drawing.Size(319, 20);
            this.script_to_execute_on_crash.TabIndex = 5;
            this.script_to_execute_on_crash.TextChanged += new System.EventHandler(this.script_to_execute_on_crash_TextChanged);
            // 
            // crash_script_label
            // 
            this.crash_script_label.AutoSize = true;
            this.crash_script_label.Location = new System.Drawing.Point(8, 110);
            this.crash_script_label.Name = "crash_script_label";
            this.crash_script_label.Size = new System.Drawing.Size(311, 13);
            this.crash_script_label.TabIndex = 10;
            this.crash_script_label.Text = "Script to execute on crash (exit time will be passed as argument):";
            // 
            // start_minimized
            // 
            this.start_minimized.AutoSize = true;
            this.start_minimized.Location = new System.Drawing.Point(193, 238);
            this.start_minimized.Name = "start_minimized";
            this.start_minimized.Size = new System.Drawing.Size(97, 17);
            this.start_minimized.TabIndex = 9;
            this.start_minimized.Text = "Start Minimized";
            this.start_minimized.UseVisualStyleBackColor = true;
            this.start_minimized.CheckedChanged += new System.EventHandler(this.start_minimized_CheckedChanged);
            // 
            // force_always_on_top
            // 
            this.force_always_on_top.AutoSize = true;
            this.force_always_on_top.Location = new System.Drawing.Point(8, 240);
            this.force_always_on_top.Name = "force_always_on_top";
            this.force_always_on_top.Size = new System.Drawing.Size(177, 17);
            this.force_always_on_top.TabIndex = 7;
            this.force_always_on_top.Text = "Keep process on top (ALT+F12)";
            this.force_always_on_top.UseVisualStyleBackColor = true;
            this.force_always_on_top.CheckedChanged += new System.EventHandler(this.force_always_on_top_CheckedChanged);
            // 
            // time_delay_label
            // 
            this.time_delay_label.AutoSize = true;
            this.time_delay_label.Location = new System.Drawing.Point(131, 60);
            this.time_delay_label.Name = "time_delay_label";
            this.time_delay_label.Size = new System.Drawing.Size(103, 13);
            this.time_delay_label.TabIndex = 4;
            this.time_delay_label.Text = "Re-launch Delay (s):";
            // 
            // command_line_arguments
            // 
            this.command_line_arguments.Location = new System.Drawing.Point(8, 80);
            this.command_line_arguments.Name = "command_line_arguments";
            this.command_line_arguments.Size = new System.Drawing.Size(112, 20);
            this.command_line_arguments.TabIndex = 2;
            this.command_line_arguments.TextChanged += new System.EventHandler(this.command_line_arguments_TextChanged);
            // 
            // cmd_line_label
            // 
            this.cmd_line_label.AutoSize = true;
            this.cmd_line_label.Location = new System.Drawing.Point(8, 60);
            this.cmd_line_label.Name = "cmd_line_label";
            this.cmd_line_label.Size = new System.Drawing.Size(80, 13);
            this.cmd_line_label.TabIndex = 2;
            this.cmd_line_label.Text = "Command Line:";
            // 
            // app_path_label
            // 
            this.app_path_label.AutoSize = true;
            this.app_path_label.Location = new System.Drawing.Point(8, 10);
            this.app_path_label.Name = "app_path_label";
            this.app_path_label.Size = new System.Drawing.Size(168, 13);
            this.app_path_label.TabIndex = 1;
            this.app_path_label.Text = "Application to watch (.bat or .exe):";
            // 
            // application_to_watch
            // 
            this.application_to_watch.Location = new System.Drawing.Point(8, 30);
            this.application_to_watch.Name = "application_to_watch";
            this.application_to_watch.Size = new System.Drawing.Size(319, 20);
            this.application_to_watch.TabIndex = 0;
            this.application_to_watch.TextChanged += new System.EventHandler(this.application_to_watch_TextChanged);
            // 
            // remote_tab
            // 
            this.remote_tab.Controls.Add(this.receive_anonymous_updates);
            this.remote_tab.Controls.Add(this.release_button);
            this.remote_tab.Controls.Add(this.password);
            this.remote_tab.Controls.Add(this.password_label);
            this.remote_tab.Controls.Add(this.username);
            this.remote_tab.Controls.Add(this.username_label);
            this.remote_tab.Controls.Add(this.update_location_button);
            this.remote_tab.Controls.Add(this.update_package_location);
            this.remote_tab.Controls.Add(this.update_location_label);
            this.remote_tab.Controls.Add(this.update_hash);
            this.remote_tab.Controls.Add(this.update_hash_label);
            this.remote_tab.Controls.Add(this.update_save_button);
            this.remote_tab.Controls.Add(this.update_password);
            this.remote_tab.Controls.Add(this.update_password_label);
            this.remote_tab.Controls.Add(this.update_channel);
            this.remote_tab.Controls.Add(this.update_channel_label);
            this.remote_tab.Controls.Add(this.update_server_address);
            this.remote_tab.Controls.Add(this.update_server_label);
            this.remote_tab.Location = new System.Drawing.Point(4, 22);
            this.remote_tab.Name = "remote_tab";
            this.remote_tab.Padding = new System.Windows.Forms.Padding(3);
            this.remote_tab.Size = new System.Drawing.Size(376, 312);
            this.remote_tab.TabIndex = 1;
            this.remote_tab.Text = "Remote";
            this.remote_tab.UseVisualStyleBackColor = true;
            // 
            // receive_anonymous_updates
            // 
            this.receive_anonymous_updates.AutoSize = true;
            this.receive_anonymous_updates.Location = new System.Drawing.Point(104, 109);
            this.receive_anonymous_updates.Name = "receive_anonymous_updates";
            this.receive_anonymous_updates.Size = new System.Drawing.Size(165, 17);
            this.receive_anonymous_updates.TabIndex = 4;
            this.receive_anonymous_updates.Text = "receive anonymous updates?";
            this.receive_anonymous_updates.UseVisualStyleBackColor = true;
            this.receive_anonymous_updates.CheckedChanged += new System.EventHandler(this.receive_anonymous_updates_CheckedChanged);
            // 
            // release_button
            // 
            this.release_button.Location = new System.Drawing.Point(255, 278);
            this.release_button.Name = "release_button";
            this.release_button.Size = new System.Drawing.Size(112, 24);
            this.release_button.TabIndex = 11;
            this.release_button.Text = "Release update";
            this.release_button.UseVisualStyleBackColor = true;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(132, 280);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(112, 20);
            this.password.TabIndex = 10;
            this.password.UseSystemPasswordChar = true;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(132, 260);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(56, 13);
            this.password_label.TabIndex = 17;
            this.password_label.Text = "Password:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(8, 280);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(112, 20);
            this.username.TabIndex = 9;
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(8, 260);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(58, 13);
            this.username_label.TabIndex = 15;
            this.username_label.Text = "Username:";
            // 
            // update_location_button
            // 
            this.update_location_button.Location = new System.Drawing.Point(337, 228);
            this.update_location_button.Name = "update_location_button";
            this.update_location_button.Size = new System.Drawing.Size(30, 24);
            this.update_location_button.TabIndex = 8;
            this.update_location_button.Text = "...";
            this.update_location_button.UseVisualStyleBackColor = true;
            // 
            // update_package_location
            // 
            this.update_package_location.Location = new System.Drawing.Point(8, 230);
            this.update_package_location.Name = "update_package_location";
            this.update_package_location.Size = new System.Drawing.Size(319, 20);
            this.update_package_location.TabIndex = 7;
            // 
            // update_location_label
            // 
            this.update_location_label.AutoSize = true;
            this.update_location_label.Location = new System.Drawing.Point(8, 210);
            this.update_location_label.Name = "update_location_label";
            this.update_location_label.Size = new System.Drawing.Size(130, 13);
            this.update_location_label.TabIndex = 11;
            this.update_location_label.Text = "Update package location:";
            // 
            // update_hash
            // 
            this.update_hash.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update_hash.Location = new System.Drawing.Point(8, 180);
            this.update_hash.Name = "update_hash";
            this.update_hash.ReadOnly = true;
            this.update_hash.Size = new System.Drawing.Size(358, 22);
            this.update_hash.TabIndex = 6;
            this.update_hash.Text = "b3d97746dbb45e92dc083db205e1fd14";
            this.update_hash.TextChanged += new System.EventHandler(this.update_hash_TextChanged);
            // 
            // update_hash_label
            // 
            this.update_hash_label.AutoSize = true;
            this.update_hash_label.Location = new System.Drawing.Point(8, 160);
            this.update_hash_label.Name = "update_hash_label";
            this.update_hash_label.Size = new System.Drawing.Size(71, 13);
            this.update_hash_label.TabIndex = 9;
            this.update_hash_label.Text = "Update hash:";
            // 
            // update_save_button
            // 
            this.update_save_button.Location = new System.Drawing.Point(314, 128);
            this.update_save_button.Name = "update_save_button";
            this.update_save_button.Size = new System.Drawing.Size(53, 24);
            this.update_save_button.TabIndex = 5;
            this.update_save_button.Text = "Save";
            this.update_save_button.UseVisualStyleBackColor = true;
            // 
            // update_password
            // 
            this.update_password.Location = new System.Drawing.Point(8, 130);
            this.update_password.Name = "update_password";
            this.update_password.Size = new System.Drawing.Size(296, 20);
            this.update_password.TabIndex = 3;
            this.update_password.UseSystemPasswordChar = true;
            // 
            // update_password_label
            // 
            this.update_password_label.AutoSize = true;
            this.update_password_label.Location = new System.Drawing.Point(8, 110);
            this.update_password_label.Name = "update_password_label";
            this.update_password_label.Size = new System.Drawing.Size(267, 13);
            this.update_password_label.TabIndex = 6;
            this.update_password_label.Text = "Update password (                                                        )";
            // 
            // update_channel
            // 
            this.update_channel.Location = new System.Drawing.Point(8, 80);
            this.update_channel.Name = "update_channel";
            this.update_channel.Size = new System.Drawing.Size(358, 20);
            this.update_channel.TabIndex = 2;
            this.update_channel.TextChanged += new System.EventHandler(this.update_channel_TextChanged);
            // 
            // update_channel_label
            // 
            this.update_channel_label.AutoSize = true;
            this.update_channel_label.Location = new System.Drawing.Point(8, 60);
            this.update_channel_label.Name = "update_channel_label";
            this.update_channel_label.Size = new System.Drawing.Size(86, 13);
            this.update_channel_label.TabIndex = 4;
            this.update_channel_label.Text = "Update channel:";
            // 
            // update_server_address
            // 
            this.update_server_address.Location = new System.Drawing.Point(8, 30);
            this.update_server_address.Name = "update_server_address";
            this.update_server_address.Size = new System.Drawing.Size(358, 20);
            this.update_server_address.TabIndex = 1;
            this.update_server_address.TextChanged += new System.EventHandler(this.update_server_address_TextChanged);
            // 
            // update_server_label
            // 
            this.update_server_label.AutoSize = true;
            this.update_server_label.Location = new System.Drawing.Point(8, 10);
            this.update_server_label.Name = "update_server_label";
            this.update_server_label.Size = new System.Drawing.Size(117, 13);
            this.update_server_label.TabIndex = 2;
            this.update_server_label.Text = "Update server address:";
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
            this.log_box.Size = new System.Drawing.Size(370, 306);
            this.log_box.TabIndex = 0;
            this.log_box.Text = "";
            // 
            // about_tab
            // 
            this.about_tab.Controls.Add(this.about_text_box);
            this.about_tab.Location = new System.Drawing.Point(4, 22);
            this.about_tab.Name = "about_tab";
            this.about_tab.Padding = new System.Windows.Forms.Padding(3);
            this.about_tab.Size = new System.Drawing.Size(376, 312);
            this.about_tab.TabIndex = 3;
            this.about_tab.Text = "About";
            this.about_tab.UseVisualStyleBackColor = true;
            // 
            // about_text_box
            // 
            this.about_text_box.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.about_text_box.Location = new System.Drawing.Point(20, 20);
            this.about_text_box.Margin = new System.Windows.Forms.Padding(0);
            this.about_text_box.Name = "about_text_box";
            this.about_text_box.Size = new System.Drawing.Size(335, 270);
            this.about_text_box.TabIndex = 1;
            this.about_text_box.Text = "Helios Phoenix Control Panel\nversion 0.1.0\n© 2015 Helios Interactive\nAll rights r" +
    "eserved.";
            // 
            // process_monitor_timer
            // 
            this.process_monitor_timer.Interval = 60;
            this.process_monitor_timer.Tick += new System.EventHandler(this.process_monitor_timer_Tick);
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
            this.toggleUIToolStripMenuItem.Click += new System.EventHandler(this.toggleUIToolStripMenuItem_Click);
            // 
            // exitPhoenixToolStripMenuItem
            // 
            this.exitPhoenixToolStripMenuItem.Name = "exitPhoenixToolStripMenuItem";
            this.exitPhoenixToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exitPhoenixToolStripMenuItem.Text = "Exit Phoenix";
            this.exitPhoenixToolStripMenuItem.Click += new System.EventHandler(this.exitPhoenixToolStripMenuItem_Click);
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
            this.Activated += new System.EventHandler(this.MainDialog_Activated);
            this.Deactivate += new System.EventHandler(this.MainDialog_Deactivate);
            this.main_tab.ResumeLayout(false);
            this.tab_local.ResumeLayout(false);
            this.tab_local.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memory_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpu_chart)).EndInit();
            this.remote_tab.ResumeLayout(false);
            this.remote_tab.PerformLayout();
            this.log.ResumeLayout(false);
            this.about_tab.ResumeLayout(false);
            this.context_menu_strip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl main_tab;
        private System.Windows.Forms.TabPage tab_local;
        private System.Windows.Forms.TabPage remote_tab;
        private System.Windows.Forms.TabPage about_tab;
        private System.Windows.Forms.TextBox application_to_watch;
        private System.Windows.Forms.Label app_path_label;
        private System.Windows.Forms.Label cmd_line_label;
        private System.Windows.Forms.TextBox command_line_arguments;
        private System.Windows.Forms.Label time_delay_label;
        private System.Windows.Forms.CheckBox force_always_on_top;
        private System.Windows.Forms.CheckBox start_minimized;
        private System.Windows.Forms.TextBox script_to_execute_on_crash;
        private System.Windows.Forms.Label crash_script_label;
        private System.Windows.Forms.Button app_path_button;
        private System.Windows.Forms.Button crash_script_button;
        private System.Windows.Forms.Label update_server_label;
        private System.Windows.Forms.TextBox update_server_address;
        private System.Windows.Forms.Label update_channel_label;
        private System.Windows.Forms.TextBox update_channel;
        private System.Windows.Forms.Label update_password_label;
        private System.Windows.Forms.TextBox update_password;
        private System.Windows.Forms.Button update_save_button;
        private System.Windows.Forms.Label update_hash_label;
        private System.Windows.Forms.TextBox update_hash;
        private System.Windows.Forms.Label update_location_label;
        private System.Windows.Forms.Button update_location_button;
        private System.Windows.Forms.TextBox update_package_location;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Button release_button;
        private System.Windows.Forms.Button watch_button;
        private System.Windows.Forms.CheckBox receive_anonymous_updates;
        private System.Windows.Forms.RichTextBox about_text_box;
        private System.Windows.Forms.CheckBox enable_screenshot_on_crash;
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
    }
}


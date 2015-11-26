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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDialog));
            this.main_tab = new System.Windows.Forms.TabControl();
            this.tab_local = new System.Windows.Forms.TabPage();
            this.time_delay_before_launch = new System.Windows.Forms.TextBox();
            this.assume_crash_if_not_responsive = new System.Windows.Forms.CheckBox();
            this.screenshot_button = new System.Windows.Forms.Button();
            this.enable_screenshot_on_crash = new System.Windows.Forms.CheckBox();
            this.start_with_windows = new System.Windows.Forms.CheckBox();
            this.watch_button = new System.Windows.Forms.Button();
            this.crash_script_button = new System.Windows.Forms.Button();
            this.app_path_button = new System.Windows.Forms.Button();
            this.script_to_execute_on_crash = new System.Windows.Forms.TextBox();
            this.crash_script_label = new System.Windows.Forms.Label();
            this.enable_metrics = new System.Windows.Forms.CheckBox();
            this.force_maximize = new System.Windows.Forms.CheckBox();
            this.force_always_on_top = new System.Windows.Forms.CheckBox();
            this.time_delay_label = new System.Windows.Forms.Label();
            this.command_line_arguments = new System.Windows.Forms.TextBox();
            this.cmd_line_label = new System.Windows.Forms.Label();
            this.app_path_label = new System.Windows.Forms.Label();
            this.application_to_watch = new System.Windows.Forms.TextBox();
            this.remote_tab = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
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
            this.status_strip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.maximum_retries = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.main_tab.SuspendLayout();
            this.tab_local.SuspendLayout();
            this.remote_tab.SuspendLayout();
            this.log.SuspendLayout();
            this.about_tab.SuspendLayout();
            this.status_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_tab
            // 
            this.main_tab.Controls.Add(this.tab_local);
            this.main_tab.Controls.Add(this.remote_tab);
            this.main_tab.Controls.Add(this.log);
            this.main_tab.Controls.Add(this.about_tab);
            this.main_tab.Dock = System.Windows.Forms.DockStyle.Top;
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
            this.tab_local.Controls.Add(this.label2);
            this.tab_local.Controls.Add(this.maximum_retries);
            this.tab_local.Controls.Add(this.time_delay_before_launch);
            this.tab_local.Controls.Add(this.assume_crash_if_not_responsive);
            this.tab_local.Controls.Add(this.screenshot_button);
            this.tab_local.Controls.Add(this.enable_screenshot_on_crash);
            this.tab_local.Controls.Add(this.start_with_windows);
            this.tab_local.Controls.Add(this.watch_button);
            this.tab_local.Controls.Add(this.crash_script_button);
            this.tab_local.Controls.Add(this.app_path_button);
            this.tab_local.Controls.Add(this.script_to_execute_on_crash);
            this.tab_local.Controls.Add(this.crash_script_label);
            this.tab_local.Controls.Add(this.enable_metrics);
            this.tab_local.Controls.Add(this.force_maximize);
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
            // time_delay_before_launch
            // 
            this.time_delay_before_launch.Location = new System.Drawing.Point(8, 130);
            this.time_delay_before_launch.Name = "time_delay_before_launch";
            this.time_delay_before_launch.Size = new System.Drawing.Size(173, 20);
            this.time_delay_before_launch.TabIndex = 3;
            this.time_delay_before_launch.TextChanged += new System.EventHandler(this.time_delay_before_launch_TextChanged);
            this.time_delay_before_launch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.time_delay_before_launch_KeyPress);
            // 
            // assume_crash_if_not_responsive
            // 
            this.assume_crash_if_not_responsive.AutoSize = true;
            this.assume_crash_if_not_responsive.Location = new System.Drawing.Point(8, 220);
            this.assume_crash_if_not_responsive.Name = "assume_crash_if_not_responsive";
            this.assume_crash_if_not_responsive.Size = new System.Drawing.Size(172, 17);
            this.assume_crash_if_not_responsive.TabIndex = 8;
            this.assume_crash_if_not_responsive.Text = "Assume crash if not responsive";
            this.assume_crash_if_not_responsive.UseVisualStyleBackColor = true;
            this.assume_crash_if_not_responsive.CheckedChanged += new System.EventHandler(this.assume_crash_if_not_responsive_CheckedChanged);
            // 
            // screenshot_button
            // 
            this.screenshot_button.Location = new System.Drawing.Point(192, 233);
            this.screenshot_button.Name = "screenshot_button";
            this.screenshot_button.Size = new System.Drawing.Size(173, 24);
            this.screenshot_button.TabIndex = 14;
            this.screenshot_button.Text = "Take a screenshot";
            this.screenshot_button.UseVisualStyleBackColor = true;
            // 
            // enable_screenshot_on_crash
            // 
            this.enable_screenshot_on_crash.AutoSize = true;
            this.enable_screenshot_on_crash.Location = new System.Drawing.Point(193, 178);
            this.enable_screenshot_on_crash.Name = "enable_screenshot_on_crash";
            this.enable_screenshot_on_crash.Size = new System.Drawing.Size(158, 17);
            this.enable_screenshot_on_crash.TabIndex = 10;
            this.enable_screenshot_on_crash.Text = "Enable screenshot on crash";
            this.enable_screenshot_on_crash.UseVisualStyleBackColor = true;
            this.enable_screenshot_on_crash.CheckedChanged += new System.EventHandler(this.enable_screenshot_on_crash_CheckedChanged);
            // 
            // start_with_windows
            // 
            this.start_with_windows.AutoSize = true;
            this.start_with_windows.Location = new System.Drawing.Point(193, 158);
            this.start_with_windows.Name = "start_with_windows";
            this.start_with_windows.Size = new System.Drawing.Size(117, 17);
            this.start_with_windows.TabIndex = 9;
            this.start_with_windows.Text = "Start with Windows";
            this.start_with_windows.UseVisualStyleBackColor = true;
            this.start_with_windows.CheckedChanged += new System.EventHandler(this.start_with_windows_CheckedChanged);
            // 
            // watch_button
            // 
            this.watch_button.Location = new System.Drawing.Point(192, 200);
            this.watch_button.Name = "watch_button";
            this.watch_button.Size = new System.Drawing.Size(173, 24);
            this.watch_button.TabIndex = 13;
            this.watch_button.Text = "Start Watching";
            this.watch_button.UseVisualStyleBackColor = true;
            this.watch_button.Click += new System.EventHandler(this.watch_button_Click);
            // 
            // crash_script_button
            // 
            this.crash_script_button.Location = new System.Drawing.Point(337, 269);
            this.crash_script_button.Name = "crash_script_button";
            this.crash_script_button.Size = new System.Drawing.Size(30, 24);
            this.crash_script_button.TabIndex = 12;
            this.crash_script_button.Text = "...";
            this.crash_script_button.UseVisualStyleBackColor = true;
            // 
            // app_path_button
            // 
            this.app_path_button.Location = new System.Drawing.Point(337, 28);
            this.app_path_button.Name = "app_path_button";
            this.app_path_button.Size = new System.Drawing.Size(30, 24);
            this.app_path_button.TabIndex = 1;
            this.app_path_button.Text = "...";
            this.app_path_button.UseVisualStyleBackColor = true;
            // 
            // script_to_execute_on_crash
            // 
            this.script_to_execute_on_crash.Location = new System.Drawing.Point(8, 271);
            this.script_to_execute_on_crash.Name = "script_to_execute_on_crash";
            this.script_to_execute_on_crash.Size = new System.Drawing.Size(319, 20);
            this.script_to_execute_on_crash.TabIndex = 11;
            this.script_to_execute_on_crash.TextChanged += new System.EventHandler(this.script_to_execute_on_crash_TextChanged);
            // 
            // crash_script_label
            // 
            this.crash_script_label.AutoSize = true;
            this.crash_script_label.Location = new System.Drawing.Point(8, 251);
            this.crash_script_label.Name = "crash_script_label";
            this.crash_script_label.Size = new System.Drawing.Size(134, 13);
            this.crash_script_label.TabIndex = 10;
            this.crash_script_label.Text = "Script to execute on crash:";
            // 
            // enable_metrics
            // 
            this.enable_metrics.AutoSize = true;
            this.enable_metrics.Location = new System.Drawing.Point(8, 200);
            this.enable_metrics.Name = "enable_metrics";
            this.enable_metrics.Size = new System.Drawing.Size(96, 17);
            this.enable_metrics.TabIndex = 7;
            this.enable_metrics.Text = "Enable Metrics";
            this.enable_metrics.UseVisualStyleBackColor = true;
            this.enable_metrics.CheckedChanged += new System.EventHandler(this.enable_metrics_CheckedChanged);
            // 
            // force_maximize
            // 
            this.force_maximize.AutoSize = true;
            this.force_maximize.Location = new System.Drawing.Point(8, 180);
            this.force_maximize.Name = "force_maximize";
            this.force_maximize.Size = new System.Drawing.Size(99, 17);
            this.force_maximize.TabIndex = 6;
            this.force_maximize.Text = "Force Maximize";
            this.force_maximize.UseVisualStyleBackColor = true;
            this.force_maximize.CheckedChanged += new System.EventHandler(this.force_maximize_CheckedChanged);
            // 
            // force_always_on_top
            // 
            this.force_always_on_top.AutoSize = true;
            this.force_always_on_top.Location = new System.Drawing.Point(8, 160);
            this.force_always_on_top.Name = "force_always_on_top";
            this.force_always_on_top.Size = new System.Drawing.Size(126, 17);
            this.force_always_on_top.TabIndex = 5;
            this.force_always_on_top.Text = "Force Always on Top";
            this.force_always_on_top.UseVisualStyleBackColor = true;
            this.force_always_on_top.CheckedChanged += new System.EventHandler(this.force_always_on_top_CheckedChanged);
            // 
            // time_delay_label
            // 
            this.time_delay_label.AutoSize = true;
            this.time_delay_label.Location = new System.Drawing.Point(8, 110);
            this.time_delay_label.Name = "time_delay_label";
            this.time_delay_label.Size = new System.Drawing.Size(166, 13);
            this.time_delay_label.TabIndex = 4;
            this.time_delay_label.Text = "Delay before re-launch (seconds):";
            // 
            // command_line_arguments
            // 
            this.command_line_arguments.Location = new System.Drawing.Point(8, 80);
            this.command_line_arguments.Name = "command_line_arguments";
            this.command_line_arguments.Size = new System.Drawing.Size(358, 20);
            this.command_line_arguments.TabIndex = 2;
            this.command_line_arguments.TextChanged += new System.EventHandler(this.command_line_arguments_TextChanged);
            // 
            // cmd_line_label
            // 
            this.cmd_line_label.AutoSize = true;
            this.cmd_line_label.Location = new System.Drawing.Point(8, 60);
            this.cmd_line_label.Name = "cmd_line_label";
            this.cmd_line_label.Size = new System.Drawing.Size(174, 13);
            this.cmd_line_label.TabIndex = 2;
            this.cmd_line_label.Text = "Command line arguments (optional):";
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
            this.remote_tab.Controls.Add(this.label1);
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
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 2);
            this.label1.TabIndex = 21;
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
            this.release_button.Location = new System.Drawing.Point(255, 268);
            this.release_button.Name = "release_button";
            this.release_button.Size = new System.Drawing.Size(112, 24);
            this.release_button.TabIndex = 11;
            this.release_button.Text = "Release update";
            this.release_button.UseVisualStyleBackColor = true;
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(132, 270);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(112, 20);
            this.password.TabIndex = 10;
            this.password.UseSystemPasswordChar = true;
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(132, 250);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(56, 13);
            this.password_label.TabIndex = 17;
            this.password_label.Text = "Password:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(8, 270);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(112, 20);
            this.username.TabIndex = 9;
            this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(8, 250);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(58, 13);
            this.username_label.TabIndex = 15;
            this.username_label.Text = "Username:";
            // 
            // update_location_button
            // 
            this.update_location_button.Location = new System.Drawing.Point(337, 218);
            this.update_location_button.Name = "update_location_button";
            this.update_location_button.Size = new System.Drawing.Size(30, 24);
            this.update_location_button.TabIndex = 8;
            this.update_location_button.Text = "...";
            this.update_location_button.UseVisualStyleBackColor = true;
            // 
            // update_package_location
            // 
            this.update_package_location.Location = new System.Drawing.Point(8, 220);
            this.update_package_location.Name = "update_package_location";
            this.update_package_location.Size = new System.Drawing.Size(319, 20);
            this.update_package_location.TabIndex = 7;
            // 
            // update_location_label
            // 
            this.update_location_label.AutoSize = true;
            this.update_location_label.Location = new System.Drawing.Point(8, 200);
            this.update_location_label.Name = "update_location_label";
            this.update_location_label.Size = new System.Drawing.Size(130, 13);
            this.update_location_label.TabIndex = 11;
            this.update_location_label.Text = "Update package location:";
            // 
            // update_hash
            // 
            this.update_hash.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update_hash.Location = new System.Drawing.Point(85, 160);
            this.update_hash.Name = "update_hash";
            this.update_hash.ReadOnly = true;
            this.update_hash.Size = new System.Drawing.Size(281, 22);
            this.update_hash.TabIndex = 6;
            this.update_hash.Text = "b3d97746dbb45e92dc083db205e1fd14";
            this.update_hash.TextChanged += new System.EventHandler(this.update_hash_TextChanged);
            // 
            // update_hash_label
            // 
            this.update_hash_label.AutoSize = true;
            this.update_hash_label.Location = new System.Drawing.Point(8, 163);
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
            this.log.Size = new System.Drawing.Size(376, 310);
            this.log.TabIndex = 4;
            this.log.Text = "Log";
            this.log.UseVisualStyleBackColor = true;
            // 
            // log_box
            // 
            this.log_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.log_box.Location = new System.Drawing.Point(3, 3);
            this.log_box.Name = "log_box";
            this.log_box.Size = new System.Drawing.Size(370, 304);
            this.log_box.TabIndex = 0;
            this.log_box.Text = "";
            // 
            // about_tab
            // 
            this.about_tab.Controls.Add(this.about_text_box);
            this.about_tab.Location = new System.Drawing.Point(4, 22);
            this.about_tab.Name = "about_tab";
            this.about_tab.Padding = new System.Windows.Forms.Padding(3);
            this.about_tab.Size = new System.Drawing.Size(376, 310);
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
            // status_strip
            // 
            this.status_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.status_strip.Location = new System.Drawing.Point(0, 339);
            this.status_strip.Name = "status_strip";
            this.status_strip.Size = new System.Drawing.Size(384, 22);
            this.status_strip.SizingGrip = false;
            this.status_strip.TabIndex = 1;
            this.status_strip.Text = "Ready";
            // 
            // status
            // 
            this.status.Enabled = false;
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(39, 17);
            this.status.Text = "Ready";
            // 
            // maximum_retries
            // 
            this.maximum_retries.Location = new System.Drawing.Point(193, 130);
            this.maximum_retries.Name = "maximum_retries";
            this.maximum_retries.Size = new System.Drawing.Size(173, 20);
            this.maximum_retries.TabIndex = 4;
            this.maximum_retries.TextChanged += new System.EventHandler(this.maximum_retries_TextChanged);
            this.maximum_retries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.maximum_retries_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Maximum retries (0 for infinity):";
            // 
            // MainDialog
            // 
            this.AccessibleDescription = "Monitors and restarts crashed applications.";
            this.AccessibleName = "Phoenix";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.status_strip);
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
            this.remote_tab.ResumeLayout(false);
            this.remote_tab.PerformLayout();
            this.log.ResumeLayout(false);
            this.about_tab.ResumeLayout(false);
            this.status_strip.ResumeLayout(false);
            this.status_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl main_tab;
        private System.Windows.Forms.TabPage tab_local;
        private System.Windows.Forms.TabPage remote_tab;
        private System.Windows.Forms.TabPage about_tab;
        private System.Windows.Forms.StatusStrip status_strip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.TextBox application_to_watch;
        private System.Windows.Forms.Label app_path_label;
        private System.Windows.Forms.Label cmd_line_label;
        private System.Windows.Forms.TextBox command_line_arguments;
        private System.Windows.Forms.Label time_delay_label;
        private System.Windows.Forms.CheckBox force_always_on_top;
        private System.Windows.Forms.CheckBox force_maximize;
        private System.Windows.Forms.CheckBox enable_metrics;
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
        private System.Windows.Forms.CheckBox start_with_windows;
        private System.Windows.Forms.CheckBox enable_screenshot_on_crash;
        private System.Windows.Forms.TabPage log;
        private System.Windows.Forms.RichTextBox log_box;
        private System.Windows.Forms.Button screenshot_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox assume_crash_if_not_responsive;
        private System.Windows.Forms.TextBox time_delay_before_launch;
        private System.Windows.Forms.TextBox maximum_retries;
        private System.Windows.Forms.Label label2;
    }
}


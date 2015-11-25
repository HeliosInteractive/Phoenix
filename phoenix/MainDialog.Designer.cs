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
            this.crashed_if_unresponsive = new System.Windows.Forms.CheckBox();
            this.screenshot_button = new System.Windows.Forms.Button();
            this.screenshot_on_crash = new System.Windows.Forms.CheckBox();
            this.verbose_logging = new System.Windows.Forms.CheckBox();
            this.start_with_windows = new System.Windows.Forms.CheckBox();
            this.watch_button = new System.Windows.Forms.Button();
            this.crash_script_button = new System.Windows.Forms.Button();
            this.app_path_button = new System.Windows.Forms.Button();
            this.crash_script_path = new System.Windows.Forms.TextBox();
            this.crash_script_label = new System.Windows.Forms.Label();
            this.metrics = new System.Windows.Forms.CheckBox();
            this.maximize = new System.Windows.Forms.CheckBox();
            this.always_on_top = new System.Windows.Forms.CheckBox();
            this.time_delay = new System.Windows.Forms.TextBox();
            this.time_delay_label = new System.Windows.Forms.Label();
            this.cmd_line = new System.Windows.Forms.TextBox();
            this.cmd_line_label = new System.Windows.Forms.Label();
            this.app_path_label = new System.Windows.Forms.Label();
            this.app_path = new System.Windows.Forms.TextBox();
            this.remote = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.about = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.status_strip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_tab.SuspendLayout();
            this.tab_local.SuspendLayout();
            this.remote.SuspendLayout();
            this.log.SuspendLayout();
            this.about.SuspendLayout();
            this.status_strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // main_tab
            // 
            this.main_tab.Controls.Add(this.tab_local);
            this.main_tab.Controls.Add(this.remote);
            this.main_tab.Controls.Add(this.log);
            this.main_tab.Controls.Add(this.about);
            this.main_tab.Dock = System.Windows.Forms.DockStyle.Top;
            this.main_tab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.main_tab.Location = new System.Drawing.Point(0, 0);
            this.main_tab.Multiline = true;
            this.main_tab.Name = "main_tab";
            this.main_tab.SelectedIndex = 0;
            this.main_tab.Size = new System.Drawing.Size(384, 336);
            this.main_tab.TabIndex = 0;
            // 
            // tab_local
            // 
            this.tab_local.Controls.Add(this.crashed_if_unresponsive);
            this.tab_local.Controls.Add(this.screenshot_button);
            this.tab_local.Controls.Add(this.screenshot_on_crash);
            this.tab_local.Controls.Add(this.verbose_logging);
            this.tab_local.Controls.Add(this.start_with_windows);
            this.tab_local.Controls.Add(this.watch_button);
            this.tab_local.Controls.Add(this.crash_script_button);
            this.tab_local.Controls.Add(this.app_path_button);
            this.tab_local.Controls.Add(this.crash_script_path);
            this.tab_local.Controls.Add(this.crash_script_label);
            this.tab_local.Controls.Add(this.metrics);
            this.tab_local.Controls.Add(this.maximize);
            this.tab_local.Controls.Add(this.always_on_top);
            this.tab_local.Controls.Add(this.time_delay);
            this.tab_local.Controls.Add(this.time_delay_label);
            this.tab_local.Controls.Add(this.cmd_line);
            this.tab_local.Controls.Add(this.cmd_line_label);
            this.tab_local.Controls.Add(this.app_path_label);
            this.tab_local.Controls.Add(this.app_path);
            this.tab_local.Location = new System.Drawing.Point(4, 22);
            this.tab_local.Name = "tab_local";
            this.tab_local.Padding = new System.Windows.Forms.Padding(3);
            this.tab_local.Size = new System.Drawing.Size(376, 310);
            this.tab_local.TabIndex = 0;
            this.tab_local.Text = "Local";
            this.tab_local.UseVisualStyleBackColor = true;
            // 
            // crashed_if_unresponsive
            // 
            this.crashed_if_unresponsive.AutoSize = true;
            this.crashed_if_unresponsive.Location = new System.Drawing.Point(8, 220);
            this.crashed_if_unresponsive.Name = "crashed_if_unresponsive";
            this.crashed_if_unresponsive.Size = new System.Drawing.Size(172, 17);
            this.crashed_if_unresponsive.TabIndex = 7;
            this.crashed_if_unresponsive.Text = "Assume crash if not responsive";
            this.crashed_if_unresponsive.UseVisualStyleBackColor = true;
            // 
            // screenshot_button
            // 
            this.screenshot_button.Location = new System.Drawing.Point(196, 223);
            this.screenshot_button.Name = "screenshot_button";
            this.screenshot_button.Size = new System.Drawing.Size(173, 24);
            this.screenshot_button.TabIndex = 14;
            this.screenshot_button.Text = "Take a screenshot";
            this.screenshot_button.UseVisualStyleBackColor = true;
            // 
            // screenshot_on_crash
            // 
            this.screenshot_on_crash.AutoSize = true;
            this.screenshot_on_crash.Location = new System.Drawing.Point(197, 200);
            this.screenshot_on_crash.Name = "screenshot_on_crash";
            this.screenshot_on_crash.Size = new System.Drawing.Size(158, 17);
            this.screenshot_on_crash.TabIndex = 10;
            this.screenshot_on_crash.Text = "Enable screenshot on crash";
            this.screenshot_on_crash.UseVisualStyleBackColor = true;
            // 
            // verbose_logging
            // 
            this.verbose_logging.AutoSize = true;
            this.verbose_logging.Location = new System.Drawing.Point(197, 180);
            this.verbose_logging.Name = "verbose_logging";
            this.verbose_logging.Size = new System.Drawing.Size(137, 17);
            this.verbose_logging.TabIndex = 9;
            this.verbose_logging.Text = "Enable verbose logging";
            this.verbose_logging.UseVisualStyleBackColor = true;
            // 
            // start_with_windows
            // 
            this.start_with_windows.AutoSize = true;
            this.start_with_windows.Location = new System.Drawing.Point(197, 160);
            this.start_with_windows.Name = "start_with_windows";
            this.start_with_windows.Size = new System.Drawing.Size(117, 17);
            this.start_with_windows.TabIndex = 8;
            this.start_with_windows.Text = "Start with Windows";
            this.start_with_windows.UseVisualStyleBackColor = true;
            // 
            // watch_button
            // 
            this.watch_button.Location = new System.Drawing.Point(196, 128);
            this.watch_button.Name = "watch_button";
            this.watch_button.Size = new System.Drawing.Size(173, 24);
            this.watch_button.TabIndex = 13;
            this.watch_button.Text = "Start Watching";
            this.watch_button.UseVisualStyleBackColor = true;
            // 
            // crash_script_button
            // 
            this.crash_script_button.Location = new System.Drawing.Point(339, 268);
            this.crash_script_button.Name = "crash_script_button";
            this.crash_script_button.Size = new System.Drawing.Size(30, 24);
            this.crash_script_button.TabIndex = 12;
            this.crash_script_button.Text = "...";
            this.crash_script_button.UseVisualStyleBackColor = true;
            // 
            // app_path_button
            // 
            this.app_path_button.Location = new System.Drawing.Point(339, 28);
            this.app_path_button.Name = "app_path_button";
            this.app_path_button.Size = new System.Drawing.Size(30, 24);
            this.app_path_button.TabIndex = 1;
            this.app_path_button.Text = "...";
            this.app_path_button.UseVisualStyleBackColor = true;
            // 
            // crash_script_path
            // 
            this.crash_script_path.Location = new System.Drawing.Point(8, 270);
            this.crash_script_path.Name = "crash_script_path";
            this.crash_script_path.Size = new System.Drawing.Size(324, 20);
            this.crash_script_path.TabIndex = 11;
            // 
            // crash_script_label
            // 
            this.crash_script_label.AutoSize = true;
            this.crash_script_label.Location = new System.Drawing.Point(8, 250);
            this.crash_script_label.Name = "crash_script_label";
            this.crash_script_label.Size = new System.Drawing.Size(134, 13);
            this.crash_script_label.TabIndex = 10;
            this.crash_script_label.Text = "Script to execute on crash:";
            // 
            // metrics
            // 
            this.metrics.AutoSize = true;
            this.metrics.Location = new System.Drawing.Point(8, 200);
            this.metrics.Name = "metrics";
            this.metrics.Size = new System.Drawing.Size(96, 17);
            this.metrics.TabIndex = 6;
            this.metrics.Text = "Enable Metrics";
            this.metrics.UseVisualStyleBackColor = true;
            // 
            // maximize
            // 
            this.maximize.AutoSize = true;
            this.maximize.Location = new System.Drawing.Point(8, 180);
            this.maximize.Name = "maximize";
            this.maximize.Size = new System.Drawing.Size(99, 17);
            this.maximize.TabIndex = 5;
            this.maximize.Text = "Force Maximize";
            this.maximize.UseVisualStyleBackColor = true;
            // 
            // always_on_top
            // 
            this.always_on_top.AutoSize = true;
            this.always_on_top.Location = new System.Drawing.Point(8, 160);
            this.always_on_top.Name = "always_on_top";
            this.always_on_top.Size = new System.Drawing.Size(126, 17);
            this.always_on_top.TabIndex = 4;
            this.always_on_top.Text = "Force Always on Top";
            this.always_on_top.UseVisualStyleBackColor = true;
            // 
            // time_delay
            // 
            this.time_delay.Location = new System.Drawing.Point(8, 130);
            this.time_delay.Name = "time_delay";
            this.time_delay.Size = new System.Drawing.Size(180, 20);
            this.time_delay.TabIndex = 3;
            // 
            // time_delay_label
            // 
            this.time_delay_label.AutoSize = true;
            this.time_delay_label.Location = new System.Drawing.Point(8, 110);
            this.time_delay_label.Name = "time_delay_label";
            this.time_delay_label.Size = new System.Drawing.Size(178, 13);
            this.time_delay_label.TabIndex = 4;
            this.time_delay_label.Text = "Time delay before launch (seconds):";
            // 
            // cmd_line
            // 
            this.cmd_line.Location = new System.Drawing.Point(8, 80);
            this.cmd_line.Name = "cmd_line";
            this.cmd_line.Size = new System.Drawing.Size(360, 20);
            this.cmd_line.TabIndex = 2;
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
            // app_path
            // 
            this.app_path.Location = new System.Drawing.Point(8, 30);
            this.app_path.Name = "app_path";
            this.app_path.Size = new System.Drawing.Size(324, 20);
            this.app_path.TabIndex = 0;
            // 
            // remote
            // 
            this.remote.Controls.Add(this.label1);
            this.remote.Controls.Add(this.checkBox5);
            this.remote.Controls.Add(this.button5);
            this.remote.Controls.Add(this.textBox11);
            this.remote.Controls.Add(this.label12);
            this.remote.Controls.Add(this.textBox10);
            this.remote.Controls.Add(this.label11);
            this.remote.Controls.Add(this.button4);
            this.remote.Controls.Add(this.textBox9);
            this.remote.Controls.Add(this.label10);
            this.remote.Controls.Add(this.textBox8);
            this.remote.Controls.Add(this.label9);
            this.remote.Controls.Add(this.button3);
            this.remote.Controls.Add(this.textBox7);
            this.remote.Controls.Add(this.label8);
            this.remote.Controls.Add(this.textBox6);
            this.remote.Controls.Add(this.label7);
            this.remote.Controls.Add(this.textBox5);
            this.remote.Controls.Add(this.label6);
            this.remote.Location = new System.Drawing.Point(4, 22);
            this.remote.Name = "remote";
            this.remote.Padding = new System.Windows.Forms.Padding(3);
            this.remote.Size = new System.Drawing.Size(376, 310);
            this.remote.TabIndex = 1;
            this.remote.Text = "Remote";
            this.remote.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 2);
            this.label1.TabIndex = 21;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(104, 109);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(165, 17);
            this.checkBox5.TabIndex = 20;
            this.checkBox5.Text = "receive anonymous updates?";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(261, 268);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(108, 24);
            this.button5.TabIndex = 19;
            this.button5.Text = "Release update";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(134, 270);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(120, 20);
            this.textBox11.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(134, 250);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "Password:";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(8, 270);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(120, 20);
            this.textBox10.TabIndex = 16;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 250);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Username:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(339, 218);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 24);
            this.button4.TabIndex = 14;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(8, 220);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(324, 20);
            this.textBox9.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 200);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Update package location:";
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(87, 160);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(281, 22);
            this.textBox8.TabIndex = 10;
            this.textBox8.Text = "b3d97746dbb45e92dc083db205e1fd14";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Update hash:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(316, 128);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(53, 24);
            this.button3.TabIndex = 8;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(8, 130);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(300, 20);
            this.textBox7.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(267, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Update password (                                                        )";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(8, 80);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(360, 20);
            this.textBox6.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Update channel:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(8, 30);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(360, 20);
            this.textBox5.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Update server address:";
            // 
            // log
            // 
            this.log.Controls.Add(this.richTextBox2);
            this.log.Location = new System.Drawing.Point(4, 22);
            this.log.Name = "log";
            this.log.Padding = new System.Windows.Forms.Padding(3);
            this.log.Size = new System.Drawing.Size(376, 310);
            this.log.TabIndex = 4;
            this.log.Text = "Log";
            this.log.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(3, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(370, 304);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // about
            // 
            this.about.Controls.Add(this.richTextBox1);
            this.about.Location = new System.Drawing.Point(4, 22);
            this.about.Name = "about";
            this.about.Padding = new System.Windows.Forms.Padding(3);
            this.about.Size = new System.Drawing.Size(376, 310);
            this.about.TabIndex = 3;
            this.about.Text = "About";
            this.about.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(20, 20);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(335, 270);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Helios Phoenix Control Panel\nversion 0.1.0\n© 2015 Helios Interactive\nAll rights r" +
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
            this.remote.ResumeLayout(false);
            this.remote.PerformLayout();
            this.log.ResumeLayout(false);
            this.about.ResumeLayout(false);
            this.status_strip.ResumeLayout(false);
            this.status_strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl main_tab;
        private System.Windows.Forms.TabPage tab_local;
        private System.Windows.Forms.TabPage remote;
        private System.Windows.Forms.TabPage about;
        private System.Windows.Forms.StatusStrip status_strip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.TextBox app_path;
        private System.Windows.Forms.Label app_path_label;
        private System.Windows.Forms.Label cmd_line_label;
        private System.Windows.Forms.TextBox cmd_line;
        private System.Windows.Forms.Label time_delay_label;
        private System.Windows.Forms.TextBox time_delay;
        private System.Windows.Forms.CheckBox always_on_top;
        private System.Windows.Forms.CheckBox maximize;
        private System.Windows.Forms.CheckBox metrics;
        private System.Windows.Forms.TextBox crash_script_path;
        private System.Windows.Forms.Label crash_script_label;
        private System.Windows.Forms.Button app_path_button;
        private System.Windows.Forms.Button crash_script_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button watch_button;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox start_with_windows;
        private System.Windows.Forms.CheckBox verbose_logging;
        private System.Windows.Forms.CheckBox screenshot_on_crash;
        private System.Windows.Forms.TabPage log;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button screenshot_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox crashed_if_unresponsive;
    }
}


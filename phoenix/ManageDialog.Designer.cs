namespace phoenix
{
    partial class ManageDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageDialog));
            this.manage_ui = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // manage_ui
            // 
            this.manage_ui.AllowNavigation = false;
            this.manage_ui.AllowWebBrowserDrop = false;
            this.manage_ui.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manage_ui.IsWebBrowserContextMenuEnabled = false;
            this.manage_ui.Location = new System.Drawing.Point(0, 0);
            this.manage_ui.MinimumSize = new System.Drawing.Size(20, 20);
            this.manage_ui.Name = "manage_ui";
            this.manage_ui.ScriptErrorsSuppressed = true;
            this.manage_ui.Size = new System.Drawing.Size(784, 561);
            this.manage_ui.TabIndex = 0;
            this.manage_ui.Url = new System.Uri("", System.UriKind.Relative);
            this.manage_ui.WebBrowserShortcutsEnabled = false;
            // 
            // ManageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.manage_ui);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageDialog";
            this.Text = "Manage remote instances";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser manage_ui;
    }
}
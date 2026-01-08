namespace AktarOtomasyon.Forms.Screens.Template
{
    partial class FrmSystemSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ucSystemSettings = new AktarOtomasyon.Forms.Screens.Template.UcSystemSettings();
            this.SuspendLayout();
            //
            // ucSystemSettings
            //
            this.ucSystemSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSystemSettings.Location = new System.Drawing.Point(0, 0);
            this.ucSystemSettings.Name = "ucSystemSettings";
            this.ucSystemSettings.Size = new System.Drawing.Size(900, 600);
            this.ucSystemSettings.TabIndex = 0;
            //
            // FrmSystemSettings
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.ucSystemSettings);
            this.Name = "FrmSystemSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistem Ayarları";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSystemSettings_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private UcSystemSettings ucSystemSettings;
    }
}

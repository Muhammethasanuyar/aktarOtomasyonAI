namespace AktarOtomasyon.Forms.Screens.Template
{
    partial class FrmTemplateMrk
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
            this.ucTemplateMrk = new AktarOtomasyon.Forms.Screens.Template.UcTemplateMrk();
            this.SuspendLayout();
            //
            // ucTemplateMrk
            //
            this.ucTemplateMrk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTemplateMrk.Location = new System.Drawing.Point(0, 0);
            this.ucTemplateMrk.Name = "ucTemplateMrk";
            this.ucTemplateMrk.Size = new System.Drawing.Size(1200, 700);
            this.ucTemplateMrk.TabIndex = 0;
            //
            // FrmTemplateMrk
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.ucTemplateMrk);
            this.Name = "FrmTemplateMrk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şablon Yönetimi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTemplateMrk_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private UcTemplateMrk ucTemplateMrk;
    }
}

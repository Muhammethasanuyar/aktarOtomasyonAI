namespace AktarOtomasyon.Forms.Screens.Satis
{
    partial class FrmSatis
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

        private void InitializeComponent()
        {
            this.ucSatis = new AktarOtomasyon.Forms.Screens.Satis.UcSatis();
            this.SuspendLayout();
            // 
            // ucSatis
            // 
            this.ucSatis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSatis.Location = new System.Drawing.Point(0, 0);
            this.ucSatis.Name = "ucSatis";
            this.ucSatis.Size = new System.Drawing.Size(1000, 600);
            this.ucSatis.TabIndex = 0;
            // 
            // FrmSatis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.ucSatis);
            this.Name = "FrmSatis";
            this.Text = "Hızlı Satış";
            this.Load += new System.EventHandler(this.FrmSatis_Load);
            this.ResumeLayout(false);
        }

        private UcSatis ucSatis;
    }
}

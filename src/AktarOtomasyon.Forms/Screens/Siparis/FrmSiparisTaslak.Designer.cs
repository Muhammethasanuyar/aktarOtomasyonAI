namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class FrmSiparisTaslak
    {
        private System.ComponentModel.IContainer components = null;
        private UcSiparisTaslak ucSiparisTaslak;

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
            this.ucSiparisTaslak = new UcSiparisTaslak();
            this.SuspendLayout();

            // ucSiparisTaslak
            this.ucSiparisTaslak.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiparisTaslak.Location = new System.Drawing.Point(0, 0);
            this.ucSiparisTaslak.Name = "ucSiparisTaslak";
            this.ucSiparisTaslak.Size = new System.Drawing.Size(800, 600);
            this.ucSiparisTaslak.TabIndex = 0;

            // FrmSiparisTaslak
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ucSiparisTaslak);
            this.Name = "FrmSiparisTaslak";
            this.Text = "Sipariş Taslak";
            this.Load += new System.EventHandler(this.FrmSiparisTaslak_Load);
            this.ResumeLayout(false);
        }
    }
}

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class FrmSiparisTeslim
    {
        private System.ComponentModel.IContainer components = null;
        private UcSiparisTeslim ucSiparisTeslim;

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
            this.ucSiparisTeslim = new UcSiparisTeslim();
            this.SuspendLayout();

            // ucSiparisTeslim
            this.ucSiparisTeslim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiparisTeslim.Location = new System.Drawing.Point(0, 0);
            this.ucSiparisTeslim.Name = "ucSiparisTeslim";
            this.ucSiparisTeslim.Size = new System.Drawing.Size(700, 500);
            this.ucSiparisTeslim.TabIndex = 0;

            // FrmSiparisTeslim
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.ucSiparisTeslim);
            this.Name = "FrmSiparisTeslim";
            this.Text = "Sipariş Teslim Alma";
            this.Load += new System.EventHandler(this.FrmSiparisTeslim_Load);
            this.ResumeLayout(false);
        }
    }
}

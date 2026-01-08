namespace AktarOtomasyon.Forms.Screens.Stok
{
    partial class FrmStokKritik
    {
        private System.ComponentModel.IContainer components = null;
        private UcStokKritik ucStokKritik;

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
            this.ucStokKritik = new UcStokKritik();
            this.SuspendLayout();

            // ucStokKritik
            this.ucStokKritik.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStokKritik.Location = new System.Drawing.Point(0, 0);
            this.ucStokKritik.Name = "ucStokKritik";
            this.ucStokKritik.Size = new System.Drawing.Size(770, 700);
            this.ucStokKritik.TabIndex = 0;

            // FrmStokKritik
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucStokKritik);
            this.Name = "FrmStokKritik";
            this.Text = "Kritik Stok";
            this.Load += new System.EventHandler(this.FrmStokKritik_Load);
            this.ResumeLayout(false);
        }
    }
}

namespace AktarOtomasyon.Forms.Screens.Stok
{
    partial class FrmStokHareket
    {
        private System.ComponentModel.IContainer components = null;
        private UcStokHareket ucStokHareket;

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
            this.ucStokHareket = new UcStokHareket();
            this.SuspendLayout();

            // ucStokHareket
            this.ucStokHareket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucStokHareket.Location = new System.Drawing.Point(0, 0);
            this.ucStokHareket.Name = "ucStokHareket";
            this.ucStokHareket.Size = new System.Drawing.Size(770, 700);
            this.ucStokHareket.TabIndex = 0;

            // FrmStokHareket
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucStokHareket);
            this.Name = "FrmStokHareket";
            this.Text = "Stok Hareketleri";
            this.Load += new System.EventHandler(this.FrmStokHareket_Load);
            this.ResumeLayout(false);
        }
    }
}

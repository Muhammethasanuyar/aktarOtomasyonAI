namespace AktarOtomasyon.Forms.Screens.Raporlar
{
    partial class FrmRaporlar
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
            this.ucRaporlar = new AktarOtomasyon.Forms.Screens.Raporlar.UcRaporlar();
            this.SuspendLayout();
            // 
            // ucRaporlar
            // 
            this.ucRaporlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucRaporlar.Location = new System.Drawing.Point(0, 0);
            this.ucRaporlar.Name = "ucRaporlar";
            this.ucRaporlar.Size = new System.Drawing.Size(800, 450);
            this.ucRaporlar.TabIndex = 0;
            // 
            // FrmRaporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucRaporlar);
            this.Name = "FrmRaporlar";
            this.Text = "Raporlar";
            this.Load += new System.EventHandler(this.FrmRaporlar_Load);
            this.ResumeLayout(false);
        }

        private UcRaporlar ucRaporlar;
    }
}

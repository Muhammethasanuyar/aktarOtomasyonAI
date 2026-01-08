namespace AktarOtomasyon.Forms.Screens.Bildirim
{
    partial class FrmBildirimMrk
    {
        private System.ComponentModel.IContainer components = null;
        private UcBildirimMrk ucBildirimMrk;

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
            this.ucBildirimMrk = new UcBildirimMrk();
            this.SuspendLayout();

            // ucBildirimMrk
            this.ucBildirimMrk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBildirimMrk.Location = new System.Drawing.Point(0, 0);
            this.ucBildirimMrk.Name = "ucBildirimMrk";
            this.ucBildirimMrk.Size = new System.Drawing.Size(770, 700);
            this.ucBildirimMrk.TabIndex = 0;

            // FrmBildirimMrk
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucBildirimMrk);
            this.Name = "FrmBildirimMrk";
            this.Text = "Bildirim Merkezi";
            this.Load += new System.EventHandler(this.FrmBildirimMrk_Load);
            this.ResumeLayout(false);
        }
    }
}

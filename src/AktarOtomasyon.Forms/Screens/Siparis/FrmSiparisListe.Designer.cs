namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class FrmSiparisListe
    {
        private System.ComponentModel.IContainer components = null;
        private UcSiparisListe ucSiparisListe;

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
            this.ucSiparisListe = new UcSiparisListe();
            this.SuspendLayout();

            // ucSiparisListe
            this.ucSiparisListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSiparisListe.Location = new System.Drawing.Point(0, 0);
            this.ucSiparisListe.Name = "ucSiparisListe";
            this.ucSiparisListe.Size = new System.Drawing.Size(900, 700);
            this.ucSiparisListe.TabIndex = 0;

            // FrmSiparisListe
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.ucSiparisListe);
            this.Name = "FrmSiparisListe";
            this.Text = "Sipariş Listesi";
            this.Load += new System.EventHandler(this.FrmSiparisListe_Load);
            this.ResumeLayout(false);
        }
    }
}

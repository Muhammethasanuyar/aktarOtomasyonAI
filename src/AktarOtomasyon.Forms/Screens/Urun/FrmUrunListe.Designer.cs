namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class FrmUrunListe
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
            this.ucUrunListe = new AktarOtomasyon.Forms.Screens.Urun.UcUrunListe();
            this.SuspendLayout();
            //
            // ucUrunListe
            //
            this.ucUrunListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUrunListe.Location = new System.Drawing.Point(0, 0);
            this.ucUrunListe.Name = "ucUrunListe";
            this.ucUrunListe.Size = new System.Drawing.Size(770, 700);
            this.ucUrunListe.TabIndex = 0;
            //
            // FrmUrunListe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucUrunListe);
            this.Name = "FrmUrunListe";
            this.Text = "Ürün Listesi";
            this.Load += new System.EventHandler(this.FrmUrunListe_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private UcUrunListe ucUrunListe;
    }
}

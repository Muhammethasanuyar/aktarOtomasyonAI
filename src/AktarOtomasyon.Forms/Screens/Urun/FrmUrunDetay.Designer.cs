namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class FrmUrunDetay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private UcUrunDetay ucUrunDetay;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucUrunDetay = new AktarOtomasyon.Forms.Screens.Urun.UcUrunDetay();
            this.SuspendLayout();
            //
            // ucUrunDetay
            //
            this.ucUrunDetay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUrunDetay.Location = new System.Drawing.Point(0, 0);
            this.ucUrunDetay.Name = "ucUrunDetay";
            this.ucUrunDetay.Size = new System.Drawing.Size(750, 680);
            this.ucUrunDetay.TabIndex = 0;
            //
            // FrmUrunDetay
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 680);
            this.Controls.Add(this.ucUrunDetay);
            this.MaximumSize = new System.Drawing.Size(770, 720);
            this.Name = "FrmUrunDetay";
            this.Text = "Ürün Detay";
            this.Load += new System.EventHandler(this.FrmUrunDetay_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

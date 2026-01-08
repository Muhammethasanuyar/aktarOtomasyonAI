namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class FrmUrunKart
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
            this.ucUrunKart = new AktarOtomasyon.Forms.Screens.Urun.UcUrunKart();
            this.SuspendLayout();
            //
            // ucUrunKart
            //
            this.ucUrunKart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUrunKart.Location = new System.Drawing.Point(0, 0);
            this.ucUrunKart.Name = "ucUrunKart";
            this.ucUrunKart.Size = new System.Drawing.Size(770, 700);
            this.ucUrunKart.TabIndex = 0;
            //
            // FrmUrunKart
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucUrunKart);
            this.Name = "FrmUrunKart";
            this.Text = "Ürün Kartı";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmUrunKart_FormClosed);
            this.Load += new System.EventHandler(this.FrmUrunKart_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private UcUrunKart ucUrunKart;
    }
}

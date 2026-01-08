namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class FrmUrunKatalog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUrunKatalog));
            this.ucUrunKatalog = new AktarOtomasyon.Forms.Screens.Urun.UcUrunKatalog();
            this.SuspendLayout();
            // 
            // ucUrunKatalog
            // 
            this.ucUrunKatalog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUrunKatalog.Location = new System.Drawing.Point(0, 0);
            this.ucUrunKatalog.Name = "ucUrunKatalog";
            this.ucUrunKatalog.Size = new System.Drawing.Size(800, 450);
            this.ucUrunKatalog.TabIndex = 0;
            // 
            // FrmUrunKatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ucUrunKatalog);
            this.Name = "FrmUrunKatalog";
            this.Text = "Ürün Kataloğu";
            this.Load += new System.EventHandler(this.FrmUrunKatalog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AktarOtomasyon.Forms.Screens.Urun.UcUrunKatalog ucUrunKatalog;
    }
}

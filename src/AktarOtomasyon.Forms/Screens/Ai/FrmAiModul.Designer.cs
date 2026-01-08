namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class FrmAiModul
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
            this.ucAiModul = new AktarOtomasyon.Forms.Screens.Ai.UcAiModul();
            this.SuspendLayout();
            //
            // ucAiModul
            //
            this.ucAiModul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAiModul.Location = new System.Drawing.Point(0, 0);
            this.ucAiModul.Name = "ucAiModul";
            this.ucAiModul.Size = new System.Drawing.Size(800, 650);
            this.ucAiModul.TabIndex = 0;
            //
            // FrmAiModul
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 650);
            this.Controls.Add(this.ucAiModul);
            this.Name = "FrmAiModul";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI İçerik Üretimi";
            this.Load += new System.EventHandler(this.FrmAiModul_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAiModul ucAiModul;
    }
}

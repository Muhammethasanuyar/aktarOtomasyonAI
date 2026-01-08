namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class FrmAiVersiyonlar
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
            this.ucAiVersiyonlar = new AktarOtomasyon.Forms.Screens.Ai.UcAiVersiyonlar();
            this.SuspendLayout();
            //
            // ucAiVersiyonlar
            //
            this.ucAiVersiyonlar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAiVersiyonlar.Location = new System.Drawing.Point(0, 0);
            this.ucAiVersiyonlar.Name = "ucAiVersiyonlar";
            this.ucAiVersiyonlar.Size = new System.Drawing.Size(900, 600);
            this.ucAiVersiyonlar.TabIndex = 0;
            //
            // FrmAiVersiyonlar
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.ucAiVersiyonlar);
            this.Name = "FrmAiVersiyonlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI İçerik Versiyon Geçmişi";
            this.Load += new System.EventHandler(this.FrmAiVersiyonlar_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAiVersiyonlar ucAiVersiyonlar;
    }
}

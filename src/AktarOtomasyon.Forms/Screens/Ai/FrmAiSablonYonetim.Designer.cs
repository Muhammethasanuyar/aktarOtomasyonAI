namespace AktarOtomasyon.Forms.Screens.Ai
{
    partial class FrmAiSablonYonetim
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
            this.ucAiSablonYonetim = new AktarOtomasyon.Forms.Screens.Ai.UcAiSablonYonetim();
            this.SuspendLayout();
            //
            // ucAiSablonYonetim
            //
            this.ucAiSablonYonetim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAiSablonYonetim.Location = new System.Drawing.Point(0, 0);
            this.ucAiSablonYonetim.Name = "ucAiSablonYonetim";
            this.ucAiSablonYonetim.Size = new System.Drawing.Size(770, 700);
            this.ucAiSablonYonetim.TabIndex = 0;
            //
            // FrmAiSablonYonetim
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucAiSablonYonetim);
            this.MaximumSize = new System.Drawing.Size(770, 700);
            this.Name = "FrmAiSablonYonetim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI Şablon Yönetimi";
            this.Load += new System.EventHandler(this.FrmAiSablonYonetim_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UcAiSablonYonetim ucAiSablonYonetim;
    }
}

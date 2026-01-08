namespace AktarOtomasyon.Forms.Screens.Test
{
    partial class FrmBarkodTest
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
            this.ucBarkodTest = new AktarOtomasyon.Forms.Screens.Test.UcBarkodTest();
            this.SuspendLayout();
            // 
            // ucBarkodTest
            // 
            this.ucBarkodTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucBarkodTest.Location = new System.Drawing.Point(0, 0);
            this.ucBarkodTest.Name = "ucBarkodTest";
            this.ucBarkodTest.Size = new System.Drawing.Size(800, 600);
            this.ucBarkodTest.TabIndex = 0;
            // 
            // FrmBarkodTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.ucBarkodTest);
            this.Name = "FrmBarkodTest";
            this.Text = " Barkod Okuma Testi";
            this.Load += new System.EventHandler(this.FrmBarkodTest_Load);
            this.ResumeLayout(false);
        }

        private UcBarkodTest ucBarkodTest;
    }
}

namespace AktarOtomasyon.Forms.Screens.Dashboard
{
    partial class FrmANA_DASH
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
            this.ucDashboard = new AktarOtomasyon.Forms.Screens.Dashboard.UcANA_DASH();
            this.SuspendLayout();
            //
            // ucDashboard
            //
            this.ucDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDashboard.Location = new System.Drawing.Point(0, 0);
            this.ucDashboard.Name = "ucDashboard";
            this.ucDashboard.Size = new System.Drawing.Size(770, 700);
            this.ucDashboard.TabIndex = 0;
            //
            // FrmANA_DASH
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 700);
            this.Controls.Add(this.ucDashboard);
            this.Name = "FrmANA_DASH";
            this.Text = "Ana Ekran";
            this.Load += new System.EventHandler(this.FrmANA_DASH_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private UcANA_DASH ucDashboard;
    }
}

namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class FrmSecurityManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ucSecurityManagement1 = new AktarOtomasyon.Forms.Screens.Security.UcSecurityManagement();
            this.SuspendLayout();
            //
            // ucSecurityManagement1
            //
            this.ucSecurityManagement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSecurityManagement1.Location = new System.Drawing.Point(0, 0);
            this.ucSecurityManagement1.Name = "ucSecurityManagement1";
            this.ucSecurityManagement1.Size = new System.Drawing.Size(1000, 600);
            this.ucSecurityManagement1.TabIndex = 0;
            //
            // FrmSecurityManagement
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.ucSecurityManagement1);
            this.Name = "FrmSecurityManagement";
            this.Text = "Güvenlik Yönetimi";
            this.ResumeLayout(false);
        }

        #endregion

        private UcSecurityManagement ucSecurityManagement1;
    }
}

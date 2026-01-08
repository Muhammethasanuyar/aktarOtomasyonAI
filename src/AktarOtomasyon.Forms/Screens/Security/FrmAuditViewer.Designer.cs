namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class FrmAuditViewer
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
            this.ucAuditViewer1 = new AktarOtomasyon.Forms.Screens.Security.UcAuditViewer();
            this.SuspendLayout();
            //
            // ucAuditViewer1
            //
            this.ucAuditViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAuditViewer1.Location = new System.Drawing.Point(0, 0);
            this.ucAuditViewer1.Name = "ucAuditViewer1";
            this.ucAuditViewer1.Size = new System.Drawing.Size(1200, 700);
            this.ucAuditViewer1.TabIndex = 0;
            //
            // FrmAuditViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.ucAuditViewer1);
            this.Name = "FrmAuditViewer";
            this.Text = "İşlem Geçmişi";
            this.ResumeLayout(false);
        }

        #endregion

        private UcAuditViewer ucAuditViewer1;
    }
}

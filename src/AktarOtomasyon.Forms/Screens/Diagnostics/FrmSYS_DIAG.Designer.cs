namespace AktarOtomasyon.Forms.Screens.Diagnostics
{
    partial class FrmSYS_DIAG
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
            this.ucSysDiag = new AktarOtomasyon.Forms.Screens.Diagnostics.UcSYS_DIAG();
            this.SuspendLayout();
            //
            // ucSysDiag
            //
            this.ucSysDiag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSysDiag.Location = new System.Drawing.Point(0, 0);
            this.ucSysDiag.Name = "ucSysDiag";
            this.ucSysDiag.Size = new System.Drawing.Size(784, 661);
            this.ucSysDiag.TabIndex = 0;
            //
            // FrmSYS_DIAG
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.ucSysDiag);
            this.Name = "FrmSYS_DIAG";
            this.Text = "Sistem Durumu";
            this.ResumeLayout(false);
        }

        #endregion

        private UcSYS_DIAG ucSysDiag;
    }
}

namespace AktarOtomasyon.Forms.Screens.Security
{
    partial class UcSecurityManagement
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageKullanici = new DevExpress.XtraTab.XtraTabPage();
            this.tabPageRol = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            //
            // tabControl
            //
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabPageKullanici;
            this.tabControl.Size = new System.Drawing.Size(1000, 600);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageKullanici,
            this.tabPageRol});
            this.tabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.tabControl_SelectedPageChanged);
            //
            // tabPageKullanici
            //
            this.tabPageKullanici.Name = "tabPageKullanici";
            this.tabPageKullanici.Size = new System.Drawing.Size(998, 575);
            this.tabPageKullanici.Text = "Kullanıcı Yönetimi";
            //
            // tabPageRol
            //
            this.tabPageRol.Name = "tabPageRol";
            this.tabPageRol.Size = new System.Drawing.Size(998, 575);
            this.tabPageRol.Text = "Rol Yönetimi";
            //
            // UcSecurityManagement
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "UcSecurityManagement";
            this.Size = new System.Drawing.Size(1000, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabPageKullanici;
        private DevExpress.XtraTab.XtraTabPage tabPageRol;
    }
}

namespace AktarOtomasyon.Forms.Screens.Diagnostics
{
    partial class UcSYS_DIAG
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
            this.grpEnvironmentInfo = new DevExpress.XtraEditors.GroupControl();
            this.lblAiApiKey = new DevExpress.XtraEditors.LabelControl();
            this.lblAiProvider = new DevExpress.XtraEditors.LabelControl();
            this.lblMediaPath = new DevExpress.XtraEditors.LabelControl();
            this.lblReportPath = new DevExpress.XtraEditors.LabelControl();
            this.lblTemplatePath = new DevExpress.XtraEditors.LabelControl();
            this.lblStorageMode = new DevExpress.XtraEditors.LabelControl();
            this.lblDatabaseServer = new DevExpress.XtraEditors.LabelControl();
            this.lblUserName = new DevExpress.XtraEditors.LabelControl();
            this.lblMachineName = new DevExpress.XtraEditors.LabelControl();
            this.lblAppVersion = new DevExpress.XtraEditors.LabelControl();
            this.gcDiagnostics = new DevExpress.XtraGrid.GridControl();
            this.gvDiagnostics = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheckName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDetails = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastRun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCopy = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnRunSelected = new DevExpress.XtraEditors.SimpleButton();
            this.btnRunAllChecks = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironmentInfo)).BeginInit();
            this.grpEnvironmentInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDiagnostics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDiagnostics)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // grpEnvironmentInfo
            //
            this.grpEnvironmentInfo.Controls.Add(this.lblAiApiKey);
            this.grpEnvironmentInfo.Controls.Add(this.lblAiProvider);
            this.grpEnvironmentInfo.Controls.Add(this.lblMediaPath);
            this.grpEnvironmentInfo.Controls.Add(this.lblReportPath);
            this.grpEnvironmentInfo.Controls.Add(this.lblTemplatePath);
            this.grpEnvironmentInfo.Controls.Add(this.lblStorageMode);
            this.grpEnvironmentInfo.Controls.Add(this.lblDatabaseServer);
            this.grpEnvironmentInfo.Controls.Add(this.lblUserName);
            this.grpEnvironmentInfo.Controls.Add(this.lblMachineName);
            this.grpEnvironmentInfo.Controls.Add(this.lblAppVersion);
            this.grpEnvironmentInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpEnvironmentInfo.Location = new System.Drawing.Point(0, 0);
            this.grpEnvironmentInfo.Name = "grpEnvironmentInfo";
            this.grpEnvironmentInfo.Size = new System.Drawing.Size(784, 180);
            this.grpEnvironmentInfo.TabIndex = 0;
            this.grpEnvironmentInfo.Text = "Ortam Bilgileri";
            //
            // lblAiApiKey
            //
            this.lblAiApiKey.Location = new System.Drawing.Point(400, 135);
            this.lblAiApiKey.Name = "lblAiApiKey";
            this.lblAiApiKey.Size = new System.Drawing.Size(63, 13);
            this.lblAiApiKey.TabIndex = 9;
            this.lblAiApiKey.Text = "API Key: ***";
            //
            // lblAiProvider
            //
            this.lblAiProvider.Location = new System.Drawing.Point(400, 115);
            this.lblAiProvider.Name = "lblAiProvider";
            this.lblAiProvider.Size = new System.Drawing.Size(88, 13);
            this.lblAiProvider.TabIndex = 8;
            this.lblAiProvider.Text = "AI Provider: None";
            //
            // lblMediaPath
            //
            this.lblMediaPath.Location = new System.Drawing.Point(15, 155);
            this.lblMediaPath.Name = "lblMediaPath";
            this.lblMediaPath.Size = new System.Drawing.Size(86, 13);
            this.lblMediaPath.TabIndex = 7;
            this.lblMediaPath.Text = "Medya Yolu: N/A";
            //
            // lblReportPath
            //
            this.lblReportPath.Location = new System.Drawing.Point(15, 135);
            this.lblReportPath.Name = "lblReportPath";
            this.lblReportPath.Size = new System.Drawing.Size(85, 13);
            this.lblReportPath.TabIndex = 6;
            this.lblReportPath.Text = "Rapor Yolu: N/A";
            //
            // lblTemplatePath
            //
            this.lblTemplatePath.Location = new System.Drawing.Point(15, 115);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(93, 13);
            this.lblTemplatePath.TabIndex = 5;
            this.lblTemplatePath.Text = "Şablon Yolu: N/A";
            //
            // lblStorageMode
            //
            this.lblStorageMode.Location = new System.Drawing.Point(15, 95);
            this.lblStorageMode.Name = "lblStorageMode";
            this.lblStorageMode.Size = new System.Drawing.Size(109, 13);
            this.lblStorageMode.TabIndex = 4;
            this.lblStorageMode.Text = "Depolama Modu: N/A";
            //
            // lblDatabaseServer
            //
            this.lblDatabaseServer.Location = new System.Drawing.Point(15, 75);
            this.lblDatabaseServer.Name = "lblDatabaseServer";
            this.lblDatabaseServer.Size = new System.Drawing.Size(87, 13);
            this.lblDatabaseServer.TabIndex = 3;
            this.lblDatabaseServer.Text = "Veritabanı: ***";
            //
            // lblUserName
            //
            this.lblUserName.Location = new System.Drawing.Point(15, 55);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(77, 13);
            this.lblUserName.TabIndex = 2;
            this.lblUserName.Text = "Kullanıcı: N/A";
            //
            // lblMachineName
            //
            this.lblMachineName.Location = new System.Drawing.Point(15, 35);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(71, 13);
            this.lblMachineName.TabIndex = 1;
            this.lblMachineName.Text = "Makine: N/A";
            //
            // lblAppVersion
            //
            this.lblAppVersion.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAppVersion.Appearance.Options.UseFont = true;
            this.lblAppVersion.Location = new System.Drawing.Point(15, 15);
            this.lblAppVersion.Name = "lblAppVersion";
            this.lblAppVersion.Size = new System.Drawing.Size(139, 13);
            this.lblAppVersion.TabIndex = 0;
            this.lblAppVersion.Text = "Uygulama Versiyon: 1.0.0";
            //
            // gcDiagnostics
            //
            this.gcDiagnostics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDiagnostics.Location = new System.Drawing.Point(0, 180);
            this.gcDiagnostics.MainView = this.gvDiagnostics;
            this.gcDiagnostics.Name = "gcDiagnostics";
            this.gcDiagnostics.Size = new System.Drawing.Size(784, 421);
            this.gcDiagnostics.TabIndex = 1;
            this.gcDiagnostics.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDiagnostics});
            //
            // gvDiagnostics
            //
            this.gvDiagnostics.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheckName,
            this.colStatus,
            this.colDetails,
            this.colLastRun});
            this.gvDiagnostics.GridControl = this.gcDiagnostics;
            this.gvDiagnostics.Name = "gvDiagnostics";
            this.gvDiagnostics.OptionsBehavior.Editable = false;
            this.gvDiagnostics.OptionsView.ShowGroupPanel = false;
            this.gvDiagnostics.OptionsView.ShowIndicator = false;
            //
            // colCheckName
            //
            this.colCheckName.Caption = "Kontrol Adı";
            this.colCheckName.FieldName = "CheckName";
            this.colCheckName.Name = "colCheckName";
            this.colCheckName.Visible = true;
            this.colCheckName.VisibleIndex = 0;
            this.colCheckName.Width = 200;
            //
            // colStatus
            //
            this.colStatus.Caption = "Durum";
            this.colStatus.FieldName = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            this.colStatus.Width = 100;
            //
            // colDetails
            //
            this.colDetails.Caption = "Detaylar";
            this.colDetails.FieldName = "Details";
            this.colDetails.Name = "colDetails";
            this.colDetails.Visible = true;
            this.colDetails.VisibleIndex = 2;
            this.colDetails.Width = 350;
            //
            // colLastRun
            //
            this.colLastRun.Caption = "Son Çalıştırma";
            this.colLastRun.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm:ss";
            this.colLastRun.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colLastRun.FieldName = "LastRun";
            this.colLastRun.Name = "colLastRun";
            this.colLastRun.Visible = true;
            this.colLastRun.VisibleIndex = 3;
            this.colLastRun.Width = 130;
            //
            // panelButtons
            //
            this.panelButtons.Controls.Add(this.btnCopy);
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnRunSelected);
            this.panelButtons.Controls.Add(this.btnRunAllChecks);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 601);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(784, 60);
            this.panelButtons.TabIndex = 2;
            //
            // btnCopy
            //
            this.btnCopy.Location = new System.Drawing.Point(488, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 30);
            this.btnCopy.TabIndex = 3;
            this.btnCopy.Text = "Kopyala";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            //
            // btnExport
            //
            this.btnExport.Location = new System.Drawing.Point(332, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Rapor Oluştur";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // btnRunSelected
            //
            this.btnRunSelected.Location = new System.Drawing.Point(176, 15);
            this.btnRunSelected.Name = "btnRunSelected";
            this.btnRunSelected.Size = new System.Drawing.Size(150, 30);
            this.btnRunSelected.TabIndex = 1;
            this.btnRunSelected.Text = "Seçili Kontrolü Çalıştır";
            this.btnRunSelected.Click += new System.EventHandler(this.btnRunSelected_Click);
            //
            // btnRunAllChecks
            //
            this.btnRunAllChecks.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRunAllChecks.Appearance.Options.UseFont = true;
            this.btnRunAllChecks.Location = new System.Drawing.Point(20, 15);
            this.btnRunAllChecks.Name = "btnRunAllChecks";
            this.btnRunAllChecks.Size = new System.Drawing.Size(150, 30);
            this.btnRunAllChecks.TabIndex = 0;
            this.btnRunAllChecks.Text = "Tüm Kontrolleri Çalıştır";
            this.btnRunAllChecks.Click += new System.EventHandler(this.btnRunAllChecks_Click);
            //
            // UcSYS_DIAG
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcDiagnostics);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.grpEnvironmentInfo);
            this.Name = "UcSYS_DIAG";
            this.Size = new System.Drawing.Size(784, 661);
            ((System.ComponentModel.ISupportInitialize)(this.grpEnvironmentInfo)).EndInit();
            this.grpEnvironmentInfo.ResumeLayout(false);
            this.grpEnvironmentInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDiagnostics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDiagnostics)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpEnvironmentInfo;
        private DevExpress.XtraEditors.LabelControl lblAppVersion;
        private DevExpress.XtraEditors.LabelControl lblMachineName;
        private DevExpress.XtraEditors.LabelControl lblUserName;
        private DevExpress.XtraEditors.LabelControl lblDatabaseServer;
        private DevExpress.XtraEditors.LabelControl lblStorageMode;
        private DevExpress.XtraEditors.LabelControl lblTemplatePath;
        private DevExpress.XtraEditors.LabelControl lblReportPath;
        private DevExpress.XtraEditors.LabelControl lblMediaPath;
        private DevExpress.XtraEditors.LabelControl lblAiProvider;
        private DevExpress.XtraEditors.LabelControl lblAiApiKey;
        private DevExpress.XtraGrid.GridControl gcDiagnostics;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDiagnostics;
        private DevExpress.XtraGrid.Columns.GridColumn colCheckName;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colDetails;
        private DevExpress.XtraGrid.Columns.GridColumn colLastRun;
        private System.Windows.Forms.Panel panelButtons;
        private DevExpress.XtraEditors.SimpleButton btnRunAllChecks;
        private DevExpress.XtraEditors.SimpleButton btnRunSelected;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.SimpleButton btnCopy;
    }
}

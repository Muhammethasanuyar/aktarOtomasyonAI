namespace AktarOtomasyon.Forms.Screens.Template
{
    partial class UcSystemSettings
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcSettings = new DevExpress.XtraGrid.GridControl();
            this.gvSettings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSettingKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSettingValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfigSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAciklama = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnBrowseReportPath = new DevExpress.XtraEditors.SimpleButton();
            this.btnBrowseTemplatePath = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblKayitSayisi = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            //
            // layoutControl1
            //
            this.layoutControl1.Controls.Add(this.gcSettings);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(900, 600);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            //
            // gcSettings
            //
            this.gcSettings.Location = new System.Drawing.Point(12, 62);
            this.gcSettings.MainView = this.gvSettings;
            this.gcSettings.Name = "gcSettings";
            this.gcSettings.Size = new System.Drawing.Size(876, 496);
            this.gcSettings.TabIndex = 4;
            this.gcSettings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSettings});
            //
            // gvSettings
            //
            this.gvSettings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSettingKey,
            this.colSettingValue,
            this.colConfigSource,
            this.colAciklama,
            this.colUpdatedAt});
            this.gvSettings.GridControl = this.gcSettings;
            this.gvSettings.Name = "gvSettings";
            this.gvSettings.OptionsBehavior.Editable = true;
            this.gvSettings.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditFormInplace;
            this.gvSettings.OptionsView.ShowGroupPanel = false;
            this.gvSettings.OptionsView.ShowIndicator = true;
            this.gvSettings.OptionsView.ColumnAutoWidth = false;
            //
            // colSettingKey
            //
            this.colSettingKey.Caption = "Ayar Anahtarı";
            this.colSettingKey.FieldName = "SettingKey";
            this.colSettingKey.Name = "colSettingKey";
            this.colSettingKey.OptionsColumn.AllowEdit = false;
            this.colSettingKey.OptionsColumn.ReadOnly = true;
            this.colSettingKey.Visible = true;
            this.colSettingKey.VisibleIndex = 0;
            this.colSettingKey.Width = 200;
            //
            // colSettingValue
            //
            this.colSettingValue.Caption = "Değer";
            this.colSettingValue.FieldName = "SettingValue";
            this.colSettingValue.Name = "colSettingValue";
            this.colSettingValue.OptionsColumn.AllowEdit = true;
            this.colSettingValue.Visible = true;
            this.colSettingValue.VisibleIndex = 1;
            this.colSettingValue.Width = 300;
            //
            // colConfigSource
            //
            this.colConfigSource.Caption = "Kaynak";
            this.colConfigSource.FieldName = "ConfigSource";
            this.colConfigSource.Name = "colConfigSource";
            this.colConfigSource.OptionsColumn.AllowEdit = false;
            this.colConfigSource.OptionsColumn.ReadOnly = true;
            this.colConfigSource.Visible = true;
            this.colConfigSource.VisibleIndex = 2;
            this.colConfigSource.Width = 100;
            //
            // colAciklama
            //
            this.colAciklama.Caption = "Açıklama";
            this.colAciklama.FieldName = "Aciklama";
            this.colAciklama.Name = "colAciklama";
            this.colAciklama.OptionsColumn.AllowEdit = false;
            this.colAciklama.OptionsColumn.ReadOnly = true;
            this.colAciklama.Visible = true;
            this.colAciklama.VisibleIndex = 3;
            this.colAciklama.Width = 250;
            //
            // colUpdatedAt
            //
            this.colUpdatedAt.Caption = "Güncellenme";
            this.colUpdatedAt.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.colUpdatedAt.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colUpdatedAt.FieldName = "UpdatedAt";
            this.colUpdatedAt.Name = "colUpdatedAt";
            this.colUpdatedAt.OptionsColumn.AllowEdit = false;
            this.colUpdatedAt.OptionsColumn.ReadOnly = true;
            this.colUpdatedAt.Visible = true;
            this.colUpdatedAt.VisibleIndex = 4;
            this.colUpdatedAt.Width = 150;
            //
            // panelControl1
            //
            this.panelControl1.Controls.Add(this.btnBrowseReportPath);
            this.panelControl1.Controls.Add(this.btnBrowseTemplatePath);
            this.panelControl1.Controls.Add(this.btnKaydet);
            this.panelControl1.Controls.Add(this.btnYenile);
            this.panelControl1.Location = new System.Drawing.Point(12, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(876, 46);
            this.panelControl1.TabIndex = 5;
            //
            // btnBrowseReportPath
            //
            this.btnBrowseReportPath.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnBrowseReportPath.Location = new System.Drawing.Point(265, 8);
            this.btnBrowseReportPath.Name = "btnBrowseReportPath";
            this.btnBrowseReportPath.Size = new System.Drawing.Size(120, 30);
            this.btnBrowseReportPath.TabIndex = 3;
            this.btnBrowseReportPath.Text = "Rapor Dizini...";
            this.btnBrowseReportPath.ToolTip = "ReportPath ayarı için dizin seçin";
            //
            // btnBrowseTemplatePath
            //
            this.btnBrowseTemplatePath.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnBrowseTemplatePath.Location = new System.Drawing.Point(139, 8);
            this.btnBrowseTemplatePath.Name = "btnBrowseTemplatePath";
            this.btnBrowseTemplatePath.Size = new System.Drawing.Size(120, 30);
            this.btnBrowseTemplatePath.TabIndex = 2;
            this.btnBrowseTemplatePath.Text = "Şablon Dizini...";
            this.btnBrowseTemplatePath.ToolTip = "TemplatePath ayarı için dizin seçin";
            //
            // btnKaydet
            //
            this.btnKaydet.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnKaydet.Location = new System.Drawing.Point(69, 8);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(64, 30);
            this.btnKaydet.TabIndex = 1;
            this.btnKaydet.Text = "Kaydet";
            //
            // btnYenile
            //
            this.btnYenile.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.btnYenile.Location = new System.Drawing.Point(5, 8);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(58, 30);
            this.btnYenile.TabIndex = 0;
            this.btnYenile.Text = "Yenile";
            //
            // panelControl2
            //
            this.panelControl2.Controls.Add(this.lblKayitSayisi);
            this.panelControl2.Location = new System.Drawing.Point(12, 562);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(876, 26);
            this.panelControl2.TabIndex = 6;
            //
            // lblKayitSayisi
            //
            this.lblKayitSayisi.Location = new System.Drawing.Point(8, 6);
            this.lblKayitSayisi.Name = "lblKayitSayisi";
            this.lblKayitSayisi.Size = new System.Drawing.Size(63, 13);
            this.lblKayitSayisi.TabIndex = 0;
            this.lblKayitSayisi.Text = "Toplam 0 ayar";
            //
            // layoutControlGroup1
            //
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(900, 600);
            this.layoutControlGroup1.TextVisible = false;
            //
            // layoutControlItem1
            //
            this.layoutControlItem1.Control = this.panelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(880, 50);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            //
            // layoutControlItem2
            //
            this.layoutControlItem2.Control = this.gcSettings;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(880, 500);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            //
            // layoutControlItem3
            //
            this.layoutControlItem3.Control = this.panelControl2;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 550);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(880, 30);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            //
            // UcSystemSettings
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "UcSystemSettings";
            this.Size = new System.Drawing.Size(900, 600);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcSettings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSettings;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingKey;
        private DevExpress.XtraGrid.Columns.GridColumn colSettingValue;
        private DevExpress.XtraGrid.Columns.GridColumn colConfigSource;
        private DevExpress.XtraGrid.Columns.GridColumn colAciklama;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdatedAt;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBrowseReportPath;
        private DevExpress.XtraEditors.SimpleButton btnBrowseTemplatePath;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblKayitSayisi;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}

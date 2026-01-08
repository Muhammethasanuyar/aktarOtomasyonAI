namespace AktarOtomasyon.Forms.Screens.Template
{
    partial class UcTemplateMrk
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gcTemplateList = new DevExpress.XtraGrid.GridControl();
            this.gvTemplateList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTemplateKod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemplateAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colModul = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAktif = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAktifVersionNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lblKayitSayisi = new DevExpress.XtraEditors.LabelControl();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.txtArama = new DevExpress.XtraEditors.TextEdit();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.cmbModul = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();

            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabTemplateDetay = new DevExpress.XtraTab.XtraTabPage();
            this.tabVersions = new DevExpress.XtraTab.XtraTabPage();

            // Tab 1 controls
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.txtTemplateKod = new DevExpress.XtraEditors.TextEdit();
            this.txtTemplateAdi = new DevExpress.XtraEditors.TextEdit();
            this.cmbDetayModul = new DevExpress.XtraEditors.ComboBoxEdit();
            this.memoAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.chkDetayAktif = new DevExpress.XtraEditors.CheckEdit();
            this.lblAktifVersion = new DevExpress.XtraEditors.LabelControl();
            this.btnYeni = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnPasifle = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();

            // Tab 2 controls
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.gcVersionList = new DevExpress.XtraGrid.GridControl();
            this.gvVersionList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVersionNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDosyaAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDosyaBoyut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreatedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApprovedAt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblVersionCount = new DevExpress.XtraEditors.LabelControl();
            this.btnUploadVersion = new DevExpress.XtraEditors.SimpleButton();
            this.btnAktifEt = new DevExpress.XtraEditors.SimpleButton();
            this.btnArsivle = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadVersion = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcTemplateList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplateList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModul.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabTemplateDetay.SuspendLayout();
            this.tabVersions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateKod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDetayModul.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDetayAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVersionList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVersionList)).BeginInit();
            this.SuspendLayout();

            //
            // splitContainerControl1
            //
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainerControl1.SplitterPosition = 400;
            this.splitContainerControl1.TabIndex = 0;

            //
            // panelControl1 (Left Panel - Template List)
            //
            this.panelControl1.Controls.Add(this.gcTemplateList);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(400, 700);
            this.panelControl1.TabIndex = 0;

            //
            // panelControl2 (Filters)
            //
            this.panelControl2.Controls.Add(this.lblKayitSayisi);
            this.panelControl2.Controls.Add(this.btnYenile);
            this.panelControl2.Controls.Add(this.txtArama);
            this.panelControl2.Controls.Add(this.chkAktif);
            this.panelControl2.Controls.Add(this.cmbModul);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(396, 100);
            this.panelControl2.TabIndex = 0;

            //
            // labelControl1
            //
            this.labelControl1.Location = new System.Drawing.Point(10, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(30, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Modül:";

            //
            // cmbModul
            //
            this.cmbModul.Location = new System.Drawing.Point(60, 7);
            this.cmbModul.Name = "cmbModul";
            this.cmbModul.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbModul.Size = new System.Drawing.Size(150, 20);
            this.cmbModul.TabIndex = 1;

            //
            // chkAktif
            //
            this.chkAktif.Location = new System.Drawing.Point(220, 7);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Sadece Aktif";
            this.chkAktif.Size = new System.Drawing.Size(100, 19);
            this.chkAktif.TabIndex = 2;

            //
            // labelControl2
            //
            this.labelControl2.Location = new System.Drawing.Point(10, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(32, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Arama:";

            //
            // txtArama
            //
            this.txtArama.Location = new System.Drawing.Point(60, 35);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(260, 20);
            this.txtArama.TabIndex = 4;

            //
            // btnYenile
            //
            this.btnYenile.Location = new System.Drawing.Point(330, 33);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(60, 23);
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "Yenile";

            //
            // lblKayitSayisi
            //
            this.lblKayitSayisi.Location = new System.Drawing.Point(10, 70);
            this.lblKayitSayisi.Name = "lblKayitSayisi";
            this.lblKayitSayisi.Size = new System.Drawing.Size(70, 13);
            this.lblKayitSayisi.TabIndex = 6;
            this.lblKayitSayisi.Text = "Toplam 0 kayıt";

            //
            // gcTemplateList
            //
            this.gcTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTemplateList.Location = new System.Drawing.Point(2, 102);
            this.gcTemplateList.MainView = this.gvTemplateList;
            this.gcTemplateList.Name = "gcTemplateList";
            this.gcTemplateList.Size = new System.Drawing.Size(396, 596);
            this.gcTemplateList.TabIndex = 1;
            this.gcTemplateList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTemplateList});

            //
            // gvTemplateList
            //
            this.gvTemplateList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTemplateKod,
            this.colTemplateAdi,
            this.colModul,
            this.colAktif,
            this.colAktifVersionNo});
            this.gvTemplateList.GridControl = this.gcTemplateList;
            this.gvTemplateList.Name = "gvTemplateList";
            this.gvTemplateList.OptionsBehavior.Editable = false;
            this.gvTemplateList.OptionsView.ShowGroupPanel = false;

            //
            // Template Grid Columns
            //
            this.colTemplateKod.Caption = "Template Kodu";
            this.colTemplateKod.FieldName = "TemplateKod";
            this.colTemplateKod.Name = "colTemplateKod";
            this.colTemplateKod.Visible = true;
            this.colTemplateKod.VisibleIndex = 0;
            this.colTemplateKod.Width = 120;

            this.colTemplateAdi.Caption = "Template Adı";
            this.colTemplateAdi.FieldName = "TemplateAdi";
            this.colTemplateAdi.Name = "colTemplateAdi";
            this.colTemplateAdi.Visible = true;
            this.colTemplateAdi.VisibleIndex = 1;
            this.colTemplateAdi.Width = 200;

            this.colModul.Caption = "Modül";
            this.colModul.FieldName = "Modul";
            this.colModul.Name = "colModul";
            this.colModul.Visible = true;
            this.colModul.VisibleIndex = 2;
            this.colModul.Width = 100;

            this.colAktif.Caption = "Aktif";
            this.colAktif.FieldName = "Aktif";
            this.colAktif.Name = "colAktif";
            this.colAktif.Visible = true;
            this.colAktif.VisibleIndex = 3;
            this.colAktif.Width = 60;

            this.colAktifVersionNo.Caption = "Aktif Versiyon";
            this.colAktifVersionNo.FieldName = "AktifVersionNo";
            this.colAktifVersionNo.Name = "colAktifVersionNo";
            this.colAktifVersionNo.Visible = true;
            this.colAktifVersionNo.VisibleIndex = 4;
            this.colAktifVersionNo.Width = 100;

            //
            // xtraTabControl1 (Right Panel - Tabs)
            //
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabTemplateDetay;
            this.xtraTabControl1.Size = new System.Drawing.Size(795, 700);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTemplateDetay,
            this.tabVersions});

            //
            // tabTemplateDetay (Tab 1)
            //
            this.tabTemplateDetay.Controls.Add(this.panelControl3);
            this.tabTemplateDetay.Name = "tabTemplateDetay";
            this.tabTemplateDetay.Size = new System.Drawing.Size(793, 675);
            this.tabTemplateDetay.Text = "Template Bilgisi";

            //
            // panelControl3 (Tab 1 content)
            //
            this.panelControl3.Controls.Add(this.btnPasifle);
            this.panelControl3.Controls.Add(this.btnKaydet);
            this.panelControl3.Controls.Add(this.btnYeni);
            this.panelControl3.Controls.Add(this.lblAktifVersion);
            this.panelControl3.Controls.Add(this.chkDetayAktif);
            this.panelControl3.Controls.Add(this.memoAciklama);
            this.panelControl3.Controls.Add(this.cmbDetayModul);
            this.panelControl3.Controls.Add(this.txtTemplateAdi);
            this.panelControl3.Controls.Add(this.txtTemplateKod);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(793, 675);
            this.panelControl3.TabIndex = 0;

            //
            // Template Detail Fields
            //
            this.labelControl3.Location = new System.Drawing.Point(20, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(73, 13);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "Template Kodu:";

            this.txtTemplateKod.Location = new System.Drawing.Point(120, 17);
            this.txtTemplateKod.Name = "txtTemplateKod";
            this.txtTemplateKod.Size = new System.Drawing.Size(200, 20);
            this.txtTemplateKod.TabIndex = 1;

            this.labelControl4.Location = new System.Drawing.Point(20, 50);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(68, 13);
            this.labelControl4.TabIndex = 2;
            this.labelControl4.Text = "Template Adı:";

            this.txtTemplateAdi.Location = new System.Drawing.Point(120, 47);
            this.txtTemplateAdi.Name = "txtTemplateAdi";
            this.txtTemplateAdi.Size = new System.Drawing.Size(400, 20);
            this.txtTemplateAdi.TabIndex = 3;

            this.labelControl5.Location = new System.Drawing.Point(20, 80);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(30, 13);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "Modül:";

            this.cmbDetayModul.Location = new System.Drawing.Point(120, 77);
            this.cmbDetayModul.Name = "cmbDetayModul";
            this.cmbDetayModul.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDetayModul.Size = new System.Drawing.Size(200, 20);
            this.cmbDetayModul.TabIndex = 5;

            this.labelControl6.Location = new System.Drawing.Point(20, 110);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(44, 13);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "Açıklama:";

            this.memoAciklama.Location = new System.Drawing.Point(120, 107);
            this.memoAciklama.Name = "memoAciklama";
            this.memoAciklama.Size = new System.Drawing.Size(400, 80);
            this.memoAciklama.TabIndex = 7;

            this.chkDetayAktif.Location = new System.Drawing.Point(120, 200);
            this.chkDetayAktif.Name = "chkDetayAktif";
            this.chkDetayAktif.Properties.Caption = "Aktif";
            this.chkDetayAktif.Size = new System.Drawing.Size(75, 19);
            this.chkDetayAktif.TabIndex = 8;

            this.lblAktifVersion.Location = new System.Drawing.Point(20, 230);
            this.lblAktifVersion.Name = "lblAktifVersion";
            this.lblAktifVersion.Size = new System.Drawing.Size(75, 13);
            this.lblAktifVersion.TabIndex = 9;
            this.lblAktifVersion.Text = "Aktif Versiyon: -";

            this.btnYeni.Location = new System.Drawing.Point(20, 260);
            this.btnYeni.Name = "btnYeni";
            this.btnYeni.Size = new System.Drawing.Size(80, 30);
            this.btnYeni.TabIndex = 10;
            this.btnYeni.Text = "Yeni";

            this.btnKaydet.Location = new System.Drawing.Point(110, 260);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(80, 30);
            this.btnKaydet.TabIndex = 11;
            this.btnKaydet.Text = "Kaydet";

            this.btnPasifle.Location = new System.Drawing.Point(200, 260);
            this.btnPasifle.Name = "btnPasifle";
            this.btnPasifle.Size = new System.Drawing.Size(80, 30);
            this.btnPasifle.TabIndex = 12;
            this.btnPasifle.Text = "Pasifle";

            //
            // tabVersions (Tab 2)
            //
            this.tabVersions.Controls.Add(this.panelControl4);
            this.tabVersions.Name = "tabVersions";
            this.tabVersions.Size = new System.Drawing.Size(793, 675);
            this.tabVersions.Text = "Versiyonlar";

            //
            // panelControl4 (Tab 2 content)
            //
            this.panelControl4.Controls.Add(this.gcVersionList);
            this.panelControl4.Controls.Add(this.btnOpenFile);
            this.panelControl4.Controls.Add(this.btnDownloadVersion);
            this.panelControl4.Controls.Add(this.btnArsivle);
            this.panelControl4.Controls.Add(this.btnAktifEt);
            this.panelControl4.Controls.Add(this.btnUploadVersion);
            this.panelControl4.Controls.Add(this.lblVersionCount);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 0);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(793, 675);
            this.panelControl4.TabIndex = 0;

            //
            // lblVersionCount
            //
            this.lblVersionCount.Location = new System.Drawing.Point(10, 10);
            this.lblVersionCount.Name = "lblVersionCount";
            this.lblVersionCount.Size = new System.Drawing.Size(86, 13);
            this.lblVersionCount.TabIndex = 0;
            this.lblVersionCount.Text = "Toplam 0 versiyon";

            //
            // Version Buttons
            //
            this.btnUploadVersion.Location = new System.Drawing.Point(10, 35);
            this.btnUploadVersion.Name = "btnUploadVersion";
            this.btnUploadVersion.Size = new System.Drawing.Size(100, 30);
            this.btnUploadVersion.TabIndex = 1;
            this.btnUploadVersion.Text = "Yükle";

            this.btnAktifEt.Location = new System.Drawing.Point(120, 35);
            this.btnAktifEt.Name = "btnAktifEt";
            this.btnAktifEt.Size = new System.Drawing.Size(100, 30);
            this.btnAktifEt.TabIndex = 2;
            this.btnAktifEt.Text = "Aktif Et";

            this.btnArsivle.Location = new System.Drawing.Point(230, 35);
            this.btnArsivle.Name = "btnArsivle";
            this.btnArsivle.Size = new System.Drawing.Size(100, 30);
            this.btnArsivle.TabIndex = 3;
            this.btnArsivle.Text = "Arşivle";

            this.btnDownloadVersion.Location = new System.Drawing.Point(340, 35);
            this.btnDownloadVersion.Name = "btnDownloadVersion";
            this.btnDownloadVersion.Size = new System.Drawing.Size(100, 30);
            this.btnDownloadVersion.TabIndex = 4;
            this.btnDownloadVersion.Text = "İndir";

            this.btnOpenFile.Location = new System.Drawing.Point(450, 35);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(100, 30);
            this.btnOpenFile.TabIndex = 5;
            this.btnOpenFile.Text = "Aç";

            //
            // gcVersionList
            //
            this.gcVersionList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcVersionList.Location = new System.Drawing.Point(10, 75);
            this.gcVersionList.MainView = this.gvVersionList;
            this.gcVersionList.Name = "gcVersionList";
            this.gcVersionList.Size = new System.Drawing.Size(773, 590);
            this.gcVersionList.TabIndex = 6;
            this.gcVersionList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVersionList});

            //
            // gvVersionList
            //
            this.gvVersionList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVersionNo,
            this.colDurum,
            this.colDosyaAdi,
            this.colDosyaBoyut,
            this.colCreatedAt,
            this.colApprovedAt,
            this.colIsActive});
            this.gvVersionList.GridControl = this.gcVersionList;
            this.gvVersionList.Name = "gvVersionList";
            this.gvVersionList.OptionsBehavior.Editable = false;
            this.gvVersionList.OptionsView.ShowGroupPanel = false;

            //
            // Version Grid Columns
            //
            this.colVersionNo.Caption = "Versiyon";
            this.colVersionNo.FieldName = "VersionNo";
            this.colVersionNo.Name = "colVersionNo";
            this.colVersionNo.Visible = true;
            this.colVersionNo.VisibleIndex = 0;
            this.colVersionNo.Width = 80;

            this.colDurum.Caption = "Durum";
            this.colDurum.FieldName = "Durum";
            this.colDurum.Name = "colDurum";
            this.colDurum.Visible = true;
            this.colDurum.VisibleIndex = 1;
            this.colDurum.Width = 100;

            this.colDosyaAdi.Caption = "Dosya Adı";
            this.colDosyaAdi.FieldName = "DosyaAdi";
            this.colDosyaAdi.Name = "colDosyaAdi";
            this.colDosyaAdi.Visible = true;
            this.colDosyaAdi.VisibleIndex = 2;
            this.colDosyaAdi.Width = 200;

            this.colDosyaBoyut.Caption = "Boyut (Bytes)";
            this.colDosyaBoyut.FieldName = "DosyaBoyut";
            this.colDosyaBoyut.Name = "colDosyaBoyut";
            this.colDosyaBoyut.Visible = true;
            this.colDosyaBoyut.VisibleIndex = 3;
            this.colDosyaBoyut.Width = 100;

            this.colCreatedAt.Caption = "Oluşturma";
            this.colCreatedAt.FieldName = "CreatedAt";
            this.colCreatedAt.Name = "colCreatedAt";
            this.colCreatedAt.Visible = true;
            this.colCreatedAt.VisibleIndex = 4;
            this.colCreatedAt.Width = 120;

            this.colApprovedAt.Caption = "Onay";
            this.colApprovedAt.FieldName = "ApprovedAt";
            this.colApprovedAt.Name = "colApprovedAt";
            this.colApprovedAt.Visible = true;
            this.colApprovedAt.VisibleIndex = 5;
            this.colApprovedAt.Width = 120;

            this.colIsActive.Caption = "Aktif";
            this.colIsActive.FieldName = "IsActive";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.Visible = true;
            this.colIsActive.VisibleIndex = 6;
            this.colIsActive.Width = 60;

            //
            // UcTemplateMrk
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UcTemplateMrk";
            this.Size = new System.Drawing.Size(1200, 700);

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcTemplateList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTemplateList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbModul.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabTemplateDetay.ResumeLayout(false);
            this.tabVersions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateKod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDetayModul.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDetayAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcVersionList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVersionList)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gcTemplateList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTemplateList;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateKod;
        private DevExpress.XtraGrid.Columns.GridColumn colTemplateAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colModul;
        private DevExpress.XtraGrid.Columns.GridColumn colAktif;
        private DevExpress.XtraGrid.Columns.GridColumn colAktifVersionNo;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lblKayitSayisi;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.TextEdit txtArama;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.ComboBoxEdit cmbModul;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabTemplateDetay;
        private DevExpress.XtraTab.XtraTabPage tabVersions;

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.TextEdit txtTemplateKod;
        private DevExpress.XtraEditors.TextEdit txtTemplateAdi;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDetayModul;
        private DevExpress.XtraEditors.MemoEdit memoAciklama;
        private DevExpress.XtraEditors.CheckEdit chkDetayAktif;
        private DevExpress.XtraEditors.LabelControl lblAktifVersion;
        private DevExpress.XtraEditors.SimpleButton btnYeni;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnPasifle;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;

        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraGrid.GridControl gcVersionList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVersionList;
        private DevExpress.XtraGrid.Columns.GridColumn colVersionNo;
        private DevExpress.XtraGrid.Columns.GridColumn colDurum;
        private DevExpress.XtraGrid.Columns.GridColumn colDosyaAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colDosyaBoyut;
        private DevExpress.XtraGrid.Columns.GridColumn colCreatedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colApprovedAt;
        private DevExpress.XtraGrid.Columns.GridColumn colIsActive;
        private DevExpress.XtraEditors.LabelControl lblVersionCount;
        private DevExpress.XtraEditors.SimpleButton btnUploadVersion;
        private DevExpress.XtraEditors.SimpleButton btnAktifEt;
        private DevExpress.XtraEditors.SimpleButton btnArsivle;
        private DevExpress.XtraEditors.SimpleButton btnDownloadVersion;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
    }
}

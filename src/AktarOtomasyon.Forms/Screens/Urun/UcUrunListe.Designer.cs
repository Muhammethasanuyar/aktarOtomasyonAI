namespace AktarOtomasyon.Forms.Screens.Urun
{
    partial class UcUrunListe
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
            this.grpFiltre = new DevExpress.XtraEditors.GroupControl();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.btnAra = new DevExpress.XtraEditors.SimpleButton();
            this.chkAktif = new DevExpress.XtraEditors.CheckEdit();
            this.chkKritik = new DevExpress.XtraEditors.CheckEdit();
            this.lkpKategori = new DevExpress.XtraEditors.GridLookUpEdit();
            this.lkpKategoriView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtArama = new DevExpress.XtraEditors.TextEdit();
            this.lblKategori = new DevExpress.XtraEditors.LabelControl();
            this.lblArama = new DevExpress.XtraEditors.LabelControl();
            this.grpIslemler = new DevExpress.XtraEditors.GroupControl();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.btnKatalog = new DevExpress.XtraEditors.SimpleButton();
            this.btnPasifle = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnYeniUrun = new DevExpress.XtraEditors.SimpleButton();
            this.grdUrunler = new DevExpress.XtraGrid.GridControl();
            this.gvUrunler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colThumbnail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.colUrunKod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarkod = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKategoriAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBirimAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAktif = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).BeginInit();
            this.grpFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKritik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKategori.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKategoriView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).BeginInit();
            this.grpIslemler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUrunler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUrunler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            this.SuspendLayout();
            //
            // grpFiltre
            //
            this.grpFiltre.Controls.Add(this.btnTemizle);
            this.grpFiltre.Controls.Add(this.btnAra);
            this.grpFiltre.Controls.Add(this.chkAktif);
            this.grpFiltre.Controls.Add(this.chkKritik);
            this.grpFiltre.Controls.Add(this.lkpKategori);
            this.grpFiltre.Controls.Add(this.txtArama);
            this.grpFiltre.Controls.Add(this.lblKategori);
            this.grpFiltre.Controls.Add(this.lblArama);
            this.grpFiltre.Location = new System.Drawing.Point(20, 20);
            this.grpFiltre.Name = "grpFiltre";
            this.grpFiltre.Size = new System.Drawing.Size(730, 120);
            this.grpFiltre.TabIndex = 0;
            this.grpFiltre.Text = "Filtrele";
            //
            // lblArama
            //
            this.lblArama.Location = new System.Drawing.Point(15, 35);
            this.lblArama.Name = "lblArama";
            this.lblArama.Size = new System.Drawing.Size(30, 13);
            this.lblArama.TabIndex = 0;
            this.lblArama.Text = "Arama";
            //
            // txtArama
            //
            this.txtArama.Location = new System.Drawing.Point(15, 50);
            this.txtArama.Name = "txtArama";
            this.txtArama.Properties.NullText = "Ürün adı veya barkod...";
            this.txtArama.Size = new System.Drawing.Size(250, 20);
            this.txtArama.TabIndex = 1;
            //
            // lblKategori
            //
            this.lblKategori.Location = new System.Drawing.Point(280, 35);
            this.lblKategori.Name = "lblKategori";
            this.lblKategori.Size = new System.Drawing.Size(39, 13);
            this.lblKategori.TabIndex = 2;
            this.lblKategori.Text = "Kategori";
            //
            // lkpKategori
            //
            this.lkpKategori.Location = new System.Drawing.Point(280, 50);
            this.lkpKategori.Name = "lkpKategori";
            this.lkpKategori.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpKategori.Properties.DisplayMember = "KategoriAdi";
            this.lkpKategori.Properties.ValueMember = "KategoriId";
            this.lkpKategori.Properties.NullText = "Tüm Kategoriler";
            this.lkpKategori.Properties.PopupView = this.lkpKategoriView;
            this.lkpKategori.Size = new System.Drawing.Size(250, 20);
            this.lkpKategori.TabIndex = 3;
            // 
            // lkpKategoriView
            // 
            this.lkpKategoriView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.lkpKategoriView.Name = "lkpKategoriView";
            this.lkpKategoriView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.lkpKategoriView.OptionsView.ShowGroupPanel = false;
            //
            // chkAktif
            //
            this.chkAktif.EditValue = true;
            this.chkAktif.Location = new System.Drawing.Point(15, 85);
            this.chkAktif.Name = "chkAktif";
            this.chkAktif.Properties.Caption = "Sadece Aktif Ürünler";
            this.chkAktif.Size = new System.Drawing.Size(150, 20);
            this.chkAktif.TabIndex = 4;
            //
            // chkKritik
            //
            this.chkKritik.EditValue = false;
            this.chkKritik.Location = new System.Drawing.Point(180, 85);
            this.chkKritik.Name = "chkKritik";
            this.chkKritik.Properties.Caption = "Sadece Kritik Stok";
            this.chkKritik.Size = new System.Drawing.Size(150, 20);
            this.chkKritik.TabIndex = 5;
            //
            // btnAra
            //
            this.btnAra.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAra.Appearance.Options.UseBackColor = true;
            this.btnAra.Location = new System.Drawing.Point(550, 45);
            this.btnAra.Name = "btnAra";
            this.btnAra.Size = new System.Drawing.Size(80, 30);
            this.btnAra.TabIndex = 6;
            this.btnAra.Text = "Ara";
            this.btnAra.Click += new System.EventHandler(this.btnAra_Click);
            //
            // btnTemizle
            //
            this.btnTemizle.Location = new System.Drawing.Point(640, 45);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(80, 30);
            this.btnTemizle.TabIndex = 7;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);
            //
            // grpIslemler
            //
            this.grpIslemler.Controls.Add(this.btnKatalog);
            this.grpIslemler.Controls.Add(this.btnYenile);
            this.grpIslemler.Controls.Add(this.btnPasifle);
            this.grpIslemler.Controls.Add(this.btnDuzenle);
            this.grpIslemler.Controls.Add(this.btnYeniUrun);
            this.grpIslemler.Location = new System.Drawing.Point(20, 150);
            this.grpIslemler.Name = "grpIslemler";
            this.grpIslemler.Size = new System.Drawing.Size(730, 60);
            this.grpIslemler.TabIndex = 1;
            this.grpIslemler.Text = "İşlemler";
            //
            // btnYeniUrun
            //
            this.btnYeniUrun.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.btnYeniUrun.Appearance.Options.UseBackColor = true;
            this.btnYeniUrun.Location = new System.Drawing.Point(15, 25);
            this.btnYeniUrun.Name = "btnYeniUrun";
            this.btnYeniUrun.Size = new System.Drawing.Size(120, 25);
            this.btnYeniUrun.TabIndex = 0;
            this.btnYeniUrun.Text = "Yeni Ürün";
            this.btnYeniUrun.Click += new System.EventHandler(this.btnYeniUrun_Click);
            //
            // btnDuzenle
            //
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.Location = new System.Drawing.Point(145, 25);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(120, 25);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);
            //
            // btnPasifle
            //
            this.btnPasifle.Enabled = false;
            this.btnPasifle.Location = new System.Drawing.Point(275, 25);
            this.btnPasifle.Name = "btnPasifle";
            this.btnPasifle.Size = new System.Drawing.Size(120, 25);
            this.btnPasifle.TabIndex = 2;
            this.btnPasifle.Text = "Pasifle";
            this.btnPasifle.Click += new System.EventHandler(this.btnPasifle_Click);
            //
            // btnYenile
            //
            this.btnYenile.Location = new System.Drawing.Point(405, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(120, 25);
            this.btnYenile.TabIndex = 3;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // btnKatalog
            // 
            this.btnKatalog.Location = new System.Drawing.Point(535, 25);
            this.btnKatalog.Name = "btnKatalog";
            this.btnKatalog.Size = new System.Drawing.Size(120, 25);
            this.btnKatalog.TabIndex = 4;
            this.btnKatalog.Text = "Katalog Görünümü";
            this.btnKatalog.Click += new System.EventHandler(this.btnKatalog_Click);
            //
            // grdUrunler
            //
            this.grdUrunler.Location = new System.Drawing.Point(20, 220);
            this.grdUrunler.MainView = this.gvUrunler;
            this.grdUrunler.Name = "grdUrunler";
            this.grdUrunler.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.grdUrunler.Size = new System.Drawing.Size(730, 460);
            this.grdUrunler.TabIndex = 2;
            this.grdUrunler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUrunler});
            //
            // gvUrunler
            //
            this.gvUrunler.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colThumbnail,
            this.colUrunKod,
            this.colUrunAdi,
            this.colBarkod,
            this.colKategoriAdi,
            this.colBirimAdi,
            this.colAktif});
            this.gvUrunler.GridControl = this.grdUrunler;
            this.gvUrunler.Name = "gvUrunler";
            this.gvUrunler.OptionsBehavior.Editable = false;
            this.gvUrunler.OptionsBehavior.ReadOnly = true;
            this.gvUrunler.OptionsView.ShowGroupPanel = false;
            this.gvUrunler.RowHeight = 50;
            this.gvUrunler.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvUrunler_FocusedRowChanged);
            this.gvUrunler.DoubleClick += new System.EventHandler(this.gvUrunler_DoubleClick);
            this.gvUrunler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvUrunler_KeyDown);
            //
            // colThumbnail
            //
            this.colThumbnail.Caption = "";
            this.colThumbnail.ColumnEdit = this.repositoryItemPictureEdit1;
            this.colThumbnail.FieldName = "Thumbnail";
            this.colThumbnail.MinWidth = 50;
            this.colThumbnail.Name = "colThumbnail";
            this.colThumbnail.OptionsColumn.AllowEdit = false;
            this.colThumbnail.OptionsColumn.AllowFocus = false;
            this.colThumbnail.OptionsColumn.ReadOnly = true;
            this.colThumbnail.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colThumbnail.Visible = true;
            this.colThumbnail.VisibleIndex = 0;
            this.colThumbnail.Width = 50;
            //
            // repositoryItemPictureEdit1
            //
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.repositoryItemPictureEdit1.NullText = " ";
            //
            // colUrunKod
            //
            this.colUrunKod.Caption = "Ürün Kodu";
            this.colUrunKod.FieldName = "UrunKod";
            this.colUrunKod.Name = "colUrunKod";
            this.colUrunKod.Visible = true;
            this.colUrunKod.VisibleIndex = 1;
            this.colUrunKod.Width = 100;
            //
            // colUrunAdi
            //
            this.colUrunAdi.Caption = "Ürün Adı";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 2;
            this.colUrunAdi.Width = 250;
            //
            // colBarkod
            //
            this.colBarkod.Caption = "Barkod";
            this.colBarkod.FieldName = "Barkod";
            this.colBarkod.Name = "colBarkod";
            this.colBarkod.Visible = true;
            this.colBarkod.VisibleIndex = 3;
            this.colBarkod.Width = 120;
            //
            // colKategoriAdi
            //
            this.colKategoriAdi.Caption = "Kategori";
            this.colKategoriAdi.FieldName = "KategoriAdi";
            this.colKategoriAdi.Name = "colKategoriAdi";
            this.colKategoriAdi.Visible = true;
            this.colKategoriAdi.VisibleIndex = 4;
            this.colKategoriAdi.Width = 120;
            //
            // colBirimAdi
            //
            this.colBirimAdi.Caption = "Birim";
            this.colBirimAdi.FieldName = "BirimAdi";
            this.colBirimAdi.Name = "colBirimAdi";
            this.colBirimAdi.Visible = true;
            this.colBirimAdi.VisibleIndex = 5;
            this.colBirimAdi.Width = 80;
            //
            // colAktif
            //
            this.colAktif.Caption = "Aktif";
            this.colAktif.FieldName = "Aktif";
            this.colAktif.Name = "colAktif";
            this.colAktif.Visible = true;
            this.colAktif.VisibleIndex = 6;
            this.colAktif.Width = 60;
            //
            // UcUrunListe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdUrunler);
            this.Controls.Add(this.grpIslemler);
            this.Controls.Add(this.grpFiltre);
            this.Name = "UcUrunListe";
            this.Size = new System.Drawing.Size(770, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).EndInit();
            this.grpFiltre.ResumeLayout(false);
            this.grpFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAktif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkKritik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKategori.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpKategoriView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpIslemler)).EndInit();
            this.grpIslemler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdUrunler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUrunler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpFiltre;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraEditors.SimpleButton btnAra;
        private DevExpress.XtraEditors.CheckEdit chkAktif;
        private DevExpress.XtraEditors.CheckEdit chkKritik;
        private DevExpress.XtraEditors.GridLookUpEdit lkpKategori;
        private DevExpress.XtraGrid.Views.Grid.GridView lkpKategoriView;
        private DevExpress.XtraEditors.TextEdit txtArama;
        private DevExpress.XtraEditors.LabelControl lblKategori;
        private DevExpress.XtraEditors.LabelControl lblArama;
        private DevExpress.XtraEditors.GroupControl grpIslemler;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.SimpleButton btnKatalog;
        private DevExpress.XtraEditors.SimpleButton btnPasifle;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnYeniUrun;
        private DevExpress.XtraGrid.GridControl grdUrunler;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUrunler;
        private DevExpress.XtraGrid.Columns.GridColumn colThumbnail;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunKod;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colBarkod;
        private DevExpress.XtraGrid.Columns.GridColumn colKategoriAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colBirimAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colAktif;
    }
}

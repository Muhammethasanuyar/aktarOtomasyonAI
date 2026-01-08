namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class UcSiparisTaslak
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
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabGenel = new DevExpress.XtraTab.XtraTabPage();
            this.tabSatirlar = new DevExpress.XtraTab.XtraTabPage();
            this.grpGenel = new DevExpress.XtraEditors.GroupControl();
            this.lblSiparisNo = new DevExpress.XtraEditors.LabelControl();
            this.txtSiparisNo = new DevExpress.XtraEditors.TextEdit();
            this.lblTedarikci = new DevExpress.XtraEditors.LabelControl();
            this.lkpTedarikci = new DevExpress.XtraEditors.LookUpEdit();
            this.btnTedarikciEkle = new DevExpress.XtraEditors.SimpleButton();
            this.lblSiparisTarih = new DevExpress.XtraEditors.LabelControl();
            this.dateSiparisTarih = new DevExpress.XtraEditors.DateEdit();
            this.lblBeklenenTeslim = new DevExpress.XtraEditors.LabelControl();
            this.dateBeklenenTeslim = new DevExpress.XtraEditors.DateEdit();
            this.lblDurum = new DevExpress.XtraEditors.LabelControl();
            this.cmbDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.memoAciklama = new DevExpress.XtraEditors.MemoEdit();
            this.gridControlSatir = new DevExpress.XtraGrid.GridControl();
            this.gridViewSatir = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBirimFiyat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTutar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeslimMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpSatirAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnSatirEkle = new DevExpress.XtraEditors.SimpleButton();
            this.btnSatirDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnSatirSil = new DevExpress.XtraEditors.SimpleButton();
            this.lblKalemSayisiLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblKalemSayisi = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamTutarLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamTutar = new DevExpress.XtraEditors.LabelControl();
            this.lblUyariSayisi = new DevExpress.XtraEditors.LabelControl();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydetVeKapat = new DevExpress.XtraEditors.SimpleButton();
            this.btnVazgec = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabGenel.SuspendLayout();
            this.tabSatirlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpGenel)).BeginInit();
            this.grpGenel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiparisNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTedarikci.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSiparisTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSiparisTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBeklenenTeslim.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBeklenenTeslim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSatir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSatir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSatirAksiyonlar)).BeginInit();
            this.grpSatirAksiyonlar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabGenel;
            this.tabControl.Size = new System.Drawing.Size(800, 540);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGenel,
            this.tabSatirlar});

            // tabGenel
            this.tabGenel.Controls.Add(this.grpGenel);
            this.tabGenel.Name = "tabGenel";
            this.tabGenel.Size = new System.Drawing.Size(794, 512);
            this.tabGenel.Text = "Genel";

            // tabSatirlar
            this.tabSatirlar.Controls.Add(this.gridControlSatir);
            this.tabSatirlar.Controls.Add(this.grpSatirAksiyonlar);
            this.tabSatirlar.Name = "tabSatirlar";
            this.tabSatirlar.PageEnabled = false;
            this.tabSatirlar.Size = new System.Drawing.Size(794, 512);
            this.tabSatirlar.Text = "Satırlar";

            // grpGenel
            this.grpGenel.Controls.Add(this.lblSiparisNo);
            this.grpGenel.Controls.Add(this.txtSiparisNo);
            this.grpGenel.Controls.Add(this.lblTedarikci);
            this.grpGenel.Controls.Add(this.lkpTedarikci);
            this.grpGenel.Controls.Add(this.btnTedarikciEkle);
            this.grpGenel.Controls.Add(this.lblSiparisTarih);
            this.grpGenel.Controls.Add(this.dateSiparisTarih);
            this.grpGenel.Controls.Add(this.lblBeklenenTeslim);
            this.grpGenel.Controls.Add(this.dateBeklenenTeslim);
            this.grpGenel.Controls.Add(this.lblDurum);
            this.grpGenel.Controls.Add(this.cmbDurum);
            this.grpGenel.Controls.Add(this.lblAciklama);
            this.grpGenel.Controls.Add(this.memoAciklama);
            this.grpGenel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGenel.Location = new System.Drawing.Point(0, 0);
            this.grpGenel.Name = "grpGenel";
            this.grpGenel.Size = new System.Drawing.Size(794, 512);
            this.grpGenel.TabIndex = 0;
            this.grpGenel.Text = "Sipariş Bilgileri";

            // lblSiparisNo
            this.lblSiparisNo.Location = new System.Drawing.Point(20, 40);
            this.lblSiparisNo.Name = "lblSiparisNo";
            this.lblSiparisNo.Size = new System.Drawing.Size(60, 13);
            this.lblSiparisNo.TabIndex = 0;
            this.lblSiparisNo.Text = "Sipariş No:";

            // txtSiparisNo
            this.txtSiparisNo.Enabled = false;
            this.txtSiparisNo.Location = new System.Drawing.Point(120, 37);
            this.txtSiparisNo.Name = "txtSiparisNo";
            this.txtSiparisNo.Size = new System.Drawing.Size(200, 20);
            this.txtSiparisNo.TabIndex = 1;

            // lblTedarikci
            this.lblTedarikci.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblTedarikci.Appearance.Options.UseForeColor = true;
            this.lblTedarikci.Location = new System.Drawing.Point(20, 70);
            this.lblTedarikci.Name = "lblTedarikci";
            this.lblTedarikci.Size = new System.Drawing.Size(57, 13);
            this.lblTedarikci.TabIndex = 2;
            this.lblTedarikci.Text = "Tedarikçi: *";

            // lkpTedarikci
            this.lkpTedarikci.Location = new System.Drawing.Point(120, 67);
            this.lkpTedarikci.Name = "lkpTedarikci";
            this.lkpTedarikci.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTedarikci.Properties.NullText = "Seçiniz...";
            this.lkpTedarikci.Size = new System.Drawing.Size(270, 20);
            this.lkpTedarikci.TabIndex = 3;
            this.lkpTedarikci.EditValueChanged += new System.EventHandler(this.lkpTedarikci_EditValueChanged);

            // btnTedarikciEkle
            this.btnTedarikciEkle.Location = new System.Drawing.Point(396, 67);
            this.btnTedarikciEkle.Name = "btnTedarikciEkle";
            this.btnTedarikciEkle.Size = new System.Drawing.Size(24, 20);
            this.btnTedarikciEkle.TabIndex = 4;
            this.btnTedarikciEkle.Text = "+";
            this.btnTedarikciEkle.ToolTip = "Yeni Tedarikçi Oluştur";
            this.btnTedarikciEkle.Click += new System.EventHandler(this.btnTedarikciEkle_Click);

            // lblSiparisTarih
            this.lblSiparisTarih.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblSiparisTarih.Appearance.Options.UseForeColor = true;
            this.lblSiparisTarih.Location = new System.Drawing.Point(20, 100);
            this.lblSiparisTarih.Name = "lblSiparisTarih";
            this.lblSiparisTarih.Size = new System.Drawing.Size(80, 13);
            this.lblSiparisTarih.TabIndex = 4;
            this.lblSiparisTarih.Text = "Sipariş Tarihi: *";

            // dateSiparisTarih
            this.dateSiparisTarih.EditValue = null;
            this.dateSiparisTarih.Location = new System.Drawing.Point(120, 97);
            this.dateSiparisTarih.Name = "dateSiparisTarih";
            this.dateSiparisTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSiparisTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateSiparisTarih.Size = new System.Drawing.Size(150, 20);
            this.dateSiparisTarih.TabIndex = 5;
            this.dateSiparisTarih.EditValueChanged += new System.EventHandler(this.dateSiparisTarih_EditValueChanged);

            // lblBeklenenTeslim
            this.lblBeklenenTeslim.Location = new System.Drawing.Point(20, 130);
            this.lblBeklenenTeslim.Name = "lblBeklenenTeslim";
            this.lblBeklenenTeslim.Size = new System.Drawing.Size(90, 13);
            this.lblBeklenenTeslim.TabIndex = 6;
            this.lblBeklenenTeslim.Text = "Beklenen Teslim:";

            // dateBeklenenTeslim
            this.dateBeklenenTeslim.EditValue = null;
            this.dateBeklenenTeslim.Location = new System.Drawing.Point(120, 127);
            this.dateBeklenenTeslim.Name = "dateBeklenenTeslim";
            this.dateBeklenenTeslim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBeklenenTeslim.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBeklenenTeslim.Size = new System.Drawing.Size(150, 20);
            this.dateBeklenenTeslim.TabIndex = 7;
            this.dateBeklenenTeslim.EditValueChanged += new System.EventHandler(this.dateBeklenenTeslim_EditValueChanged);

            // lblDurum
            this.lblDurum.Location = new System.Drawing.Point(20, 160);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(35, 13);
            this.lblDurum.TabIndex = 8;
            this.lblDurum.Text = "Durum:";

            // cmbDurum
            this.cmbDurum.Enabled = false;
            this.cmbDurum.Location = new System.Drawing.Point(120, 157);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDurum.Size = new System.Drawing.Size(150, 20);
            this.cmbDurum.TabIndex = 9;

            // lblAciklama
            this.lblAciklama.Location = new System.Drawing.Point(20, 190);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(47, 13);
            this.lblAciklama.TabIndex = 10;
            this.lblAciklama.Text = "Açıklama:";

            // memoAciklama
            this.memoAciklama.Location = new System.Drawing.Point(120, 187);
            this.memoAciklama.Name = "memoAciklama";
            this.memoAciklama.Size = new System.Drawing.Size(400, 100);
            this.memoAciklama.TabIndex = 11;
            this.memoAciklama.TextChanged += new System.EventHandler(this.memoAciklama_TextChanged);

            // gridControlSatir
            this.gridControlSatir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSatir.Location = new System.Drawing.Point(0, 0);
            this.gridControlSatir.MainView = this.gridViewSatir;
            this.gridControlSatir.Name = "gridControlSatir";
            this.gridControlSatir.Size = new System.Drawing.Size(794, 432);
            this.gridControlSatir.TabIndex = 0;
            this.gridControlSatir.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSatir});

            // gridViewSatir
            this.gridViewSatir.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUrunAdi,
            this.colMiktar,
            this.colBirimFiyat,
            this.colTutar,
            this.colTeslimMiktar});
            this.gridViewSatir.GridControl = this.gridControlSatir;
            this.gridViewSatir.Name = "gridViewSatir";
            this.gridViewSatir.OptionsBehavior.Editable = false;
            this.gridViewSatir.OptionsView.ShowGroupPanel = false;
            this.gridViewSatir.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewSatir_FocusedRowChanged);
            this.gridViewSatir.DoubleClick += new System.EventHandler(this.gridViewSatir_DoubleClick);

            // colUrunAdi
            this.colUrunAdi.Caption = "Ürün";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 0;
            this.colUrunAdi.Width = 250;

            // colMiktar
            this.colMiktar.Caption = "Miktar";
            this.colMiktar.DisplayFormat.FormatString = "n2";
            this.colMiktar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMiktar.FieldName = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.Visible = true;
            this.colMiktar.VisibleIndex = 1;
            this.colMiktar.Width = 100;

            // colBirimFiyat
            this.colBirimFiyat.Caption = "Birim Fiyat";
            this.colBirimFiyat.DisplayFormat.FormatString = "c2";
            this.colBirimFiyat.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBirimFiyat.FieldName = "BirimFiyat";
            this.colBirimFiyat.Name = "colBirimFiyat";
            this.colBirimFiyat.Visible = true;
            this.colBirimFiyat.VisibleIndex = 2;
            this.colBirimFiyat.Width = 100;

            // colTutar
            this.colTutar.Caption = "Tutar";
            this.colTutar.DisplayFormat.FormatString = "c2";
            this.colTutar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTutar.FieldName = "Tutar";
            this.colTutar.Name = "colTutar";
            this.colTutar.Visible = true;
            this.colTutar.VisibleIndex = 3;
            this.colTutar.Width = 100;

            // colTeslimMiktar
            this.colTeslimMiktar.Caption = "Teslim Miktar";
            this.colTeslimMiktar.DisplayFormat.FormatString = "n2";
            this.colTeslimMiktar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTeslimMiktar.FieldName = "TeslimMiktar";
            this.colTeslimMiktar.Name = "colTeslimMiktar";
            this.colTeslimMiktar.Visible = true;
            this.colTeslimMiktar.VisibleIndex = 4;
            this.colTeslimMiktar.Width = 100;

            // grpSatirAksiyonlar
            this.grpSatirAksiyonlar.Controls.Add(this.btnSatirEkle);
            this.grpSatirAksiyonlar.Controls.Add(this.btnSatirDuzenle);
            this.grpSatirAksiyonlar.Controls.Add(this.btnSatirSil);
            this.grpSatirAksiyonlar.Controls.Add(this.lblKalemSayisiLabel);
            this.grpSatirAksiyonlar.Controls.Add(this.lblKalemSayisi);
            this.grpSatirAksiyonlar.Controls.Add(this.lblToplamTutarLabel);
            this.grpSatirAksiyonlar.Controls.Add(this.lblToplamTutar);
            this.grpSatirAksiyonlar.Controls.Add(this.lblUyariSayisi);
            this.grpSatirAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSatirAksiyonlar.Location = new System.Drawing.Point(0, 432);
            this.grpSatirAksiyonlar.Name = "grpSatirAksiyonlar";
            this.grpSatirAksiyonlar.Size = new System.Drawing.Size(794, 80);
            this.grpSatirAksiyonlar.TabIndex = 1;
            this.grpSatirAksiyonlar.Text = "İşlemler ve Özet";

            // btnSatirEkle
            this.btnSatirEkle.Location = new System.Drawing.Point(10, 30);
            this.btnSatirEkle.Name = "btnSatirEkle";
            this.btnSatirEkle.Size = new System.Drawing.Size(100, 23);
            this.btnSatirEkle.TabIndex = 0;
            this.btnSatirEkle.Text = "Satır Ekle";
            this.btnSatirEkle.Click += new System.EventHandler(this.btnSatirEkle_Click);

            // btnSatirDuzenle
            this.btnSatirDuzenle.Enabled = false;
            this.btnSatirDuzenle.Location = new System.Drawing.Point(120, 30);
            this.btnSatirDuzenle.Name = "btnSatirDuzenle";
            this.btnSatirDuzenle.Size = new System.Drawing.Size(100, 23);
            this.btnSatirDuzenle.TabIndex = 1;
            this.btnSatirDuzenle.Text = "Düzenle";
            this.btnSatirDuzenle.Click += new System.EventHandler(this.btnSatirDuzenle_Click);

            // btnSatirSil
            this.btnSatirSil.Enabled = false;
            this.btnSatirSil.Location = new System.Drawing.Point(230, 30);
            this.btnSatirSil.Name = "btnSatirSil";
            this.btnSatirSil.Size = new System.Drawing.Size(100, 23);
            this.btnSatirSil.TabIndex = 2;
            this.btnSatirSil.Text = "Sil";
            this.btnSatirSil.Click += new System.EventHandler(this.btnSatirSil_Click);

            // lblKalemSayisiLabel
            this.lblKalemSayisiLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKalemSayisiLabel.Appearance.Options.UseFont = true;
            this.lblKalemSayisiLabel.Location = new System.Drawing.Point(360, 35);
            this.lblKalemSayisiLabel.Name = "lblKalemSayisiLabel";
            this.lblKalemSayisiLabel.Size = new System.Drawing.Size(72, 15);
            this.lblKalemSayisiLabel.TabIndex = 3;
            this.lblKalemSayisiLabel.Text = "Toplam Kalem:";

            // lblKalemSayisi
            this.lblKalemSayisi.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKalemSayisi.Appearance.Options.UseFont = true;
            this.lblKalemSayisi.Location = new System.Drawing.Point(440, 35);
            this.lblKalemSayisi.Name = "lblKalemSayisi";
            this.lblKalemSayisi.Size = new System.Drawing.Size(37, 15);
            this.lblKalemSayisi.TabIndex = 4;
            this.lblKalemSayisi.Text = "0 kalem";

            // lblToplamTutarLabel
            this.lblToplamTutarLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblToplamTutarLabel.Appearance.Options.UseFont = true;
            this.lblToplamTutarLabel.Location = new System.Drawing.Point(530, 35);
            this.lblToplamTutarLabel.Name = "lblToplamTutarLabel";
            this.lblToplamTutarLabel.Size = new System.Drawing.Size(79, 17);
            this.lblToplamTutarLabel.TabIndex = 5;
            this.lblToplamTutarLabel.Text = "Toplam Tutar:";

            // lblToplamTutar
            this.lblToplamTutar.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblToplamTutar.Appearance.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblToplamTutar.Appearance.Options.UseFont = true;
            this.lblToplamTutar.Appearance.Options.UseForeColor = true;
            this.lblToplamTutar.Location = new System.Drawing.Point(620, 34);
            this.lblToplamTutar.Name = "lblToplamTutar";
            this.lblToplamTutar.Size = new System.Drawing.Size(39, 20);
            this.lblToplamTutar.TabIndex = 6;
            this.lblToplamTutar.Text = "0,00 ₺";

            // lblUyariSayisi
            this.lblUyariSayisi.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUyariSayisi.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 171, 0);
            this.lblUyariSayisi.Appearance.Options.UseFont = true;
            this.lblUyariSayisi.Appearance.Options.UseForeColor = true;
            this.lblUyariSayisi.Location = new System.Drawing.Point(360, 55);
            this.lblUyariSayisi.Name = "lblUyariSayisi";
            this.lblUyariSayisi.Size = new System.Drawing.Size(0, 15);
            this.lblUyariSayisi.TabIndex = 7;
            this.lblUyariSayisi.Text = "";

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnKaydet);
            this.grpAksiyonlar.Controls.Add(this.btnKaydetVeKapat);
            this.grpAksiyonlar.Controls.Add(this.btnVazgec);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 540);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(800, 60);
            this.grpAksiyonlar.TabIndex = 1;
            this.grpAksiyonlar.Text = "İşlemler";

            // btnKaydet
            this.btnKaydet.Location = new System.Drawing.Point(10, 25);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(100, 23);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);

            // btnKaydetVeKapat
            this.btnKaydetVeKapat.Location = new System.Drawing.Point(120, 25);
            this.btnKaydetVeKapat.Name = "btnKaydetVeKapat";
            this.btnKaydetVeKapat.Size = new System.Drawing.Size(130, 23);
            this.btnKaydetVeKapat.TabIndex = 1;
            this.btnKaydetVeKapat.Text = "Kaydet ve Kapat";
            this.btnKaydetVeKapat.Click += new System.EventHandler(this.btnKaydetVeKapat_Click);

            // btnVazgec
            this.btnVazgec.Location = new System.Drawing.Point(260, 25);
            this.btnVazgec.Name = "btnVazgec";
            this.btnVazgec.Size = new System.Drawing.Size(100, 23);
            this.btnVazgec.TabIndex = 2;
            this.btnVazgec.Text = "Vazgeç";
            this.btnVazgec.Click += new System.EventHandler(this.btnVazgec_Click);

            // UcSiparisTaslak
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcSiparisTaslak";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabGenel.ResumeLayout(false);
            this.tabSatirlar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpGenel)).EndInit();
            this.grpGenel.ResumeLayout(false);
            this.grpGenel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiparisNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTedarikci.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSiparisTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateSiparisTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBeklenenTeslim.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBeklenenTeslim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSatir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSatir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSatirAksiyonlar)).EndInit();
            this.grpSatirAksiyonlar.ResumeLayout(false);
            this.grpSatirAksiyonlar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabGenel;
        private DevExpress.XtraTab.XtraTabPage tabSatirlar;
        private DevExpress.XtraEditors.GroupControl grpGenel;
        private DevExpress.XtraEditors.LabelControl lblSiparisNo;
        private DevExpress.XtraEditors.TextEdit txtSiparisNo;
        private DevExpress.XtraEditors.LabelControl lblTedarikci;
        private DevExpress.XtraEditors.LookUpEdit lkpTedarikci;
        private DevExpress.XtraEditors.SimpleButton btnTedarikciEkle;
        private DevExpress.XtraEditors.LabelControl lblSiparisTarih;
        private DevExpress.XtraEditors.DateEdit dateSiparisTarih;
        private DevExpress.XtraEditors.LabelControl lblBeklenenTeslim;
        private DevExpress.XtraEditors.DateEdit dateBeklenenTeslim;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDurum;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.MemoEdit memoAciklama;
        private DevExpress.XtraGrid.GridControl gridControlSatir;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSatir;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colBirimFiyat;
        private DevExpress.XtraGrid.Columns.GridColumn colTutar;
        private DevExpress.XtraGrid.Columns.GridColumn colTeslimMiktar;
        private DevExpress.XtraEditors.GroupControl grpSatirAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnSatirEkle;
        private DevExpress.XtraEditors.SimpleButton btnSatirDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnSatirSil;
        private DevExpress.XtraEditors.LabelControl lblKalemSayisiLabel;
        private DevExpress.XtraEditors.LabelControl lblKalemSayisi;
        private DevExpress.XtraEditors.LabelControl lblToplamTutarLabel;
        private DevExpress.XtraEditors.LabelControl lblToplamTutar;
        private DevExpress.XtraEditors.LabelControl lblUyariSayisi;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnKaydetVeKapat;
        private DevExpress.XtraEditors.SimpleButton btnVazgec;
    }
}

namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class UcSiparisListe
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
            this.grpFiltre = new DevExpress.XtraEditors.GroupControl();
            this.lblDurum = new DevExpress.XtraEditors.LabelControl();
            this.cmbDurum = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblTedarikci = new DevExpress.XtraEditors.LabelControl();
            this.lkpTedarikci = new DevExpress.XtraEditors.LookUpEdit();
            this.lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this.dateBaslangic = new DevExpress.XtraEditors.DateEdit();
            this.lblBitis = new DevExpress.XtraEditors.LabelControl();
            this.dateBitis = new DevExpress.XtraEditors.DateEdit();
            this.btnFiltrele = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSiparisNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTedarikciAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSiparisTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBeklenenTeslimTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDurum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToplamTutar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnYeniSiparis = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnDurumGuncelle = new DevExpress.XtraEditors.SimpleButton();
            this.btnKritikStoktan = new DevExpress.XtraEditors.SimpleButton();
            this.btnTeslimAl = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).BeginInit();
            this.grpFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTedarikci.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // grpFiltre
            this.grpFiltre.Controls.Add(this.lblDurum);
            this.grpFiltre.Controls.Add(this.cmbDurum);
            this.grpFiltre.Controls.Add(this.lblTedarikci);
            this.grpFiltre.Controls.Add(this.lkpTedarikci);
            this.grpFiltre.Controls.Add(this.lblBaslangic);
            this.grpFiltre.Controls.Add(this.dateBaslangic);
            this.grpFiltre.Controls.Add(this.lblBitis);
            this.grpFiltre.Controls.Add(this.dateBitis);
            this.grpFiltre.Controls.Add(this.btnFiltrele);
            this.grpFiltre.Controls.Add(this.btnTemizle);
            this.grpFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltre.Location = new System.Drawing.Point(0, 0);
            this.grpFiltre.Name = "grpFiltre";
            this.grpFiltre.Size = new System.Drawing.Size(900, 100);
            this.grpFiltre.TabIndex = 0;
            this.grpFiltre.Text = "Filtre";

            // lblDurum
            this.lblDurum.Location = new System.Drawing.Point(10, 30);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(35, 13);
            this.lblDurum.TabIndex = 0;
            this.lblDurum.Text = "Durum:";

            // cmbDurum
            this.cmbDurum.Location = new System.Drawing.Point(60, 27);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDurum.Size = new System.Drawing.Size(120, 20);
            this.cmbDurum.TabIndex = 1;

            // lblTedarikci
            this.lblTedarikci.Location = new System.Drawing.Point(200, 30);
            this.lblTedarikci.Name = "lblTedarikci";
            this.lblTedarikci.Size = new System.Drawing.Size(50, 13);
            this.lblTedarikci.TabIndex = 2;
            this.lblTedarikci.Text = "Tedarikçi:";

            // lkpTedarikci
            this.lkpTedarikci.Location = new System.Drawing.Point(260, 27);
            this.lkpTedarikci.Name = "lkpTedarikci";
            this.lkpTedarikci.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpTedarikci.Properties.NullText = "Tümü";
            this.lkpTedarikci.Size = new System.Drawing.Size(200, 20);
            this.lkpTedarikci.TabIndex = 3;

            // lblBaslangic
            this.lblBaslangic.Location = new System.Drawing.Point(10, 60);
            this.lblBaslangic.Name = "lblBaslangic";
            this.lblBaslangic.Size = new System.Drawing.Size(80, 13);
            this.lblBaslangic.TabIndex = 4;
            this.lblBaslangic.Text = "Başlangıç Tarih:";

            // dateBaslangic
            this.dateBaslangic.EditValue = null;
            this.dateBaslangic.Location = new System.Drawing.Point(100, 57);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBaslangic.Size = new System.Drawing.Size(120, 20);
            this.dateBaslangic.TabIndex = 5;

            // lblBitis
            this.lblBitis.Location = new System.Drawing.Point(240, 60);
            this.lblBitis.Name = "lblBitis";
            this.lblBitis.Size = new System.Drawing.Size(60, 13);
            this.lblBitis.TabIndex = 6;
            this.lblBitis.Text = "Bitiş Tarih:";

            // dateBitis
            this.dateBitis.EditValue = null;
            this.dateBitis.Location = new System.Drawing.Point(310, 57);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBitis.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBitis.Size = new System.Drawing.Size(120, 20);
            this.dateBitis.TabIndex = 7;

            // btnFiltrele
            this.btnFiltrele.Location = new System.Drawing.Point(450, 55);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(80, 23);
            this.btnFiltrele.TabIndex = 8;
            this.btnFiltrele.Text = "Filtrele";
            this.btnFiltrele.Click += new System.EventHandler(this.btnFiltrele_Click);

            // btnTemizle
            this.btnTemizle.Location = new System.Drawing.Point(540, 55);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(80, 23);
            this.btnTemizle.TabIndex = 9;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // gridControl
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 100);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(900, 540);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});

            // gridView
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSiparisNo,
            this.colTedarikciAdi,
            this.colSiparisTarih,
            this.colBeklenenTeslimTarih,
            this.colDurum,
            this.colToplamTutar});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowAutoFilterRow = true;
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);

            // colSiparisNo
            this.colSiparisNo.Caption = "Sipariş No";
            this.colSiparisNo.FieldName = "SiparisNo";
            this.colSiparisNo.Name = "colSiparisNo";
            this.colSiparisNo.Visible = true;
            this.colSiparisNo.VisibleIndex = 0;
            this.colSiparisNo.Width = 100;

            // colTedarikciAdi
            this.colTedarikciAdi.Caption = "Tedarikçi";
            this.colTedarikciAdi.FieldName = "TedarikciAdi";
            this.colTedarikciAdi.Name = "colTedarikciAdi";
            this.colTedarikciAdi.Visible = true;
            this.colTedarikciAdi.VisibleIndex = 1;
            this.colTedarikciAdi.Width = 200;

            // colSiparisTarih
            this.colSiparisTarih.Caption = "Sipariş Tarihi";
            this.colSiparisTarih.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.colSiparisTarih.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSiparisTarih.FieldName = "SiparisTarih";
            this.colSiparisTarih.Name = "colSiparisTarih";
            this.colSiparisTarih.Visible = true;
            this.colSiparisTarih.VisibleIndex = 2;
            this.colSiparisTarih.Width = 100;

            // colBeklenenTeslimTarih
            this.colBeklenenTeslimTarih.Caption = "Beklenen Teslim";
            this.colBeklenenTeslimTarih.DisplayFormat.FormatString = "dd.MM.yyyy";
            this.colBeklenenTeslimTarih.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colBeklenenTeslimTarih.FieldName = "BeklenenTeslimTarih";
            this.colBeklenenTeslimTarih.Name = "colBeklenenTeslimTarih";
            this.colBeklenenTeslimTarih.Visible = true;
            this.colBeklenenTeslimTarih.VisibleIndex = 3;
            this.colBeklenenTeslimTarih.Width = 100;

            // colDurum
            this.colDurum.Caption = "Durum";
            this.colDurum.FieldName = "Durum";
            this.colDurum.Name = "colDurum";
            this.colDurum.Visible = true;
            this.colDurum.VisibleIndex = 4;
            this.colDurum.Width = 100;
            this.colDurum.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDurum.AppearanceCell.Options.UseFont = true;

            // colToplamTutar
            this.colToplamTutar.Caption = "Toplam Tutar";
            this.colToplamTutar.DisplayFormat.FormatString = "c2";
            this.colToplamTutar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colToplamTutar.FieldName = "ToplamTutar";
            this.colToplamTutar.Name = "colToplamTutar";
            this.colToplamTutar.Visible = true;
            this.colToplamTutar.VisibleIndex = 5;
            this.colToplamTutar.Width = 100;

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnYeniSiparis);
            this.grpAksiyonlar.Controls.Add(this.btnDuzenle);
            this.grpAksiyonlar.Controls.Add(this.btnDurumGuncelle);
            this.grpAksiyonlar.Controls.Add(this.btnKritikStoktan);
            this.grpAksiyonlar.Controls.Add(this.btnTeslimAl);
            this.grpAksiyonlar.Controls.Add(this.btnYenile);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 640);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(900, 60);
            this.grpAksiyonlar.TabIndex = 2;
            this.grpAksiyonlar.Text = "Aksiyonlar";

            // btnYeniSiparis
            this.btnYeniSiparis.Location = new System.Drawing.Point(10, 25);
            this.btnYeniSiparis.Name = "btnYeniSiparis";
            this.btnYeniSiparis.Size = new System.Drawing.Size(100, 23);
            this.btnYeniSiparis.TabIndex = 0;
            this.btnYeniSiparis.Text = "Yeni Sipariş";
            this.btnYeniSiparis.Click += new System.EventHandler(this.btnYeniSiparis_Click);

            // btnDuzenle
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.Location = new System.Drawing.Point(120, 25);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(100, 23);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);

            // btnDurumGuncelle
            this.btnDurumGuncelle.Enabled = false;
            this.btnDurumGuncelle.Location = new System.Drawing.Point(230, 25);
            this.btnDurumGuncelle.Name = "btnDurumGuncelle";
            this.btnDurumGuncelle.Size = new System.Drawing.Size(130, 23);
            this.btnDurumGuncelle.TabIndex = 2;
            this.btnDurumGuncelle.Text = "Durumu Güncelle";
            this.btnDurumGuncelle.Click += new System.EventHandler(this.btnDurumGuncelle_Click);

            // btnKritikStoktan
            this.btnKritikStoktan.Location = new System.Drawing.Point(370, 25);
            this.btnKritikStoktan.Name = "btnKritikStoktan";
            this.btnKritikStoktan.Size = new System.Drawing.Size(130, 23);
            this.btnKritikStoktan.TabIndex = 3;
            this.btnKritikStoktan.Text = "Kritik Stoktan Oluştur";
            this.btnKritikStoktan.Click += new System.EventHandler(this.btnKritikStoktan_Click);

            // btnTeslimAl
            this.btnTeslimAl.Enabled = false;
            this.btnTeslimAl.Location = new System.Drawing.Point(510, 25);
            this.btnTeslimAl.Name = "btnTeslimAl";
            this.btnTeslimAl.Size = new System.Drawing.Size(100, 23);
            this.btnTeslimAl.TabIndex = 4;
            this.btnTeslimAl.Text = "Teslim Al";
            this.btnTeslimAl.Click += new System.EventHandler(this.btnTeslimAl_Click);

            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(620, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(100, 23);
            this.btnYenile.TabIndex = 5;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // UcSiparisListe
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.grpFiltre);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcSiparisListe";
            this.Size = new System.Drawing.Size(900, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).EndInit();
            this.grpFiltre.ResumeLayout(false);
            this.grpFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTedarikci.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.GroupControl grpFiltre;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDurum;
        private DevExpress.XtraEditors.LabelControl lblTedarikci;
        private DevExpress.XtraEditors.LookUpEdit lkpTedarikci;
        private DevExpress.XtraEditors.LabelControl lblBaslangic;
        private DevExpress.XtraEditors.DateEdit dateBaslangic;
        private DevExpress.XtraEditors.LabelControl lblBitis;
        private DevExpress.XtraEditors.DateEdit dateBitis;
        private DevExpress.XtraEditors.SimpleButton btnFiltrele;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colSiparisNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTedarikciAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colSiparisTarih;
        private DevExpress.XtraGrid.Columns.GridColumn colBeklenenTeslimTarih;
        private DevExpress.XtraGrid.Columns.GridColumn colDurum;
        private DevExpress.XtraGrid.Columns.GridColumn colToplamTutar;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnYeniSiparis;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnDurumGuncelle;
        private DevExpress.XtraEditors.SimpleButton btnKritikStoktan;
        private DevExpress.XtraEditors.SimpleButton btnTeslimAl;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
    }
}

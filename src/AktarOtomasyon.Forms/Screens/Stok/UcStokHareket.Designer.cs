namespace AktarOtomasyon.Forms.Screens.Stok
{
    partial class UcStokHareket
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
            this.lblHareketTip = new DevExpress.XtraEditors.LabelControl();
            this.cmbHareketTip = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblBaslangic = new DevExpress.XtraEditors.LabelControl();
            this.dateBaslangic = new DevExpress.XtraEditors.DateEdit();
            this.lblBitis = new DevExpress.XtraEditors.LabelControl();
            this.dateBitis = new DevExpress.XtraEditors.DateEdit();
            this.lblArama = new DevExpress.XtraEditors.LabelControl();
            this.txtArama = new DevExpress.XtraEditors.TextEdit();
            this.btnFiltrele = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHareketTip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHareketTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAciklama = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnYeniHareket = new DevExpress.XtraEditors.SimpleButton();
            this.btnDuzenle = new DevExpress.XtraEditors.SimpleButton();
            this.btnSil = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).BeginInit();
            this.grpFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHareketTip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // grpFiltre
            this.grpFiltre.Controls.Add(this.lblHareketTip);
            this.grpFiltre.Controls.Add(this.cmbHareketTip);
            this.grpFiltre.Controls.Add(this.lblBaslangic);
            this.grpFiltre.Controls.Add(this.dateBaslangic);
            this.grpFiltre.Controls.Add(this.lblBitis);
            this.grpFiltre.Controls.Add(this.dateBitis);
            this.grpFiltre.Controls.Add(this.lblArama);
            this.grpFiltre.Controls.Add(this.txtArama);
            this.grpFiltre.Controls.Add(this.btnFiltrele);
            this.grpFiltre.Controls.Add(this.btnTemizle);
            this.grpFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltre.Location = new System.Drawing.Point(0, 0);
            this.grpFiltre.Name = "grpFiltre";
            this.grpFiltre.Size = new System.Drawing.Size(770, 80);
            this.grpFiltre.TabIndex = 0;
            this.grpFiltre.Text = "Filtre";

            // lblHareketTip
            this.lblHareketTip.Location = new System.Drawing.Point(10, 30);
            this.lblHareketTip.Name = "lblHareketTip";
            this.lblHareketTip.Size = new System.Drawing.Size(60, 13);
            this.lblHareketTip.TabIndex = 0;
            this.lblHareketTip.Text = "Hareket Tipi:";

            // cmbHareketTip
            this.cmbHareketTip.Location = new System.Drawing.Point(80, 27);
            this.cmbHareketTip.Name = "cmbHareketTip";
            this.cmbHareketTip.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbHareketTip.Size = new System.Drawing.Size(100, 20);
            this.cmbHareketTip.TabIndex = 1;

            // lblBaslangic
            this.lblBaslangic.Location = new System.Drawing.Point(200, 30);
            this.lblBaslangic.Name = "lblBaslangic";
            this.lblBaslangic.Size = new System.Drawing.Size(70, 13);
            this.lblBaslangic.TabIndex = 2;
            this.lblBaslangic.Text = "Başlangıç Tar:";

            // dateBaslangic
            this.dateBaslangic.EditValue = null;
            this.dateBaslangic.Location = new System.Drawing.Point(280, 27);
            this.dateBaslangic.Name = "dateBaslangic";
            this.dateBaslangic.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBaslangic.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBaslangic.Size = new System.Drawing.Size(100, 20);
            this.dateBaslangic.TabIndex = 3;

            // lblBitis
            this.lblBitis.Location = new System.Drawing.Point(400, 30);
            this.lblBitis.Name = "lblBitis";
            this.lblBitis.Size = new System.Drawing.Size(50, 13);
            this.lblBitis.TabIndex = 4;
            this.lblBitis.Text = "Bitiş Tar:";

            // dateBitis
            this.dateBitis.EditValue = null;
            this.dateBitis.Location = new System.Drawing.Point(460, 27);
            this.dateBitis.Name = "dateBitis";
            this.dateBitis.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBitis.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBitis.Size = new System.Drawing.Size(100, 20);
            this.dateBitis.TabIndex = 5;

            // lblArama
            this.lblArama.Location = new System.Drawing.Point(10, 56);
            this.lblArama.Name = "lblArama";
            this.lblArama.Size = new System.Drawing.Size(55, 13);
            this.lblArama.TabIndex = 6;
            this.lblArama.Text = "Ürün Ara:";

            // txtArama
            this.txtArama.Location = new System.Drawing.Point(80, 53);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(250, 20);
            this.txtArama.TabIndex = 7;

            // btnFiltrele
            this.btnFiltrele.Location = new System.Drawing.Point(580, 25);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(85, 23);
            this.btnFiltrele.TabIndex = 8;
            this.btnFiltrele.Text = "Filtrele";
            this.btnFiltrele.Click += new System.EventHandler(this.btnFiltrele_Click);

            // btnTemizle
            this.btnTemizle.Location = new System.Drawing.Point(670, 25);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(85, 23);
            this.btnTemizle.TabIndex = 9;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // gridControl
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 80);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(770, 560);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});

            // gridView
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHareketTip,
            this.colUrunAdi,
            this.colMiktar,
            this.colHareketTarih,
            this.colAciklama});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);

            // colHareketTip
            this.colHareketTip.Caption = "Hareket Tipi";
            this.colHareketTip.FieldName = "HareketTip";
            this.colHareketTip.Name = "colHareketTip";
            this.colHareketTip.Visible = true;
            this.colHareketTip.VisibleIndex = 0;
            this.colHareketTip.Width = 100;

            // colUrunAdi
            this.colUrunAdi.Caption = "Ürün Adı";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 1;
            this.colUrunAdi.Width = 250;

            // colMiktar
            this.colMiktar.Caption = "Miktar";
            this.colMiktar.DisplayFormat.FormatString = "N2";
            this.colMiktar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMiktar.FieldName = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.Visible = true;
            this.colMiktar.VisibleIndex = 2;
            this.colMiktar.Width = 100;

            // colHareketTarih
            this.colHareketTarih.Caption = "Tarih";
            this.colHareketTarih.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.colHareketTarih.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colHareketTarih.FieldName = "HareketTarih";
            this.colHareketTarih.Name = "colHareketTarih";
            this.colHareketTarih.Visible = true;
            this.colHareketTarih.VisibleIndex = 3;
            this.colHareketTarih.Width = 130;

            // colAciklama
            this.colAciklama.Caption = "Açıklama";
            this.colAciklama.FieldName = "Aciklama";
            this.colAciklama.Name = "colAciklama";
            this.colAciklama.Visible = true;
            this.colAciklama.VisibleIndex = 4;
            this.colAciklama.Width = 190;

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnYeniHareket);
            this.grpAksiyonlar.Controls.Add(this.btnDuzenle);
            this.grpAksiyonlar.Controls.Add(this.btnSil);
            this.grpAksiyonlar.Controls.Add(this.btnYenile);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 640);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(770, 60);
            this.grpAksiyonlar.TabIndex = 2;
            this.grpAksiyonlar.Text = "Aksiyonlar";

            // btnYeniHareket
            this.btnYeniHareket.Location = new System.Drawing.Point(10, 25);
            this.btnYeniHareket.Name = "btnYeniHareket";
            this.btnYeniHareket.Size = new System.Drawing.Size(120, 23);
            this.btnYeniHareket.TabIndex = 0;
            this.btnYeniHareket.Text = "Yeni Hareket";
            this.btnYeniHareket.Click += new System.EventHandler(this.btnYeniHareket_Click);

            // btnDuzenle
            this.btnDuzenle.Enabled = false;
            this.btnDuzenle.Location = new System.Drawing.Point(135, 25);
            this.btnDuzenle.Name = "btnDuzenle";
            this.btnDuzenle.Size = new System.Drawing.Size(85, 23);
            this.btnDuzenle.TabIndex = 1;
            this.btnDuzenle.Text = "Düzenle";
            this.btnDuzenle.Click += new System.EventHandler(this.btnDuzenle_Click);

            // btnSil
            this.btnSil.Enabled = false;
            this.btnSil.Location = new System.Drawing.Point(225, 25);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(85, 23);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);

            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(670, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(85, 23);
            this.btnYenile.TabIndex = 3;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // UcStokHareket
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.grpFiltre);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcStokHareket";
            this.Size = new System.Drawing.Size(770, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).EndInit();
            this.grpFiltre.ResumeLayout(false);
            this.grpFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbHareketTip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBaslangic.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBitis.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.GroupControl grpFiltre;
        private DevExpress.XtraEditors.LabelControl lblHareketTip;
        private DevExpress.XtraEditors.ComboBoxEdit cmbHareketTip;
        private DevExpress.XtraEditors.LabelControl lblBaslangic;
        private DevExpress.XtraEditors.DateEdit dateBaslangic;
        private DevExpress.XtraEditors.LabelControl lblBitis;
        private DevExpress.XtraEditors.DateEdit dateBitis;
        private DevExpress.XtraEditors.LabelControl lblArama;
        private DevExpress.XtraEditors.TextEdit txtArama;
        private DevExpress.XtraEditors.SimpleButton btnFiltrele;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colHareketTip;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colHareketTarih;
        private DevExpress.XtraGrid.Columns.GridColumn colAciklama;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnYeniHareket;
        private DevExpress.XtraEditors.SimpleButton btnDuzenle;
        private DevExpress.XtraEditors.SimpleButton btnSil;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
    }
}

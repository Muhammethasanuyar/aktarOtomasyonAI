namespace AktarOtomasyon.Forms.Screens.Bildirim
{
    partial class UcBildirimMrk
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
            this.btnFiltrele = new DevExpress.XtraEditors.SimpleButton();
            this.btnTemizle = new DevExpress.XtraEditors.SimpleButton();
            this.lblOkunmamisSayisi = new DevExpress.XtraEditors.LabelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBildirimTip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBaslik = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIcerik = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOlusturmaTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOkundu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnOkundu = new DevExpress.XtraEditors.SimpleButton();
            this.btnTumunuOkundu = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).BeginInit();
            this.grpFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // grpFiltre
            this.grpFiltre.Controls.Add(this.lblDurum);
            this.grpFiltre.Controls.Add(this.cmbDurum);
            this.grpFiltre.Controls.Add(this.btnFiltrele);
            this.grpFiltre.Controls.Add(this.btnTemizle);
            this.grpFiltre.Controls.Add(this.lblOkunmamisSayisi);
            this.grpFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltre.Location = new System.Drawing.Point(0, 0);
            this.grpFiltre.Name = "grpFiltre";
            this.grpFiltre.Size = new System.Drawing.Size(770, 80);
            this.grpFiltre.TabIndex = 0;
            this.grpFiltre.Text = "Filtre";

            // lblDurum
            this.lblDurum.Location = new System.Drawing.Point(10, 30);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(35, 13);
            this.lblDurum.TabIndex = 0;
            this.lblDurum.Text = "Durum:";

            // cmbDurum
            this.cmbDurum.Location = new System.Drawing.Point(10, 47);
            this.cmbDurum.Name = "cmbDurum";
            this.cmbDurum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDurum.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDurum.Size = new System.Drawing.Size(120, 20);
            this.cmbDurum.TabIndex = 1;

            // btnFiltrele
            this.btnFiltrele.Location = new System.Drawing.Point(140, 45);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(75, 23);
            this.btnFiltrele.TabIndex = 2;
            this.btnFiltrele.Text = "Filtrele";
            this.btnFiltrele.Click += new System.EventHandler(this.btnFiltrele_Click);

            // btnTemizle
            this.btnTemizle.Location = new System.Drawing.Point(220, 45);
            this.btnTemizle.Name = "btnTemizle";
            this.btnTemizle.Size = new System.Drawing.Size(75, 23);
            this.btnTemizle.TabIndex = 3;
            this.btnTemizle.Text = "Temizle";
            this.btnTemizle.Click += new System.EventHandler(this.btnTemizle_Click);

            // lblOkunmamisSayisi
            this.lblOkunmamisSayisi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblOkunmamisSayisi.Appearance.Options.UseFont = true;
            this.lblOkunmamisSayisi.Location = new System.Drawing.Point(320, 50);
            this.lblOkunmamisSayisi.Name = "lblOkunmamisSayisi";
            this.lblOkunmamisSayisi.Size = new System.Drawing.Size(85, 14);
            this.lblOkunmamisSayisi.TabIndex = 4;
            this.lblOkunmamisSayisi.Text = "Okunmamış: 0";

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
            this.colBildirimTip,
            this.colBaslik,
            this.colIcerik,
            this.colOlusturmaTarih,
            this.colOkundu});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);

            // colBildirimTip
            this.colBildirimTip.Caption = "Bildirim Tipi";
            this.colBildirimTip.FieldName = "BildirimTip";
            this.colBildirimTip.Name = "colBildirimTip";
            this.colBildirimTip.Visible = true;
            this.colBildirimTip.VisibleIndex = 0;
            this.colBildirimTip.Width = 120;

            // colBaslik
            this.colBaslik.Caption = "Başlık";
            this.colBaslik.FieldName = "Baslik";
            this.colBaslik.Name = "colBaslik";
            this.colBaslik.Visible = true;
            this.colBaslik.VisibleIndex = 1;
            this.colBaslik.Width = 200;

            // colIcerik
            this.colIcerik.Caption = "İçerik";
            this.colIcerik.FieldName = "Icerik";
            this.colIcerik.Name = "colIcerik";
            this.colIcerik.Visible = true;
            this.colIcerik.VisibleIndex = 2;
            this.colIcerik.Width = 280;

            // colOlusturmaTarih
            this.colOlusturmaTarih.Caption = "Tarih";
            this.colOlusturmaTarih.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
            this.colOlusturmaTarih.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOlusturmaTarih.FieldName = "OlusturmaTarih";
            this.colOlusturmaTarih.Name = "colOlusturmaTarih";
            this.colOlusturmaTarih.Visible = true;
            this.colOlusturmaTarih.VisibleIndex = 3;
            this.colOlusturmaTarih.Width = 120;

            // colOkundu
            this.colOkundu.Caption = "Okundu";
            this.colOkundu.FieldName = "Okundu";
            this.colOkundu.Name = "colOkundu";
            this.colOkundu.Visible = true;
            this.colOkundu.VisibleIndex = 4;
            this.colOkundu.Width = 70;

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnOkundu);
            this.grpAksiyonlar.Controls.Add(this.btnTumunuOkundu);
            this.grpAksiyonlar.Controls.Add(this.btnYenile);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 640);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(770, 60);
            this.grpAksiyonlar.TabIndex = 2;
            this.grpAksiyonlar.Text = "Aksiyonlar";

            // btnOkundu
            this.btnOkundu.Enabled = false;
            this.btnOkundu.Location = new System.Drawing.Point(10, 25);
            this.btnOkundu.Name = "btnOkundu";
            this.btnOkundu.Size = new System.Drawing.Size(120, 23);
            this.btnOkundu.TabIndex = 0;
            this.btnOkundu.Text = "Okundu İşaretle";
            this.btnOkundu.Click += new System.EventHandler(this.btnOkundu_Click);

            // btnTumunuOkundu
            this.btnTumunuOkundu.Location = new System.Drawing.Point(135, 25);
            this.btnTumunuOkundu.Name = "btnTumunuOkundu";
            this.btnTumunuOkundu.Size = new System.Drawing.Size(150, 23);
            this.btnTumunuOkundu.TabIndex = 1;
            this.btnTumunuOkundu.Text = "Tümünü Okundu Yap";
            this.btnTumunuOkundu.Click += new System.EventHandler(this.btnTumunuOkundu_Click);

            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(670, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(85, 23);
            this.btnYenile.TabIndex = 2;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // UcBildirimMrk
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.grpFiltre);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcBildirimMrk";
            this.Size = new System.Drawing.Size(770, 700);
            ((System.ComponentModel.ISupportInitialize)(this.grpFiltre)).EndInit();
            this.grpFiltre.ResumeLayout(false);
            this.grpFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.GroupControl grpFiltre;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDurum;
        private DevExpress.XtraEditors.SimpleButton btnFiltrele;
        private DevExpress.XtraEditors.SimpleButton btnTemizle;
        private DevExpress.XtraEditors.LabelControl lblOkunmamisSayisi;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colBildirimTip;
        private DevExpress.XtraGrid.Columns.GridColumn colBaslik;
        private DevExpress.XtraGrid.Columns.GridColumn colIcerik;
        private DevExpress.XtraGrid.Columns.GridColumn colOlusturmaTarih;
        private DevExpress.XtraGrid.Columns.GridColumn colOkundu;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnOkundu;
        private DevExpress.XtraEditors.SimpleButton btnTumunuOkundu;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
    }
}

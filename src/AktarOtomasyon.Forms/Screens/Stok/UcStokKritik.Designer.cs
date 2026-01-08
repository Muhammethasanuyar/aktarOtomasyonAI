namespace AktarOtomasyon.Forms.Screens.Stok
{
    partial class UcStokKritik
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblUyari = new DevExpress.XtraEditors.LabelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMevcutStok = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinStok = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnStokGor = new DevExpress.XtraEditors.SimpleButton();
            this.btnSiparisOner = new DevExpress.XtraEditors.SimpleButton();
            this.btnSiparisTaslak = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // pnlTop
            this.pnlTop.Controls.Add(this.lblUyari);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(770, 40);
            this.pnlTop.TabIndex = 0;

            // lblUyari
            this.lblUyari.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblUyari.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblUyari.Appearance.Options.UseFont = true;
            this.lblUyari.Appearance.Options.UseForeColor = true;
            this.lblUyari.Location = new System.Drawing.Point(10, 12);
            this.lblUyari.Name = "lblUyari";
            this.lblUyari.Size = new System.Drawing.Size(200, 16);
            this.lblUyari.TabIndex = 0;
            this.lblUyari.Text = "Kritik seviyedeki ürün sayısı: 0";

            // gridControl
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 40);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(770, 600);
            this.gridControl.TabIndex = 1;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});

            // gridView
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUrunAdi,
            this.colMevcutStok,
            this.colMinStok});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);

            // colUrunAdi
            this.colUrunAdi.Caption = "Ürün Adı";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 0;
            this.colUrunAdi.Width = 400;

            // colMevcutStok
            this.colMevcutStok.Caption = "Mevcut Stok";
            this.colMevcutStok.DisplayFormat.FormatString = "N2";
            this.colMevcutStok.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMevcutStok.FieldName = "MevcutStok";
            this.colMevcutStok.Name = "colMevcutStok";
            this.colMevcutStok.Visible = true;
            this.colMevcutStok.VisibleIndex = 1;
            this.colMevcutStok.Width = 150;

            // colMinStok
            this.colMinStok.Caption = "Kritik Seviye";
            this.colMinStok.DisplayFormat.FormatString = "N2";
            this.colMinStok.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMinStok.FieldName = "MinStok";
            this.colMinStok.Name = "colMinStok";
            this.colMinStok.Visible = true;
            this.colMinStok.VisibleIndex = 2;
            this.colMinStok.Width = 150;

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnStokGor);
            this.grpAksiyonlar.Controls.Add(this.btnSiparisOner);
            this.grpAksiyonlar.Controls.Add(this.btnSiparisTaslak);
            this.grpAksiyonlar.Controls.Add(this.btnYenile);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 640);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(770, 60);
            this.grpAksiyonlar.TabIndex = 2;
            this.grpAksiyonlar.Text = "Aksiyonlar";

            // btnStokGor
            this.btnStokGor.Enabled = false;
            this.btnStokGor.Location = new System.Drawing.Point(10, 25);
            this.btnStokGor.Name = "btnStokGor";
            this.btnStokGor.Size = new System.Drawing.Size(100, 23);
            this.btnStokGor.TabIndex = 0;
            this.btnStokGor.Text = "Stok Gör";
            this.btnStokGor.Click += new System.EventHandler(this.btnStokGor_Click);

            // btnSiparisOner
            this.btnSiparisOner.Enabled = false;
            this.btnSiparisOner.Location = new System.Drawing.Point(115, 25);
            this.btnSiparisOner.Name = "btnSiparisOner";
            this.btnSiparisOner.Size = new System.Drawing.Size(120, 23);
            this.btnSiparisOner.TabIndex = 1;
            this.btnSiparisOner.Text = "Sipariş Öner";
            this.btnSiparisOner.Click += new System.EventHandler(this.btnSiparisOner_Click);

            // btnSiparisTaslak
            this.btnSiparisTaslak.Enabled = false;
            this.btnSiparisTaslak.Location = new System.Drawing.Point(240, 25);
            this.btnSiparisTaslak.Name = "btnSiparisTaslak";
            this.btnSiparisTaslak.Size = new System.Drawing.Size(150, 23);
            this.btnSiparisTaslak.TabIndex = 2;
            this.btnSiparisTaslak.Text = "Sipariş Taslağı Oluştur";
            this.btnSiparisTaslak.Click += new System.EventHandler(this.btnSiparisTaslak_Click);

            // btnYenile
            this.btnYenile.Location = new System.Drawing.Point(670, 25);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(85, 23);
            this.btnYenile.TabIndex = 3;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);

            // UcStokKritik
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcStokKritik";
            this.Size = new System.Drawing.Size(770, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.LabelControl lblUyari;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colMevcutStok;
        private DevExpress.XtraGrid.Columns.GridColumn colMinStok;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnStokGor;
        private DevExpress.XtraEditors.SimpleButton btnSiparisOner;
        private DevExpress.XtraEditors.SimpleButton btnSiparisTaslak;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
    }
}

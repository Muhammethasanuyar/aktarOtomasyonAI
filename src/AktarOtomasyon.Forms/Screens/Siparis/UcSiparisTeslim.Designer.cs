namespace AktarOtomasyon.Forms.Screens.Siparis
{
    partial class UcSiparisTeslim
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
            this.grpSiparisBilgi = new DevExpress.XtraEditors.GroupControl();
            this.lblSiparisNo = new DevExpress.XtraEditors.LabelControl();
            this.txtSiparisNo = new DevExpress.XtraEditors.TextEdit();
            this.lblTedarikci = new DevExpress.XtraEditors.LabelControl();
            this.txtTedarikci = new DevExpress.XtraEditors.TextEdit();
            this.lblSiparisTarih = new DevExpress.XtraEditors.LabelControl();
            this.dateSiparisTarih = new DevExpress.XtraEditors.LabelControl();
            this.lblDurum = new DevExpress.XtraEditors.LabelControl();
            this.txtDurum = new DevExpress.XtraEditors.TextEdit();
            this.grpTeslim = new DevExpress.XtraEditors.GroupControl();
            this.lblTeslimTarih = new DevExpress.XtraEditors.LabelControl();
            this.dateTeslimTarih = new DevExpress.XtraEditors.DateEdit();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTeslimMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKalan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpAksiyonlar = new DevExpress.XtraEditors.GroupControl();
            this.btnTeslimAl = new DevExpress.XtraEditors.SimpleButton();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpSiparisBilgi)).BeginInit();
            this.grpSiparisBilgi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiparisNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikci.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDurum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTeslim)).BeginInit();
            this.grpTeslim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTeslimTarih.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTeslimTarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).BeginInit();
            this.grpAksiyonlar.SuspendLayout();
            this.SuspendLayout();

            // grpSiparisBilgi
            this.grpSiparisBilgi.Controls.Add(this.lblSiparisNo);
            this.grpSiparisBilgi.Controls.Add(this.txtSiparisNo);
            this.grpSiparisBilgi.Controls.Add(this.lblTedarikci);
            this.grpSiparisBilgi.Controls.Add(this.txtTedarikci);
            this.grpSiparisBilgi.Controls.Add(this.lblSiparisTarih);
            this.grpSiparisBilgi.Controls.Add(this.dateSiparisTarih);
            this.grpSiparisBilgi.Controls.Add(this.lblDurum);
            this.grpSiparisBilgi.Controls.Add(this.txtDurum);
            this.grpSiparisBilgi.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSiparisBilgi.Location = new System.Drawing.Point(0, 0);
            this.grpSiparisBilgi.Name = "grpSiparisBilgi";
            this.grpSiparisBilgi.Size = new System.Drawing.Size(700, 120);
            this.grpSiparisBilgi.TabIndex = 0;
            this.grpSiparisBilgi.Text = "Sipariş Bilgileri";

            // lblSiparisNo
            this.lblSiparisNo.Location = new System.Drawing.Point(20, 30);
            this.lblSiparisNo.Name = "lblSiparisNo";
            this.lblSiparisNo.Size = new System.Drawing.Size(60, 13);
            this.lblSiparisNo.TabIndex = 0;
            this.lblSiparisNo.Text = "Sipariş No:";

            // txtSiparisNo
            this.txtSiparisNo.Enabled = false;
            this.txtSiparisNo.Location = new System.Drawing.Point(100, 27);
            this.txtSiparisNo.Name = "txtSiparisNo";
            this.txtSiparisNo.Size = new System.Drawing.Size(150, 20);
            this.txtSiparisNo.TabIndex = 1;

            // lblTedarikci
            this.lblTedarikci.Location = new System.Drawing.Point(20, 60);
            this.lblTedarikci.Name = "lblTedarikci";
            this.lblTedarikci.Size = new System.Drawing.Size(50, 13);
            this.lblTedarikci.TabIndex = 2;
            this.lblTedarikci.Text = "Tedarikçi:";

            // txtTedarikci
            this.txtTedarikci.Enabled = false;
            this.txtTedarikci.Location = new System.Drawing.Point(100, 57);
            this.txtTedarikci.Name = "txtTedarikci";
            this.txtTedarikci.Size = new System.Drawing.Size(250, 20);
            this.txtTedarikci.TabIndex = 3;

            // lblSiparisTarih
            this.lblSiparisTarih.Location = new System.Drawing.Point(400, 30);
            this.lblSiparisTarih.Name = "lblSiparisTarih";
            this.lblSiparisTarih.Size = new System.Drawing.Size(75, 13);
            this.lblSiparisTarih.TabIndex = 4;
            this.lblSiparisTarih.Text = "Sipariş Tarihi:";

            // dateSiparisTarih
            this.dateSiparisTarih.Location = new System.Drawing.Point(490, 30);
            this.dateSiparisTarih.Name = "dateSiparisTarih";
            this.dateSiparisTarih.Size = new System.Drawing.Size(100, 13);
            this.dateSiparisTarih.TabIndex = 5;
            this.dateSiparisTarih.Text = "-";

            // lblDurum
            this.lblDurum.Location = new System.Drawing.Point(400, 60);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(35, 13);
            this.lblDurum.TabIndex = 6;
            this.lblDurum.Text = "Durum:";

            // txtDurum
            this.txtDurum.Enabled = false;
            this.txtDurum.Location = new System.Drawing.Point(490, 57);
            this.txtDurum.Name = "txtDurum";
            this.txtDurum.Size = new System.Drawing.Size(120, 20);
            this.txtDurum.TabIndex = 7;

            // grpTeslim
            this.grpTeslim.Controls.Add(this.lblTeslimTarih);
            this.grpTeslim.Controls.Add(this.dateTeslimTarih);
            this.grpTeslim.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTeslim.Location = new System.Drawing.Point(0, 120);
            this.grpTeslim.Name = "grpTeslim";
            this.grpTeslim.Size = new System.Drawing.Size(700, 70);
            this.grpTeslim.TabIndex = 1;
            this.grpTeslim.Text = "Teslim Bilgileri";

            // lblTeslimTarih
            this.lblTeslimTarih.Location = new System.Drawing.Point(20, 35);
            this.lblTeslimTarih.Name = "lblTeslimTarih";
            this.lblTeslimTarih.Size = new System.Drawing.Size(75, 13);
            this.lblTeslimTarih.TabIndex = 0;
            this.lblTeslimTarih.Text = "Teslim Tarihi:";

            // dateTeslimTarih
            this.dateTeslimTarih.EditValue = null;
            this.dateTeslimTarih.Location = new System.Drawing.Point(100, 32);
            this.dateTeslimTarih.Name = "dateTeslimTarih";
            this.dateTeslimTarih.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTeslimTarih.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTeslimTarih.Size = new System.Drawing.Size(150, 20);
            this.dateTeslimTarih.TabIndex = 1;

            // gridControl
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 190);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(700, 250);
            this.gridControl.TabIndex = 2;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});

            // gridView
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUrunAdi,
            this.colMiktar,
            this.colTeslimMiktar,
            this.colKalan});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;

            // colUrunAdi
            this.colUrunAdi.Caption = "Ürün";
            this.colUrunAdi.FieldName = "UrunAdi";
            this.colUrunAdi.Name = "colUrunAdi";
            this.colUrunAdi.Visible = true;
            this.colUrunAdi.VisibleIndex = 0;
            this.colUrunAdi.Width = 250;

            // colMiktar
            this.colMiktar.Caption = "Sipariş Miktarı";
            this.colMiktar.DisplayFormat.FormatString = "n2";
            this.colMiktar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMiktar.FieldName = "Miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.Visible = true;
            this.colMiktar.VisibleIndex = 1;
            this.colMiktar.Width = 100;

            // colTeslimMiktar
            this.colTeslimMiktar.Caption = "Teslim Edilen";
            this.colTeslimMiktar.DisplayFormat.FormatString = "n2";
            this.colTeslimMiktar.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTeslimMiktar.FieldName = "TeslimMiktar";
            this.colTeslimMiktar.Name = "colTeslimMiktar";
            this.colTeslimMiktar.Visible = true;
            this.colTeslimMiktar.VisibleIndex = 2;
            this.colTeslimMiktar.Width = 100;

            // colKalan
            this.colKalan.Caption = "Kalan";
            this.colKalan.DisplayFormat.FormatString = "n2";
            this.colKalan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colKalan.FieldName = "Kalan";
            this.colKalan.Name = "colKalan";
            this.colKalan.UnboundExpression = "[Miktar] - [TeslimMiktar]";
            this.colKalan.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.colKalan.Visible = true;
            this.colKalan.VisibleIndex = 3;
            this.colKalan.Width = 100;

            // grpAksiyonlar
            this.grpAksiyonlar.Controls.Add(this.btnTeslimAl);
            this.grpAksiyonlar.Controls.Add(this.btnIptal);
            this.grpAksiyonlar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAksiyonlar.Location = new System.Drawing.Point(0, 440);
            this.grpAksiyonlar.Name = "grpAksiyonlar";
            this.grpAksiyonlar.Size = new System.Drawing.Size(700, 60);
            this.grpAksiyonlar.TabIndex = 3;
            this.grpAksiyonlar.Text = "İşlemler";

            // btnTeslimAl
            this.btnTeslimAl.Location = new System.Drawing.Point(10, 25);
            this.btnTeslimAl.Name = "btnTeslimAl";
            this.btnTeslimAl.Size = new System.Drawing.Size(100, 23);
            this.btnTeslimAl.TabIndex = 0;
            this.btnTeslimAl.Text = "Teslim Al";
            this.btnTeslimAl.Click += new System.EventHandler(this.btnTeslimAl_Click);

            // btnIptal
            this.btnIptal.Location = new System.Drawing.Point(120, 25);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(100, 23);
            this.btnIptal.TabIndex = 1;
            this.btnIptal.Text = "İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);

            // UcSiparisTeslim
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.grpTeslim);
            this.Controls.Add(this.grpSiparisBilgi);
            this.Controls.Add(this.grpAksiyonlar);
            this.Name = "UcSiparisTeslim";
            this.Size = new System.Drawing.Size(700, 500);
            ((System.ComponentModel.ISupportInitialize)(this.grpSiparisBilgi)).EndInit();
            this.grpSiparisBilgi.ResumeLayout(false);
            this.grpSiparisBilgi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiparisNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTedarikci.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDurum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpTeslim)).EndInit();
            this.grpTeslim.ResumeLayout(false);
            this.grpTeslim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTeslimTarih.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTeslimTarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAksiyonlar)).EndInit();
            this.grpAksiyonlar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.GroupControl grpSiparisBilgi;
        private DevExpress.XtraEditors.LabelControl lblSiparisNo;
        private DevExpress.XtraEditors.TextEdit txtSiparisNo;
        private DevExpress.XtraEditors.LabelControl lblTedarikci;
        private DevExpress.XtraEditors.TextEdit txtTedarikci;
        private DevExpress.XtraEditors.LabelControl lblSiparisTarih;
        private DevExpress.XtraEditors.LabelControl dateSiparisTarih;
        private DevExpress.XtraEditors.LabelControl lblDurum;
        private DevExpress.XtraEditors.TextEdit txtDurum;
        private DevExpress.XtraEditors.GroupControl grpTeslim;
        private DevExpress.XtraEditors.LabelControl lblTeslimTarih;
        private DevExpress.XtraEditors.DateEdit dateTeslimTarih;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAdi;
        private DevExpress.XtraGrid.Columns.GridColumn colMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colTeslimMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colKalan;
        private DevExpress.XtraEditors.GroupControl grpAksiyonlar;
        private DevExpress.XtraEditors.SimpleButton btnTeslimAl;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}

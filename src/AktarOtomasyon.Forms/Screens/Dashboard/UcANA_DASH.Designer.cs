namespace AktarOtomasyon.Forms.Screens.Dashboard
{
    partial class UcANA_DASH
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
            this.pnlTopWidgets = new DevExpress.XtraEditors.PanelControl();
            this.tablePanelWidgets = new DevExpress.Utils.Layout.TablePanel();
            this.widgetBildirim = new DevExpress.XtraEditors.PanelControl();
            this.lblBildirimDesc = new DevExpress.XtraEditors.LabelControl();
            this.lblBildirimCount = new DevExpress.XtraEditors.LabelControl();
            this.lblBildirimTitle = new DevExpress.XtraEditors.LabelControl();
            this.widgetTotalUrun = new DevExpress.XtraEditors.PanelControl();
            this.lblTotalUrunDesc = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalUrunCount = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalUrunTitle = new DevExpress.XtraEditors.LabelControl();
            this.widgetBekleyenSiparis = new DevExpress.XtraEditors.PanelControl();
            this.lblBekleyenSiparisDesc = new DevExpress.XtraEditors.LabelControl();
            this.lblBekleyenSiparisCount = new DevExpress.XtraEditors.LabelControl();
            this.lblBekleyenSiparisTitle = new DevExpress.XtraEditors.LabelControl();
            this.widgetKritikStok = new DevExpress.XtraEditors.PanelControl();
            this.lblKritikStokDesc = new DevExpress.XtraEditors.LabelControl();
            this.lblKritikStokCount = new DevExpress.XtraEditors.LabelControl();
            this.lblKritikStokTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlCenter = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpBildirimlerGrid = new DevExpress.XtraEditors.GroupControl();
            this.grdBildirimler = new DevExpress.XtraGrid.GridControl();
            this.gvBildirimler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBildirimTip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMesaj = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOkundu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grpHareketlerGrid = new DevExpress.XtraEditors.GroupControl();
            this.grdHareketler = new DevExpress.XtraGrid.GridControl();
            this.gvHareketler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHareketTip = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHareketTarih = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopWidgets)).BeginInit();
            this.pnlTopWidgets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelWidgets)).BeginInit();
            this.tablePanelWidgets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetBildirim)).BeginInit();
            this.widgetBildirim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetTotalUrun)).BeginInit();
            this.widgetTotalUrun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetBekleyenSiparis)).BeginInit();
            this.widgetBekleyenSiparis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetKritikStok)).BeginInit();
            this.widgetKritikStok.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).BeginInit();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpBildirimlerGrid)).BeginInit();
            this.grpBildirimlerGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBildirimler)).BeginInit();
            this.grdBildirimler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBildirimler});
            ((System.ComponentModel.ISupportInitialize)(this.gvBildirimler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHareketlerGrid)).BeginInit();
            this.grpHareketlerGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHareketler)).BeginInit();
            this.grdHareketler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHareketler});
            ((System.ComponentModel.ISupportInitialize)(this.gvHareketler)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopWidgets
            // 
            this.pnlTopWidgets.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTopWidgets.Controls.Add(this.tablePanelWidgets);
            this.pnlTopWidgets.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopWidgets.Location = new System.Drawing.Point(0, 0);
            this.pnlTopWidgets.Name = "pnlTopWidgets";
            this.pnlTopWidgets.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTopWidgets.Size = new System.Drawing.Size(1000, 160);
            this.pnlTopWidgets.TabIndex = 0;
            // 
            // tablePanelWidgets
            // 
            this.tablePanelWidgets.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F)});
            this.tablePanelWidgets.Controls.Add(this.widgetBildirim);
            this.tablePanelWidgets.Controls.Add(this.widgetTotalUrun);
            this.tablePanelWidgets.Controls.Add(this.widgetBekleyenSiparis);
            this.tablePanelWidgets.Controls.Add(this.widgetKritikStok);
            this.tablePanelWidgets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelWidgets.Location = new System.Drawing.Point(10, 10);
            this.tablePanelWidgets.Name = "tablePanelWidgets";
            this.tablePanelWidgets.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 140F)});
            this.tablePanelWidgets.Size = new System.Drawing.Size(980, 140);
            this.tablePanelWidgets.TabIndex = 0;
            // 
            // widgetBildirim
            // 
            this.widgetBildirim.Appearance.BackColor = System.Drawing.Color.White;
            this.widgetBildirim.Appearance.Options.UseBackColor = true;
            this.widgetBildirim.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanelWidgets.SetColumn(this.widgetBildirim, 3);
            this.widgetBildirim.Controls.Add(this.lblBildirimDesc);
            this.widgetBildirim.Controls.Add(this.lblBildirimCount);
            this.widgetBildirim.Controls.Add(this.lblBildirimTitle);
            this.widgetBildirim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widgetBildirim.Location = new System.Drawing.Point(745, 10);
            this.widgetBildirim.Margin = new System.Windows.Forms.Padding(10);
            this.widgetBildirim.Name = "widgetBildirim";
            this.tablePanelWidgets.SetRow(this.widgetBildirim, 0);
            this.widgetBildirim.Size = new System.Drawing.Size(225, 120);
            this.widgetBildirim.TabIndex = 3;
            // 
            // lblBildirimDesc
            // 
            this.lblBildirimDesc.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBildirimDesc.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblBildirimDesc.Appearance.Options.UseFont = true;
            this.lblBildirimDesc.Appearance.Options.UseForeColor = true;
            this.lblBildirimDesc.Location = new System.Drawing.Point(20, 85);
            this.lblBildirimDesc.Name = "lblBildirimDesc";
            this.lblBildirimDesc.Size = new System.Drawing.Size(99, 15);
            this.lblBildirimDesc.TabIndex = 2;
            this.lblBildirimDesc.Text = "Okunmamış Mesaj";
            // 
            // lblBildirimCount
            // 
            this.lblBildirimCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblBildirimCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblBildirimCount.Appearance.Options.UseFont = true;
            this.lblBildirimCount.Appearance.Options.UseForeColor = true;
            this.lblBildirimCount.Location = new System.Drawing.Point(20, 35);
            this.lblBildirimCount.Name = "lblBildirimCount";
            this.lblBildirimCount.Size = new System.Drawing.Size(22, 50);
            this.lblBildirimCount.TabIndex = 1;
            this.lblBildirimCount.Text = "0";
            // 
            // lblBildirimTitle
            // 
            this.lblBildirimTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblBildirimTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblBildirimTitle.Appearance.Options.UseFont = true;
            this.lblBildirimTitle.Appearance.Options.UseForeColor = true;
            this.lblBildirimTitle.Location = new System.Drawing.Point(20, 15);
            this.lblBildirimTitle.Name = "lblBildirimTitle";
            this.lblBildirimTitle.Size = new System.Drawing.Size(73, 17);
            this.lblBildirimTitle.TabIndex = 0;
            this.lblBildirimTitle.Text = "BİLDİRİMLER";
            // 
            // widgetTotalUrun
            // 
            this.widgetTotalUrun.Appearance.BackColor = System.Drawing.Color.White;
            this.widgetTotalUrun.Appearance.Options.UseBackColor = true;
            this.widgetTotalUrun.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanelWidgets.SetColumn(this.widgetTotalUrun, 2);
            this.widgetTotalUrun.Controls.Add(this.lblTotalUrunDesc);
            this.widgetTotalUrun.Controls.Add(this.lblTotalUrunCount);
            this.widgetTotalUrun.Controls.Add(this.lblTotalUrunTitle);
            this.widgetTotalUrun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widgetTotalUrun.Location = new System.Drawing.Point(500, 10);
            this.widgetTotalUrun.Margin = new System.Windows.Forms.Padding(10);
            this.widgetTotalUrun.Name = "widgetTotalUrun";
            this.tablePanelWidgets.SetRow(this.widgetTotalUrun, 0);
            this.widgetTotalUrun.Size = new System.Drawing.Size(225, 120);
            this.widgetTotalUrun.TabIndex = 2;
            // 
            // lblTotalUrunDesc
            // 
            this.lblTotalUrunDesc.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalUrunDesc.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalUrunDesc.Appearance.Options.UseFont = true;
            this.lblTotalUrunDesc.Appearance.Options.UseForeColor = true;
            this.lblTotalUrunDesc.Location = new System.Drawing.Point(20, 85);
            this.lblTotalUrunDesc.Name = "lblTotalUrunDesc";
            this.lblTotalUrunDesc.Size = new System.Drawing.Size(95, 15);
            this.lblTotalUrunDesc.TabIndex = 2;
            this.lblTotalUrunDesc.Text = "Sistemdeki Ürünler";
            // 
            // lblTotalUrunCount
            // 
            this.lblTotalUrunCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalUrunCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.lblTotalUrunCount.Appearance.Options.UseFont = true;
            this.lblTotalUrunCount.Appearance.Options.UseForeColor = true;
            this.lblTotalUrunCount.Location = new System.Drawing.Point(20, 35);
            this.lblTotalUrunCount.Name = "lblTotalUrunCount";
            this.lblTotalUrunCount.Size = new System.Drawing.Size(22, 50);
            this.lblTotalUrunCount.TabIndex = 1;
            this.lblTotalUrunCount.Text = "0";
            // 
            // lblTotalUrunTitle
            // 
            this.lblTotalUrunTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalUrunTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTotalUrunTitle.Appearance.Options.UseFont = true;
            this.lblTotalUrunTitle.Appearance.Options.UseForeColor = true;
            this.lblTotalUrunTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTotalUrunTitle.Name = "lblTotalUrunTitle";
            this.lblTotalUrunTitle.Size = new System.Drawing.Size(92, 17);
            this.lblTotalUrunTitle.TabIndex = 0;
            this.lblTotalUrunTitle.Text = "TOPLAM ÜRÜN";
            // 
            // widgetBekleyenSiparis
            // 
            this.widgetBekleyenSiparis.Appearance.BackColor = System.Drawing.Color.White;
            this.widgetBekleyenSiparis.Appearance.Options.UseBackColor = true;
            this.widgetBekleyenSiparis.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanelWidgets.SetColumn(this.widgetBekleyenSiparis, 1);
            this.widgetBekleyenSiparis.Controls.Add(this.lblBekleyenSiparisDesc);
            this.widgetBekleyenSiparis.Controls.Add(this.lblBekleyenSiparisCount);
            this.widgetBekleyenSiparis.Controls.Add(this.lblBekleyenSiparisTitle);
            this.widgetBekleyenSiparis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widgetBekleyenSiparis.Location = new System.Drawing.Point(255, 10);
            this.widgetBekleyenSiparis.Margin = new System.Windows.Forms.Padding(10);
            this.widgetBekleyenSiparis.Name = "widgetBekleyenSiparis";
            this.tablePanelWidgets.SetRow(this.widgetBekleyenSiparis, 0);
            this.widgetBekleyenSiparis.Size = new System.Drawing.Size(225, 120);
            this.widgetBekleyenSiparis.TabIndex = 1;
            // 
            // lblBekleyenSiparisDesc
            // 
            this.lblBekleyenSiparisDesc.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBekleyenSiparisDesc.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblBekleyenSiparisDesc.Appearance.Options.UseFont = true;
            this.lblBekleyenSiparisDesc.Appearance.Options.UseForeColor = true;
            this.lblBekleyenSiparisDesc.Location = new System.Drawing.Point(20, 85);
            this.lblBekleyenSiparisDesc.Name = "lblBekleyenSiparisDesc";
            this.lblBekleyenSiparisDesc.Size = new System.Drawing.Size(89, 15);
            this.lblBekleyenSiparisDesc.TabIndex = 2;
            this.lblBekleyenSiparisDesc.Text = "İşlem Bekliyor...";
            // 
            // lblBekleyenSiparisCount
            // 
            this.lblBekleyenSiparisCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblBekleyenSiparisCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblBekleyenSiparisCount.Appearance.Options.UseFont = true;
            this.lblBekleyenSiparisCount.Appearance.Options.UseForeColor = true;
            this.lblBekleyenSiparisCount.Location = new System.Drawing.Point(20, 35);
            this.lblBekleyenSiparisCount.Name = "lblBekleyenSiparisCount";
            this.lblBekleyenSiparisCount.Size = new System.Drawing.Size(22, 50);
            this.lblBekleyenSiparisCount.TabIndex = 1;
            this.lblBekleyenSiparisCount.Text = "0";
            // 
            // lblBekleyenSiparisTitle
            // 
            this.lblBekleyenSiparisTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblBekleyenSiparisTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblBekleyenSiparisTitle.Appearance.Options.UseFont = true;
            this.lblBekleyenSiparisTitle.Appearance.Options.UseForeColor = true;
            this.lblBekleyenSiparisTitle.Location = new System.Drawing.Point(20, 15);
            this.lblBekleyenSiparisTitle.Name = "lblBekleyenSiparisTitle";
            this.lblBekleyenSiparisTitle.Size = new System.Drawing.Size(126, 17);
            this.lblBekleyenSiparisTitle.TabIndex = 0;
            this.lblBekleyenSiparisTitle.Text = "BEKLEYEN SİPARİŞLER";
            // 
            // widgetKritikStok
            // 
            this.widgetKritikStok.Appearance.BackColor = System.Drawing.Color.White;
            this.widgetKritikStok.Appearance.Options.UseBackColor = true;
            this.widgetKritikStok.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanelWidgets.SetColumn(this.widgetKritikStok, 0);
            this.widgetKritikStok.Controls.Add(this.lblKritikStokDesc);
            this.widgetKritikStok.Controls.Add(this.lblKritikStokCount);
            this.widgetKritikStok.Controls.Add(this.lblKritikStokTitle);
            this.widgetKritikStok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.widgetKritikStok.Location = new System.Drawing.Point(10, 10);
            this.widgetKritikStok.Margin = new System.Windows.Forms.Padding(10);
            this.widgetKritikStok.Name = "widgetKritikStok";
            this.tablePanelWidgets.SetRow(this.widgetKritikStok, 0);
            this.widgetKritikStok.Size = new System.Drawing.Size(225, 120);
            this.widgetKritikStok.TabIndex = 0;
            // 
            // lblKritikStokDesc
            // 
            this.lblKritikStokDesc.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblKritikStokDesc.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblKritikStokDesc.Appearance.Options.UseFont = true;
            this.lblKritikStokDesc.Appearance.Options.UseForeColor = true;
            this.lblKritikStokDesc.Location = new System.Drawing.Point(20, 85);
            this.lblKritikStokDesc.Name = "lblKritikStokDesc";
            this.lblKritikStokDesc.Size = new System.Drawing.Size(84, 15);
            this.lblKritikStokDesc.TabIndex = 2;
            this.lblKritikStokDesc.Text = "Acil Ürün Sayısı";
            // 
            // lblKritikStokCount
            // 
            this.lblKritikStokCount.Appearance.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblKritikStokCount.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblKritikStokCount.Appearance.Options.UseFont = true;
            this.lblKritikStokCount.Appearance.Options.UseForeColor = true;
            this.lblKritikStokCount.Location = new System.Drawing.Point(20, 35);
            this.lblKritikStokCount.Name = "lblKritikStokCount";
            this.lblKritikStokCount.Size = new System.Drawing.Size(22, 50);
            this.lblKritikStokCount.TabIndex = 1;
            this.lblKritikStokCount.Text = "0";
            // 
            // lblKritikStokTitle
            // 
            this.lblKritikStokTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblKritikStokTitle.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblKritikStokTitle.Appearance.Options.UseFont = true;
            this.lblKritikStokTitle.Appearance.Options.UseForeColor = true;
            this.lblKritikStokTitle.Location = new System.Drawing.Point(20, 15);
            this.lblKritikStokTitle.Name = "lblKritikStokTitle";
            this.lblKritikStokTitle.Size = new System.Drawing.Size(74, 17);
            this.lblKritikStokTitle.TabIndex = 0;
            this.lblKritikStokTitle.Text = "KRİTİK STOK";
            // 
            // pnlCenter
            // 
            this.pnlCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCenter.Controls.Add(this.splitContainerControl1);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 160);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.pnlCenter.Size = new System.Drawing.Size(1000, 540);
            this.pnlCenter.TabIndex = 1;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(10, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grpBildirimlerGrid);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grpHareketlerGrid);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(980, 530);
            this.splitContainerControl1.SplitterPosition = 480;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // grpBildirimlerGrid
            // 
            this.grpBildirimlerGrid.Appearance.BorderColor = System.Drawing.Color.White;
            this.grpBildirimlerGrid.Appearance.Options.UseBorderColor = true;
            this.grpBildirimlerGrid.Controls.Add(this.grdBildirimler);
            this.grpBildirimlerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpBildirimlerGrid.Location = new System.Drawing.Point(0, 0);
            this.grpBildirimlerGrid.Name = "grpBildirimlerGrid";
            this.grpBildirimlerGrid.Size = new System.Drawing.Size(480, 530);
            this.grpBildirimlerGrid.TabIndex = 0;
            this.grpBildirimlerGrid.Text = "Son Bildirimler";
            // 
            // grdBildirimler
            // 
            this.grdBildirimler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBildirimler.Location = new System.Drawing.Point(2, 23);
            this.grdBildirimler.MainView = this.gvBildirimler;
            this.grdBildirimler.Name = "grdBildirimler";
            this.grdBildirimler.Size = new System.Drawing.Size(476, 505);
            this.grdBildirimler.TabIndex = 0;
            this.grdBildirimler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBildirimler});
            // 
            // gvBildirimler
            // 
            this.gvBildirimler.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBildirimTip,
            this.colMesaj,
            this.colTarih,
            this.colOkundu});
            this.gvBildirimler.GridControl = this.grdBildirimler;
            this.gvBildirimler.Name = "gvBildirimler";
            this.gvBildirimler.OptionsBehavior.Editable = false;
            this.gvBildirimler.OptionsView.ShowGroupPanel = false;
            // 
            // colBildirimTip
            // 
            this.colBildirimTip.Caption = "Tip";
            this.colBildirimTip.FieldName = "bildirim_tip";
            this.colBildirimTip.Name = "colBildirimTip";
            this.colBildirimTip.Visible = true;
            this.colBildirimTip.VisibleIndex = 0;
            this.colBildirimTip.Width = 80;
            // 
            // colMesaj
            // 
            this.colMesaj.Caption = "Mesaj";
            this.colMesaj.FieldName = "mesaj";
            this.colMesaj.Name = "colMesaj";
            this.colMesaj.Visible = true;
            this.colMesaj.VisibleIndex = 1;
            this.colMesaj.Width = 250;
            // 
            // colTarih
            // 
            this.colTarih.Caption = "Tarih";
            this.colTarih.FieldName = "olusturma_tarih";
            this.colTarih.Name = "colTarih";
            this.colTarih.Visible = true;
            this.colTarih.VisibleIndex = 2;
            this.colTarih.Width = 120;
            // 
            // colOkundu
            // 
            this.colOkundu.Caption = "Okundu";
            this.colOkundu.FieldName = "okundu";
            this.colOkundu.Name = "colOkundu";
            this.colOkundu.Visible = false;
            this.colOkundu.VisibleIndex = 3;
            // 
            // grpHareketlerGrid
            // 
            this.grpHareketlerGrid.Appearance.BorderColor = System.Drawing.Color.White;
            this.grpHareketlerGrid.Appearance.Options.UseBorderColor = true;
            this.grpHareketlerGrid.Controls.Add(this.grdHareketler);
            this.grpHareketlerGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHareketlerGrid.Location = new System.Drawing.Point(0, 0);
            this.grpHareketlerGrid.Name = "grpHareketlerGrid";
            this.grpHareketlerGrid.Size = new System.Drawing.Size(490, 530);
            this.grpHareketlerGrid.TabIndex = 0;
            this.grpHareketlerGrid.Text = "Son Stok Hareketleri";
            // 
            // grdHareketler
            // 
            this.grdHareketler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHareketler.Location = new System.Drawing.Point(2, 23);
            this.grdHareketler.MainView = this.gvHareketler;
            this.grdHareketler.Name = "grdHareketler";
            this.grdHareketler.Size = new System.Drawing.Size(486, 505);
            this.grdHareketler.TabIndex = 0;
            this.grdHareketler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHareketler});
            // 
            // gvHareketler
            // 
            this.gvHareketler.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUrunAd,
            this.colHareketTip,
            this.colMiktar,
            this.colHareketTarih});
            this.gvHareketler.GridControl = this.grdHareketler;
            this.gvHareketler.Name = "gvHareketler";
            this.gvHareketler.OptionsBehavior.Editable = false;
            this.gvHareketler.OptionsView.ShowGroupPanel = false;
            // 
            // colUrunAd
            // 
            this.colUrunAd.Caption = "Ürün";
            this.colUrunAd.FieldName = "urun_ad";
            this.colUrunAd.Name = "colUrunAd";
            this.colUrunAd.Visible = true;
            this.colUrunAd.VisibleIndex = 0;
            this.colUrunAd.Width = 180;
            // 
            // colHareketTip
            // 
            this.colHareketTip.Caption = "Tip";
            this.colHareketTip.FieldName = "hareket_tip";
            this.colHareketTip.Name = "colHareketTip";
            this.colHareketTip.Visible = true;
            this.colHareketTip.VisibleIndex = 1;
            this.colHareketTip.Width = 80;
            // 
            // colMiktar
            // 
            this.colMiktar.Caption = "Miktar";
            this.colMiktar.FieldName = "miktar";
            this.colMiktar.Name = "colMiktar";
            this.colMiktar.Visible = true;
            this.colMiktar.VisibleIndex = 2;
            this.colMiktar.Width = 80;
            // 
            // colHareketTarih
            // 
            this.colHareketTarih.Caption = "Tarih";
            this.colHareketTarih.FieldName = "hareket_tarih";
            this.colHareketTarih.Name = "colHareketTarih";
            this.colHareketTarih.Visible = true;
            this.colHareketTarih.VisibleIndex = 3;
            this.colHareketTarih.Width = 120;
            // 
            // UcANA_DASH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlTopWidgets);
            this.Name = "UcANA_DASH";
            this.Size = new System.Drawing.Size(1000, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pnlTopWidgets)).EndInit();
            this.pnlTopWidgets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanelWidgets)).EndInit();
            this.tablePanelWidgets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.widgetBildirim)).EndInit();
            this.widgetBildirim.ResumeLayout(false);
            this.widgetBildirim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetTotalUrun)).EndInit();
            this.widgetTotalUrun.ResumeLayout(false);
            this.widgetTotalUrun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetBekleyenSiparis)).EndInit();
            this.widgetBekleyenSiparis.ResumeLayout(false);
            this.widgetBekleyenSiparis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widgetKritikStok)).EndInit();
            this.widgetKritikStok.ResumeLayout(false);
            this.widgetKritikStok.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCenter)).EndInit();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpBildirimlerGrid)).EndInit();
            this.grpBildirimlerGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBildirimler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBildirimler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpHareketlerGrid)).EndInit();
            this.grpHareketlerGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHareketler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHareketler)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTopWidgets;
        private DevExpress.Utils.Layout.TablePanel tablePanelWidgets;
        private DevExpress.XtraEditors.PanelControl widgetBildirim;
        private DevExpress.XtraEditors.LabelControl lblBildirimDesc;
        private DevExpress.XtraEditors.LabelControl lblBildirimCount;
        private DevExpress.XtraEditors.LabelControl lblBildirimTitle;
        private DevExpress.XtraEditors.PanelControl widgetTotalUrun;
        private DevExpress.XtraEditors.LabelControl lblTotalUrunDesc;
        private DevExpress.XtraEditors.LabelControl lblTotalUrunCount;
        private DevExpress.XtraEditors.LabelControl lblTotalUrunTitle;
        private DevExpress.XtraEditors.PanelControl widgetBekleyenSiparis;
        private DevExpress.XtraEditors.LabelControl lblBekleyenSiparisDesc;
        private DevExpress.XtraEditors.LabelControl lblBekleyenSiparisCount;
        private DevExpress.XtraEditors.LabelControl lblBekleyenSiparisTitle;
        private DevExpress.XtraEditors.PanelControl widgetKritikStok;
        private DevExpress.XtraEditors.LabelControl lblKritikStokDesc;
        private DevExpress.XtraEditors.LabelControl lblKritikStokCount;
        private DevExpress.XtraEditors.LabelControl lblKritikStokTitle;
        private DevExpress.XtraEditors.PanelControl pnlCenter;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl grpBildirimlerGrid;
        private DevExpress.XtraGrid.GridControl grdBildirimler;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBildirimler;
        private DevExpress.XtraGrid.Columns.GridColumn colBildirimTip;
        private DevExpress.XtraGrid.Columns.GridColumn colMesaj;
        private DevExpress.XtraGrid.Columns.GridColumn colTarih;
        private DevExpress.XtraGrid.Columns.GridColumn colOkundu;
        private DevExpress.XtraEditors.GroupControl grpHareketlerGrid;
        private DevExpress.XtraGrid.GridControl grdHareketler;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHareketler;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAd;
        private DevExpress.XtraGrid.Columns.GridColumn colHareketTip;
        private DevExpress.XtraGrid.Columns.GridColumn colMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colHareketTarih;
    }
}

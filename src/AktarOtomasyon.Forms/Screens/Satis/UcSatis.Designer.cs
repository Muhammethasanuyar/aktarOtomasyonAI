namespace AktarOtomasyon.Forms.Screens.Satis
{
    partial class UcSatis
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
            this.components = new System.ComponentModel.Container();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.grpUrunGiris = new DevExpress.XtraEditors.GroupControl();
            this.btnSepeteEkle = new DevExpress.XtraEditors.SimpleButton();
            this.numMiktar = new DevExpress.XtraEditors.SpinEdit();
            this.lblMiktar = new DevExpress.XtraEditors.LabelControl();
            this.lblFiyat = new DevExpress.XtraEditors.LabelControl();
            this.lblUrunAdi = new DevExpress.XtraEditors.LabelControl();
            this.txtBarkod = new DevExpress.XtraEditors.TextEdit();
            this.lblBarkod = new DevExpress.XtraEditors.LabelControl();
            this.grpSepet = new DevExpress.XtraEditors.GroupControl();
            this.gcSepet = new DevExpress.XtraGrid.GridControl();
            this.gvSepet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUrunAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBirimFiyat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMiktar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTutar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlAlt = new DevExpress.XtraEditors.PanelControl();
            this.btnSatisYap = new DevExpress.XtraEditors.SimpleButton();
            this.lblGenelToplam = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamBaslik = new DevExpress.XtraEditors.LabelControl();
            this.picUrun = new DevExpress.XtraEditors.PictureEdit();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpUrunGiris)).BeginInit();
            this.grpUrunGiris.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarkod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSepet)).BeginInit();
            this.grpSepet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSepet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSepet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAlt)).BeginInit();
            this.pnlAlt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUrun.Properties)).BeginInit();
            this.SuspendLayout();

            // splitContainerControl1
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.grpUrunGiris);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.grpSepet);
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlAlt);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 600);
            this.splitContainerControl1.SplitterPosition = 350;
            this.splitContainerControl1.TabIndex = 0;

            // grpUrunGiris
            this.grpUrunGiris.Controls.Add(this.picUrun);
            this.grpUrunGiris.Controls.Add(this.btnSepeteEkle);
            this.grpUrunGiris.Controls.Add(this.numMiktar);
            this.grpUrunGiris.Controls.Add(this.lblMiktar);
            this.grpUrunGiris.Controls.Add(this.lblFiyat);
            this.grpUrunGiris.Controls.Add(this.lblUrunAdi);
            this.grpUrunGiris.Controls.Add(this.txtBarkod);
            this.grpUrunGiris.Controls.Add(this.lblBarkod);
            this.grpUrunGiris.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUrunGiris.Location = new System.Drawing.Point(0, 0);
            this.grpUrunGiris.Name = "grpUrunGiris";
            this.grpUrunGiris.Size = new System.Drawing.Size(350, 600);
            this.grpUrunGiris.Text = "Ürün Girişi";

            // picUrun
            this.picUrun.Location = new System.Drawing.Point(20, 250);
            this.picUrun.Name = "picUrun";
            this.picUrun.Properties.ShowMenu = false;
            this.picUrun.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picUrun.Size = new System.Drawing.Size(300, 200);

            // lblBarkod
            this.lblBarkod.Location = new System.Drawing.Point(20, 40);
            this.lblBarkod.Text = "Barkod Okut:";

            // txtBarkod
            this.txtBarkod.Location = new System.Drawing.Point(20, 60);
            this.txtBarkod.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBarkod.Properties.Appearance.Options.UseFont = true;
            this.txtBarkod.Size = new System.Drawing.Size(300, 28);
            this.txtBarkod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarkod_KeyDown);

            // lblUrunAdi
            this.lblUrunAdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUrunAdi.Appearance.Options.UseFont = true;
            this.lblUrunAdi.Location = new System.Drawing.Point(20, 100);
            this.lblUrunAdi.Size = new System.Drawing.Size(300, 25);
            this.lblUrunAdi.Text = "Ürün Seçilmedi";

            // lblFiyat
            this.lblFiyat.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblFiyat.Appearance.ForeColor = System.Drawing.Color.Green;
            this.lblFiyat.Appearance.Options.UseFont = true;
            this.lblFiyat.Appearance.Options.UseForeColor = true;
            this.lblFiyat.Location = new System.Drawing.Point(20, 130);
            this.lblFiyat.Text = "0,00 ₺";

            // lblMiktar
            this.lblMiktar.Location = new System.Drawing.Point(20, 170);
            this.lblMiktar.Text = "Miktar:";

            // numMiktar
            this.numMiktar.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMiktar.Location = new System.Drawing.Point(20, 190);
            this.numMiktar.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.numMiktar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.numMiktar.Properties.MaxValue = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numMiktar.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.numMiktar.Size = new System.Drawing.Size(100, 28);

            // btnSepeteEkle
            this.btnSepeteEkle.Appearance.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSepeteEkle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSepeteEkle.Appearance.Options.UseBackColor = true;
            this.btnSepeteEkle.Appearance.Options.UseFont = true;
            this.btnSepeteEkle.Location = new System.Drawing.Point(140, 185);
            this.btnSepeteEkle.Name = "btnSepeteEkle";
            this.btnSepeteEkle.Size = new System.Drawing.Size(180, 40);
            this.btnSepeteEkle.Text = "Sepete Ekle";
            this.btnSepeteEkle.Click += new System.EventHandler(this.btnSepeteEkle_Click);

            // grpSepet
            this.grpSepet.Controls.Add(this.gcSepet);
            this.grpSepet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSepet.Location = new System.Drawing.Point(0, 0);
            this.grpSepet.Text = "Alışveriş Sepeti";

            // gcSepet
            this.gcSepet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSepet.MainView = this.gvSepet;
            this.gcSepet.Name = "gcSepet";
            this.gcSepet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gvSepet });

            // gvSepet
            this.gvSepet.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
                this.colUrunAd, this.colMiktar, this.colBirimFiyat, this.colTutar });
            this.gvSepet.GridControl = this.gcSepet;
            this.gvSepet.Name = "gvSepet";
            this.gvSepet.OptionsView.ShowGroupPanel = false;
            this.gvSepet.OptionsView.ShowFooter = false;

            this.colUrunAd.Caption = "Ürün";
            this.colUrunAd.FieldName = "UrunAdi";
            this.colUrunAd.Visible = true;

            this.colMiktar.Caption = "Miktar";
            this.colMiktar.FieldName = "Miktar";
            this.colMiktar.Visible = true;

            this.colBirimFiyat.Caption = "Birim Fiyat";
            this.colBirimFiyat.FieldName = "SatisFiyati";
            this.colBirimFiyat.Visible = true;

            this.colTutar.Caption = "Tutar";
            this.colTutar.FieldName = "ToplamTutar";
            this.colTutar.Visible = true;

            // pnlAlt
            this.pnlAlt.Controls.Add(this.btnSatisYap);
            this.pnlAlt.Controls.Add(this.lblGenelToplam);
            this.pnlAlt.Controls.Add(this.lblToplamBaslik);
            this.pnlAlt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAlt.Height = 80;

            // lblToplamBaslik
            this.lblToplamBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblToplamBaslik.Location = new System.Drawing.Point(20, 30);
            this.lblToplamBaslik.Text = "GENEL TOPLAM:";

            // lblGenelToplam
            this.lblGenelToplam.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblGenelToplam.Appearance.ForeColor = System.Drawing.Color.Maroon;
            this.lblGenelToplam.Appearance.Options.UseFont = true;
            this.lblGenelToplam.Appearance.Options.UseForeColor = true;
            this.lblGenelToplam.Location = new System.Drawing.Point(150, 20);
            this.lblGenelToplam.Text = "0,00 ₺";

            // btnSatisYap
            this.btnSatisYap.Appearance.BackColor = System.Drawing.Color.DarkGreen;
            this.btnSatisYap.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSatisYap.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnSatisYap.Appearance.Options.UseBackColor = true;
            this.btnSatisYap.Appearance.Options.UseFont = true;
            this.btnSatisYap.Appearance.Options.UseForeColor = true;
            this.btnSatisYap.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSatisYap.Location = new System.Drawing.Point(440, 2);
            this.btnSatisYap.Size = new System.Drawing.Size(200, 76);
            this.btnSatisYap.Text = "SATIŞI TAMAMLA";
            this.btnSatisYap.Click += new System.EventHandler(this.btnSatisYap_Click);

            this.Controls.Add(this.splitContainerControl1);
            this.Name = "UcSatis";
            this.Size = new System.Drawing.Size(1000, 600);
            
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpUrunGiris)).EndInit();
            this.grpUrunGiris.ResumeLayout(false);
            this.grpUrunGiris.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMiktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarkod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpSepet)).EndInit();
            this.grpSepet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSepet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSepet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlAlt)).EndInit();
            this.pnlAlt.ResumeLayout(false);
            this.pnlAlt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUrun.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl grpUrunGiris;
        private DevExpress.XtraEditors.TextEdit txtBarkod;
        private DevExpress.XtraEditors.LabelControl lblBarkod;
        private DevExpress.XtraEditors.LabelControl lblUrunAdi;
        private DevExpress.XtraEditors.LabelControl lblFiyat;
        private DevExpress.XtraEditors.SpinEdit numMiktar;
        private DevExpress.XtraEditors.LabelControl lblMiktar;
        private DevExpress.XtraEditors.SimpleButton btnSepeteEkle;
        private DevExpress.XtraEditors.PictureEdit picUrun;
        private DevExpress.XtraEditors.GroupControl grpSepet;
        private DevExpress.XtraGrid.GridControl gcSepet;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSepet;
        private DevExpress.XtraEditors.PanelControl pnlAlt;
        private DevExpress.XtraEditors.SimpleButton btnSatisYap;
        private DevExpress.XtraEditors.LabelControl lblGenelToplam;
        private DevExpress.XtraEditors.LabelControl lblToplamBaslik;
        private DevExpress.XtraGrid.Columns.GridColumn colUrunAd;
        private DevExpress.XtraGrid.Columns.GridColumn colMiktar;
        private DevExpress.XtraGrid.Columns.GridColumn colBirimFiyat;
        private DevExpress.XtraGrid.Columns.GridColumn colTutar;
    }
}

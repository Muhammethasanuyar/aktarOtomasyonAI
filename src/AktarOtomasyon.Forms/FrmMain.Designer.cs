namespace AktarOtomasyon.Forms
{
    partial class FrmMain
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement_Dashboard = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Urunler = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_UrunListe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_UrunKart = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Stok = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_StokHareket = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_StokKritik = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Siparisler = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_SiparisListe = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_SiparisTaslak = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Raporlar = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Satis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_BarkodTest = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_AI = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_AiModul = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_AiSablonYnt = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Yonetim = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_BildirimMrk = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_TemplateMrk = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_SysSettings = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_SysDiag = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_SecurityMgmt = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_AuditViewer = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Kullanici = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_ParolaDegistir = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlElement_Cikis = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tabbedMdiManager = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.stsUser = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedMdiManager)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_Dashboard,
            this.accordionControlElement_Urunler,
            this.accordionControlElement_Stok,
            this.accordionControlElement_Siparisler,
            this.accordionControlElement_Raporlar,
            this.accordionControlElement_Yonetim,
            this.accordionControlElement_Kullanici,
            this.accordionControlElement_BarkodTest});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(260, 745);
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement_Dashboard
            // 
            this.accordionControlElement_Dashboard.Name = "accordionControlElement_Dashboard";
            this.accordionControlElement_Dashboard.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_Dashboard.Text = "Ana Ekran";
            this.accordionControlElement_Dashboard.Click += new System.EventHandler(this.accordionControlElement_Dashboard_Click);
            // 
            // accordionControlElement_Urunler
            // 
            this.accordionControlElement_Urunler.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_UrunListe,
            this.accordionControlElement_UrunKart});
            this.accordionControlElement_Urunler.Expanded = true;
            this.accordionControlElement_Urunler.Name = "accordionControlElement_Urunler";
            this.accordionControlElement_Urunler.Text = "Ürünler";
            // 
            // accordionControlElement_UrunListe
            // 
            this.accordionControlElement_UrunListe.Name = "accordionControlElement_UrunListe";
            this.accordionControlElement_UrunListe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_UrunListe.Text = "Ürün Listesi";
            this.accordionControlElement_UrunListe.Click += new System.EventHandler(this.accordionControlElement_UrunListe_Click);
            // 
            // accordionControlElement_UrunKart
            // 
            this.accordionControlElement_UrunKart.Name = "accordionControlElement_UrunKart";
            this.accordionControlElement_UrunKart.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_UrunKart.Text = "Yeni Ürün";
            this.accordionControlElement_UrunKart.Click += new System.EventHandler(this.accordionControlElement_UrunKart_Click);
            // 
            // accordionControlElement_Stok
            // 
            this.accordionControlElement_Stok.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_StokHareket,
            this.accordionControlElement_StokKritik});
            this.accordionControlElement_Stok.Name = "accordionControlElement_Stok";
            this.accordionControlElement_Stok.Text = "Stok";
            // 
            // accordionControlElement_StokHareket
            // 
            this.accordionControlElement_StokHareket.Name = "accordionControlElement_StokHareket";
            this.accordionControlElement_StokHareket.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_StokHareket.Text = "Stok Hareketleri";
            this.accordionControlElement_StokHareket.Click += new System.EventHandler(this.accordionControlElement_StokHareket_Click);
            // 
            // accordionControlElement_StokKritik
            // 
            this.accordionControlElement_StokKritik.Name = "accordionControlElement_StokKritik";
            this.accordionControlElement_StokKritik.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_StokKritik.Text = "Kritik Stok";
            this.accordionControlElement_StokKritik.Click += new System.EventHandler(this.accordionControlElement_StokKritik_Click);
            // 
            // accordionControlElement_Siparisler
            // 
            this.accordionControlElement_Siparisler.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_SiparisListe,
            this.accordionControlElement_SiparisTaslak});
            this.accordionControlElement_Siparisler.Name = "accordionControlElement_Siparisler";
            this.accordionControlElement_Siparisler.Text = "Siparişler";
            // 
            // accordionControlElement_SiparisListe
            // 
            this.accordionControlElement_SiparisListe.Name = "accordionControlElement_SiparisListe";
            this.accordionControlElement_SiparisListe.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_SiparisListe.Text = "Sipariş Listesi";
            this.accordionControlElement_SiparisListe.Click += new System.EventHandler(this.accordionControlElement_SiparisListe_Click);
            // 
            // accordionControlElement_SiparisTaslak
            // 
            this.accordionControlElement_SiparisTaslak.Name = "accordionControlElement_SiparisTaslak";
            this.accordionControlElement_SiparisTaslak.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_SiparisTaslak.Text = "Yeni Sipariş";
            this.accordionControlElement_SiparisTaslak.Click += new System.EventHandler(this.accordionControlElement_SiparisTaslak_Click);
            // 
            // accordionControlElement_Raporlar
            // 
            this.accordionControlElement_Raporlar.Name = "accordionControlElement_Raporlar";
            this.accordionControlElement_Raporlar.Text = "Raporlar";
            // 
            // accordionControlElement_AI
            // 
            this.accordionControlElement_AI.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_AiModul,
            this.accordionControlElement_AiSablonYnt});
            this.accordionControlElement_AI.Name = "accordionControlElement_AI";
            this.accordionControlElement_AI.Text = "AI İçerik";
            // 
            // accordionControlElement_AiModul
            // 
            this.accordionControlElement_AiModul.Name = "accordionControlElement_AiModul";
            this.accordionControlElement_AiModul.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_AiModul.Text = "İçerik Üretimi";
            this.accordionControlElement_AiModul.Click += new System.EventHandler(this.accordionControlElement_AiModul_Click);
            // 
            // accordionControlElement_AiSablonYnt
            // 
            this.accordionControlElement_AiSablonYnt.Name = "accordionControlElement_AiSablonYnt";
            this.accordionControlElement_AiSablonYnt.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_AiSablonYnt.Text = "Şablon Yönetimi";
            this.accordionControlElement_AiSablonYnt.Click += new System.EventHandler(this.accordionControlElement_AiSablonYnt_Click);
            // 
            // accordionControlElement_Yonetim
            // 
            this.accordionControlElement_Yonetim.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_BildirimMrk,
            this.accordionControlElement_TemplateMrk,
            this.accordionControlElement_SysSettings,
            this.accordionControlElement_SysDiag,
            this.accordionControlElement_SecurityMgmt,
            this.accordionControlElement_AuditViewer});
            this.accordionControlElement_Yonetim.Name = "accordionControlElement_Yonetim";
            this.accordionControlElement_Yonetim.Text = "Yönetim";
            // 
            // accordionControlElement_BildirimMrk
            // 
            this.accordionControlElement_BildirimMrk.Name = "accordionControlElement_BildirimMrk";
            this.accordionControlElement_BildirimMrk.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_BildirimMrk.Text = "Bildirim Merkezi";
            this.accordionControlElement_BildirimMrk.Click += new System.EventHandler(this.accordionControlElement_BildirimMrk_Click);
            // 
            // accordionControlElement_TemplateMrk
            // 
            this.accordionControlElement_TemplateMrk.Name = "accordionControlElement_TemplateMrk";
            this.accordionControlElement_TemplateMrk.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_TemplateMrk.Text = "Şablon Yönetimi";
            this.accordionControlElement_TemplateMrk.Click += new System.EventHandler(this.accordionControlElement_TemplateMrk_Click);
            // 
            // accordionControlElement_SysSettings
            // 
            this.accordionControlElement_SysSettings.Name = "accordionControlElement_SysSettings";
            this.accordionControlElement_SysSettings.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_SysSettings.Text = "Sistem Ayarları";
            this.accordionControlElement_SysSettings.Click += new System.EventHandler(this.accordionControlElement_SysSettings_Click);
            // 
            // accordionControlElement_SysDiag
            // 
            this.accordionControlElement_SysDiag.Name = "accordionControlElement_SysDiag";
            this.accordionControlElement_SysDiag.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_SysDiag.Text = "Sistem Durumu";
            this.accordionControlElement_SysDiag.Click += new System.EventHandler(this.accordionControlElement_SysDiag_Click);
            // 
            // accordionControlElement_SecurityMgmt
            // 
            this.accordionControlElement_SecurityMgmt.Name = "accordionControlElement_SecurityMgmt";
            this.accordionControlElement_SecurityMgmt.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_SecurityMgmt.Text = "Güvenlik Yönetimi";
            this.accordionControlElement_SecurityMgmt.Click += new System.EventHandler(this.accordionControlElement_SecurityMgmt_Click);
            // 
            // accordionControlElement_AuditViewer
            // 
            this.accordionControlElement_AuditViewer.Name = "accordionControlElement_AuditViewer";
            this.accordionControlElement_AuditViewer.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_AuditViewer.Text = "İşlem Geçmişi";
            this.accordionControlElement_AuditViewer.Click += new System.EventHandler(this.accordionControlElement_AuditViewer_Click);
            // 
            // accordionControlElement_Kullanici
            // 
            this.accordionControlElement_Kullanici.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement_ParolaDegistir,
            this.accordionControlElement_Cikis});
            this.accordionControlElement_Kullanici.Name = "accordionControlElement_Kullanici";
            this.accordionControlElement_Kullanici.Text = "Kullanıcı";
            // 
            // accordionControlElement_ParolaDegistir
            // 
            this.accordionControlElement_ParolaDegistir.Name = "accordionControlElement_ParolaDegistir";
            this.accordionControlElement_ParolaDegistir.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_ParolaDegistir.Text = "Parola Değiştir";
            this.accordionControlElement_ParolaDegistir.Click += new System.EventHandler(this.accordionControlElement_ParolaDegistir_Click);
            // 
            // accordionControlElement_Satis
            // 
            this.accordionControlElement_Satis.Name = "accordionControlElement_Satis";
            this.accordionControlElement_Satis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_Satis.Text = "Hızlı Satış";
            this.accordionControlElement_Satis.Click += new System.EventHandler(this.accordionControlElement_Satis_Click);
            // 
            // accordionControlElement_BarkodTest
            // 
            this.accordionControlElement_BarkodTest.Name = "accordionControlElement_BarkodTest";
            this.accordionControlElement_BarkodTest.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_BarkodTest.Text = "Test Barkod Okuma";
            this.accordionControlElement_BarkodTest.Click += new System.EventHandler(this.accordionControlElement_BarkodTest_Click);
            // 
            // accordionControlElement_Cikis
            // 
            this.accordionControlElement_Cikis.Name = "accordionControlElement_Cikis";
            this.accordionControlElement_Cikis.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement_Cikis.Text = "Çıkış";
            this.accordionControlElement_Cikis.Click += new System.EventHandler(this.accordionControlElement_Cikis_Click);
            // 
            // tabbedMdiManager
            // 
            this.tabbedMdiManager.MdiParent = this;
            this.tabbedMdiManager.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tabbedMdiManager.HeaderButtons = DevExpress.XtraTab.TabButtons.Close;
            this.tabbedMdiManager.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stsVersion,
            this.stsUser});
            this.statusStrip1.Location = new System.Drawing.Point(260, 723);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stsVersion
            // 
            this.stsVersion.Name = "stsVersion";
            this.stsVersion.Size = new System.Drawing.Size(28, 17);
            this.stsVersion.Text = "v1.0";
            // 
            // stsUser
            // 
            this.stsUser.Name = "stsUser";
            this.stsUser.Size = new System.Drawing.Size(89, 17);
            this.stsUser.Spring = true;
            this.stsUser.Text = "Kullanıcı: Admin";
            this.stsUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 745);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.accordionControl1);
            this.IsMdiContainer = true;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aktar Otomasyon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedMdiManager)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Dashboard;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Urunler;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_UrunListe;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_UrunKart;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Stok;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_StokHareket;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_StokKritik;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Siparisler;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_SiparisListe;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_SiparisTaslak;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Raporlar;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Satis;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_BarkodTest;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_AI;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_AiModul;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_AiSablonYnt;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Yonetim;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_BildirimMrk;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_TemplateMrk;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_SysSettings;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_SysDiag;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_SecurityMgmt;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_AuditViewer;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Kullanici;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_ParolaDegistir;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement_Cikis;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager tabbedMdiManager;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stsVersion;
        private System.Windows.Forms.ToolStripStatusLabel stsUser;
    }
}

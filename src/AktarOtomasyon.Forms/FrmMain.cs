using System;
using System.Drawing;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Forms.Helpers;
using DevExpress.XtraBars.Navigation;

namespace AktarOtomasyon.Forms
{
    /// <summary>
    /// Ana MDI form - Ana Ekran
    /// KURAL: IsMdiContainer = true, tüm ekranlar MDI child olarak açılır.
    /// </summary>
    public partial class FrmMain : DevExpress.XtraEditors.XtraForm
    {
        private const string EKRAN_KOD = "ANA_DASH";

        public FrmMain()
        {
            InitializeComponent();
            InitializeMdi();
        }

        private void InitializeMdi()
        {
            this.IsMdiContainer = true;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                // NavigationManager already initialized in Program.cs
                // NavigationManager.Initialize();

                var ekranBilgi = InterfaceFactory.KulEkran.EkranGetir(EKRAN_KOD);
                if (ekranBilgi != null)
                {
                    this.Text = ekranBilgi.MenudekiAdi;
                    InterfaceFactory.KulEkran.VersiyonLogla(EKRAN_KOD, CommonFunction.GetAppVersion());
                }
                else
                {
                    this.Text = "Aktar Otomasyon - Ana Ekran";
                }

                UpdateStatusBar();
                ApplyMenuPermissions(); // NEW: Apply permission-based menu visibility
                ApplyModernSidebarTheme(); // Apply modern theme to sidebar
                NavigationManager.OpenScreen("ANA_DASH", this);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("FrmMain_Load error: {0}", ex.Message), "MAIN");
                this.Text = "Aktar Otomasyon";
            }
        }

        private void UpdateStatusBar()
        {
            stsVersion.Text = string.Format("v{0}", CommonFunction.GetAppVersion());
            stsUser.Text = SessionManager.IsAuthenticated
                ? string.Format("Kullanıcı: {0}", SessionManager.AdSoyad)
                : "Kullanıcı: -";
        }

        private void accordionControlElement_Dashboard_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("ANA_DASH", this);
        }

        private void accordionControlElement_UrunListe_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("URUN_LISTE", this);
        }

        private void accordionControlElement_UrunKart_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("URUN_KART", this);
        }

        private void accordionControlElement_StokHareket_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("STOK_HAREKET", this);
        }

        private void accordionControlElement_StokKritik_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("STOK_KRITIK", this);
        }

        private void accordionControlElement_BildirimMrk_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("BILDIRIM_MRK", this);
        }

        private void accordionControlElement_SiparisListe_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("SIP_LISTE", this);
        }

        private void accordionControlElement_SiparisTaslak_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("SIP_TASLAK", this, null);
        }

        private void accordionControlElement_AiModul_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("AI_MODUL", this);
        }

        private void accordionControlElement_AiSablonYnt_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("AI_SABLON_YNT", this);
        }

        private void accordionControlElement_TemplateMrk_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("TEMPLATE_MRK", this);
        }

        private void accordionControlElement_SysSettings_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("SYS_SETTINGS", this);
        }

        private void accordionControlElement_SysDiag_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("SYS_DIAG", this);
        }

        private void accordionControlElement_SecurityMgmt_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("SEC_YONETIM", this);
        }

        private void accordionControlElement_AuditViewer_Click(object sender, EventArgs e)
        {
            NavigationManager.OpenScreen("AUDIT_VIEWER", this);
        }

        private void accordionControlElement_ParolaDegistir_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new Screens.Security.DlgChangePassword())
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        DMLManager.ShowInfo("Parolanız başarıyla değiştirildi.");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ParolaDegistir error: {0}", ex.Message), "MAIN");
                DMLManager.ShowError(string.Format("Parola değiştirme hatası: {0}", ex.Message));
            }
        }

        // Add Reports Click Handler
        private void accordionControlElement_Raporlar_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.OpenScreen("RAPORLAR", this);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("Raporlar click error: {0}", ex.Message), "MAIN");
            }
        }

        private void accordionControlElement_Satis_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.OpenScreen("SATIS_YAP", this);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("Satis click error: {0}", ex.Message), "MAIN");
            }
        }

        private void accordionControlElement_BarkodTest_Click(object sender, EventArgs e)
        {
            try
            {
                NavigationManager.OpenScreen("BARKOD_TEST", this);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("BarkodTest click error: {0}", ex.Message), "MAIN");
            }
        }

        private void accordionControlElement_Cikis_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show(
                    "Uygulamadan çıkmak istediğinizden emin misiniz?",
                    "Çıkış",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SessionManager.Logout();
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("Cikis error: {0}", ex.Message), "MAIN");
            }
        }

        /// <summary>
        /// Apply modern theme to AccordionControl sidebar
        /// </summary>
        private void ApplyModernSidebarTheme()
        {
            try
            {
                ModernTheme.ApplyModernSidebar(accordionControl1);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ApplyModernSidebarTheme error: {0}", ex.Message), "MAIN");
            }
        }

        /// <summary>
        /// Apply permission-based visibility to menu items
        /// Hides menu items if user doesn't have required permissions
        /// </summary>
        private void ApplyMenuPermissions()
        {
            try
            {
                // Security Management - requires SECURITY_MANAGE permission
                accordionControlElement_SecurityMgmt.Visible =
                    PermissionHelper.CheckPermission("SECURITY_MANAGE", showError: false);

                // Audit Viewer - requires AUDIT_VIEW permission
                accordionControlElement_AuditViewer.Visible =
                    PermissionHelper.CheckPermission("AUDIT_VIEW", showError: false);

                // Password Change - always visible for authenticated users
                accordionControlElement_ParolaDegistir.Visible = SessionManager.IsAuthenticated;

                // Exit - always visible
                accordionControlElement_Cikis.Visible = true;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ApplyMenuPermissions error: {0}", ex.Message), "MAIN");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Helpers;

namespace AktarOtomasyon.Forms.Managers
{
    /// <summary>
    /// Ekran navigasyon yöneticisi.
    /// KURAL: Tüm ekran açma işlemleri buradan yapılır.
    /// KURAL: Aynı ekran birden fazla açılmaz (MDI tab yönetimi).
    /// </summary>
    public static class NavigationManager
    {
        private static readonly Dictionary<string, Type> _screenMap =
            new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Ekran kayıtlarını başlatır.
        /// KURAL: Her yeni ekran buraya RegisterScreen ile eklenmeli.
        /// </summary>
        public static void Initialize()
        {
            // Sprint 1: Dashboard
            RegisterScreen("ANA_DASH", typeof(Screens.Dashboard.FrmANA_DASH));

            // Sprint 2: Urun module
            RegisterScreen("URUN_LISTE", typeof(Screens.Urun.FrmUrunListe));
            RegisterScreen("URUN_KART", typeof(Screens.Urun.FrmUrunKart));
            RegisterScreen("URUN_KATALOG", typeof(Screens.Urun.FrmUrunKatalog));
            RegisterScreen("URUN_DETAY", typeof(Screens.Urun.FrmUrunDetay));

            // Sprint 3: Stok module
            RegisterScreen("STOK_HAREKET", typeof(Screens.Stok.FrmStokHareket));
            RegisterScreen("STOK_KRITIK", typeof(Screens.Stok.FrmStokKritik));

            // Sprint 3: Bildirim module
            RegisterScreen("BILDIRIM_MRK", typeof(Screens.Bildirim.FrmBildirimMrk));

            // Sprint 4: Siparis module
            RegisterScreen("SIP_LISTE", typeof(Screens.Siparis.FrmSiparisListe));
            RegisterScreen("SIP_TASLAK", typeof(Screens.Siparis.FrmSiparisTaslak));
            RegisterScreen("SIP_TESLIM", typeof(Screens.Siparis.FrmSiparisTeslim));

            // Sprint 5: AI module
            RegisterScreen("AI_MODUL", typeof(Screens.Ai.FrmAiModul));
            RegisterScreen("AI_VERSIYON", typeof(Screens.Ai.FrmAiVersiyonlar));
            RegisterScreen("AI_SABLON_YNT", typeof(Screens.Ai.FrmAiSablonYonetim));

            // Sprint 6: Template/Report Management
            RegisterScreen("TEMPLATE_MRK", typeof(Screens.Template.FrmTemplateMrk));
            RegisterScreen("SYS_SETTINGS", typeof(Screens.Template.FrmSystemSettings));

            // Sprint 7: Security & Audit
            RegisterScreen("SEC_YONETIM", typeof(Screens.Security.FrmSecurityManagement));
            RegisterScreen("AUDIT_VIEWER", typeof(Screens.Security.FrmAuditViewer));

            // Sprint 8: System Diagnostics
            RegisterScreen("SYS_DIAG", typeof(Screens.Diagnostics.FrmSYS_DIAG));

            // Sprint 9: Reports (Iteration 3)
            RegisterScreen("RAPORLAR", typeof(Screens.Raporlar.FrmRaporlar));

            // Sprint 10: Sales & Barcode (Iteration 4)
            RegisterScreen("SATIS_YAP", typeof(Screens.Satis.FrmSatis));
            RegisterScreen("BARKOD_TEST", typeof(Screens.Test.FrmBarkodTest));
        }

        /// <summary>
        /// Yeni ekran kaydeder.
        /// </summary>
        /// <param name="ekranKod">Ekran kodu (kul_ekran tablosundaki)</param>
        /// <param name="formType">Form tipi (FrmBase'den türemeli)</param>
        private static void RegisterScreen(string ekranKod, Type formType)
        {
            if (!typeof(FrmBase).IsAssignableFrom(formType))
            {
                throw new InvalidOperationException(
                    string.Format("Form {0} must inherit from FrmBase", formType.Name)
                );
            }

            _screenMap[ekranKod] = formType;
        }

        /// <summary>
        /// Ekran açar veya var olan ekranı aktif eder.
        /// </summary>
        /// <param name="ekranKod">Açılacak ekranın kodu</param>
        /// <param name="mdiParent">MDI parent form (genellikle FrmMain)</param>
        public static void OpenScreen(string ekranKod, Form mdiParent)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ekranKod))
                {
                    MessageBox.Show("Ekran kodu belirtilmedi.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_screenMap.ContainsKey(ekranKod))
                {
                    MessageBox.Show(string.Format("Ekran '{0}' tanımlı değil.", ekranKod), "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // NEW: Permission check before opening screen
                if (!PermissionHelper.CanOpenScreen(ekranKod, showError: true))
                {
                    return; // Error message already shown by PermissionHelper
                }

                // Check if already open
                var existingForm = FindOpenForm(ekranKod, mdiParent);
                if (existingForm != null)
                {
                    existingForm.Activate();
                    return;
                }

                // Create new instance
                var formType = _screenMap[ekranKod];
                var form = (FrmBase)Activator.CreateInstance(formType, ekranKod);
                form.MdiParent = mdiParent;
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("OpenScreen failed for {0}: {1}", ekranKod, ex.Message), "NAV");
                MessageBox.Show(string.Format("Ekran açılırken hata oluştu: {0}", ex.Message), "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ekran açar veya var olan ekranı aktif eder (parametre ile).
        /// </summary>
        /// <param name="ekranKod">Açılacak ekranın kodu</param>
        /// <param name="mdiParent">MDI parent form (genellikle FrmMain)</param>
        /// <param name="parameter">Ekrana geçilecek parametre (örn: UrunId)</param>
        public static void OpenScreen(string ekranKod, Form mdiParent, object parameter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ekranKod))
                {
                    MessageBox.Show("Ekran kodu belirtilmedi.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_screenMap.ContainsKey(ekranKod))
                {
                    MessageBox.Show(string.Format("Ekran '{0}' tanımlı değil.", ekranKod), "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // NEW: Permission check before opening screen
                if (!PermissionHelper.CanOpenScreen(ekranKod, showError: true))
                {
                    return; // Error message already shown by PermissionHelper
                }

                // For URUN_KART and URUN_DETAY, allow multiple instances for different products
                if ((ekranKod == "URUN_KART" || ekranKod == "URUN_DETAY") && parameter != null)
                {
                    var existingForm = FindOpenFormWithParameter(ekranKod, mdiParent, parameter);
                    if (existingForm != null)
                    {
                        existingForm.Activate();
                        return;
                    }
                }
                else
                {
                    // For other screens, prevent duplicates
                    var existingForm = FindOpenForm(ekranKod, mdiParent);
                    if (existingForm != null)
                    {
                        existingForm.Activate();
                        return;
                    }
                }

                // Create new instance with parameter
                var formType = _screenMap[ekranKod];
                FrmBase form;

                if (parameter != null)
                {
                    form = (FrmBase)Activator.CreateInstance(formType, ekranKod, parameter);
                }
                else
                {
                    form = (FrmBase)Activator.CreateInstance(formType, ekranKod);
                }

                form.MdiParent = mdiParent;
                form.Show();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("OpenScreen failed for {0}: {1}", ekranKod, ex.Message), "NAV");
                MessageBox.Show(string.Format("Ekran açılırken hata oluştu: {0}", ex.Message), "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Belirtilen ekran kodu ile açık bir form var mı diye kontrol eder.
        /// </summary>
        private static Form FindOpenForm(string ekranKod, Form mdiParent)
        {
            if (mdiParent == null)
                return null;

            foreach (Form childForm in mdiParent.MdiChildren)
            {
                var baseForm = childForm as FrmBase;
                if (baseForm != null &&
                    string.Equals(baseForm.EkranKod, ekranKod, StringComparison.OrdinalIgnoreCase))
                {
                    return childForm;
                }
            }

            return null;
        }

        /// <summary>
        /// Belirtilen ekran kodu ve parametre ile açık bir form var mı diye kontrol eder.
        /// URUN_KART gibi aynı ekranın birden fazla farklı parametre ile açılabildiği durumlarda kullanılır.
        /// </summary>
        private static Form FindOpenFormWithParameter(string ekranKod, Form mdiParent, object parameter)
        {
            if (mdiParent == null)
                return null;

            foreach (Form childForm in mdiParent.MdiChildren)
            {
                var baseForm = childForm as FrmBase;
                if (baseForm != null &&
                    string.Equals(baseForm.EkranKod, ekranKod, StringComparison.OrdinalIgnoreCase))
                {
                    // URUN_KART için UrunId kontrolü
                    if (ekranKod == "URUN_KART")
                    {
                        var frmUrunKart = childForm as Screens.Urun.FrmUrunKart;
                        if (frmUrunKart != null && frmUrunKart.UrunId == (int?)parameter)
                        {
                            return childForm;
                        }
                    }
                    // URUN_DETAY için UrunId kontrolü
                    else if (ekranKod == "URUN_DETAY")
                    {
                        var frmUrunDetay = childForm as Screens.Urun.FrmUrunDetay;
                        if (frmUrunDetay != null && frmUrunDetay.UrunId == (int)parameter)
                        {
                            return childForm;
                        }
                    }
                }
            }

            return null;
        }
    }
}

using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Common.Service;
using AktarOtomasyon.Urun.Interface;
using AktarOtomasyon.Urun.Service;
using AktarOtomasyon.Stok.Interface;
using AktarOtomasyon.Stok.Service;
using AktarOtomasyon.Siparis.Interface;
using AktarOtomasyon.Siparis.Service;
using AktarOtomasyon.Ai.Interface;
using AktarOtomasyon.Ai.Service;
using AktarOtomasyon.Template.Interface;
using AktarOtomasyon.Template.Service;
using AktarOtomasyon.Security.Interface;
using AktarOtomasyon.Security.Service;
using AktarOtomasyon.Audit.Interface;
using AktarOtomasyon.Audit.Service;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// T�m interface'lere tek noktadan eri�im sa�layan factory.
    /// UI katman� sadece bu factory �zerinden service'lere ula��r.
    /// </summary>
    public static class InterfaceFactory
    {
        // Common
        public static IKulEkranInterface KulEkran { get { return new KulEkranService(); } }
        public static ICommonInterface Common { get { return new CommonService(); } }
        public static IBildirimInterface Bildirim { get { return new BildirimService(); } }
        public static ISystemSettingService SystemSetting { get { return new SystemSettingService(); } }

        // Urun
        public static IUrunInterface Urun { get { return new UrunService(); } }

        // Stok
        public static IStokInterface Stok { get { return new StokService(); } }

        // Siparis
        public static ISiparisInterface Siparis { get { return new SiparisService(); } }

        // AI
        public static IAiInterface Ai { get { return new AiService(); } }

        // Template
        public static ITemplateService Template { get { return new TemplateService(); } }

        // Security & Authentication (Sprint 7)
        public static IAuthService Auth { get { return new AuthService(); } }
        public static ISecurityService Security { get { return new SecurityService(); } }
        public static IAuditService Audit { get { return new AuditService(); } }
    }
}

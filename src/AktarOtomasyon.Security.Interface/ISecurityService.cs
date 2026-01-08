using System.Collections.Generic;
using AktarOtomasyon.Security.Interface.Models.User;
using AktarOtomasyon.Security.Interface.Models.Role;
using AktarOtomasyon.Security.Interface.Models.Permission;
using AktarOtomasyon.Security.Interface.Models.Assignment;

namespace AktarOtomasyon.Security.Interface
{
    /// <summary>
    /// Security service interface for User/Role/Permission management
    /// Comprehensive CRUD operations and assignment management
    /// </summary>
    public interface ISecurityService
    {
        // ==================== User Management ====================
        string KullaniciKaydet(KullaniciModel model);
        List<KullaniciListeItemDto> KullaniciListele(KullaniciFiltre filtre);
        KullaniciModel KullaniciGetir(int kullaniciId);
        string KullaniciPasifle(int kullaniciId, int updatedBy);

        // ==================== Role Management ====================
        string RolKaydet(RolDto model);
        List<RolListeItemDto> RolListele(bool? aktif);
        RolDto RolGetir(int rolId);
        string RolPasifle(int rolId, int updatedBy);

        // ==================== Permission Management ====================
        List<YetkiDto> YetkiListele(string modul = null);
        YetkiDto YetkiGetir(int yetkiId);

        // ==================== User-Role Assignments ====================
        string KullaniciRolEkle(int kullaniciId, int rolId, int createdBy);
        string KullaniciRolSil(int kullaniciId, int rolId, int updatedBy);
        List<KullaniciRolDto> KullaniciRolListele(int kullaniciId);

        // ==================== Role-Permission Assignments ====================
        string RolYetkiEkle(int rolId, int yetkiId, int createdBy);
        string RolYetkiSil(int rolId, int yetkiId, int updatedBy);
        List<RolYetkiDto> RolYetkiListele(int rolId);

        // ==================== Screen-Permission Mappings ====================
        string EkranYetkiEkle(string ekranKod, int yetkiId);
        string EkranYetkiSil(string ekranKod, int yetkiId);
        List<EkranYetkiDto> EkranYetkiListele(string ekranKod);

        // ==================== Effective Permissions (CRITICAL) ====================
        /// <summary>
        /// CRITICAL: Gets user's effective permissions (via all roles)
        /// Used by UI for permission checks
        /// </summary>
        List<YetkiDto> KullaniciYetkiListele(int kullaniciId);

        /// <summary>
        /// CRITICAL: Checks if user has specific permission
        /// Used by UI for permission validation
        /// </summary>
        bool KullaniciYetkiKontrol(int kullaniciId, string yetkiKod);
    }
}

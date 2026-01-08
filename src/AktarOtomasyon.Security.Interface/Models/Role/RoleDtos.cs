using System;

namespace AktarOtomasyon.Security.Interface.Models.Role
{
    /// <summary>
    /// Full role model for create/update operations
    /// </summary>
    public class RolDto
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }

    /// <summary>
    /// Role list item for grid display (includes user/permission counts)
    /// </summary>
    public class RolListeItemDto
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int KullaniciSayisi { get; set; }
        public int YetkiSayisi { get; set; }
    }
}

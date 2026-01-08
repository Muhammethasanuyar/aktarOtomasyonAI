using System;

namespace AktarOtomasyon.Security.Interface.Models.Assignment
{
    /// <summary>
    /// User-Role assignment
    /// </summary>
    public class KullaniciRolDto
    {
        public int KullaniciRolId { get; set; }
        public int KullaniciId { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    /// <summary>
    /// Role-Permission assignment
    /// </summary>
    public class RolYetkiDto
    {
        public int RolYetkiId { get; set; }
        public int RolId { get; set; }
        public int YetkiId { get; set; }
        public string YetkiKod { get; set; }
        public string YetkiAdi { get; set; }
        public string Aciklama { get; set; }
        public string Modul { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    /// <summary>
    /// Screen-Permission mapping
    /// </summary>
    public class EkranYetkiDto
    {
        public int EkranYetkiId { get; set; }
        public string EkranKod { get; set; }
        public int YetkiId { get; set; }
        public string YetkiKod { get; set; }
        public string YetkiAdi { get; set; }
        public string Aciklama { get; set; }
        public string Modul { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

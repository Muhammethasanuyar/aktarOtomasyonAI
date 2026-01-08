using System;

namespace AktarOtomasyon.Security.Interface.Models.Permission
{
    /// <summary>
    /// Permission model (read-only, managed via seed scripts)
    /// </summary>
    public class YetkiDto
    {
        public int YetkiId { get; set; }
        public string YetkiKod { get; set; }
        public string YetkiAdi { get; set; }
        public string Aciklama { get; set; }
        public string Modul { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}

using System;

namespace AktarOtomasyon.Security.Interface.Models.Role
{
    public class RolListeItemDto
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public int KullaniciSayisi { get; set; }
        public int YetkiSayisi { get; set; }
        public bool Aktif { get; set; }
    }
}

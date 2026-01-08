using System;

namespace AktarOtomasyon.Security.Interface.Models.User
{
    public class KullaniciListeItemDto
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Roller { get; set; }
        public bool Aktif { get; set; }
    }
}

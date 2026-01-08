using System;

namespace AktarOtomasyon.Security.Interface.Models.User
{
    public class KullaniciFiltre
    {
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public bool? Aktif { get; set; }
    }
}

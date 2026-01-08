using System;

namespace AktarOtomasyon.Security.Interface.Models.Assignment
{
    public class KullaniciRolDto
    {
        public int KullaniciRolId { get; set; }
        public int KullaniciId { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
    }
}

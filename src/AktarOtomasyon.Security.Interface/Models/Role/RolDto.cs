using System;

namespace AktarOtomasyon.Security.Interface.Models.Role
{
    public class RolDto
    {
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
    }
}

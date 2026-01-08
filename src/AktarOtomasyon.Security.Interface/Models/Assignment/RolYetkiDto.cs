using System;

namespace AktarOtomasyon.Security.Interface.Models.Assignment
{
    public class RolYetkiDto
    {
        public int RolYetkiId { get; set; }
        public int RolId { get; set; }
        public int YetkiId { get; set; }
        public string YetkiKod { get; set; }
        public string YetkiAdi { get; set; }
    }
}

using System;

namespace AktarOtomasyon.Security.Interface.Models.Auth
{
    /// <summary>
    /// Change password DTO - STUB for Sprint 7 Frontend
    /// </summary>
    public class ChangePasswordDto
    {
        public int KullaniciId { get; set; }
        public string EskiParola { get; set; }
        public string YeniParola { get; set; }
    }
}

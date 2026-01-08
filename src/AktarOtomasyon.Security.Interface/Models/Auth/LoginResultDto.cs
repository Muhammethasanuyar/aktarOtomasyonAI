using System;

namespace AktarOtomasyon.Security.Interface.Models.Auth
{
    /// <summary>
    /// Login result DTO - STUB for Sprint 7 Frontend
    /// </summary>
    public class LoginResultDto
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public DateTime? SonGirisTarih { get; set; }
    }
}

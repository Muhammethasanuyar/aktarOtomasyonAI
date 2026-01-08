using System;

namespace AktarOtomasyon.Security.Interface.Models.Auth
{
    /// <summary>
    /// Login request data
    /// </summary>
    public class LoginRequestDto
    {
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
    }

    /// <summary>
    /// Login result with success flag and user info
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

    /// <summary>
    /// Password change request data
    /// </summary>
    public class ChangePasswordDto
    {
        public int KullaniciId { get; set; }
        public string EskiParola { get; set; }
        public string YeniParola { get; set; }
    }
}

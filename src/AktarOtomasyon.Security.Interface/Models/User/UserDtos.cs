using System;

namespace AktarOtomasyon.Security.Interface.Models.User
{
    /// <summary>
    /// Full user model for create/update operations
    /// </summary>
    public class KullaniciModel
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; } // Used only for create/reset, not returned
        public bool Aktif { get; set; }
        public DateTime? SonGirisTarih { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }

    /// <summary>
    /// User list item for grid display (includes role names)
    /// </summary>
    public class KullaniciListeItemDto
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public bool Aktif { get; set; }
        public DateTime? SonGirisTarih { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Roller { get; set; } // Comma-separated role names
    }

    /// <summary>
    /// Filter criteria for user listing
    /// </summary>
    public class KullaniciFiltreDto
    {
        public bool? Aktif { get; set; }
        public string Arama { get; set; }

        public KullaniciFiltreDto()
        {
            Aktif = true; // Default: active users only
        }
    }
}

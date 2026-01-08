using System;

namespace AktarOtomasyon.Stok.Interface
{
    /// <summary>
    /// Stok hareket modeli.
    /// </summary>
    public class StokHareketModel
    {
        public int HareketId { get; set; }
        public int UrunId { get; set; }
        public string HareketTip { get; set; } // GIRIS, CIKIS
        public decimal Miktar { get; set; }
        public DateTime HareketTarih { get; set; }
        public string Aciklama { get; set; }
        public string ReferansTip { get; set; } // e.g., "SIPARIS", "SATIS"
        public int? ReferansId { get; set; } // Reference to source document
        public int? KullaniciId { get; set; } // User who created the movement
    }
}

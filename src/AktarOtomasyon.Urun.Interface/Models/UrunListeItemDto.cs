namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün liste item DTO - Grid/liste görünümleri için
    /// JOIN ile kategori ve birim adlarını içerir
    /// </summary>
    public class UrunListeItemDto
    {
        public int UrunId { get; set; }
        public string UrunKod { get; set; }
        public string UrunAdi { get; set; }
        public int? KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public int? BirimId { get; set; }
        public string BirimAdi { get; set; }
        public decimal? AlisFiyati { get; set; }
        public decimal? SatisFiyati { get; set; }
        public string Barkod { get; set; }
        public bool Aktif { get; set; }
        public string AnaGorselPath { get; set; }
    }
}

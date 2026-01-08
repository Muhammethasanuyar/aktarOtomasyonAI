namespace AktarOtomasyon.Urun.Interface
{
    /// <summary>
    /// Ürün modeli.
    /// </summary>
    public class UrunModel
    {
        public int UrunId { get; set; }
        public string UrunKod { get; set; }
        public string UrunAdi { get; set; }
        public int? KategoriId { get; set; }
        public int? BirimId { get; set; }
        public decimal? AlisFiyati { get; set; }
        public decimal? SatisFiyati { get; set; }
        public string Barkod { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
    }
}

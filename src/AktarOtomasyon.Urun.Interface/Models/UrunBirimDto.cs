namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün birim DTO - Birim yönetimi için
    /// </summary>
    public class UrunBirimDto
    {
        public int BirimId { get; set; }
        public string BirimKod { get; set; }
        public string BirimAdi { get; set; }
        public bool Aktif { get; set; }
        public int UrunSayisi { get; set; }
    }
}

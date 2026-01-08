namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün kategori DTO - Kategori yönetimi için
    /// Hiyerarşik yapı desteği (ust_kategori_id)
    /// </summary>
    public class UrunKategoriDto
    {
        public int KategoriId { get; set; }
        public string KategoriKod { get; set; }
        public string KategoriAdi { get; set; }
        public int? UstKategoriId { get; set; }
        public string UstKategoriAdi { get; set; }
        public bool Aktif { get; set; }
        public int UrunSayisi { get; set; }
    }
}

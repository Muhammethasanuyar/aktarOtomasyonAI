namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün filtreleme DTO - Listele metodları için
    /// </summary>
    public class UrunFiltreDto
    {
        public bool? Aktif { get; set; }
        public int? KategoriId { get; set; }
        public string Arama { get; set; }

        public UrunFiltreDto()
        {
            Aktif = true;
        }
    }
}

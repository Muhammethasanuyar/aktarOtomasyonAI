namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün stok ayarları DTO
    /// Sprint 2 Frontend: Property isimleri daha anlaşılır hale getirildi
    /// </summary>
    public class UrunStokAyarDto
    {
        public int AyarId { get; set; }
        public int UrunId { get; set; }

        /// <summary>
        /// Minimum stok seviyesi - bu seviyenin altına düşünce uyarı verilir
        /// </summary>
        public decimal MinimumStok { get; set; }

        /// <summary>
        /// Hedef stok seviyesi - sipariş verildiğinde bu miktara ulaşmak hedeflenir
        /// </summary>
        public decimal? HedefStok { get; set; }

        /// <summary>
        /// Emniyet stoku - beklenmeyen durumlar için tutulması gereken stok
        /// </summary>
        public decimal EmniyetStoku { get; set; }

        /// <summary>
        /// Tedarik süresi (gün) - sipariş verildikten sonra ürünün gelme süresi
        /// </summary>
        public int? TedarikSuresi { get; set; }
    }
}

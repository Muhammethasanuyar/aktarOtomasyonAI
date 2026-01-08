namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// Bildirim işlemleri interface.
    /// </summary>
    public interface IBildirimInterface
    {
        /// <summary>
        /// Bildirim ekler.
        /// </summary>
        string Ekle(BildirimModel bildirim);

        /// <summary>
        /// Bildirimleri listeler.
        /// </summary>
        System.Collections.Generic.List<BildirimModel> Listele(int? kullaniciId = null, bool? okundu = null);

        /// <summary>
        /// Bildirimi okundu olarak işaretler.
        /// </summary>
        string Okundu(int bildirimId);

        /// <summary>
        /// Okunmamış bildirim sayısını döndürür.
        /// </summary>
        int OkunmamisSayisi(int? kullaniciId = null);
    }
}

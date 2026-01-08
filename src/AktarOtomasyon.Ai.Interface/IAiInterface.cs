using System.Collections.Generic;

namespace AktarOtomasyon.Ai.Interface
{
    /// <summary>
    /// AI işlemleri interface.
    /// KURAL: AI çağrıları yalnız Ai.Service içinde yapılır.
    /// UI doğrudan API key veya provider görmez.
    /// </summary>
    public interface IAiInterface
    {
        /// <summary>
        /// Ürün için AI içerik getirir.
        /// </summary>
        AiIcerikModel IcerikGetir(int urunId);

        /// <summary>
        /// AI içerik kaydeder.
        /// </summary>
        string IcerikKaydet(AiIcerikModel icerik);

        /// <summary>
        /// İçerik versiyonlarını listeler.
        /// </summary>
        List<AiIcerikVersiyonModel> VersiyonListele(int urunId);

        /// <summary>
        /// İçeriği onaylar (Taslak → Aktif).
        /// </summary>
        string IcerikOnayla(int icerikId);

        /// <summary>
        /// AI ile içerik üretir.
        /// </summary>
        /// <param name="urunId">Ürün ID</param>
        /// <param name="sablonKod">Kullanılacak şablon kodu</param>
        /// <returns>Üretilen içerik veya hata</returns>
        AiUretimSonuc IcerikUret(int urunId, string sablonKod);

        // Template management methods
        /// <summary>
        /// AI şablonlarını listeler.
        /// </summary>
        /// <param name="aktif">null=hepsi, true=aktif, false=pasif</param>
        List<AiSablonListModel> SablonListele(bool? aktif = true);

        /// <summary>
        /// Şablon kodu ile şablon getirir.
        /// </summary>
        AiSablonModel SablonGetir(string sablonKod);

        /// <summary>
        /// Şablon kaydeder (yeni veya güncelleme).
        /// </summary>
        string SablonKaydet(AiSablonModel sablon);

        /// <summary>
        /// Şablon siler.
        /// </summary>
        string SablonSil(int sablonId);

        /// <summary>
        /// Şablon aktiflik durumunu günceller.
        /// </summary>
        string SablonAktiflikGuncelle(int sablonId, bool aktif);
    }

    public class AiUretimSonuc
    {
        public bool Basarili { get; set; }
        public string Hata { get; set; }
        public string UretilenIcerik { get; set; }
    }
}

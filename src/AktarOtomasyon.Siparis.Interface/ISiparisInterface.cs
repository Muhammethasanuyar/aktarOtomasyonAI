using System.Collections.Generic;

namespace AktarOtomasyon.Siparis.Interface
{
    public interface ISiparisInterface
    {
        // Core methods
        string TaslakOlustur(int tedarikciId);
        string SatirEkle(SiparisSatirModel satir);
        List<SiparisSatirModel> SatirListele(int siparisId);
        List<SiparisModel> Listele();
        string DurumGuncelle(int siparisId, string durum);

        // Advanced methods (Sprint 4)
        string TaslakOlusturKritikStoktan(int tedarikciId);
        string TeslimAl(int siparisId);

        // Sprint 4 Frontend - Additional methods
        /// <summary>
        /// Taslak sipariş oluşturur ve oluşturulan sipariş bilgilerini döner.
        /// </summary>
        /// <param name="tedarikciId">Tedarikçi ID</param>
        /// <returns>Oluşturulan sipariş modeli (SiparisId ile)</returns>
        SiparisModel TaslakOlusturDetayli(int tedarikciId);

        /// <summary>
        /// Sipariş bilgilerini ID'ye göre getirir.
        /// </summary>
        /// <param name="siparisId">Sipariş ID</param>
        /// <returns>Sipariş modeli</returns>
        SiparisModel Getir(int siparisId);

        /// <summary>
        /// Sipariş başlık bilgilerini günceller (sadece TASLAK durumundayken).
        /// </summary>
        /// <param name="model">Güncellenecek sipariş modeli</param>
        /// <returns>null = başarı, mesaj = hata</returns>
        string GuncelleHeader(SiparisModel model);

        /// <summary>
        /// Sipariş satırını siler.
        /// </summary>
        /// <param name="satirId">Satır ID</param>
        /// <returns>null = başarı, mesaj = hata</returns>
        string SatirSil(int satirId);

        /// <summary>
        /// Sipariş satırını günceller.
        /// </summary>
        /// <param name="satir">Güncellenecek satır modeli</param>
        /// <returns>null = başarı, mesaj = hata</returns>
        string SatirGuncelle(SiparisSatirModel satir);
    }
}

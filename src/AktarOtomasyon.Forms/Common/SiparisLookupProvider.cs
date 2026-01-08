using System;
using System.Collections.Generic;
using AktarOtomasyon.Common.Interface;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Tedarikçi ve sipariş durum lookup verilerini önbellekleyen sağlayıcı.
    /// 30 dakikalık cache süresi ile tekrar eden veritabanı çağrılarını önler.
    /// </summary>
    public static class SiparisLookupProvider
    {
        private static List<TedarikciModel> _tedarikciCache = null;
        private static DateTime? _tedarikciCacheTime = null;
        private static readonly int CacheMinutes = 30;

        /// <summary>
        /// Tedarikçi listesini getirir. İlk çağrıda veya cache süresi dolduğunda veritabanından yükler.
        /// </summary>
        /// <param name="forceRefresh">True ise cache'i atlar ve veritabanından yeniden yükler</param>
        /// <returns>Aktif tedarikçi listesi</returns>
        public static List<TedarikciModel> GetTedarikciListe(bool forceRefresh = false)
        {
            if (forceRefresh ||
                _tedarikciCache == null ||
                !_tedarikciCacheTime.HasValue ||
                DateTime.Now.Subtract(_tedarikciCacheTime.Value).TotalMinutes > CacheMinutes)
            {
                _tedarikciCache = InterfaceFactory.Common.TedarikciListele(aktif: true);
                _tedarikciCacheTime = DateTime.Now;
            }
            return _tedarikciCache ?? new List<TedarikciModel>();
        }

        /// <summary>
        /// Sipariş durum listesini getirir (static enum).
        /// </summary>
        /// <returns>Sipariş durum listesi</returns>
        public static List<SiparisDurumItem> GetSiparisDurumlari()
        {
            return new List<SiparisDurumItem>
            {
                new SiparisDurumItem { Kod = "TASLAK", Tanim = "Taslak" },
                new SiparisDurumItem { Kod = "GONDERILDI", Tanim = "Gönderildi" },
                new SiparisDurumItem { Kod = "KISMI", Tanim = "Kısmi Teslim" },
                new SiparisDurumItem { Kod = "TAMAMLANDI", Tanim = "Tamamlandı" },
                new SiparisDurumItem { Kod = "IPTAL", Tanim = "İptal" }
            };
        }

        /// <summary>
        /// Tüm cache'i temizler. Yeni tedarikçi eklendiğinde çağrılabilir.
        /// </summary>
        public static void ClearCache()
        {
            _tedarikciCache = null;
            _tedarikciCacheTime = null;
        }
    }

    /// <summary>
    /// Sipariş durum bilgilerini tutan yardımcı sınıf.
    /// </summary>
    public class SiparisDurumItem
    {
        public string Kod { get; set; }
        public string Tanim { get; set; }
    }
}

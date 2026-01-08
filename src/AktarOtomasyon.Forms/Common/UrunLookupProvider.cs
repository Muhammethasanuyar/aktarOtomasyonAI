using System;
using System.Collections.Generic;
using AktarOtomasyon.Urun.Interface.Models;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Kategori, birim ve ürün lookup verilerini önbellekleyen sağlayıcı.
    /// 30 dakikalık cache süresi ile tekrar eden veritabanı çağrılarını önler.
    /// </summary>
    public static class UrunLookupProvider
    {
        private static List<UrunKategoriDto> _kategoriCache = null;
        private static List<UrunBirimDto> _birimCache = null;
        private static List<UrunListeItemDto> _urunCache = null;
        private static DateTime? _kategoriCacheTime = null;
        private static DateTime? _birimCacheTime = null;
        private static DateTime? _urunCacheTime = null;
        private static readonly int CacheMinutes = 30;

        /// <summary>
        /// Kategori listesini getirir. İlk çağrıda veya cache süresi dolduğunda veritabanından yükler.
        /// </summary>
        /// <param name="forceRefresh">True ise cache'i atlar ve veritabanından yeniden yükler</param>
        /// <returns>Aktif kategori listesi</returns>
        public static List<UrunKategoriDto> GetKategoriler(bool forceRefresh = false)
        {
            if (forceRefresh ||
                _kategoriCache == null ||
                !_kategoriCacheTime.HasValue ||
                DateTime.Now.Subtract(_kategoriCacheTime.Value).TotalMinutes > CacheMinutes)
            {
                _kategoriCache = InterfaceFactory.Urun.KategoriListele(aktif: true);
                
                // Sprint 9: Fix encoding issues
                if (_kategoriCache != null)
                {
                    foreach (var item in _kategoriCache)
                    {
                        item.KategoriAdi = TextHelper.FixEncoding(item.KategoriAdi);
                    }
                }

                _kategoriCacheTime = DateTime.Now;
            }
            return _kategoriCache;
        }

        /// <summary>
        /// Birim listesini getirir. İlk çağrıda veya cache süresi dolduğunda veritabanından yükler.
        /// </summary>
        /// <param name="forceRefresh">True ise cache'i atlar ve veritabanından yeniden yükler</param>
        /// <returns>Aktif birim listesi</returns>
        public static List<UrunBirimDto> GetBirimler(bool forceRefresh = false)
        {
            if (forceRefresh ||
                _birimCache == null ||
                !_birimCacheTime.HasValue ||
                DateTime.Now.Subtract(_birimCacheTime.Value).TotalMinutes > CacheMinutes)
            {
                _birimCache = InterfaceFactory.Urun.BirimListele(aktif: true);

                // Sprint 9: Fix encoding issues (same pattern as GetKategoriler)
                if (_birimCache != null)
                {
                    foreach (var item in _birimCache)
                    {
                        item.BirimAdi = TextHelper.FixEncoding(item.BirimAdi);
                    }
                }

                _birimCacheTime = DateTime.Now;
            }
            return _birimCache;
        }

        /// <summary>
        /// Ürün listesini getirir. İlk çağrıda veya cache süresi dolduğunda veritabanından yükler.
        /// </summary>
        /// <param name="forceRefresh">True ise cache'i atlar ve veritabanından yeniden yükler</param>
        /// <returns>Aktif ürün listesi</returns>
        public static List<UrunListeItemDto> GetUrunListe(bool forceRefresh = false)
        {
            if (forceRefresh ||
                _urunCache == null ||
                !_urunCacheTime.HasValue ||
                DateTime.Now.Subtract(_urunCacheTime.Value).TotalMinutes > CacheMinutes)
            {
                var filtre = new UrunFiltreDto { Aktif = true };
                _urunCache = InterfaceFactory.Urun.Listele(filtre);
                _urunCacheTime = DateTime.Now;
            }
            return _urunCache ?? new List<UrunListeItemDto>();
        }

        /// <summary>
        /// Tüm cache'i temizler. Yeni kategori/birim/ürün eklendiğinde çağrılabilir.
        /// </summary>
        public static void ClearCache()
        {
            _kategoriCache = null;
            _birimCache = null;
            _urunCache = null;
            _kategoriCacheTime = null;
            _birimCacheTime = null;
            _urunCacheTime = null;
        }
    }
}

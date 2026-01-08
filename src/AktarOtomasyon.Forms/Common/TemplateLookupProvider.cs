using System;
using System.Collections.Generic;
using System.Linq;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Template yönetimi için lookup verilerini önbellekleyen sağlayıcı.
    /// Modül listesi ve storage mode seçeneklerini cache'ler.
    /// </summary>
    public static class TemplateLookupProvider
    {
        private static List<string> _modulListesiCache = null;
        private static DateTime? _modulCacheTime = null;
        private static readonly int CacheMinutes = 30;

        /// <summary>
        /// Unique modül listesini template'lerden çekerek döndürür.
        /// İlk çağrıda veya cache süresi dolduğunda veritabanından yükler.
        /// </summary>
        /// <param name="forceRefresh">True ise cache'i atlar ve veritabanından yeniden yükler</param>
        /// <returns>Benzersiz modül listesi (alfabetik sıralı)</returns>
        public static List<string> GetModulListesi(bool forceRefresh = false)
        {
            if (forceRefresh ||
                _modulListesiCache == null ||
                !_modulCacheTime.HasValue ||
                DateTime.Now.Subtract(_modulCacheTime.Value).TotalMinutes > CacheMinutes)
            {
                try
                {
                    // Template'lerden unique modül listesi çek
                    var templates = InterfaceFactory.Template.TemplateListele();
                    _modulListesiCache = templates
                        .Select(t => t.Modul)
                        .Distinct()
                        .OrderBy(m => m)
                        .ToList();
                    _modulCacheTime = DateTime.Now;
                }
                catch
                {
                    // Hata durumunda default modüller döndür
                    _modulListesiCache = new List<string> { "Common", "Siparis", "Urun", "Stok", "System" };
                }
            }

            return _modulListesiCache ?? new List<string>();
        }

        /// <summary>
        /// Storage mode seçeneklerini döndürür (static, cache'e gerek yok).
        /// </summary>
        /// <returns>FileSystem ve Database seçenekleri</returns>
        public static List<string> GetStorageModeListesi()
        {
            return new List<string> { "FileSystem", "Database" };
        }

        /// <summary>
        /// Tüm cache'i temizler. Yeni template eklendiğinde çağrılabilir.
        /// </summary>
        public static void ClearCache()
        {
            _modulListesiCache = null;
            _modulCacheTime = null;
        }
    }
}

using System;
using System.Text;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Metin işlemleri için yardımcı sınıf.
    /// Encoding sorunlarını düzeltmek için kullanılır.
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// Bozuk karakterli (UTF-8 byte'ları Windows-1252 olarak okunmuş) metinleri düzeltir.
        /// Örnek: "DoÄŸal" (C4 9F) -> "Doğal"
        /// </summary>
        /// <param name="text">Düzeltilecek metin</param>
        /// <returns>Düzeltilmiş metin</returns>
        public static string FixEncoding(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            try
            {
                // 1. Windows-1252 (Latin1) Encoding
                // Bu adımda ExceptionFallback önemli: Eğer metin Latin1'de olmayan karakterler içeriyorsa (örn Çince, Emoji vb)
                // bunları "bozuk UTF-8" silsilesi sanıp çevirmeye çalışmak veriyi bozar. O yüzden hata verdirtip orijinalini koruyoruz.
                var latin1 = Encoding.GetEncoding(1252, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

                // 2. UTF-8 Encoding (with Exception Fallback)
                // Bu adım KİLİT NOKTA: Varsayılan UTF8 yerine, hata fırlatan UTF8 kullanıyoruz.
                // Eğer byte dizisi geçerli bir UTF-8 değilse (örneğin "Ç" harfinin byte'ı tek başına geçersizdir),
                // anında hata fırlatır ve catch bloğuna düşeriz. Böylece "Kahve ve Çay" bozulmaz.
                var utf8StrictMode = Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);

                var bytes = latin1.GetBytes(text);
                var decoded = utf8StrictMode.GetString(bytes);

                return decoded;
            }
            catch
            {
                // Herhangi bir aşamada (Latin1'e çevirirken veya UTF-8 diye okurken) hata alırsak
                // metin "çift kodlanmış" (double encoded) değil demektir.
                // Orijinal halini olduğu gibi döndür.
                return text;
            }
        }
    }
}

using System.Collections.Generic;

namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// Ortak işlemler için interface.
    /// </summary>
    public interface ICommonInterface
    {
        /// <summary>
        /// Veritabanı bağlantı testi yapar.
        /// </summary>
        /// <returns>null = başarı, mesaj = hata</returns>
        string BaglantıTest();

        /// <summary>
        /// Aktif tedarikçileri listeler.
        /// </summary>
        /// <param name="aktif">Aktiflik durumu filtresi (null = tümü, true = sadece aktif, false = sadece pasif)</param>
        /// <returns>Tedarikçi listesi</returns>
        List<TedarikciModel> TedarikciListele(bool? aktif = true);

        /// <summary>
        /// Tedarikçi kaydeder veya günceller.
        /// </summary>
        /// <param name="tedarikci">Tedarikçi modeli (TedarikciId null ise yeni kayıt, değilse güncelleme)</param>
        /// <returns>null = başarı, mesaj = hata</returns>
        string TedarikciKaydet(TedarikciModel tedarikci);
    }
}

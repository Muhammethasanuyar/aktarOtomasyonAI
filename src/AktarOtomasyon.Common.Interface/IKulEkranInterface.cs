namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// kul_ekran tablosu işlemleri için interface.
    /// Ekran yönetimi ve versiyon loglama.
    /// </summary>
    public interface IKulEkranInterface
    {
        /// <summary>
        /// Ekran koduna göre ekran bilgisini getirir.
        /// </summary>
        /// <param name="ekranKod">Ekran kodu</param>
        /// <returns>Ekran bilgisi veya null</returns>
        KulEkranModel EkranGetir(string ekranKod);

        /// <summary>
        /// Ekran açılışında versiyon loglar.
        /// </summary>
        /// <param name="ekranKod">Ekran kodu</param>
        /// <param name="versiyon">Uygulama versiyonu</param>
        /// <param name="kullaniciId">Kullanıcı ID (opsiyonel)</param>
        /// <returns>null = başarı, mesaj = hata</returns>
        string VersiyonLogla(string ekranKod, string versiyon, int? kullaniciId = null);
    }
}

using System;
using System.Windows.Forms;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// DML (Data Manipulation Language) işlemleri için standart yönetici.
    /// Kaydet, Sil, Güncelle işlemlerini tek noktadan yönetir.
    /// UI'da Interface çağrısından dönen hataları standart şekilde işler.
    /// </summary>
    public static class DMLManager
    {
        /// <summary>
        /// Interface çağrısından dönen sonucu işler.
        /// Hata varsa kullanıcıya gösterir ve false döndürür.
        /// Başarılı ise true döndürür.
        /// </summary>
        /// <param name="hata">Service'ten dönen hata mesajı (null = başarı)</param>
        /// <param name="basariMesaji">Başarı durumunda gösterilecek mesaj (opsiyonel)</param>
        /// <returns>İşlem başarılı ise true, hata varsa false</returns>
        public static bool IslemKontrol(string hata, string basariMesaji = null)
        {
            if (hata != null)
            {
                MessageBox.Show(
                    hata,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (!string.IsNullOrEmpty(basariMesaji))
            {
                MessageBox.Show(
                    basariMesaji,
                    "Bilgi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            return true;
        }

        /// <summary>
        /// Kaydetme işlemi için standart kontrol.
        /// </summary>
        public static bool KaydetKontrol(string hata)
        {
            return IslemKontrol(hata, "Kayıt başarıyla tamamlandı.");
        }

        /// <summary>
        /// Silme işlemi için standart kontrol.
        /// </summary>
        public static bool SilKontrol(string hata)
        {
            return IslemKontrol(hata, "Silme işlemi başarıyla tamamlandı.");
        }

        /// <summary>
        /// Güncelleme işlemi için standart kontrol.
        /// </summary>
        public static bool GuncelleKontrol(string hata)
        {
            return IslemKontrol(hata, "Güncelleme başarıyla tamamlandı.");
        }

        /// <summary>
        /// Silme öncesi onay sorar.
        /// </summary>
        public static bool SilmeOnayAl(string mesaj = "Bu kaydı silmek istediğinizden emin misiniz?")
        {
            var result = MessageBox.Show(
                mesaj,
                "Silme Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Hata mesajı gösterir.
        /// </summary>
        public static void ShowError(string mesaj)
        {
            MessageBox.Show(mesaj, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Bilgi mesajı gösterir.
        /// </summary>
        public static void ShowInfo(string mesaj)
        {
            MessageBox.Show(mesaj, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Uyarı mesajı gösterir.
        /// </summary>
        public static void ShowWarning(string mesaj)
        {
            MessageBox.Show(mesaj, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

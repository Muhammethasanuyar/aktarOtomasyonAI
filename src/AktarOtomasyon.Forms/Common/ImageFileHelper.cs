using System;
using System.Collections.Generic;
using System.IO;
using AktarOtomasyon.Forms.Managers;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Geçici görsel dosya yönetimi için yardımcı sınıf.
    /// Yeni ürün oluşturulurken görsellerin geçici klasörde saklanmasını sağlar.
    /// </summary>
    public static class ImageFileHelper
    {
        private const string TEMP_FOLDER_BASE = "App_Data\\Images\\Temp";
        private const string PERMANENT_FOLDER_BASE = "App_Data\\Images\\Urunler";

        /// <summary>
        /// Yeni ürün için benzersiz geçici klasör oluşturur
        /// </summary>
        /// <returns>Tuple: (sessionGuid, fullTempPath) veya hata durumunda null</returns>
        public static Tuple<string, string> CreateTempImageFolder()
        {
            try
            {
                string sessionGuid = Guid.NewGuid().ToString();
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = Path.Combine(baseDir, TEMP_FOLDER_BASE, sessionGuid);

                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                return new Tuple<string, string>(sessionGuid, tempPath);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("CreateTempImageFolder error: {0}", ex.Message), "IMAGE_HELPER");
                return null;
            }
        }

        /// <summary>
        /// Görsel dosyasını geçici klasöre kopyalar
        /// </summary>
        /// <param name="sourceFilePath">Kaynak dosya yolu</param>
        /// <param name="tempFolderPath">Geçici klasör yolu</param>
        /// <returns>Geçici klasördeki yeni dosya yolu veya hata durumunda null</returns>
        public static string CopyToTempFolder(string sourceFilePath, string tempFolderPath)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                    return null;

                string extension = Path.GetExtension(sourceFilePath);
                string newFileName = Guid.NewGuid().ToString() + extension;
                string targetPath = Path.Combine(tempFolderPath, newFileName);

                File.Copy(sourceFilePath, targetPath, true);
                return targetPath;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("CopyToTempFolder error: {0}", ex.Message), "IMAGE_HELPER");
                return null;
            }
        }

        /// <summary>
        /// Geçici klasördeki tüm görselleri ürünün kalıcı klasörüne taşır
        /// </summary>
        /// <param name="tempGuid">Geçici klasör GUID'i</param>
        /// <param name="urunId">Ürün ID'si</param>
        /// <returns>Taşınan dosyalar listesi: (tempPath, relativePermanentPath)</returns>
        public static List<Tuple<string, string>> MoveTempImagesToPermanent(string tempGuid, int urunId)
        {
            var movedFiles = new List<Tuple<string, string>>();

            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = Path.Combine(baseDir, TEMP_FOLDER_BASE, tempGuid);

                if (!Directory.Exists(tempPath))
                    return movedFiles;

                // Kalıcı klasörü oluştur
                string permanentRelative = Path.Combine(PERMANENT_FOLDER_BASE, urunId.ToString());
                string permanentPath = Path.Combine(baseDir, permanentRelative);

                if (!Directory.Exists(permanentPath))
                    Directory.CreateDirectory(permanentPath);

                // Tüm dosyaları taşı
                string[] files = Directory.GetFiles(tempPath);
                foreach (string tempFile in files)
                {
                    string fileName = Path.GetFileName(tempFile);
                    string permanentFile = Path.Combine(permanentPath, fileName);

                    File.Move(tempFile, permanentFile);

                    // Veritabanı için relative path döndür
                    string relativePath = Path.Combine(permanentRelative, fileName);
                    movedFiles.Add(new Tuple<string, string>(tempFile, relativePath));
                }

                // Başarılı taşıma sonrası geçici klasörü temizle
                CleanupTempFolder(tempGuid);

                return movedFiles;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("MoveTempImagesToPermanent error: {0}", ex.Message), "IMAGE_HELPER");
                return movedFiles;
            }
        }

        /// <summary>
        /// Geçici klasörü ve içeriğini siler
        /// </summary>
        /// <param name="tempGuid">Geçici klasör GUID'i</param>
        public static void CleanupTempFolder(string tempGuid)
        {
            try
            {
                if (string.IsNullOrEmpty(tempGuid))
                    return;

                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string tempPath = Path.Combine(baseDir, TEMP_FOLDER_BASE, tempGuid);

                if (Directory.Exists(tempPath))
                    Directory.Delete(tempPath, true);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("CleanupTempFolder error: {0}", ex.Message), "IMAGE_HELPER");
            }
        }

        /// <summary>
        /// Geçici klasörden belirli bir dosyayı siler
        /// </summary>
        /// <param name="tempFilePath">Silinecek dosya yolu</param>
        public static void RemoveTempFile(string tempFilePath)
        {
            try
            {
                if (File.Exists(tempFilePath))
                    File.Delete(tempFilePath);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("RemoveTempFile error: {0}", ex.Message), "IMAGE_HELPER");
            }
        }
    }
}

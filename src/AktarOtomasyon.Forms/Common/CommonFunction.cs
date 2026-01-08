using System;
using System.Configuration;
using System.IO;

namespace AktarOtomasyon.Forms.Common
{
    /// <summary>
    /// Ortak yardımcı fonksiyonlar.
    /// Report ve Template path'leri tek noktadan yönetilir.
    /// </summary>
    public static class CommonFunction
    {
        /// <summary>
        /// Gets setting value with priority: Environment Var → DB → App.config
        /// </summary>
        public static string GetConfigValue(string key, string defaultValue = null)
        {
            return AktarOtomasyon.Common.Service.SystemSettingService.GetSettingValue(key, defaultValue);
        }

        /// <summary>
        /// Rapor dizini yolunu döndürür (priority-based config).
        /// Priority: Environment Variable → DB system_setting → App.config
        /// </summary>
        public static string GetReportDirectoryPath()
        {
            var path = GetConfigValue("ReportPath", ".\\reports");

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            return path;
        }

        /// <summary>
        /// Şablon dizini yolunu döndürür (priority-based config).
        /// Priority: Environment Variable → DB system_setting → App.config
        /// </summary>
        public static string GetTemplateDirectoryPath()
        {
            var path = GetConfigValue("TemplatePath", ".\\templates");

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            return path;
        }

        /// <summary>
        /// Uygulama sürümünü döndürür.
        /// </summary>
        public static string GetAppVersion()
        {
            return ConfigurationManager.AppSettings["AppVersion"] ?? "1.0.0";
        }
    }
}

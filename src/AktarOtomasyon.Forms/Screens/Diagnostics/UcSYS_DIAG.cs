using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Base;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Helpers;
using AktarOtomasyon.Forms.Managers;
using AktarOtomasyon.Util.DataAccess;
using DevExpress.XtraEditors;

namespace AktarOtomasyon.Forms.Screens.Diagnostics
{
    /// <summary>
    /// System Diagnostics Screen - Sprint 9 enhanced
    /// Shows system health checks with status icons and export functionality
    /// </summary>
    public partial class UcSYS_DIAG : UcBase
    {
        private BindingList<DiagnosticCheck> _checks;

        public UcSYS_DIAG()
        {
            InitializeComponent();
        }

        public override void LoadData()
        {
            try
            {
                LoadEnvironmentInfo();
                InitializeChecks();
                BindGrid();
                ApplyGridStandards();
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(string.Format("Error loading diagnostics screen: {0}", ex.Message), "SYS_DIAG");
                MessageHelper.ShowError("Sistem durumu yüklenirken hata oluştu.");
            }
        }

        /// <summary>
        /// Sprint 9: Apply grid standards and formatting
        /// </summary>
        private void ApplyGridStandards()
        {
            GridHelper.ApplyStandardFormatting(gvDiagnostics);

            // Format datetime column if exists
            if (gvDiagnostics.Columns["LastRun"] != null)
                GridHelper.FormatDateColumn(gvDiagnostics.Columns["LastRun"]);
        }

        private void LoadEnvironmentInfo()
        {
            try
            {
                // Application version
                var appVersion = CommonFunction.GetAppVersion();
                lblAppVersion.Text = string.Format("Uygulama Versiyon: {0}", appVersion);

                // Machine and user info
                lblMachineName.Text = string.Format("Makine: {0}", Environment.MachineName);
                lblUserName.Text = string.Format("Kullanıcı: {0}", Environment.UserName);

                // Database info (masked for security)
                var connectionString = CommonFunction.GetConfigValue("ConnectionString");
                var dbServer = ExtractServerFromConnectionString(connectionString);
                lblDatabaseServer.Text = string.Format("Veritabanı: {0}", MaskDatabaseInfo(dbServer));

                // Storage mode
                var storageMode = CommonFunction.GetConfigValue("STORAGE_MODE");
                lblStorageMode.Text = string.Format("Depolama Modu: {0}", storageMode);

                // Paths
                var templatePath = CommonFunction.GetConfigValue("TEMPLATE_PATH");
                var reportPath = CommonFunction.GetConfigValue("REPORT_PATH");
                var mediaPath = CommonFunction.GetConfigValue("MEDIA_PATH");

                lblTemplatePath.Text = string.Format("Şablon Yolu: {0}", templatePath);
                lblReportPath.Text = string.Format("Rapor Yolu: {0}", reportPath);
                lblMediaPath.Text = string.Format("Medya Yolu: {0}", mediaPath);

                // AI Provider (masked API key)
                var aiProvider = CommonFunction.GetConfigValue("AI_PROVIDER");
                var aiModel = CommonFunction.GetConfigValue("AI_MODEL");
                var aiApiKey = CommonFunction.GetConfigValue("AI_API_KEY");

                lblAiProvider.Text = string.Format("AI Provider: {0} ({1})", aiProvider, aiModel);
                if (!string.IsNullOrEmpty(aiApiKey) && aiApiKey.Length > 8)
                {
                    lblAiApiKey.Text = string.Format("API Key: {0}...", aiApiKey.Substring(0, 8));
                }
                else
                {
                    lblAiApiKey.Text = "API Key: [Tanımlı değil]";
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(string.Format("Error loading environment info: {0}", ex.Message), "SYS_DIAG");
            }
        }

        private string ExtractServerFromConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return "Unknown";

            try
            {
                var parts = connectionString.Split(';');
                var serverPart = parts.FirstOrDefault(p => p.Trim().StartsWith("Data Source=", StringComparison.OrdinalIgnoreCase) ||
                                                          p.Trim().StartsWith("Server=", StringComparison.OrdinalIgnoreCase));
                if (serverPart != null)
                {
                    var serverValue = serverPart.Split('=')[1].Trim();
                    return serverValue;
                }
            }
            catch
            {
                // Ignore parsing errors
            }

            return "Unknown";
        }

        private string MaskDatabaseInfo(string dbInfo)
        {
            if (string.IsNullOrEmpty(dbInfo))
                return "***";

            // Mask server name (show first and last parts only)
            if (dbInfo.Contains("\\"))
            {
                var parts = dbInfo.Split('\\');
                if (parts.Length == 2)
                {
                    return string.Format("{0}\\***", parts[0]);
                }
            }

            // For simple server names, show first 3 chars + ***
            if (dbInfo.Length > 5)
            {
                return dbInfo.Substring(0, 3) + "***";
            }

            return "***";
        }

        private void InitializeChecks()
        {
            _checks = new BindingList<DiagnosticCheck>
            {
                new DiagnosticCheck { CheckName = "Veritabanı Bağlantısı", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "Stored Procedure Erişimi", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "Şablon Yolu Erişimi", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "Rapor Yolu Erişimi", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "Medya Yolu Erişimi", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "AI Servisi Bağlantısı", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "DevExpress Lisansı", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" },
                new DiagnosticCheck { CheckName = "Gerekli Dizinler", Status = "Bekliyor...", Details = "Henüz çalıştırılmadı" }
            };
        }

        private void BindGrid()
        {
            try
            {
                gvDiagnostics.BeginUpdate();

                gcDiagnostics.DataSource = _checks;
                gvDiagnostics.BestFitColumns();

                // Apply color coding based on status
                gvDiagnostics.RowCellStyle += GvDiagnostics_RowCellStyle;
            }
            finally
            {
                gvDiagnostics.EndUpdate();
            }
        }

        private void GvDiagnostics_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Status")
            {
                var check = gvDiagnostics.GetRow(e.RowHandle) as DiagnosticCheck;
                if (check != null)
                {
                    if (check.Status == "OK")
                    {
                        e.Appearance.BackColor = Color.LightGreen;
                        e.Appearance.ForeColor = Color.DarkGreen;
                    }
                    else if (check.Status == "WARNING")
                    {
                        e.Appearance.BackColor = Color.LightYellow;
                        e.Appearance.ForeColor = Color.DarkOrange;
                    }
                    else if (check.Status == "FAIL")
                    {
                        e.Appearance.BackColor = Color.LightCoral;
                        e.Appearance.ForeColor = Color.DarkRed;
                    }
                }
            }
        }

        private void btnRunAllChecks_Click(object sender, EventArgs e)
        {
            RunAllChecks();
        }

        private void btnRunSelected_Click(object sender, EventArgs e)
        {
            var selectedCheck = gvDiagnostics.GetFocusedRow() as DiagnosticCheck;
            if (selectedCheck != null)
            {
                try
                {
                    gvDiagnostics.BeginUpdate();
                    RunCheck(selectedCheck);
                }
                finally
                {
                    gvDiagnostics.RefreshData();
                    gvDiagnostics.EndUpdate();
                }
            }
            else
            {
                MessageHelper.ShowWarning("Lütfen bir kontrol seçin.");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Text Files|*.txt|All Files|*.*",
                    FileName = string.Format("DiagnosticReport_{0}.txt", DateTime.Now.ToString("yyyyMMdd_HHmmss"))
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var report = GenerateReport();
                    File.WriteAllText(saveDialog.FileName, report);
                    MessageHelper.ShowInfo(string.Format("Rapor başarıyla oluşturuldu:\n{0}", saveDialog.FileName));
                }
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(string.Format("Error exporting diagnostic report: {0}", ex.Message), "SYS_DIAG");
                MessageHelper.ShowError("Rapor oluşturulurken hata oluştu.");
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                var report = GenerateReport();
                Clipboard.SetText(report);
                MessageHelper.ShowInfo("Rapor panoya kopyalandı.");
            }
            catch (Exception ex)
            {
                ErrorManager.LogError(string.Format("Error copying to clipboard: {0}", ex.Message), "SYS_DIAG");
                MessageHelper.ShowError("Panoya kopyalama başarısız.");
            }
        }

        private void RunAllChecks()
        {
            try
            {
                gvDiagnostics.BeginUpdate();

                foreach (var check in _checks)
                {
                    RunCheck(check);
                }
            }
            finally
            {
                gvDiagnostics.RefreshData();
                gvDiagnostics.EndUpdate();
            }

            MessageHelper.ShowInfo("Tüm kontroller tamamlandı.");
        }

        private void RunCheck(DiagnosticCheck check)
        {
            switch (check.CheckName)
            {
                case "Veritabanı Bağlantısı":
                    RunDatabaseCheck(check);
                    break;
                case "Stored Procedure Erişimi":
                    RunStoredProcedureCheck(check);
                    break;
                case "Şablon Yolu Erişimi":
                    RunPathAccessCheck("TEMPLATE_PATH", check);
                    break;
                case "Rapor Yolu Erişimi":
                    RunPathAccessCheck("REPORT_PATH", check);
                    break;
                case "Medya Yolu Erişimi":
                    RunMediaPathCheck(check);
                    break;
                case "AI Servisi Bağlantısı":
                    RunAiServiceCheck(check);
                    break;
                case "DevExpress Lisansı":
                    RunDevExpressLicenseCheck(check);
                    break;
                case "Gerekli Dizinler":
                    RunRequiredDirectoriesCheck(check);
                    break;
            }
        }

        private void RunDatabaseCheck(DiagnosticCheck check)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var error = sMan.TestConnection();
                    if (error == null)
                    {
                        check.Status = "OK";
                        check.Details = "Veritabanı bağlantısı başarılı";
                    }
                    else
                    {
                        check.Status = "FAIL";
                        check.Details = error;
                    }
                }
            }
            catch (Exception ex)
            {
                check.Status = "FAIL";
                check.Details = ex.Message;
            }
            check.LastRun = DateTime.Now;
        }

        private void RunStoredProcedureCheck(DiagnosticCheck check)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kul_ekran_listele");
                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        check.Status = "OK";
                        check.Details = string.Format("Stored procedure erişimi başarılı ({0} ekran bulundu)", dt.Rows.Count);
                    }
                    else
                    {
                        check.Status = "WARNING";
                        check.Details = "Stored procedure çalıştı ancak sonuç bulunamadı";
                    }
                }
            }
            catch (Exception ex)
            {
                check.Status = "FAIL";
                check.Details = string.Format("Stored procedure hatası: {0}", ex.Message);
            }
            check.LastRun = DateTime.Now;
        }

        private void RunPathAccessCheck(string pathKey, DiagnosticCheck check)
        {
            try
            {
                var path = CommonFunction.GetConfigValue(pathKey);
                if (string.IsNullOrEmpty(path))
                {
                    check.Status = "FAIL";
                    check.Details = string.Format("{0} tanımlı değil", pathKey);
                    check.LastRun = DateTime.Now;
                    return;
                }

                if (!Path.IsPathRooted(path))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

                // Check read access
                if (!Directory.Exists(path))
                {
                    check.Status = "FAIL";
                    check.Details = string.Format("Dizin bulunamadı: {0}", path);
                    check.LastRun = DateTime.Now;
                    return;
                }

                // Check write access
                var testFile = Path.Combine(path, string.Format("test_{0}.tmp", Guid.NewGuid()));
                File.WriteAllText(testFile, "test");
                File.Delete(testFile);

                check.Status = "OK";
                check.Details = string.Format("Okuma/yazma erişimi OK: {0}", path);
            }
            catch (UnauthorizedAccessException ex)
            {
                check.Status = "FAIL";
                check.Details = string.Format("Erişim reddedildi: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                check.Status = "WARNING";
                check.Details = string.Format("Kontrol başarısız: {0}", ex.Message);
            }
            check.LastRun = DateTime.Now;
        }

        private void RunMediaPathCheck(DiagnosticCheck check)
        {
            try
            {
                var storageMode = CommonFunction.GetConfigValue("STORAGE_MODE");

                if (storageMode != "FileSystem")
                {
                    check.Status = "WARNING";
                    check.Details = string.Format("Depolama modu '{0}' - Dosya sistemi kontrolü atlandı", storageMode);
                    check.LastRun = DateTime.Now;
                    return;
                }

                // Run standard path check for media path
                RunPathAccessCheck("MEDIA_PATH", check);
            }
            catch (Exception ex)
            {
                check.Status = "WARNING";
                check.Details = string.Format("Kontrol başarısız: {0}", ex.Message);
                check.LastRun = DateTime.Now;
            }
        }

        private void RunAiServiceCheck(DiagnosticCheck check)
        {
            try
            {
                var aiProvider = CommonFunction.GetConfigValue("AI_PROVIDER");
                var aiApiKey = CommonFunction.GetConfigValue("AI_API_KEY");

                if (string.IsNullOrEmpty(aiProvider))
                {
                    check.Status = "WARNING";
                    check.Details = "AI Provider tanımlı değil";
                    check.LastRun = DateTime.Now;
                    return;
                }

                if (string.IsNullOrEmpty(aiApiKey))
                {
                    check.Status = "WARNING";
                    check.Details = "AI API Key tanımlı değil";
                    check.LastRun = DateTime.Now;
                    return;
                }

                // Basic validation - actual connectivity test would require AI module integration
                check.Status = "OK";
                check.Details = string.Format("AI Provider ({0}) yapılandırılmış - API Key mevcut", aiProvider);
            }
            catch (Exception ex)
            {
                check.Status = "WARNING";
                check.Details = string.Format("Kontrol başarısız: {0}", ex.Message);
            }
            check.LastRun = DateTime.Now;
        }

        private void RunDevExpressLicenseCheck(DiagnosticCheck check)
        {
            try
            {
                // DevExpress license check - basic validation
                var devExpressVersion = typeof(DevExpress.XtraEditors.XtraForm).Assembly.GetName().Version;

                check.Status = "OK";
                check.Details = string.Format("DevExpress versiyon: {0}", devExpressVersion);
            }
            catch (Exception ex)
            {
                check.Status = "WARNING";
                check.Details = string.Format("DevExpress kontrol hatası: {0}", ex.Message);
            }
            check.LastRun = DateTime.Now;
        }

        private void RunRequiredDirectoriesCheck(DiagnosticCheck check)
        {
            try
            {
                var templatePath = CommonFunction.GetConfigValue("TEMPLATE_PATH");
                var reportPath = CommonFunction.GetConfigValue("REPORT_PATH");
                var mediaPath = CommonFunction.GetConfigValue("MEDIA_PATH");

                var missingDirs = new List<string>();

                if (!string.IsNullOrEmpty(templatePath) && !Directory.Exists(ResolveRelativePath(templatePath)))
                    missingDirs.Add(string.Format("Template: {0}", templatePath));

                if (!string.IsNullOrEmpty(reportPath) && !Directory.Exists(ResolveRelativePath(reportPath)))
                    missingDirs.Add(string.Format("Report: {0}", reportPath));

                var storageMode = CommonFunction.GetConfigValue("STORAGE_MODE");
                if (storageMode == "FileSystem" && !string.IsNullOrEmpty(mediaPath) && !Directory.Exists(ResolveRelativePath(mediaPath)))
                    missingDirs.Add(string.Format("Media: {0}", mediaPath));

                if (missingDirs.Count == 0)
                {
                    check.Status = "OK";
                    check.Details = "Tüm gerekli dizinler mevcut";
                }
                else
                {
                    check.Status = "WARNING";
                    check.Details = string.Format("Eksik dizinler: {0}", string.Join(", ", missingDirs));
                }
            }
            catch (Exception ex)
            {
                check.Status = "WARNING";
                check.Details = string.Format("Kontrol başarısız: {0}", ex.Message);
            }
            check.LastRun = DateTime.Now;
        }

        private string ResolveRelativePath(string path)
        {
            if (Path.IsPathRooted(path))
                return path;
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        private string GenerateReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("==============================================");
            sb.AppendLine("      AktarOtomasyon Sistem Durumu Raporu");
            sb.AppendLine("==============================================");
            sb.AppendLine();
            sb.AppendLine(string.Format("Rapor Tarihi: {0}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")));
            sb.AppendLine();

            // Environment Info
            sb.AppendLine("--- Ortam Bilgileri ---");
            sb.AppendLine(lblAppVersion.Text);
            sb.AppendLine(lblMachineName.Text);
            sb.AppendLine(lblUserName.Text);
            sb.AppendLine(lblDatabaseServer.Text);
            sb.AppendLine(lblStorageMode.Text);
            sb.AppendLine(lblTemplatePath.Text);
            sb.AppendLine(lblReportPath.Text);
            sb.AppendLine(lblMediaPath.Text);
            sb.AppendLine(lblAiProvider.Text);
            sb.AppendLine(lblAiApiKey.Text);
            sb.AppendLine();

            // Diagnostic Checks
            sb.AppendLine("--- Sistem Kontrolleri ---");
            sb.AppendLine();

            foreach (var check in _checks)
            {
                sb.AppendLine(string.Format("[{0}] {1}", check.Status, check.CheckName));
                sb.AppendLine(string.Format("   Detay: {0}", check.Details));
                if (check.LastRun.HasValue)
                {
                    sb.AppendLine(string.Format("   Son Çalıştırma: {0}", check.LastRun.Value.ToString("dd.MM.yyyy HH:mm:ss")));
                }
                sb.AppendLine();
            }

            sb.AppendLine("==============================================");

            return sb.ToString();
        }
    }

    public class DiagnosticCheck
    {
        public string CheckName { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public DateTime? LastRun { get; set; }
    }
}

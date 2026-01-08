using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Helpers;
using AktarOtomasyon.Forms.Managers;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors; // Added for WindowsFormsSettings

namespace AktarOtomasyon.Forms
{
    /// <summary>
    /// Uygulama giriş noktası
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Ana giriş noktası
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Global exception handlers
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // SPRINT 8: Load environment variables from .env file (if exists)
            // NOTE: Requires DotNetEnv NuGet package to be installed
            // Run: Install-Package DotNetEnv -Version 3.0.0
            LoadEnvironmentVariables();

            // SPRINT 8: Validate configuration before proceeding (fail fast)
            var validationResult = ConfigurationValidator.Validate();

            // Show warnings (non-critical issues)
            if (validationResult.Warnings.Count > 0)
            {
                var warningMsg = new StringBuilder();
                warningMsg.AppendLine("Uyarılar:");
                warningMsg.AppendLine();
                foreach (var warning in validationResult.Warnings)
                {
                    warningMsg.AppendLine("• " + warning);
                }

                ErrorManager.LogMessage(warningMsg.ToString(), "STARTUP");
            }

            // Show errors and exit if validation failed
            if (!validationResult.IsValid)
            {
                var errorMsg = new StringBuilder();
                errorMsg.AppendLine("Kritik yapılandırma hataları tespit edildi:");
                errorMsg.AppendLine();
                foreach (var error in validationResult.Errors)
                {
                    errorMsg.AppendLine("• " + error);
                }
                errorMsg.AppendLine();
                errorMsg.AppendLine("Lütfen yapılandırmayı kontrol edin ve uygulamayı yeniden başlatın.");

                MessageBox.Show(errorMsg.ToString(), "Yapılandırma Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                ErrorManager.LogMessage(errorMsg.ToString(), "STARTUP");
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set DevExpress theme
            // SPRINT 9: Modern UI Update (Windows 11 Style)
            // "WXI" skin is vector-based, modern, and supports clean aesthetics (Rounded corners, extra padding)
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.Skins.SkinManager.EnableMdiFormSkins();

            // Enable DirectX Hardware Acceleration for smoother rendering
            WindowsFormsSettings.ForceDirectXPaint();
            
            // Set Font globally (Segoe UI is standard for modern Windows apps)
            WindowsFormsSettings.DefaultFont = Common.ModernTheme.Typography.Body;
            WindowsFormsSettings.DefaultMenuFont = Common.ModernTheme.Typography.Body;

            // Enable animations and transitions for modern web-like experience
            WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            WindowsFormsSettings.AllowAutoScale = DevExpress.Utils.DefaultBoolean.True;
            WindowsFormsSettings.EnableFormSkins();
            
            // Animation settings for smooth transitions
            WindowsFormsSettings.DefaultLookAndFeel.UseDefaultLookAndFeel = true;
            
            // Apply WXI Skin
            UserLookAndFeel.Default.SetSkinStyle("WXI");

            // Apply Blue Theme Customization and Animations
            ApplyBlueTheme();
            ApplyAnimations();

            // Initialize NavigationManager (register all screens)
            NavigationManager.Initialize();

            // Show login form FIRST
            using (var loginForm = new Screens.Security.FrmLogin())
            {
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    // User cancelled or login failed - exit application
                    return;
                }
            }

            // Verify authentication succeeded
            if (!SessionManager.IsAuthenticated)
            {
                MessageBox.Show("Oturum açılamadı. Uygulama kapatılıyor.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Authentication successful - show main form
            Application.Run(new FrmMain());
        }

        private static void Application_ThreadException(object sender,
            System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorManager.HandleGlobalException(e.Exception, true);
        }

        private static void CurrentDomain_UnhandledException(object sender,
            UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                ErrorManager.HandleGlobalException(ex, false);
            }
        }

        /// <summary>
        /// Apply blue theme customization to DevExpress components
        /// </summary>
        private static void ApplyBlueTheme()
        {
            try
            {
                // Mavi tonları: Primary: #2196F3, Secondary: #64B5F6, Accent: #1976D2
                var primaryBlue = Color.FromArgb(33, 150, 243);      // #2196F3
                var secondaryBlue = Color.FromArgb(100, 181, 246);  // #64B5F6
                var accentBlue = Color.FromArgb(25, 118, 210);      // #1976D2
                var lightBlue = Color.FromArgb(227, 242, 253);     // #E3F2FD

                // Button appearance customization
                WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("WXI");
                
                // Note: Button styling is handled per-button using ButtonHelper class
                // Global button colors are applied through the WXI skin theme
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("ApplyBlueTheme error: " + ex.Message, "STARTUP");
            }
        }

        /// <summary>
        /// Apply animations and transitions for modern web-like experience
        /// </summary>
        private static void ApplyAnimations()
        {
            try
            {
                // Enable smooth scrolling
                WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
                
                // Enable form animations
                WindowsFormsSettings.EnableFormSkins();
                WindowsFormsSettings.EnableMdiFormSkins();
                
                // Enable transition effects
                WindowsFormsSettings.AllowAutoScale = DevExpress.Utils.DefaultBoolean.True;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage("ApplyAnimations error: " + ex.Message, "STARTUP");
            }
        }

        /// <summary>
        /// Loads environment variables from .env file (Sprint 8)
        /// Falls back gracefully if DotNetEnv package is not installed
        /// </summary>
        private static void LoadEnvironmentVariables()
        {
            try
            {
                var envPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ".env");

                if (File.Exists(envPath))
                {
                    // FUTURE: Use DotNetEnv to load .env file
                    // This requires DotNetEnv NuGet package: Install-Package DotNetEnv
                    // DotNetEnv.Env.Load(envPath);
                    ErrorManager.LogMessage(".env file found but DotNetEnv package not installed. Using system environment variables.", "STARTUP");
                }
                else
                {
                    ErrorManager.LogMessage(
                        "No .env file found. Using App.config and system environment variables.",
                        "STARTUP");
                }
            }
            catch (TypeLoadException)
            {
                // DotNetEnv package not installed - gracefully fall back to system env vars
                ErrorManager.LogMessage(
                    "DotNetEnv package not found. Install with: Install-Package DotNetEnv",
                    "STARTUP");
            }
            catch (Exception ex)
            {
                // Log but don't fail startup if .env loading fails
                ErrorManager.LogMessage(
                    string.Format("Warning: Failed to load .env file: {0}", ex.Message),
                    "STARTUP");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AktarOtomasyon.Forms.Common;
using AktarOtomasyon.Forms.Managers;

namespace AktarOtomasyon.Forms.Helpers
{
    /// <summary>
    /// Helper class for permission checks across UI
    /// Simplifies permission management in forms
    /// </summary>
    public static class PermissionHelper
    {
        /// <summary>
        /// Check if current user has specific permission
        /// Optionally shows error message if denied
        /// </summary>
        /// <param name="yetkiKod">Permission code to check</param>
        /// <param name="showError">If true, shows error message when permission denied</param>
        /// <returns>True if user has permission, false otherwise</returns>
        public static bool CheckPermission(string yetkiKod, bool showError = true)
        {
            try
            {
                // Check authentication first
                if (!SessionManager.IsAuthenticated)
                {
                    if (showError)
                    {
                        DMLManager.ShowWarning("Oturum açmanız gerekmektedir.");
                    }
                    return false;
                }

                // Check permission
                bool hasPermission = SessionManager.HasPermission(yetkiKod);

                if (!hasPermission && showError)
                {
                    DMLManager.ShowWarning(string.Format("Bu işlem için '{0}' yetkisine sahip değilsiniz.", yetkiKod));
                    ErrorManager.LogMessage(string.Format("Permission denied: {0} for user {1}", yetkiKod, SessionManager.KullaniciAdi), "PERMISSION");
                }

                return hasPermission;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("CheckPermission failed for {0}: {1}", yetkiKod, ex.Message), "PERMISSION");

                if (showError)
                {
                    DMLManager.ShowError(string.Format("Yetki kontrolü sırasında hata oluştu: {0}", ex.Message));
                }

                // Deny by default on error
                return false;
            }
        }

        /// <summary>
        /// Get required permissions for specific screen
        /// </summary>
        /// <param name="ekranKod">Screen code</param>
        /// <returns>List of permission codes required for screen</returns>
        public static List<string> GetScreenPermissions(string ekranKod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ekranKod))
                    return new List<string>();

                // Query screen permissions via InterfaceFactory
                var ekranYetkiler = InterfaceFactory.Security.EkranYetkiListele(ekranKod);

                if (ekranYetkiler == null || ekranYetkiler.Count == 0)
                    return new List<string>();

                return ekranYetkiler.Select(ey => ey.YetkiKod).ToList();
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("GetScreenPermissions failed for {0}: {1}", ekranKod, ex.Message), "PERMISSION");
                return new List<string>();
            }
        }

        /// <summary>
        /// Check if user can open specific screen
        /// Returns true if no permissions required OR user has at least one required permission
        /// </summary>
        /// <param name="ekranKod">Screen code</param>
        /// <param name="showError">If true, shows error message when access denied</param>
        /// <returns>True if user can open screen, false otherwise</returns>
        public static bool CanOpenScreen(string ekranKod, bool showError = true)
        {
            try
            {
                if (!SessionManager.IsAuthenticated)
                {
                    if (showError)
                    {
                        DMLManager.ShowWarning("Oturum açmanız gerekmektedir.");
                    }
                    return false;
                }

                // Get required permissions for this screen
                var requiredPermissions = GetScreenPermissions(ekranKod);

                // If no permissions required, allow access
                if (requiredPermissions == null || requiredPermissions.Count == 0)
                {
                    return true;
                }

                // Check if user has ANY of the required permissions
                bool hasAccess = requiredPermissions.Any(perm => SessionManager.HasPermission(perm));

                if (!hasAccess && showError)
                {
                    DMLManager.ShowWarning(string.Format("Bu ekranı açmak için yetkiniz bulunmamaktadır.\n\nEkran: {0}\nGerekli yetkiler: {1}", ekranKod, string.Join(", ", requiredPermissions)));
                    ErrorManager.LogMessage(string.Format("Screen access denied: {0} for user {1}", ekranKod, SessionManager.KullaniciAdi), "PERMISSION");
                }

                return hasAccess;
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("CanOpenScreen failed for {0}: {1}", ekranKod, ex.Message), "PERMISSION");

                if (showError)
                {
                    DMLManager.ShowError(string.Format("Ekran yetki kontrolü sırasında hata oluştu: {0}", ex.Message));
                }

                // Deny by default on error
                return false;
            }
        }

        /// <summary>
        /// Apply permissions to controls in a form
        /// Disables controls if user doesn't have required permission
        /// </summary>
        /// <param name="parent">Parent control (Form or UserControl)</param>
        /// <param name="yetkiKod">Permission code</param>
        public static void ApplyPermissions(Control parent, string yetkiKod)
        {
            if (parent == null || string.IsNullOrWhiteSpace(yetkiKod))
                return;

            try
            {
                bool hasPermission = SessionManager.HasPermission(yetkiKod);

                // Recursively disable controls
                ApplyPermissionsRecursive(parent, hasPermission);
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("ApplyPermissions failed: {0}", ex.Message), "PERMISSION");
            }
        }

        /// <summary>
        /// Recursively apply permissions to all child controls
        /// </summary>
        private static void ApplyPermissionsRecursive(Control control, bool hasPermission)
        {
            if (control == null)
                return;

            // Don't disable labels and read-only controls
            bool isEditableControl = !(control is Label) &&
                                    !(control is GroupBox) &&
                                    !(control is Panel);

            if (isEditableControl && control is Button)
            {
                control.Enabled = hasPermission;
            }
            else if (isEditableControl && control is TextBox)
            {
                ((TextBox)control).ReadOnly = !hasPermission;
            }

            // Recurse into child controls
            foreach (Control child in control.Controls)
            {
                ApplyPermissionsRecursive(child, hasPermission);
            }
        }

        /// <summary>
        /// Check if user has any of the specified permissions
        /// </summary>
        /// <param name="yetkiKodlar">List of permission codes</param>
        /// <returns>True if user has at least one permission</returns>
        public static bool HasAnyPermission(params string[] yetkiKodlar)
        {
            if (!SessionManager.IsAuthenticated)
                return false;

            if (yetkiKodlar == null || yetkiKodlar.Length == 0)
                return false;

            return yetkiKodlar.Any(yk => SessionManager.HasPermission(yk));
        }

        /// <summary>
        /// Check if user has all of the specified permissions
        /// </summary>
        /// <param name="yetkiKodlar">List of permission codes</param>
        /// <returns>True if user has all permissions</returns>
        public static bool HasAllPermissions(params string[] yetkiKodlar)
        {
            if (!SessionManager.IsAuthenticated)
                return false;

            if (yetkiKodlar == null || yetkiKodlar.Length == 0)
                return true;

            return yetkiKodlar.All(yk => SessionManager.HasPermission(yk));
        }
    }
}

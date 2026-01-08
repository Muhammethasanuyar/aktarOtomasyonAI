using System;
using System.Collections.Generic;
using System.Linq;
using AktarOtomasyon.Security.Interface;
using AktarOtomasyon.Security.Interface.Models.Auth;
using AktarOtomasyon.Security.Interface.Models.Permission;
using AktarOtomasyon.Forms.Common;

namespace AktarOtomasyon.Forms.Managers
{
    /// <summary>
    /// Static class to store current user session information
    /// Thread-safe singleton pattern for session management
    /// </summary>
    public static class SessionManager
    {
        private static readonly object _lock = new object();
        private static List<YetkiDto> _cachedPermissions = null;

        #region Properties

        /// <summary>
        /// Current logged-in user ID
        /// </summary>
        public static int KullaniciId { get; private set; }

        /// <summary>
        /// Current logged-in username
        /// </summary>
        public static string KullaniciAdi { get; private set; }

        /// <summary>
        /// Current logged-in user's full name
        /// </summary>
        public static string AdSoyad { get; private set; }

        /// <summary>
        /// Current logged-in user's email
        /// </summary>
        public static string Email { get; private set; }

        /// <summary>
        /// Current logged-in user's last login date
        /// </summary>
        public static DateTime? SonGirisTarih { get; private set; }

        /// <summary>
        /// Indicates whether user is authenticated
        /// </summary>
        public static bool IsAuthenticated { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize session with login result
        /// </summary>
        /// <param name="result">Login result from AuthService</param>
        public static void Login(LoginResultDto result)
        {
            if (result == null || !result.Success)
            {
                throw new ArgumentException("Invalid login result");
            }

            lock (_lock)
            {
                KullaniciId = result.KullaniciId;
                KullaniciAdi = result.KullaniciAdi;
                AdSoyad = result.AdSoyad;
                Email = result.Email;
                SonGirisTarih = result.SonGirisTarih;
                IsAuthenticated = true;

                // Load and cache permissions
                LoadPermissions();

                ErrorManager.LogMessage(string.Format("User logged in: {0}", KullaniciAdi), "SESSION");
            }
        }

        /// <summary>
        /// Clear current session and logout user
        /// </summary>
        public static void Logout()
        {
            lock (_lock)
            {
                ErrorManager.LogMessage(string.Format("User logged out: {0}", KullaniciAdi), "SESSION");

                KullaniciId = 0;
                KullaniciAdi = null;
                AdSoyad = null;
                Email = null;
                SonGirisTarih = null;
                IsAuthenticated = false;
                _cachedPermissions = null;
            }
        }

        /// <summary>
        /// Check if current user has specific permission
        /// Cached for performance
        /// </summary>
        /// <param name="yetkiKod">Permission code to check</param>
        /// <returns>True if user has permission, false otherwise</returns>
        public static bool HasPermission(string yetkiKod)
        {
            if (!IsAuthenticated)
                return false;

            if (string.IsNullOrWhiteSpace(yetkiKod))
                return false;

            lock (_lock)
            {
                // Load permissions if not cached
                if (_cachedPermissions == null)
                {
                    LoadPermissions();
                }

                // Check in cached permissions
                if (_cachedPermissions != null)
                {
                    return _cachedPermissions.Any(y => y.YetkiKod.Equals(yetkiKod, StringComparison.OrdinalIgnoreCase));
                }

                return false;
            }
        }

        /// <summary>
        /// Get all cached permissions for current user
        /// </summary>
        /// <returns>List of permissions</returns>
        public static List<YetkiDto> GetCachedPermissions()
        {
            lock (_lock)
            {
                if (_cachedPermissions == null)
                {
                    LoadPermissions();
                }

                return _cachedPermissions ?? new List<YetkiDto>();
            }
        }

        /// <summary>
        /// Reload permissions from database (use after role/permission changes)
        /// </summary>
        public static void RefreshPermissions()
        {
            lock (_lock)
            {
                LoadPermissions();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Load user permissions from backend and cache them
        /// </summary>
        private static void LoadPermissions()
        {
            if (!IsAuthenticated)
            {
                _cachedPermissions = new List<YetkiDto>();
                return;
            }

            try
            {
                // Load effective permissions via SecurityService
                _cachedPermissions = InterfaceFactory.Security.KullaniciYetkiListele(KullaniciId);

                if (_cachedPermissions == null)
                {
                    _cachedPermissions = new List<YetkiDto>();
                }

                ErrorManager.LogMessage(string.Format("Loaded {0} permissions for user {1}", _cachedPermissions.Count, KullaniciAdi), "SESSION");
            }
            catch (Exception ex)
            {
                ErrorManager.LogMessage(string.Format("LoadPermissions failed: {0}", ex.Message), "SESSION");
                _cachedPermissions = new List<YetkiDto>();
            }
        }

        #endregion
    }
}

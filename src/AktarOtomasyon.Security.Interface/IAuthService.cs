using System;
using AktarOtomasyon.Security.Interface.Models.Auth;

namespace AktarOtomasyon.Security.Interface
{
    /// <summary>
    /// Authentication service interface
    /// Handles login, password changes, and session management
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates user with username and password using PBKDF2
        /// </summary>
        /// <param name="request">Login credentials</param>
        /// <returns>LoginResultDto with Success flag and user info</returns>
        LoginResultDto Login(LoginRequestDto request);

        /// <summary>
        /// Updates user's last login timestamp
        /// Called after successful authentication
        /// </summary>
        /// <param name="kullaniciId">User ID</param>
        /// <returns>null on success, error message on failure</returns>
        string UpdateLastLogin(int kullaniciId);

        /// <summary>
        /// Changes user password (requires old password verification)
        /// Used when user changes their own password
        /// </summary>
        /// <param name="dto">Password change data</param>
        /// <returns>null on success, error message on failure</returns>
        string ChangePassword(ChangePasswordDto dto);

        /// <summary>
        /// Resets user password (admin only, no old password required)
        /// Used when admin resets a user's forgotten password
        /// </summary>
        /// <param name="kullaniciId">User ID to reset password for</param>
        /// <param name="yeniParola">New password (will be hashed)</param>
        /// <param name="resetBy">Admin user ID performing the reset</param>
        /// <returns>null on success, error message on failure</returns>
        string ResetPassword(int kullaniciId, string yeniParola, int resetBy);
    }
}

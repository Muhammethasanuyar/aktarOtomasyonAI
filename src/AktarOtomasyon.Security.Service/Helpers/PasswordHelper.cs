using System;
using System.Security.Cryptography;

namespace AktarOtomasyon.Security.Service.Helpers
{
    /// <summary>
    /// PBKDF2-based password hashing helper
    /// Implements secure password hashing with salt and constant-time comparison
    /// </summary>
    public static class PasswordHelper
    {
        private const int SaltSize = 32; // 256 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 10000; // PBKDF2 iterations

        /// <summary>
        /// Hashes a password using PBKDF2 with a random salt
        /// </summary>
        /// <param name="password">Plain text password</param>
        /// <param name="hash">Base64-encoded hash output</param>
        /// <param name="salt">Base64-encoded salt output</param>
        public static void HashPassword(string password, out string hash, out string salt)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // Generate 32-byte random salt
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            // Hash with PBKDF2 (10,000 iterations)
            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
            {
                hashBytes = pbkdf2.GetBytes(HashSize);
            }

            // Return as base64
            hash = Convert.ToBase64String(hashBytes);
            salt = Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Verifies a password against stored hash and salt
        /// Uses constant-time comparison to prevent timing attacks
        /// </summary>
        /// <param name="password">Plain text password to verify</param>
        /// <param name="storedHash">Base64-encoded stored hash</param>
        /// <param name="storedSalt">Base64-encoded stored salt</param>
        /// <returns>True if password matches</returns>
        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                return false;

            try
            {
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                byte[] storedHashBytes = Convert.FromBase64String(storedHash);

                // Hash input password with stored salt
                byte[] computedHashBytes;
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations))
                {
                    computedHashBytes = pbkdf2.GetBytes(HashSize);
                }

                // Constant-time comparison (prevent timing attacks)
                return ConstantTimeCompare(storedHashBytes, computedHashBytes);
            }
            catch
            {
                // Invalid base64 or other errors
                return false;
            }
        }

        /// <summary>
        /// Constant-time byte array comparison
        /// Prevents timing attacks by ensuring comparison always takes the same time
        /// </summary>
        /// <param name="a">First byte array</param>
        /// <param name="b">Second byte array</param>
        /// <returns>True if arrays are equal</returns>
        private static bool ConstantTimeCompare(byte[] a, byte[] b)
        {
            if (a == null || b == null)
                return false;

            if (a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }
    }
}

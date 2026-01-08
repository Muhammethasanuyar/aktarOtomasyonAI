using System;
using System.Security.Cryptography;

namespace AktarOtomasyon.Tools
{
    /// <summary>
    /// Utility to generate PBKDF2 hash for admin password
    /// Run this to get the correct hash/salt for seed file
    /// </summary>
    class GenerateAdminHash
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("Aktar Otomasyon - Admin Password Hash Generator");
            Console.WriteLine("===========================================");
            Console.WriteLine();

            string password = "Admin123!";

            Console.WriteLine("Generating PBKDF2 hash for password: " + password);
            Console.WriteLine();

            string hash, salt;
            HashPassword(password, out hash, out salt);

            Console.WriteLine("RESULTS:");
            Console.WriteLine("--------");
            Console.WriteLine();
            Console.WriteLine("Password: " + password);
            Console.WriteLine();
            Console.WriteLine("Salt (Base64):");
            Console.WriteLine(salt);
            Console.WriteLine();
            Console.WriteLine("Hash (Base64):");
            Console.WriteLine(hash);
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine("UPDATE SEED FILE:");
            Console.WriteLine("===========================================");
            Console.WriteLine();
            Console.WriteLine("Copy these values to: db/seed/008_sprint7_security_seed.sql");
            Console.WriteLine();
            Console.WriteLine("DECLARE @sample_salt NVARCHAR(256) = '" + salt + "'");
            Console.WriteLine("DECLARE @sample_hash NVARCHAR(512) = '" + hash + "'");
            Console.WriteLine();
            Console.WriteLine("===========================================");
            Console.WriteLine();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private const int SaltSize = 32; // 256 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 10000; // PBKDF2 iterations

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
    }
}

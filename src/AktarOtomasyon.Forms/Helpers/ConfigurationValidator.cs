using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Forms.Helpers
{
    /// <summary>
    /// Validates application configuration on startup (Sprint 8).
    /// KURAL: Fail fast if critical config is missing.
    /// </summary>
    public static class ConfigurationValidator
    {
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public List<string> Errors { get; set; }
            public List<string> Warnings { get; set; }

            public ValidationResult()
            {
                Errors = new List<string>();
                Warnings = new List<string>();
            }
        }

        /// <summary>
        /// Validates all critical configuration on startup.
        /// </summary>
        public static ValidationResult Validate()
        {
            var result = new ValidationResult { IsValid = true };

            // 1. Database Connection
            ValidateDatabaseConnection(result);

            // 2. Required Directories
            ValidateRequiredDirectories(result);

            // 3. AI Configuration (warnings only, not critical)
            ValidateAiConfiguration(result);

            // 4. Environment Variable
            ValidateEnvironment(result);

            result.IsValid = result.Errors.Count == 0;
            return result;
        }

        private static void ValidateDatabaseConnection(ValidationResult result)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var error = sMan.TestConnection();
                    if (error != null)
                    {
                        result.Errors.Add(string.Format("Database connection failed: {0}", error));
                    }
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(string.Format("Database configuration error: {0}", ex.Message));
            }
        }

        private static void ValidateRequiredDirectories(ValidationResult result)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var requiredDirs = new[] { "logs", "reports", "templates", "media" };

            foreach (var dir in requiredDirs)
            {
                var fullPath = Path.Combine(baseDir, dir);
                if (!Directory.Exists(fullPath))
                {
                    try
                    {
                        Directory.CreateDirectory(fullPath);
                        result.Warnings.Add(string.Format("Created missing directory: {0}", dir));
                    }
                    catch (Exception ex)
                    {
                        result.Errors.Add(string.Format("Cannot create directory '{0}': {1}", dir, ex.Message));
                    }
                }
            }

            // Validate media/products subdirectory
            var mediaProductsPath = Path.Combine(baseDir, "media", "products");
            if (!Directory.Exists(mediaProductsPath))
            {
                try
                {
                    Directory.CreateDirectory(mediaProductsPath);
                }
                catch (Exception ex)
                {
                    result.Errors.Add(string.Format("Cannot create media/products directory: {0}", ex.Message));
                }
            }
        }

        private static void ValidateAiConfiguration(ValidationResult result)
        {
            var aiProvider = Environment.GetEnvironmentVariable("AI_PROVIDER")
                           ?? ConfigurationManager.AppSettings["AI_PROVIDER"];

            var aiApiKey = Environment.GetEnvironmentVariable("AI_API_KEY")
                         ?? ConfigurationManager.AppSettings["AI_API_KEY"];

            if (string.IsNullOrEmpty(aiProvider))
            {
                result.Warnings.Add("AI_PROVIDER not configured. AI features will be unavailable.");
            }

            if (string.IsNullOrEmpty(aiApiKey))
            {
                result.Warnings.Add("AI_API_KEY not configured. AI features will be unavailable.");
            }
        }

        private static void ValidateEnvironment(ValidationResult result)
        {
            var env = Environment.GetEnvironmentVariable("ENVIRONMENT")
                    ?? ConfigurationManager.AppSettings["ENVIRONMENT"]
                    ?? "Development";

            var validEnvs = new[] { "Development", "Staging", "Production" };
            if (!validEnvs.Contains(env))
            {
                result.Warnings.Add(string.Format(
                    "Invalid ENVIRONMENT value '{0}'. Expected: {1}",
                    env, string.Join(", ", validEnvs)));
            }
        }
    }
}

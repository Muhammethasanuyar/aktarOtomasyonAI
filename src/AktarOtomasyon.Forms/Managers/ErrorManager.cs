using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AktarOtomasyon.Forms.Managers
{
    /// <summary>
    /// Sprint 8: Log severity levels for structured logging
    /// </summary>
    public enum LogLevel
    {
        DEBUG = 0,      // Development only - verbose debugging info
        INFO = 1,       // Normal operations - informational messages
        WARNING = 2,    // Potential issues - warnings that need attention
        ERROR = 3,      // Errors requiring attention - handled exceptions
        CRITICAL = 4    // System-breaking errors - unhandled exceptions
    }

    /// <summary>
    /// Global hata yönetimi ve loglama.
    /// KURAL: Tüm global exception'lar buradan geçer.
    /// KURAL: Log dosyaları logs/ klasörüne yazılır.
    /// KURAL: Sprint 8 - PII ve secret veriler log'a yazılmadan redact edilir.
    /// KURAL: Sprint 8 - Structured log levels ile filtering.
    /// </summary>
    public static class ErrorManager
    {
        private static readonly string LogDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "logs"
        );

        private static LogLevel? _minimumLogLevel = null;

        /// <summary>
        /// Sprint 8: Minimum log level from configuration (env var or App.config).
        /// Logs below this level are not written (filtered out).
        /// Default: INFO (production should use INFO or WARNING)
        /// </summary>
        private static LogLevel MinimumLogLevel
        {
            get
            {
                if (_minimumLogLevel.HasValue)
                    return _minimumLogLevel.Value;

                // Read from environment variable first, then App.config
                var logLevelStr = Environment.GetEnvironmentVariable("LOG_LEVEL")
                                ?? ConfigurationManager.AppSettings["LOG_LEVEL"]
                                ?? "INFO";  // Default to INFO

                LogLevel level;
                if (Enum.TryParse(logLevelStr, true, out level))
                {
                    _minimumLogLevel = level;
                }
                else
                {
                    _minimumLogLevel = LogLevel.INFO;  // Fallback to INFO if invalid
                }

                return _minimumLogLevel.Value;
            }
        }

        /// <summary>
        /// Global exception'ları yakalar, loglar ve kullanıcıya gösterir.
        /// </summary>
        /// <param name="ex">Yakalanan exception</param>
        /// <param name="isThreadException">UI thread exception mı?</param>
        public static void HandleGlobalException(Exception ex, bool isThreadException)
        {
            try
            {
                LogException(ex, isThreadException);

                var message = "Beklenmeyen bir hata oluştu. Lütfen sistem yöneticisine başvurunuz.\n\n" +
                             string.Format("Hata Kodu: {0:yyyyMMddHHmmss}", DateTime.Now);

                MessageBox.Show(message, "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Kritik sistem hatası. Uygulama kapatılacak.", "Kritik Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exception'ı dosyaya loglar (Sprint 8: with PII redaction).
        /// </summary>
        private static void LogException(Exception ex, bool isThreadException)
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                var logFile = Path.Combine(LogDirectory, string.Format("error_{0:yyyyMMdd}.txt", DateTime.Now));

                var logEntry = new System.Text.StringBuilder();
                logEntry.AppendLine("=".PadRight(80, '='));
                logEntry.AppendLine(string.Format("Timestamp: {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
                logEntry.AppendLine(string.Format("Exception Type: {0}", (isThreadException ? "UI Thread" : "Unhandled Domain")));

                // Sprint 8: Redact sensitive data from exception messages
                logEntry.AppendLine(string.Format("Message: {0}", RedactSensitiveData(ex.Message ?? "")));
                logEntry.AppendLine(string.Format("Source: {0}", ex.Source ?? ""));
                logEntry.AppendLine("Stack Trace:");
                logEntry.AppendLine(RedactSensitiveData(ex.StackTrace ?? ""));

                if (ex.InnerException != null)
                {
                    logEntry.AppendLine();
                    logEntry.AppendLine("Inner Exception:");
                    logEntry.AppendLine(string.Format("Message: {0}", RedactSensitiveData(ex.InnerException.Message ?? "")));
                    logEntry.AppendLine(string.Format("Stack Trace: {0}", RedactSensitiveData(ex.InnerException.StackTrace ?? "")));
                }

                logEntry.AppendLine("=".PadRight(80, '='));
                logEntry.AppendLine();

                File.AppendAllText(logFile, logEntry.ToString());
            }
            catch
            {
                // Silent fail - logging hatası uygulamayı durdurmamalı
            }
        }

        /// <summary>
        /// Uygulama mesajlarını loglar (hata olmayan durumlar için).
        /// Sprint 8: Structured logging with log levels and filtering.
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="source">Mesajın kaynağı (örn: "NAV", "MAIN", "STARTUP")</param>
        /// <param name="level">Log severity level (default: INFO)</param>
        public static void LogMessage(string message, string source = null, LogLevel level = LogLevel.INFO)
        {
            try
            {
                // Sprint 8: Filter logs below minimum level
                if (level < MinimumLogLevel)
                    return;  // Skip logging

                if (!Directory.Exists(LogDirectory))
                    Directory.CreateDirectory(LogDirectory);

                // Sprint 8: Redact sensitive data before logging
                var safeMessage = RedactSensitiveData(message);

                var logFile = Path.Combine(LogDirectory, string.Format("app_{0:yyyyMMdd}.txt", DateTime.Now));

                // Sprint 8: Include log level in output
                var logEntry = string.Format("[{0:yyyy-MM-dd HH:mm:ss}] [{1}] {2}: {3}\n",
                    DateTime.Now,
                    level.ToString().PadRight(8),  // Align log levels
                    source ?? "APP",
                    safeMessage);

                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
                // Silent fail - logging hatası uygulamayı durdurmamalı
            }
        }

        /// <summary>
        /// Sprint 8: Convenience method for DEBUG level logs.
        /// Only logged if LOG_LEVEL=DEBUG (development).
        /// </summary>
        public static void LogDebug(string message, string source = null)
        {
            LogMessage(message, source, LogLevel.DEBUG);
        }

        /// <summary>
        /// Sprint 8: Convenience method for WARNING level logs.
        /// Use for non-critical issues that should be investigated.
        /// </summary>
        public static void LogWarning(string message, string source = null)
        {
            LogMessage(message, source, LogLevel.WARNING);
        }

        /// <summary>
        /// Sprint 8: Convenience method for ERROR level logs.
        /// Use for handled exceptions or error conditions.
        /// </summary>
        public static void LogError(string message, string source = null)
        {
            LogMessage(message, source, LogLevel.ERROR);
        }

        /// <summary>
        /// Sprint 8: Convenience method for CRITICAL level logs.
        /// Use for system-breaking errors or unhandled exceptions.
        /// </summary>
        public static void LogCritical(string message, string source = null)
        {
            LogMessage(message, source, LogLevel.CRITICAL);
        }

        /// <summary>
        /// Sprint 8: Redacts sensitive data (PII, secrets) from log messages.
        /// Prevents password leaks, API key exposure, and GDPR violations.
        /// </summary>
        /// <param name="text">Text that may contain sensitive data</param>
        /// <returns>Redacted text with sensitive data masked</returns>
        private static string RedactSensitiveData(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var redacted = text;

            // 1. Connection String Passwords
            // Matches: Password=...; or PWD=...; or pwd=...;
            redacted = Regex.Replace(redacted,
                @"(Password|PWD|pwd)\s*=\s*[^;]+",
                "$1=***REDACTED***",
                RegexOptions.IgnoreCase);

            // 2. Connection String User IDs (less sensitive but still redact)
            // Matches: User Id=...; or UID=...; or User=...;
            redacted = Regex.Replace(redacted,
                @"(User\s*Id|UID|User)\s*=\s*[^;]+",
                "$1=***REDACTED***",
                RegexOptions.IgnoreCase);

            // 3. API Keys
            // Matches: api_key=..., api-key:..., apikey:..., Bearer ...
            redacted = Regex.Replace(redacted,
                @"(api[_-]?key|apikey|bearer)\s*[=:]\s*[\w\-\.]+",
                "$1=***REDACTED***",
                RegexOptions.IgnoreCase);

            // 4. Authorization Headers
            // Matches: Authorization: Bearer ...
            redacted = Regex.Replace(redacted,
                @"(Authorization\s*:\s*Bearer\s+)[\w\-\.]+",
                "$1***REDACTED***",
                RegexOptions.IgnoreCase);

            // 5. Email Addresses (GDPR compliance)
            // Matches: user@example.com -> u***@example.com
            redacted = Regex.Replace(redacted,
                @"\b([a-zA-Z0-9])[a-zA-Z0-9._-]*@([a-zA-Z0-9.-]+\.[a-zA-Z]{2,})\b",
                "$1***@$2",
                RegexOptions.IgnoreCase);

            // 6. Turkish ID Numbers (TC Kimlik No - 11 digits)
            // Matches: 12345678901 -> ***REDACTED***
            redacted = Regex.Replace(redacted,
                @"\b\d{11}\b",
                "***REDACTED_TC***");

            // 7. Credit Card Numbers (various formats)
            // Matches: 1234-5678-9012-3456 or 1234567890123456
            redacted = Regex.Replace(redacted,
                @"\b\d{4}[\s\-]?\d{4}[\s\-]?\d{4}[\s\-]?\d{4}\b",
                "***REDACTED_CC***");

            // 8. Turkish Phone Numbers
            // Matches: 0532 123 45 67, 05321234567, +90 532 123 45 67
            redacted = Regex.Replace(redacted,
                @"(\+90[\s\-]?)?(0?5\d{2})[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}",
                "***REDACTED_PHONE***");

            // 9. JWT Tokens (typically very long base64 strings with dots)
            // Matches: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWI...
            redacted = Regex.Replace(redacted,
                @"eyJ[a-zA-Z0-9_-]*\.eyJ[a-zA-Z0-9_-]*\.[a-zA-Z0-9_-]*",
                "***REDACTED_JWT***");

            // 10. AI API Keys (Gemini, OpenAI, Claude patterns)
            // Matches: AIza..., sk-..., claude-...
            redacted = Regex.Replace(redacted,
                @"\b(AIza|sk-|claude-)[\w\-]{20,}",
                "$1***REDACTED***");

            return redacted;
        }
    }
}

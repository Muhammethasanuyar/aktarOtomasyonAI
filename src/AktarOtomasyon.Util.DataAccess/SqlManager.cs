using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AktarOtomasyon.Util.DataAccess
{
    /// <summary>
    /// SQL Server bağlantı yöneticisi.
    /// KURAL: using(sMan) standardı ile kullanılır.
    /// </summary>
    public class SqlManager : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private bool _disposed = false;

        public SqlManager()
        {
            var connStringEntry = ConfigurationManager.ConnectionStrings["Db"];

            // SPRINT 8: Check for environment variable override (from .env or system)
            var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbTrustedConn = Environment.GetEnvironmentVariable("DB_TRUSTED_CONNECTION");

            string connectionString;

            if (!string.IsNullOrEmpty(dbServer) && !string.IsNullOrEmpty(dbName))
            {
                // Build connection string from environment variables
                if (dbTrustedConn == "true")
                {
                    // Windows Authentication
                    connectionString = string.Format(
                        "Server={0};Database={1};Trusted_Connection=True;TrustServerCertificate=True;",
                        dbServer, dbName);
                }
                else
                {
                    // SQL Authentication
                    var dbUser = Environment.GetEnvironmentVariable("DB_USER");
                    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

                    if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
                    {
                        throw new InvalidOperationException(
                            "DB_USER and DB_PASSWORD environment variables must be set when DB_TRUSTED_CONNECTION=false");
                    }

                    connectionString = string.Format(
                        "Server={0};Database={1};User Id={2};Password={3};TrustServerCertificate=True;",
                        dbServer, dbName, dbUser, dbPassword);
                }
            }
            else
            {
                // Fallback to App.config
                if (connStringEntry == null || string.IsNullOrEmpty(connStringEntry.ConnectionString))
                {
                    throw new InvalidOperationException(
                        "Connection string 'Db' not found in App.config and no environment variables set.");
                }
                connectionString = connStringEntry.ConnectionString;
            }

            _connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Bağlantı testi yapar.
        /// </summary>
        /// <returns>null = başarı, mesaj = hata</returns>
        public string TestConnection()
        {
            try
            {
                _connection.Open();
                _connection.Close();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Bağlantıyı açar (with retry policy for transient failures - Sprint 8).
        /// </summary>
        public void Open()
        {
            if (_connection.State == ConnectionState.Open)
                return;

            // SPRINT 8: Connection retry policy
            int maxRetries = 3;
            int retryDelayMs = 1000; // Start with 1 second
            int[] transientErrorCodes = { 40197, 40501, 40613, 49918, 49919, 49920, -1, -2 };

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    _connection.Open();
                    return; // Success
                }
                catch (SqlException sqlEx)
                {
                    // Check if this is a transient error and we have retries left
                    if (attempt < maxRetries && IsTransientError(sqlEx, transientErrorCodes))
                    {
                        // Log transient error and retry
                        System.Diagnostics.Debug.WriteLine(
                            string.Format("Transient DB error (attempt {0}/{1}): {2}",
                                attempt, maxRetries, sqlEx.Message));

                        // Exponential backoff: 1s, 2s, 4s
                        System.Threading.Thread.Sleep(retryDelayMs);
                        retryDelayMs *= 2;
                    }
                    else
                    {
                        // Non-transient errors or final attempt: throw immediately
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Determines if a SQL exception is transient (retriable).
        /// </summary>
        private bool IsTransientError(SqlException sqlEx, int[] transientErrorCodes)
        {
            foreach (SqlError error in sqlEx.Errors)
            {
                foreach (int code in transientErrorCodes)
                {
                    if (error.Number == code)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Bağlantıyı kapatır.
        /// </summary>
        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        /// <summary>
        /// SQL komutu oluşturur.
        /// </summary>
        public SqlCommand CreateCommand(string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            Open();
            var cmd = _connection.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            // Transaction varsa command'a ata
            if (_transaction != null)
            {
                cmd.Transaction = _transaction;
            }

            return cmd;
        }

        /// <summary>
        /// DataTable döndüren sorgu çalıştırır.
        /// </summary>
        public DataTable ExecuteQuery(SqlCommand command)
        {
            var dt = new DataTable();
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Non-query (INSERT, UPDATE, DELETE) çalıştırır.
        /// </summary>
        public int ExecuteNonQuery(SqlCommand command)
        {
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// Scalar değer döndürür.
        /// </summary>
        public object ExecuteScalar(SqlCommand command)
        {
            return command.ExecuteScalar();
        }

        /// <summary>
        /// Transaction başlatır.
        /// KURAL: Commit veya Rollback çağrılana kadar tüm işlemler transaction içinde.
        /// </summary>
        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Zaten aktif bir transaction var. Nested transaction desteklenmez.");
            }

            Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Transaction'ı commit eder (kalıcı hale getirir).
        /// </summary>
        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Commit için önce BeginTransaction() çağrılmalı.");
            }

            try
            {
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        /// <summary>
        /// Transaction'ı geri alır (rollback).
        /// </summary>
        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Rollback için önce BeginTransaction() çağrılmalı.");
            }

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Transaction commit edilmemişse otomatik rollback
                    if (_transaction != null)
                    {
                        try
                        {
                            _transaction.Rollback();
                        }
                        catch
                        {
                            // Rollback hatası ignore edilir (connection zaten kapalı olabilir)
                        }
                        finally
                        {
                            _transaction.Dispose();
                            _transaction = null;
                        }
                    }

                    Close();
                    if (_connection != null)
                        _connection.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Common.Interface.Models;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Common.Service
{
    /// <summary>
    /// System settings service implementation with priority: Env Var → DB → App.config
    /// </summary>
    public class SystemSettingService : ISystemSettingService
    {
        public List<SystemSettingDto> SettingListele()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_system_setting_listele", CommandType.StoredProcedure);
                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<SystemSettingDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new SystemSettingDto
                        {
                            SettingKey = row["setting_key"] != DBNull.Value ? row["setting_key"].ToString() : null,
                            SettingValue = row["setting_value"] != DBNull.Value ? row["setting_value"].ToString() : null,
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                            UpdatedAt = Convert.ToDateTime(row["updated_at"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<SystemSettingDto>();
            }
        }

        public SystemSettingDto SettingGetir(string key)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_system_setting_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@setting_key", SqlDbType.NVarChar, 100) { Value = key });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new SystemSettingDto
                    {
                        SettingKey = row["setting_key"] != DBNull.Value ? row["setting_key"].ToString() : null,
                        SettingValue = row["setting_value"] != DBNull.Value ? row["setting_value"].ToString() : null,
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                        UpdatedAt = Convert.ToDateTime(row["updated_at"])
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string SettingKaydet(SystemSettingDto dto)
        {
            try
            {
                if (dto == null)
                    return "Setting DTO boş olamaz.";

                if (string.IsNullOrWhiteSpace(dto.SettingKey))
                    return "Setting key zorunludur.";

                if (dto.SettingValue == null)
                    return "Setting value zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_system_setting_kaydet", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@setting_key", SqlDbType.NVarChar, 100)
                        { Value = dto.SettingKey });
                    cmd.Parameters.Add(new SqlParameter("@setting_value", SqlDbType.NVarChar, -1)
                        { Value = dto.SettingValue });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                        { Value = (object)dto.Aciklama ?? DBNull.Value });

                    sMan.ExecuteNonQuery(cmd);

                    // Audit log
                    LogAudit(sMan, "SYSTEM_SETTING", 0, "UPDATE",
                        string.Format("{{\"key\":\"{0}\",\"value\":\"{1}\"}}", dto.SettingKey, dto.SettingValue));

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Setting kaydedilemedi: " + ex.Message;
            }
        }

        /// <summary>
        /// Gets setting value with priority: Environment Var → DB → App.config
        /// </summary>
        public static string GetSettingValue(string key, string defaultValue = null)
        {
            // 1. Check environment variable
            var envValue = Environment.GetEnvironmentVariable(key);
            if (!string.IsNullOrEmpty(envValue))
                return envValue;

            // 2. Check database
            try
            {
                var service = new SystemSettingService();
                var dbSetting = service.SettingGetir(key);
                if (dbSetting != null && !string.IsNullOrEmpty(dbSetting.SettingValue))
                    return dbSetting.SettingValue;
            }
            catch
            {
                // Fall through to App.config
            }

            // 3. Check App.config
            var configValue = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(configValue))
                return configValue;

            // 4. Return default
            return defaultValue;
        }

        private void LogAudit(SqlManager sMan, string entity, int entityId, string action, string detailJson)
        {
            try
            {
                var cmd = sMan.CreateCommand("sp_audit_log_ekle", CommandType.StoredProcedure);
                cmd.Parameters.Add(new SqlParameter("@entity", SqlDbType.NVarChar, 100) { Value = entity });
                cmd.Parameters.Add(new SqlParameter("@entity_id", SqlDbType.Int) { Value = entityId });
                cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar, 50) { Value = action });
                cmd.Parameters.Add(new SqlParameter("@detail_json", SqlDbType.NVarChar, -1)
                    { Value = (object)detailJson ?? DBNull.Value });

                sMan.ExecuteNonQuery(cmd);
            }
            catch
            {
                // Silent fail
            }
        }
    }
}

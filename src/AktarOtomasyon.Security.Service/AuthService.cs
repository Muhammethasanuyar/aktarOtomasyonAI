using System;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Util.DataAccess;
using AktarOtomasyon.Security.Interface;
using AktarOtomasyon.Security.Interface.Models.Auth;
using AktarOtomasyon.Security.Service.Helpers;

namespace AktarOtomasyon.Security.Service
{
    public class AuthService : IAuthService
    {
        public LoginResultDto Login(LoginRequestDto request)
        {
            var result = new LoginResultDto { Success = false };

            try
            {
                // Validation
                if (request == null || string.IsNullOrWhiteSpace(request.KullaniciAdi)
                    || string.IsNullOrWhiteSpace(request.Parola))
                {
                    result.ErrorMessage = "Kullanıcı adı ve parola gereklidir";
                    return result;
                }

                using (var sMan = new SqlManager())
                {
                    // Get user credentials
                    var cmd = sMan.CreateCommand("sp_kullanici_getir_login", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_adi", SqlDbType.NVarChar, 50)
                        { Value = request.KullaniciAdi });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                    {
                        result.ErrorMessage = "Kullanıcı adı veya parola hatalı";
                        return result;
                    }

                    var row = dt.Rows[0];

                    // Check if active
                    if (!Convert.ToBoolean(row["aktif"]))
                    {
                        result.ErrorMessage = "Kullanıcı hesabı pasif durumda";
                        return result;
                    }

                    // Verify password with PBKDF2
                    string storedHash = row["parola_hash"].ToString();
                    string storedSalt = row["parola_salt"].ToString();

                    if (!PasswordHelper.VerifyPassword(request.Parola, storedHash, storedSalt))
                    {
                        result.ErrorMessage = "Kullanıcı adı veya parola hatalı";
                        return result;
                    }

                    // Success - populate result
                    result.Success = true;
                    result.KullaniciId = Convert.ToInt32(row["kullanici_id"]);
                    result.KullaniciAdi = row["kullanici_adi"].ToString();
                    result.AdSoyad = row["ad_soyad"].ToString();
                    result.Email = row["email"] != DBNull.Value ? row["email"].ToString() : null;
                    result.SonGirisTarih = row["son_giris_tarih"] != DBNull.Value
                        ? (DateTime?)row["son_giris_tarih"] : null;

                    // Update last login
                    UpdateLastLogin(result.KullaniciId);

                    return result;
                }
            }
            catch (SqlException sqlEx)
            {
                result.ErrorMessage = sqlEx.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Giriş sırasında hata oluştu";
                return result;
            }
        }

        public string ChangePassword(ChangePasswordDto dto)
        {
            try
            {
                // Validation
                if (dto == null) return "Geçersiz istek";
                if (string.IsNullOrWhiteSpace(dto.EskiParola)) return "Eski parola gerekli";
                if (string.IsNullOrWhiteSpace(dto.YeniParola)) return "Yeni parola gerekli";
                if (dto.YeniParola.Length < 8) return "Yeni parola en az 8 karakter olmalı";

                using (var sMan = new SqlManager())
                {
                    // Get current credentials
                    var getCmd = sMan.CreateCommand("sp_kullanici_getir_login", CommandType.StoredProcedure);
                    getCmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = dto.KullaniciId });

                    var dt = sMan.ExecuteQuery(getCmd);
                    if (dt.Rows.Count == 0)
                        return "Kullanıcı bulunamadı";

                    string currentHash = dt.Rows[0]["parola_hash"].ToString();
                    string currentSalt = dt.Rows[0]["parola_salt"].ToString();

                    // Verify old password
                    if (!PasswordHelper.VerifyPassword(dto.EskiParola, currentHash, currentSalt))
                        return "Mevcut parola hatalı";

                    // Generate new hash/salt
                    string newHash, newSalt;
                    PasswordHelper.HashPassword(dto.YeniParola, out newHash, out newSalt);

                    // Update password
                    var updateCmd = sMan.CreateCommand("sp_kullanici_parola_guncelle", CommandType.StoredProcedure);
                    updateCmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = dto.KullaniciId });
                    updateCmd.Parameters.Add(new SqlParameter("@yeni_parola_hash", SqlDbType.NVarChar, 512)
                        { Value = newHash });
                    updateCmd.Parameters.Add(new SqlParameter("@yeni_parola_salt", SqlDbType.NVarChar, 256)
                        { Value = newSalt });
                    updateCmd.Parameters.Add(new SqlParameter("@parola_iterasyon", SqlDbType.Int)
                        { Value = 10000 });

                    sMan.ExecuteNonQuery(updateCmd);
                    return null; // Success
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Parola değiştirme hatası: " + ex.Message; }
        }

        public string ResetPassword(int kullaniciId, string yeniParola, int resetBy)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(yeniParola)) return "Yeni parola gerekli";
                if (yeniParola.Length < 8) return "Yeni parola en az 8 karakter olmalı";

                // Generate new hash/salt
                string newHash, newSalt;
                PasswordHelper.HashPassword(yeniParola, out newHash, out newSalt);

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_parola_sifirla", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    cmd.Parameters.Add(new SqlParameter("@yeni_parola_hash", SqlDbType.NVarChar, 512) { Value = newHash });
                    cmd.Parameters.Add(new SqlParameter("@yeni_parola_salt", SqlDbType.NVarChar, 256) { Value = newSalt });
                    cmd.Parameters.Add(new SqlParameter("@parola_iterasyon", SqlDbType.Int) { Value = 10000 });
                    cmd.Parameters.Add(new SqlParameter("@reset_by", SqlDbType.Int) { Value = resetBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Parola sıfırlama hatası: " + ex.Message; }
        }

        public string UpdateLastLogin(int kullaniciId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_son_giris_guncelle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch { return null; } // Silent fail - not critical
        }
    }
}

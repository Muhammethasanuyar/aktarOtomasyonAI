using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Util.DataAccess;
using AktarOtomasyon.Security.Interface;
using AktarOtomasyon.Security.Interface.Models.User;
using AktarOtomasyon.Security.Interface.Models.Role;
using AktarOtomasyon.Security.Interface.Models.Permission;
using AktarOtomasyon.Security.Interface.Models.Assignment;
using AktarOtomasyon.Security.Service.Helpers;

namespace AktarOtomasyon.Security.Service
{
    public class SecurityService : ISecurityService
    {
        #region User Management

        public string KullaniciKaydet(KullaniciModel model)
        {
            try
            {
                // Validation
                if (model == null) return "Kullanıcı bilgisi boş olamaz";
                if (string.IsNullOrWhiteSpace(model.KullaniciAdi)) return "Kullanıcı adı zorunludur";
                if (string.IsNullOrWhiteSpace(model.AdSoyad)) return "Ad soyad zorunludur";

                // For new users, password is required
                if (model.KullaniciId == 0 && string.IsNullOrWhiteSpace(model.Parola))
                    return "Yeni kullanıcı için parola zorunludur";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_kaydet", CommandType.StoredProcedure);

                    var paramKullaniciId = new SqlParameter("@kullanici_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = model.KullaniciId == 0 ? (object)DBNull.Value : model.KullaniciId
                    };
                    cmd.Parameters.Add(paramKullaniciId);

                    cmd.Parameters.Add(new SqlParameter("@kullanici_adi", SqlDbType.NVarChar, 50)
                        { Value = model.KullaniciAdi });
                    cmd.Parameters.Add(new SqlParameter("@ad_soyad", SqlDbType.NVarChar, 100)
                        { Value = model.AdSoyad });
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 100)
                        { Value = (object)model.Email ?? DBNull.Value });

                    // Password hashing for new users
                    if (model.KullaniciId == 0 && !string.IsNullOrWhiteSpace(model.Parola))
                    {
                        string hash, salt;
                        PasswordHelper.HashPassword(model.Parola, out hash, out salt);

                        cmd.Parameters.Add(new SqlParameter("@parola_hash", SqlDbType.NVarChar, 512)
                            { Value = hash });
                        cmd.Parameters.Add(new SqlParameter("@parola_salt", SqlDbType.NVarChar, 256)
                            { Value = salt });
                        cmd.Parameters.Add(new SqlParameter("@parola_iterasyon", SqlDbType.Int)
                            { Value = 10000 });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter("@parola_hash", SqlDbType.NVarChar, 512)
                            { Value = DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@parola_salt", SqlDbType.NVarChar, 256)
                            { Value = DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@parola_iterasyon", SqlDbType.Int)
                            { Value = DBNull.Value });
                    }

                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = model.Aktif });

                    sMan.ExecuteNonQuery(cmd);
                    model.KullaniciId = Convert.ToInt32(paramKullaniciId.Value);

                    return null; // Success
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Kullanıcı kaydedilemedi: " + ex.Message; }
        }

        public List<KullaniciListeItemDto> KullaniciListele(KullaniciFiltre filtre)
        {
            try
            {
                if (filtre == null) filtre = new KullaniciFiltre();

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)filtre.Aktif ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@kullanici_adi", SqlDbType.NVarChar, 50)
                        { Value = (object)filtre.KullaniciAdi ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ad_soyad", SqlDbType.NVarChar, 100)
                        { Value = (object)filtre.AdSoyad ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<KullaniciListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new KullaniciListeItemDto
                        {
                            KullaniciId = Convert.ToInt32(row["kullanici_id"]),
                            KullaniciAdi = row["kullanici_adi"].ToString(),
                            AdSoyad = row["ad_soyad"].ToString(),
                            Email = row["email"] != DBNull.Value ? row["email"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            Roller = row["roller"] != DBNull.Value ? row["roller"].ToString() : null
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<KullaniciListeItemDto>();
            }
        }

        public KullaniciModel KullaniciGetir(int kullaniciId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new KullaniciModel
                    {
                        KullaniciId = Convert.ToInt32(row["kullanici_id"]),
                        KullaniciAdi = row["kullanici_adi"].ToString(),
                        AdSoyad = row["ad_soyad"].ToString(),
                        Email = row["email"] != DBNull.Value ? row["email"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                        // Password hash is intentionally excluded from DTOs
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public string KullaniciPasifle(int kullaniciId, int updatedBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    cmd.Parameters.Add(new SqlParameter("@updated_by", SqlDbType.Int) { Value = updatedBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Kullanıcı pasifleştirilemedi: " + ex.Message; }
        }

        #endregion

        #region Role Management

        public string RolKaydet(RolDto model)
        {
            try
            {
                if (model == null) return "Rol bilgisi boş olamaz";
                if (string.IsNullOrWhiteSpace(model.RolAdi)) return "Rol adı zorunludur";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_kaydet", CommandType.StoredProcedure);

                    var paramRolId = new SqlParameter("@rol_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = model.RolId == 0 ? (object)DBNull.Value : model.RolId
                    };
                    cmd.Parameters.Add(paramRolId);

                    cmd.Parameters.Add(new SqlParameter("@rol_adi", SqlDbType.NVarChar, 50)
                        { Value = model.RolAdi });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 200)
                        { Value = (object)model.Aciklama ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = model.Aktif });

                    sMan.ExecuteNonQuery(cmd);
                    model.RolId = Convert.ToInt32(paramRolId.Value);

                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Rol kaydedilemedi: " + ex.Message; }
        }

        public List<RolListeItemDto> RolListele(bool? aktif)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)aktif ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<RolListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new RolListeItemDto
                        {
                            RolId = Convert.ToInt32(row["rol_id"]),
                            RolAdi = row["rol_adi"].ToString(),
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            KullaniciSayisi = row["kullanici_sayisi"] != DBNull.Value
                                ? Convert.ToInt32(row["kullanici_sayisi"]) : 0,
                            YetkiSayisi = row["yetki_sayisi"] != DBNull.Value
                                ? Convert.ToInt32(row["yetki_sayisi"]) : 0
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<RolListeItemDto>();
            }
        }

        public RolDto RolGetir(int rolId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new RolDto
                    {
                        RolId = Convert.ToInt32(row["rol_id"]),
                        RolAdi = row["rol_adi"].ToString(),
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public string RolPasifle(int rolId, int updatedBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });
                    cmd.Parameters.Add(new SqlParameter("@updated_by", SqlDbType.Int) { Value = updatedBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Rol pasifleştirilemedi: " + ex.Message; }
        }

        #endregion

        #region Permission Management

        public List<YetkiDto> YetkiListele(string modul = null)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_yetki_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@modul", SqlDbType.NVarChar, 50)
                        { Value = (object)modul ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<YetkiDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new YetkiDto
                        {
                            YetkiId = Convert.ToInt32(row["yetki_id"]),
                            YetkiKod = row["yetki_kod"].ToString(),
                            YetkiAdi = row["yetki_adi"].ToString(),
                            Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<YetkiDto>();
            }
        }

        public YetkiDto YetkiGetir(int yetkiId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_yetki_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@yetki_id", SqlDbType.Int) { Value = yetkiId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new YetkiDto
                    {
                        YetkiId = Convert.ToInt32(row["yetki_id"]),
                        YetkiKod = row["yetki_kod"].ToString(),
                        YetkiAdi = row["yetki_adi"].ToString(),
                        Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region User-Role Assignments

        public string KullaniciRolEkle(int kullaniciId, int rolId, int createdBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_rol_ekle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });
                    cmd.Parameters.Add(new SqlParameter("@created_by", SqlDbType.Int) { Value = createdBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Rol atanamadı: " + ex.Message; }
        }

        public string KullaniciRolSil(int kullaniciId, int rolId, int updatedBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_rol_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });
                    cmd.Parameters.Add(new SqlParameter("@updated_by", SqlDbType.Int) { Value = updatedBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Rol kaldırılamadı: " + ex.Message; }
        }

        public List<KullaniciRolDto> KullaniciRolListele(int kullaniciId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_rol_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<KullaniciRolDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new KullaniciRolDto
                        {
                            KullaniciId = Convert.ToInt32(row["kullanici_id"]),
                            RolId = Convert.ToInt32(row["rol_id"]),
                            RolAdi = row["rol_adi"].ToString()
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<KullaniciRolDto>();
            }
        }

        #endregion

        #region Role-Permission Assignments

        public string RolYetkiEkle(int rolId, int yetkiId, int createdBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_yetki_ekle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });
                    cmd.Parameters.Add(new SqlParameter("@yetki_id", SqlDbType.Int) { Value = yetkiId });
                    cmd.Parameters.Add(new SqlParameter("@created_by", SqlDbType.Int) { Value = createdBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Yetki atanamadı: " + ex.Message; }
        }

        public string RolYetkiSil(int rolId, int yetkiId, int updatedBy)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_yetki_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });
                    cmd.Parameters.Add(new SqlParameter("@yetki_id", SqlDbType.Int) { Value = yetkiId });
                    cmd.Parameters.Add(new SqlParameter("@updated_by", SqlDbType.Int) { Value = updatedBy });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Yetki kaldırılamadı: " + ex.Message; }
        }

        public List<RolYetkiDto> RolYetkiListele(int rolId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_rol_yetki_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@rol_id", SqlDbType.Int) { Value = rolId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<RolYetkiDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new RolYetkiDto
                        {
                            RolId = Convert.ToInt32(row["rol_id"]),
                            YetkiId = Convert.ToInt32(row["yetki_id"]),
                            YetkiKod = row["yetki_kod"].ToString(),
                            YetkiAdi = row["yetki_adi"].ToString()
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<RolYetkiDto>();
            }
        }

        #endregion

        #region Screen-Permission Mappings

        public string EkranYetkiEkle(string ekranKod, int yetkiId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ekran_yetki_ekle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50) { Value = ekranKod });
                    cmd.Parameters.Add(new SqlParameter("@yetki_id", SqlDbType.Int) { Value = yetkiId });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Ekran yetkisi eklenemedi: " + ex.Message; }
        }

        public string EkranYetkiSil(string ekranKod, int yetkiId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ekran_yetki_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50) { Value = ekranKod });
                    cmd.Parameters.Add(new SqlParameter("@yetki_id", SqlDbType.Int) { Value = yetkiId });

                    sMan.ExecuteNonQuery(cmd);
                    return null;
                }
            }
            catch (SqlException sqlEx) { return sqlEx.Message; }
            catch (Exception ex) { return "Ekran yetkisi silinemedi: " + ex.Message; }
        }

        public List<EkranYetkiDto> EkranYetkiListele(string ekranKod)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ekran_yetki_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50) { Value = ekranKod });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<EkranYetkiDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new EkranYetkiDto
                        {
                            EkranKod = row["ekran_kod"].ToString(),
                            YetkiId = Convert.ToInt32(row["yetki_id"]),
                            YetkiKod = row["yetki_kod"].ToString()
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<EkranYetkiDto>();
            }
        }

        #endregion

        #region Effective Permissions (CRITICAL)

        public List<YetkiDto> KullaniciYetkiListele(int kullaniciId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_yetki_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<YetkiDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new YetkiDto
                        {
                            YetkiId = Convert.ToInt32(row["yetki_id"]),
                            YetkiKod = row["yetki_kod"].ToString(),
                            YetkiAdi = row["yetki_adi"].ToString(),
                            Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<YetkiDto>();
            }
        }

        public bool KullaniciYetkiKontrol(int kullaniciId, string yetkiKod)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kullanici_yetki_kontrol", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int) { Value = kullaniciId });
                    cmd.Parameters.Add(new SqlParameter("@yetki_kod", SqlDbType.NVarChar, 50) { Value = yetkiKod });

                    var result = sMan.ExecuteScalar(cmd);
                    return result != null && Convert.ToInt32(result) == 1;
                }
            }
            catch
            {
                return false; // Deny access on error
            }
        }

        #endregion
    }
}

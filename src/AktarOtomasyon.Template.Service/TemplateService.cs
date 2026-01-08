using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using AktarOtomasyon.Template.Interface;
using AktarOtomasyon.Template.Interface.Models;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Template.Service
{
    /// <summary>
    /// Template service implementation
    /// KURAL: Stateless, try/catch, using(sMan), SP-only data access
    /// </summary>
    public class TemplateService : ITemplateService
    {
        #region Template Operations

        public List<TemplateListeItemDto> TemplateListele(TemplateFiltreDto filtre = null)
        {
            try
            {
                if (filtre == null)
                    filtre = new TemplateFiltreDto();

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@modul", SqlDbType.NVarChar, 50)
                        { Value = (object)filtre.Modul ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)filtre.Aktif ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@arama", SqlDbType.NVarChar, 100)
                        { Value = (object)filtre.Arama ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<TemplateListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new TemplateListeItemDto
                        {
                            TemplateId = Convert.ToInt32(row["template_id"]),
                            TemplateKod = row["template_kod"] != DBNull.Value ? row["template_kod"].ToString() : null,
                            TemplateAdi = row["template_adi"] != DBNull.Value ? row["template_adi"].ToString() : null,
                            Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            CreatedAt = Convert.ToDateTime(row["created_at"]),
                            AktifVersionId = row["aktif_version_id"] != DBNull.Value ? Convert.ToInt32(row["aktif_version_id"]) : (int?)null,
                            AktifVersionNo = row["aktif_version_no"] != DBNull.Value ? Convert.ToInt32(row["aktif_version_no"]) : (int?)null
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<TemplateListeItemDto>();
            }
        }

        public TemplateModel TemplateGetirById(int templateId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_id", SqlDbType.Int) { Value = templateId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return MapRowToTemplateModel(row);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TemplateModel TemplateGetirByKod(string templateKod)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_kod", SqlDbType.NVarChar, 50) { Value = templateKod });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return MapRowToTemplateModel(row);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string TemplateKaydet(TemplateModel model)
        {
            try
            {
                // Validation
                if (model == null)
                    return "Template modeli boş olamaz.";

                if (string.IsNullOrWhiteSpace(model.TemplateKod))
                    return "Template kodu zorunludur.";

                if (string.IsNullOrWhiteSpace(model.TemplateAdi))
                    return "Template adı zorunludur.";

                if (string.IsNullOrWhiteSpace(model.Modul))
                    return "Modül zorunludur.";

                // Normalize template_kod (trim/upper)
                model.TemplateKod = model.TemplateKod.Trim().ToUpper();

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_kaydet", CommandType.StoredProcedure);

                    var paramTemplateId = new SqlParameter("@template_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = model.TemplateId == 0 ? (object)DBNull.Value : model.TemplateId
                    };
                    cmd.Parameters.Add(paramTemplateId);

                    cmd.Parameters.Add(new SqlParameter("@template_kod", SqlDbType.NVarChar, 50)
                        { Value = model.TemplateKod });
                    cmd.Parameters.Add(new SqlParameter("@template_adi", SqlDbType.NVarChar, 200)
                        { Value = model.TemplateAdi });
                    cmd.Parameters.Add(new SqlParameter("@modul", SqlDbType.NVarChar, 50)
                        { Value = model.Modul });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                        { Value = (object)model.Aciklama ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = model.Aktif });

                    sMan.ExecuteNonQuery(cmd);
                    model.TemplateId = Convert.ToInt32(paramTemplateId.Value);

                    // Audit log
                    LogAudit(sMan, "TEMPLATE", model.TemplateId, model.TemplateId == 0 ? "CREATE" : "UPDATE",
                        string.Format("{{\"template_kod\":\"{0}\",\"template_adi\":\"{1}\"}}", model.TemplateKod, model.TemplateAdi));

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Template kaydedilemedi: " + ex.Message;
            }
        }

        public string TemplatePasifle(int templateId)
        {
            try
            {
                if (templateId <= 0)
                    return "Geçersiz template ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_id", SqlDbType.Int) { Value = templateId });

                    sMan.ExecuteNonQuery(cmd);

                    // Audit log
                    LogAudit(sMan, "TEMPLATE", templateId, "DEACTIVATE", null);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Template pasifleştirilemedi: " + ex.Message;
            }
        }

        #endregion

        #region Version Operations

        public List<TemplateVersionListeItemDto> VersionListele(int templateId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_version_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_id", SqlDbType.Int) { Value = templateId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<TemplateVersionListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new TemplateVersionListeItemDto
                        {
                            TemplateVersionId = Convert.ToInt32(row["template_version_id"]),
                            TemplateId = Convert.ToInt32(row["template_id"]),
                            VersionNo = Convert.ToInt32(row["version_no"]),
                            Durum = row["durum"] != DBNull.Value ? row["durum"].ToString() : null,
                            DosyaAdi = row["dosya_adi"] != DBNull.Value ? row["dosya_adi"].ToString() : null,
                            DosyaBoyut = row["dosya_boyut"] != DBNull.Value ? Convert.ToInt64(row["dosya_boyut"]) : (long?)null,
                            Sha256 = row["sha256"] != DBNull.Value ? row["sha256"].ToString() : null,
                            CreatedAt = Convert.ToDateTime(row["created_at"]),
                            ApprovedAt = row["approved_at"] != DBNull.Value ? Convert.ToDateTime(row["approved_at"]) : (DateTime?)null,
                            IsActive = Convert.ToBoolean(row["is_active"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<TemplateVersionListeItemDto>();
            }
        }

        public TemplateVersionModel VersionGetir(int templateVersionId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("SELECT * FROM template_version WHERE template_version_id = @id", CommandType.Text);
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = templateVersionId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return MapRowToVersionModel(row);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string VersionEkle(int templateId, string dosyaAdi, string dosyaYolu, string mimeType, long dosyaBoyut, string sha256, string notlar = null)
        {
            try
            {
                // Validation
                if (templateId <= 0)
                    return "Geçersiz template ID.";

                if (string.IsNullOrWhiteSpace(dosyaAdi))
                    return "Dosya adı zorunludur.";

                if (string.IsNullOrWhiteSpace(dosyaYolu))
                    return "Dosya yolu zorunludur.";

                // BR: File size limit (10MB default)
                const long maxSizeMB = 10;
                if (dosyaBoyut > maxSizeMB * 1024 * 1024)
                    return string.Format("Dosya boyutu {0}MB sınırını aşıyor.", maxSizeMB);

                // BR: SHA256 required for integrity
                if (string.IsNullOrWhiteSpace(sha256))
                    return "SHA256 hash zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_version_ekle", CommandType.StoredProcedure);

                    var paramVersionId = new SqlParameter("@template_version_id", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramVersionId);

                    cmd.Parameters.Add(new SqlParameter("@template_id", SqlDbType.Int) { Value = templateId });

                    var paramVersionNo = new SqlParameter("@version_no", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(paramVersionNo);

                    cmd.Parameters.Add(new SqlParameter("@dosya_adi", SqlDbType.NVarChar, 255) { Value = dosyaAdi });
                    cmd.Parameters.Add(new SqlParameter("@dosya_yolu", SqlDbType.NVarChar, 500) { Value = dosyaYolu });
                    cmd.Parameters.Add(new SqlParameter("@mime_type", SqlDbType.NVarChar, 100)
                        { Value = (object)mimeType ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@dosya_boyut", SqlDbType.BigInt) { Value = dosyaBoyut });
                    cmd.Parameters.Add(new SqlParameter("@sha256", SqlDbType.NVarChar, 64) { Value = sha256 });
                    cmd.Parameters.Add(new SqlParameter("@notlar", SqlDbType.NVarChar, -1)
                        { Value = (object)notlar ?? DBNull.Value });

                    sMan.ExecuteNonQuery(cmd);

                    int versionId = Convert.ToInt32(paramVersionId.Value);
                    int versionNo = Convert.ToInt32(paramVersionNo.Value);

                    // Audit log
                    LogAudit(sMan, "TEMPLATE_VERSION", versionId, "UPLOAD",
                        string.Format("{{\"template_id\":{0},\"version_no\":{1},\"dosya_adi\":\"{2}\"}}",
                        templateId, versionNo, dosyaAdi));

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Version eklenemedi: " + ex.Message;
            }
        }

        public string VersionAktifEt(int templateId, int templateVersionId)
        {
            try
            {
                if (templateId <= 0 || templateVersionId <= 0)
                    return "Geçersiz ID'ler.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_version_aktif_et", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_id", SqlDbType.Int) { Value = templateId });
                    cmd.Parameters.Add(new SqlParameter("@template_version_id", SqlDbType.Int) { Value = templateVersionId });

                    sMan.ExecuteNonQuery(cmd);

                    // Audit log
                    LogAudit(sMan, "TEMPLATE_VERSION", templateVersionId, "ACTIVATE",
                        string.Format("{{\"template_id\":{0}}}", templateId));

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Version aktif edilemedi: " + ex.Message;
            }
        }

        public string VersionArsivle(int templateVersionId)
        {
            try
            {
                if (templateVersionId <= 0)
                    return "Geçersiz version ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_template_version_arsivle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@template_version_id", SqlDbType.Int) { Value = templateVersionId });

                    sMan.ExecuteNonQuery(cmd);

                    // Audit log
                    LogAudit(sMan, "TEMPLATE_VERSION", templateVersionId, "ARCHIVE", null);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Version arşivlenemedi: " + ex.Message;
            }
        }

        #endregion

        #region File Operations

        public TemplateDosyaResultDto DosyaGetir(int templateVersionId)
        {
            try
            {
                var version = VersionGetir(templateVersionId);
                if (version == null)
                    return null;

                // BR: Path traversal protection
                if (version.DosyaYolu.Contains(".."))
                    throw new InvalidOperationException("Güvenlik ihlali: Path traversal tespit edildi.");

                // Return file info (UI/caller will handle actual file reading)
                return new TemplateDosyaResultDto
                {
                    DosyaYolu = version.DosyaYolu,
                    DosyaAdi = version.DosyaAdi,
                    MimeType = version.MimeType,
                    DosyaBoyut = version.DosyaBoyut
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region Helper Methods

        private TemplateModel MapRowToTemplateModel(DataRow row)
        {
            return new TemplateModel
            {
                TemplateId = Convert.ToInt32(row["template_id"]),
                TemplateKod = row["template_kod"] != DBNull.Value ? row["template_kod"].ToString() : null,
                TemplateAdi = row["template_adi"] != DBNull.Value ? row["template_adi"].ToString() : null,
                Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                Aktif = Convert.ToBoolean(row["aktif"]),
                AktifVersionId = row["aktif_version_id"] != DBNull.Value ? Convert.ToInt32(row["aktif_version_id"]) : (int?)null,
                AktifVersionNo = row["aktif_version_no"] != DBNull.Value ? Convert.ToInt32(row["aktif_version_no"]) : (int?)null
            };
        }

        private TemplateVersionModel MapRowToVersionModel(DataRow row)
        {
            return new TemplateVersionModel
            {
                TemplateVersionId = Convert.ToInt32(row["template_version_id"]),
                TemplateId = Convert.ToInt32(row["template_id"]),
                VersionNo = Convert.ToInt32(row["version_no"]),
                Durum = row["durum"] != DBNull.Value ? row["durum"].ToString() : null,
                DosyaAdi = row["dosya_adi"] != DBNull.Value ? row["dosya_adi"].ToString() : null,
                DosyaYolu = row["dosya_yolu"] != DBNull.Value ? row["dosya_yolu"].ToString() : null,
                MimeType = row["mime_type"] != DBNull.Value ? row["mime_type"].ToString() : null,
                DosyaBoyut = row["dosya_boyut"] != DBNull.Value ? Convert.ToInt64(row["dosya_boyut"]) : (long?)null,
                Sha256 = row["sha256"] != DBNull.Value ? row["sha256"].ToString() : null,
                Notlar = row["notlar"] != DBNull.Value ? row["notlar"].ToString() : null,
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                CreatedBy = row["created_by"] != DBNull.Value ? Convert.ToInt32(row["created_by"]) : (int?)null,
                ApprovedAt = row["approved_at"] != DBNull.Value ? Convert.ToDateTime(row["approved_at"]) : (DateTime?)null,
                ApprovedBy = row["approved_by"] != DBNull.Value ? Convert.ToInt32(row["approved_by"]) : (int?)null
            };
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
                // Silent fail for audit - don't break main operation
            }
        }

        #endregion
    }
}

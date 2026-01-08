using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Util.DataAccess;
using AktarOtomasyon.Audit.Interface;
using AktarOtomasyon.Audit.Interface.Models;

namespace AktarOtomasyon.Audit.Service
{
    public class AuditService : IAuditService
    {
        public List<AuditListeItemDto> AuditListele(AuditFiltre filtre)
        {
            try
            {
                if (filtre == null) filtre = new AuditFiltre();

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_audit_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@entity", SqlDbType.NVarChar, 100)
                        { Value = (object)filtre.Entity ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar, 50)
                        { Value = (object)filtre.Action ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = (object)filtre.KullaniciId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@baslangic_tarih", SqlDbType.DateTime)
                        { Value = (object)filtre.BaslangicTarih ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@bitis_tarih", SqlDbType.DateTime)
                        { Value = (object)filtre.BitisTarih ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@top", SqlDbType.Int)
                        { Value = filtre.Top });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<AuditListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new AuditListeItemDto
                        {
                            AuditId = Convert.ToInt32(row["audit_id"]),
                            Entity = row["entity"].ToString(),
                            EntityId = row["entity_id"] != DBNull.Value ? (int?)row["entity_id"] : null,
                            Action = row["action"].ToString(),
                            KullaniciId = Convert.ToInt32(row["created_by"]),
                            KullaniciAdi = row["kullanici_adi"].ToString(),
                            AdSoyad = row["ad_soyad"].ToString(),
                            CreatedAt = Convert.ToDateTime(row["created_at"])
                        });
                    }

                    return liste;
                }
            }
            catch
            {
                return new List<AuditListeItemDto>();
            }
        }

        public AuditDetayDto AuditGetir(int auditId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_audit_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@audit_id", SqlDbType.Int) { Value = auditId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0) return null;

                    var row = dt.Rows[0];
                    return new AuditDetayDto
                    {
                        AuditId = Convert.ToInt32(row["audit_id"]),
                        Entity = row["entity"].ToString(),
                        EntityId = row["entity_id"] != DBNull.Value ? (int?)row["entity_id"] : null,
                        Action = row["action"].ToString(),
                        JsonData = row["detail_json"] != DBNull.Value ? row["detail_json"].ToString() : null,
                        KullaniciId = Convert.ToInt32(row["created_by"]),
                        KullaniciAdi = row["kullanici_adi"].ToString(),
                        AdSoyad = row["ad_soyad"].ToString(),
                        CreatedAt = Convert.ToDateTime(row["created_at"])
                    };
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

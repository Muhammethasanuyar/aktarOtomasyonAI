using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Common.Service
{
    /// <summary>
    /// Bildirim service implementasyonu.
    /// </summary>
    public class BildirimService : IBildirimInterface
    {
        public string Ekle(BildirimModel bildirim)
        {
            try
            {
                // Validation
                if (bildirim == null)
                    return "Bildirim modeli boş olamaz.";
                if (string.IsNullOrWhiteSpace(bildirim.BildirimTip))
                    return "Bildirim tipi zorunludur.";
                if (string.IsNullOrWhiteSpace(bildirim.Baslik))
                    return "Bildirim başlığı zorunludur.";

                using (var sMan = new SqlManager())
                {
                    return EkleInternal(sMan, bildirim);
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Bildirim eklenemedi: " + ex.Message;
            }
        }

        /// <summary>
        /// Public method for adding notification within an existing transaction.
        /// Used by StokService to participate in same transaction.
        /// </summary>
        public string EkleInternal(SqlManager sMan, BildirimModel bildirim)
        {
            var cmd = sMan.CreateCommand("sp_bildirim_ekle", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@bildirim_tip", SqlDbType.NVarChar, 50)
                { Value = bildirim.BildirimTip });
            cmd.Parameters.Add(new SqlParameter("@baslik", SqlDbType.NVarChar, 200)
                { Value = bildirim.Baslik });
            cmd.Parameters.Add(new SqlParameter("@icerik", SqlDbType.NVarChar, -1)
                { Value = bildirim.Icerik ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@referans_tip", SqlDbType.NVarChar, 50)
                { Value = bildirim.ReferansTip ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@referans_id", SqlDbType.Int)
                { Value = bildirim.ReferansId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                { Value = bildirim.KullaniciId ?? (object)DBNull.Value });

            var outputParam = new SqlParameter("@bildirim_id", SqlDbType.Int)
                { Direction = ParameterDirection.Output };
            cmd.Parameters.Add(outputParam);

            sMan.ExecuteNonQuery(cmd);

            bildirim.BildirimId = (int)outputParam.Value;
            return null; // Success
        }

        public List<BildirimModel> Listele(int? kullaniciId = null, bool? okundu = null)
        {
            var result = new List<BildirimModel>();

            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_bildirim_listele", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = kullaniciId ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@okundu", SqlDbType.Bit)
                        { Value = okundu ?? (object)DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new BildirimModel
                        {
                            BildirimId = Convert.ToInt32(row["bildirim_id"]),
                            BildirimTip = row["bildirim_tip"].ToString(),
                            Baslik = row["baslik"].ToString(),
                            Icerik = row["icerik"] != DBNull.Value ? row["icerik"].ToString() : null,
                            ReferansTip = row["referans_tip"] != DBNull.Value ? row["referans_tip"].ToString() : null,
                            ReferansId = row["referans_id"] != DBNull.Value ? Convert.ToInt32(row["referans_id"]) : (int?)null,
                            Okundu = Convert.ToBoolean(row["okundu"]),
                            KullaniciId = row["kullanici_id"] != DBNull.Value ? Convert.ToInt32(row["kullanici_id"]) : (int?)null,
                            OlusturmaTarih = Convert.ToDateTime(row["olusturma_tarih"]),
                            OkunmaTarih = row["okunma_tarih"] != DBNull.Value ? Convert.ToDateTime(row["okunma_tarih"]) : (DateTime?)null
                        });
                    }
                }
            }
            catch (Exception)
            {
                // Return empty list on error
            }

            return result;
        }

        public string Okundu(int bildirimId)
        {
            try
            {
                if (bildirimId <= 0)
                    return "Geçersiz bildirim ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_bildirim_okundu", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@bildirim_id", SqlDbType.Int) { Value = bildirimId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // Success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Bildirim okundu olarak işaretlenemedi: " + ex.Message;
            }
        }

        public int OkunmamisSayisi(int? kullaniciId = null)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_bildirim_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = kullaniciId ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@okundu", SqlDbType.Bit) { Value = false });

                    var dt = sMan.ExecuteQuery(cmd);
                    return dt.Rows.Count;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

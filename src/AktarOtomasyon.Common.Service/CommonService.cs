using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Common.Service
{
    /// <summary>
    /// Ortak işlemler service implementasyonu.
    /// </summary>
    public class CommonService : ICommonInterface
    {
        public string BaglantıTest()
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    return sMan.TestConnection();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<TedarikciModel> TedarikciListele(bool? aktif = true)
        {
            var result = new List<TedarikciModel>();

            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_tedarikci_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = aktif ?? (object)DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new TedarikciModel
                        {
                            TedarikciId = Convert.ToInt32(row["tedarikci_id"]),
                            TedarikciKod = row["tedarikci_kod"] != DBNull.Value ? row["tedarikci_kod"].ToString() : string.Empty,
                            TedarikciAdi = row["tedarikci_adi"].ToString(),
                            Yetkili = row["yetkili"] != DBNull.Value ? row["yetkili"].ToString() : string.Empty,
                            Telefon = row["telefon"] != DBNull.Value ? row["telefon"].ToString() : string.Empty,
                            Email = row["email"] != DBNull.Value ? row["email"].ToString() : string.Empty,
                            Adres = row["adres"] != DBNull.Value ? row["adres"].ToString() : string.Empty,
                            Aktif = Convert.ToBoolean(row["aktif"])
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

        public string TedarikciKaydet(TedarikciModel tedarikci)
        {
            try
            {
                if (tedarikci == null)
                    return "Tedarikçi modeli boş olamaz.";

                if (string.IsNullOrWhiteSpace(tedarikci.TedarikciAdi))
                    return "Tedarikçi adı zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_tedarikci_kaydet", CommandType.StoredProcedure);

                    var tedarikciIdParam = new SqlParameter("@tedarikci_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = tedarikci.TedarikciId > 0 ? (object)tedarikci.TedarikciId : DBNull.Value
                    };
                    cmd.Parameters.Add(tedarikciIdParam);

                    cmd.Parameters.Add(new SqlParameter("@tedarikci_kod", SqlDbType.NVarChar, 20)
                        { Value = string.IsNullOrWhiteSpace(tedarikci.TedarikciKod) ? (object)DBNull.Value : tedarikci.TedarikciKod });
                    cmd.Parameters.Add(new SqlParameter("@tedarikci_adi", SqlDbType.NVarChar, 200)
                        { Value = tedarikci.TedarikciAdi });
                    cmd.Parameters.Add(new SqlParameter("@yetkili", SqlDbType.NVarChar, 100)
                        { Value = string.IsNullOrWhiteSpace(tedarikci.Yetkili) ? (object)DBNull.Value : tedarikci.Yetkili });
                    cmd.Parameters.Add(new SqlParameter("@telefon", SqlDbType.NVarChar, 20)
                        { Value = string.IsNullOrWhiteSpace(tedarikci.Telefon) ? (object)DBNull.Value : tedarikci.Telefon });
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar, 100)
                        { Value = string.IsNullOrWhiteSpace(tedarikci.Email) ? (object)DBNull.Value : tedarikci.Email });
                    cmd.Parameters.Add(new SqlParameter("@adres", SqlDbType.NVarChar, 500)
                        { Value = string.IsNullOrWhiteSpace(tedarikci.Adres) ? (object)DBNull.Value : tedarikci.Adres });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = tedarikci.Aktif });

                    sMan.ExecuteNonQuery(cmd);

                    // Update model with returned ID
                    if (tedarikciIdParam.Value != DBNull.Value)
                        tedarikci.TedarikciId = Convert.ToInt32(tedarikciIdParam.Value);

                    return null; // Success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Tedarikçi kaydedilemedi: " + ex.Message;
            }
        }
    }
}

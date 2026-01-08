using System;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Common.Service
{
    /// <summary>
    /// kul_ekran tablosu için service implementasyonu.
    /// KURAL: Service katmanı stateless - class-level değişken tutulmaz.
    /// KURAL: Her metodda try/catch, hata = ex.Message.
    /// KURAL: DB erişimi using(sMan) ile yapılır.
    /// </summary>
    public class KulEkranService : IKulEkranInterface
    {
        public KulEkranModel EkranGetir(string ekranKod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ekranKod))
                    return null;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kul_ekran_getir", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
                    {
                        Value = ekranKod
                    });

                    var dt = sMan.ExecuteQuery(cmd);

                    if (dt == null || dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new KulEkranModel
                    {
                        EkranId = Convert.ToInt32(row["ekran_id"]),
                        EkranKod = row["ekran_kod"] != DBNull.Value ? row["ekran_kod"].ToString() : null,
                        MenudekiAdi = row["menudeki_adi"] != DBNull.Value ? row["menudeki_adi"].ToString() : null,
                        FormAdi = row["form_adi"] != DBNull.Value ? row["form_adi"].ToString() : null,
                        Modul = row["modul"] != DBNull.Value ? row["modul"].ToString() : null,
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                    };
                }
            }
            catch (SqlException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string VersiyonLogla(string ekranKod, string versiyon, int? kullaniciId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ekranKod))
                    return "Ekran kodu boş olamaz.";

                if (string.IsNullOrWhiteSpace(versiyon))
                    return "Versiyon bilgisi boş olamaz.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_kul_ekran_versiyon_logla", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@ekran_kod", SqlDbType.NVarChar, 50)
                    {
                        Value = ekranKod
                    });

                    cmd.Parameters.Add(new SqlParameter("@versiyon", SqlDbType.NVarChar, 20)
                    {
                        Value = versiyon
                    });

                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                    {
                        Value = (object)kullaniciId ?? DBNull.Value
                    });

                    sMan.ExecuteNonQuery(cmd);

                    return null; // başarı
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0} - {1}", sqlEx.Number, sqlEx.Message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

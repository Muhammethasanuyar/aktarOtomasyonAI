using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Ai.Interface;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Ai.Service
{
    /// <summary>
    /// AI service implementasyonu.
    /// KURAL: AI çağrıları yalnız bu katmanda yapılır.
    /// Provider değişimi sadece config ile yapılabilir.
    /// </summary>
    public class AiService : IAiInterface
    {
        public AiIcerikModel IcerikGetir(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_icerik_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new AiIcerikModel
                    {
                        IcerikId = Convert.ToInt32(row["icerik_id"]),
                        UrunId = Convert.ToInt32(row["urun_id"]),
                        Icerik = row["icerik"].ToString(),
                        Durum = row["durum"].ToString(),
                        Kaynaklar = row["kaynaklar"] != DBNull.Value ? row["kaynaklar"].ToString() : null,
                        OlusturmaTarih = Convert.ToDateTime(row["olusturma_tarih"]),
                        OnayTarih = row["onay_tarih"] != DBNull.Value ? Convert.ToDateTime(row["onay_tarih"]) : (DateTime?)null
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string IcerikKaydet(AiIcerikModel icerik)
        {
            try
            {
                // Validation
                if (icerik == null)
                    return "İçerik modeli boş olamaz.";
                if (icerik.UrunId <= 0)
                    return "Geçersiz ürün ID.";
                if (string.IsNullOrWhiteSpace(icerik.Icerik))
                    return "İçerik boş olamaz.";

                // Security check - forbidden keywords
                var securityError = CheckForbiddenContent(icerik.Icerik);
                if (securityError != null)
                    return securityError;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_icerik_kaydet", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                        { Value = icerik.UrunId });
                    cmd.Parameters.Add(new SqlParameter("@icerik", SqlDbType.NVarChar, -1)
                        { Value = icerik.Icerik });
                    cmd.Parameters.Add(new SqlParameter("@durum", SqlDbType.NVarChar, 20)
                        { Value = icerik.Durum ?? "TASLAK" });
                    cmd.Parameters.Add(new SqlParameter("@kaynaklar", SqlDbType.NVarChar, -1)
                        { Value = (object)icerik.Kaynaklar ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@sablon_kod", SqlDbType.NVarChar, 50)
                        { Value = (object)icerik.SablonKod ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@provider", SqlDbType.NVarChar, 50)
                        { Value = (object)icerik.Provider ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = (object)icerik.KullaniciId ?? DBNull.Value });

                    var paramIcerikId = new SqlParameter("@icerik_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramIcerikId);

                    sMan.ExecuteNonQuery(cmd);
                    icerik.IcerikId = Convert.ToInt32(paramIcerikId.Value);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0}", sqlEx.Message);
            }
            catch (Exception ex)
            {
                return string.Format("İçerik kaydedilemedi: {0}", ex.Message);
            }
        }

        public List<AiIcerikVersiyonModel> VersiyonListele(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_icerik_versiyon_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<AiIcerikVersiyonModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new AiIcerikVersiyonModel
                        {
                            VersiyonId = Convert.ToInt32(row["versiyon_id"]),
                            IcerikId = Convert.ToInt32(row["icerik_id"]),
                            VersiyonNo = Convert.ToInt32(row["versiyon_no"]),
                            Icerik = row["icerik"].ToString(),
                            OlusturmaTarih = Convert.ToDateTime(row["olusturma_tarih"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<AiIcerikVersiyonModel>();
            }
        }

        public string IcerikOnayla(int icerikId)
        {
            try
            {
                if (icerikId <= 0)
                    return "Geçersiz içerik ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_icerik_onayla", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@icerik_id", SqlDbType.Int) { Value = icerikId });
                    cmd.Parameters.Add(new SqlParameter("@onaylayan_kullanici_id", SqlDbType.Int)
                        { Value = DBNull.Value }); // TODO: Get from session context

                    sMan.ExecuteNonQuery(cmd);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0}", sqlEx.Message);
            }
            catch (Exception ex)
            {
                return string.Format("İçerik onaylanamadı: {0}", ex.Message);
            }
        }

        public AiUretimSonuc IcerikUret(int urunId, string sablonKod)
        {
            try
            {
                // 1. Validate inputs
                if (urunId <= 0)
                    return new AiUretimSonuc
                    {
                        Basarili = false,
                        Hata = "Geçersiz ürün ID."
                    };

                if (string.IsNullOrWhiteSpace(sablonKod))
                    return new AiUretimSonuc
                    {
                        Basarili = false,
                        Hata = "Şablon kodu zorunludur."
                    };

                // 2. Get AI provider
                var provider = AiProviderBase.GetProvider();

                // 3. Generate content via provider
                var sonuc = provider.Generate(urunId, sablonKod);

                // 4. Security check on generated content
                if (sonuc.Basarili && !string.IsNullOrEmpty(sonuc.UretilenIcerik))
                {
                    var securityError = CheckForbiddenContent(sonuc.UretilenIcerik);
                    if (securityError != null)
                    {
                        return new AiUretimSonuc
                        {
                            Basarili = false,
                            Hata = securityError
                        };
                    }
                }

                // 5. Auto-save if successful
                if (sonuc.Basarili)
                {
                    var icerik = new AiIcerikModel
                    {
                        UrunId = urunId,
                        Icerik = sonuc.UretilenIcerik,
                        Durum = "TASLAK",
                        SablonKod = sablonKod,
                        Provider = provider.GetType().Name.Replace("Provider", "").ToUpper()
                    };

                    var kaydetHata = IcerikKaydet(icerik);
                    if (kaydetHata != null)
                    {
                        sonuc.Basarili = false;
                        sonuc.Hata = string.Format("İçerik oluşturuldu ancak kaydedilemedi: {0}", kaydetHata);
                    }
                }

                return sonuc;
            }
            catch (Exception ex)
            {
                return new AiUretimSonuc
                {
                    Basarili = false,
                    Hata = string.Format("İçerik üretimi sırasında hata: {0}", ex.Message)
                };
            }
        }

        /// <summary>
        /// Checks content for forbidden medical/health claims.
        /// Returns null if OK, error message if forbidden content found.
        /// </summary>
        private string CheckForbiddenContent(string icerik)
        {
            if (string.IsNullOrWhiteSpace(icerik))
                return null;

            // Turkish keyword list for medical claims
            var forbiddenKeywords = new[]
            {
                "tedavi eder",
                "tedavi edilir",
                "iyileştirir",
                "önler",
                "korur",
                "kanser",
                "diyabet",
                "hastalık tedavisi",
                "hastalığı tedavi",
                "hastalıktan korur",
                "kesin çözüm",
                "garantili",
                "mucizevi",
                "şifa bulur",
                "şifa verir"
            };

            var lowerContent = icerik.ToLowerInvariant();

            foreach (var keyword in forbiddenKeywords)
            {
                if (lowerContent.Contains(keyword.ToLowerInvariant()))
                {
                    return string.Format("Güvenlik uyarısı: İçerik yasak ifade içeriyor: '{0}'", keyword);
                }
            }

            return null; // OK
        }

        // Template helper methods (not in interface, internal use)

        public AiSablonModel SablonGetir(string sablonKod)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_sablon_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@sablon_kod", SqlDbType.NVarChar, 50)
                        { Value = sablonKod });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new AiSablonModel
                    {
                        SablonId = Convert.ToInt32(row["sablon_id"]),
                        SablonKod = row["sablon_kod"].ToString(),
                        SablonAdi = row["sablon_adi"].ToString(),
                        PromptSablonu = row["prompt_sablonu"].ToString(),
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AiSablonListModel> SablonListele(bool? aktif = true)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_sablon_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)aktif ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<AiSablonListModel>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new AiSablonListModel
                        {
                            SablonId = Convert.ToInt32(row["sablon_id"]),
                            SablonKod = row["sablon_kod"].ToString(),
                            SablonAdi = row["sablon_adi"].ToString(),
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<AiSablonListModel>();
            }
        }

        public string SablonKaydet(AiSablonModel sablon)
        {
            try
            {
                // Validation
                if (sablon == null)
                    return "Şablon modeli boş olamaz.";
                if (string.IsNullOrWhiteSpace(sablon.SablonKod))
                    return "Şablon kodu zorunludur.";
                if (string.IsNullOrWhiteSpace(sablon.SablonAdi))
                    return "Şablon adı zorunludur.";
                if (string.IsNullOrWhiteSpace(sablon.PromptSablonu))
                    return "Prompt şablonu zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_sablon_kaydet", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@sablon_id", SqlDbType.Int)
                        { Value = sablon.SablonId });
                    cmd.Parameters.Add(new SqlParameter("@sablon_kod", SqlDbType.NVarChar, 50)
                        { Value = sablon.SablonKod.ToUpper() });
                    cmd.Parameters.Add(new SqlParameter("@sablon_adi", SqlDbType.NVarChar, 200)
                        { Value = sablon.SablonAdi });
                    cmd.Parameters.Add(new SqlParameter("@prompt_sablonu", SqlDbType.NVarChar, -1)
                        { Value = sablon.PromptSablonu });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                        { Value = (object)sablon.Aciklama ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = sablon.Aktif });

                    sMan.ExecuteNonQuery(cmd);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0}", sqlEx.Message);
            }
            catch (Exception ex)
            {
                return string.Format("Şablon kaydedilemedi: {0}", ex.Message);
            }
        }

        public string SablonSil(int sablonId)
        {
            try
            {
                if (sablonId <= 0)
                    return "Geçersiz şablon ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_sablon_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@sablon_id", SqlDbType.Int) { Value = sablonId });

                    sMan.ExecuteNonQuery(cmd);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0}", sqlEx.Message);
            }
            catch (Exception ex)
            {
                return string.Format("Şablon silinemedi: {0}", ex.Message);
            }
        }

        public string SablonAktiflikGuncelle(int sablonId, bool aktif)
        {
            try
            {
                if (sablonId <= 0)
                    return "Geçersiz şablon ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_ai_sablon_aktiflik_guncelle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@sablon_id", SqlDbType.Int) { Value = sablonId });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit) { Value = aktif });

                    sMan.ExecuteNonQuery(cmd);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return string.Format("Veritabanı hatası: {0}", sqlEx.Message);
            }
            catch (Exception ex)
            {
                return string.Format("Aktiflik güncellenemedi: {0}", ex.Message);
            }
        }
    }
}

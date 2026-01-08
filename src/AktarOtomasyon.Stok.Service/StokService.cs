using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AktarOtomasyon.Common.Interface;
using AktarOtomasyon.Common.Service;
using AktarOtomasyon.Stok.Interface;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Stok.Service
{
    public class StokService : IStokInterface
    {
        public string HareketEkle(StokHareketModel hareket)
        {
            try
            {
                // Validation
                if (hareket == null)
                    return "Stok hareketi modeli boş olamaz.";
                if (hareket.UrunId <= 0)
                    return "Geçersiz ürün ID.";
                if (string.IsNullOrWhiteSpace(hareket.HareketTip))
                    return "Hareket tipi zorunludur.";
                if (!new[] { "GIRIS", "CIKIS", "SAYIM" }.Contains(hareket.HareketTip.ToUpper()))
                    return "Hareket tipi GIRIS, CIKIS veya SAYIM olmalıdır.";
                if (hareket.Miktar <= 0)
                    return "Miktar sıfırdan büyük olmalıdır.";

                using (var sMan = new SqlManager())
                {
                    sMan.BeginTransaction();
                    try
                    {
                        // 1. Add stock movement
                        var cmd = sMan.CreateCommand("sp_stok_hareket_ekle", CommandType.StoredProcedure);

                        cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                            { Value = hareket.UrunId });
                        cmd.Parameters.Add(new SqlParameter("@hareket_tip", SqlDbType.NVarChar, 20)
                            { Value = hareket.HareketTip.ToUpper() });
                        cmd.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                            { Value = hareket.Miktar, Precision = 18, Scale = 2 });
                        cmd.Parameters.Add(new SqlParameter("@referans_tip", SqlDbType.NVarChar, 50)
                            { Value = DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@referans_id", SqlDbType.Int)
                            { Value = DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                            { Value = hareket.Aciklama ?? (object)DBNull.Value });
                        cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                            { Value = DBNull.Value });

                        sMan.ExecuteNonQuery(cmd);

                        // 2. Get current stock level
                        var cmdStok = sMan.CreateCommand("sp_stok_durum_getir", CommandType.StoredProcedure);
                        cmdStok.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                            { Value = hareket.UrunId });

                        var dtStok = sMan.ExecuteQuery(cmdStok);
                        if (dtStok.Rows.Count == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Ürün stok durumu bulunamadı.";
                        }

                        var mevcutStok = Convert.ToDecimal(dtStok.Rows[0]["mevcut_stok"]);

                        // 3. Get product name and critical stock threshold
                        var cmdAyar = sMan.CreateCommand("sp_urun_stok_ayar_getir", CommandType.StoredProcedure);
                        cmdAyar.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                            { Value = hareket.UrunId });

                        var dtAyar = sMan.ExecuteQuery(cmdAyar);
                        if (dtAyar.Rows.Count > 0)
                        {
                            var kritikStok = Convert.ToDecimal(dtAyar.Rows[0]["kritik_stok"]);

                            // 4. If critical, create notification
                            if (mevcutStok <= kritikStok)
                            {
                                // Get product name for notification
                                var cmdUrun = sMan.CreateCommand("SELECT urun_adi FROM urun WHERE urun_id = @urun_id", CommandType.Text);
                                cmdUrun.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = hareket.UrunId });
                                var dtUrun = sMan.ExecuteQuery(cmdUrun);
                                var urunAdi = dtUrun.Rows.Count > 0 ? dtUrun.Rows[0]["urun_adi"].ToString() : "Bilinmeyen Ürün";

                                var bildirimModel = new BildirimModel
                                {
                                    BildirimTip = "STOK_KRITIK",
                                    Baslik = "Kritik Stok Uyarısı",
                                    Icerik = string.Format(
                                        "{0} ürününün stok seviyesi kritik! Mevcut: {1}, Kritik Seviye: {2}",
                                        urunAdi, mevcutStok, kritikStok
                                    ),
                                    ReferansTip = "URUN",
                                    ReferansId = hareket.UrunId,
                                    KullaniciId = null
                                };

                                var bildirimService = new BildirimService();
                                var bildirimError = bildirimService.EkleInternal(sMan, bildirimModel);

                                if (bildirimError != null)
                                {
                                    sMan.RollbackTransaction();
                                    return "Bildirim oluşturulamadı: " + bildirimError;
                                }
                            }
                        }

                        sMan.CommitTransaction();
                        return null; // Success
                    }
                    catch
                    {
                        sMan.RollbackTransaction();
                        throw;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Stok hareketi eklenemedi: " + ex.Message;
            }
        }

        public decimal DurumGetir(int urunId)
        {
            try
            {
                if (urunId <= 0)
                    return 0;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_stok_durum_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);

                    if (dt.Rows.Count == 0)
                        return 0;

                    return Convert.ToDecimal(dt.Rows[0]["mevcut_stok"]);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<StokKritikModel> KritikListele()
        {
            var result = new List<StokKritikModel>();

            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_stok_kritik_listele", CommandType.StoredProcedure);
                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new StokKritikModel
                        {
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            UrunAdi = row["urun_adi"].ToString(),
                            MevcutStok = Convert.ToDecimal(row["mevcut_stok"]),
                            MinStok = Convert.ToDecimal(row["min_stok"])
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

        public List<StokHareketListeDto> HareketListele()
        {
            var result = new List<StokHareketListeDto>();

            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_stok_hareket_listele", CommandType.StoredProcedure);
                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new StokHareketListeDto
                        {
                            HareketId = Convert.ToInt32(row["hareket_id"]),
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            UrunAdi = row["urun_adi"].ToString(),
                            HareketTip = row["hareket_tip"].ToString(),
                            Miktar = Convert.ToDecimal(row["miktar"]),
                            HareketTarih = Convert.ToDateTime(row["hareket_tarih"]),
                            Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : string.Empty,
                            OncekiBakiye = row["onceki_bakiye"] != DBNull.Value ? Convert.ToDecimal(row["onceki_bakiye"]) : 0,
                            SonrakiBakiye = row["sonraki_bakiye"] != DBNull.Value ? Convert.ToDecimal(row["sonraki_bakiye"]) : 0
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

        /// <summary>
        /// Transaction-aware version of HareketEkle for use within existing transactions.
        /// Used by SiparisService.TeslimAl to create stock movements atomically.
        /// </summary>
        public string HareketEkleInternal(SqlManager sMan, StokHareketModel hareket)
        {
            // NO using(sMan) - caller owns the connection
            // NO transaction management - caller manages transaction

            // Validation (same as HareketEkle)
            if (hareket == null)
                return "Stok hareketi modeli boş olamaz.";
            if (hareket.UrunId <= 0)
                return "Geçersiz ürün ID.";
            if (string.IsNullOrWhiteSpace(hareket.HareketTip))
                return "Hareket tipi zorunludur.";
            if (!new[] { "GIRIS", "CIKIS", "SAYIM" }.Contains(hareket.HareketTip.ToUpper()))
                return "Hareket tipi GIRIS, CIKIS veya SAYIM olmalıdır.";
            if (hareket.Miktar <= 0)
                return "Miktar sıfırdan büyük olmalıdır.";

            // Add stock movement
            var cmd = sMan.CreateCommand("sp_stok_hareket_ekle", CommandType.StoredProcedure);

            cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                { Value = hareket.UrunId });
            cmd.Parameters.Add(new SqlParameter("@hareket_tip", SqlDbType.NVarChar, 20)
                { Value = hareket.HareketTip.ToUpper() });
            cmd.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                { Value = hareket.Miktar, Precision = 18, Scale = 2 });
            cmd.Parameters.Add(new SqlParameter("@referans_tip", SqlDbType.NVarChar, 50)
                { Value = hareket.ReferansTip ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@referans_id", SqlDbType.Int)
                { Value = hareket.ReferansId ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                { Value = hareket.Aciklama ?? (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                { Value = hareket.KullaniciId ?? (object)DBNull.Value });

            sMan.ExecuteNonQuery(cmd);

            return null; // Success
        }

        public List<SatisRaporuDto> SatisRaporuGetir(System.DateTime baslangic, System.DateTime bitis)
        {
            var list = new List<SatisRaporuDto>();
            try
            {
                using (var sMan = new SqlManager())
                {
                    var query = @"
                        SELECT 
                            u.urun_id, 
                            u.urun_adi, 
                            SUM(sh.miktar) as toplam_miktar,
                            COUNT(sh.hareket_id) as islem_sayisi
                        FROM stok_hareket sh
                        INNER JOIN urun u ON sh.urun_id = u.urun_id
                        WHERE sh.hareket_tip = 'CIKIS'
                        AND sh.tarih >= @baslangic AND sh.tarih <= @bitis
                        GROUP BY u.urun_id, u.urun_adi
                        ORDER BY toplam_miktar DESC";

                    var cmd = sMan.CreateCommand(query, CommandType.Text);
                    cmd.Parameters.Add(new SqlParameter("@baslangic", SqlDbType.DateTime) { Value = baslangic });
                    cmd.Parameters.Add(new SqlParameter("@bitis", SqlDbType.DateTime) { Value = bitis });

                    var dt = sMan.ExecuteQuery(cmd);
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new SatisRaporuDto
                        {
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            UrunAdi = row["urun_adi"].ToString(),
                            ToplamMiktar = Convert.ToDecimal(row["toplam_miktar"]),
                            IslemSayisi = Convert.ToInt32(row["islem_sayisi"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("SatisRaporuGetir Error: " + ex.Message);
            }
            return list;
        }

        public StokDurumDto StokDurumGetir(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_stok_durum_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    
                    // Also get critical stock level from stok_ayar
                    decimal kritikStok = 0;
                    var cmdAyar = sMan.CreateCommand("sp_urun_stok_ayar_getir", CommandType.StoredProcedure);
                    cmdAyar.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });
                    var dtAyar = sMan.ExecuteQuery(cmdAyar);
                    if (dtAyar.Rows.Count > 0)
                    {
                        kritikStok = Convert.ToDecimal(dtAyar.Rows[0]["kritik_stok"]);
                    }

                    return new StokDurumDto
                    {
                        UrunId = urunId,
                        MevcutStok = Convert.ToDecimal(row["mevcut_stok"]),
                        KritikStok = kritikStok
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AktarOtomasyon.Siparis.Interface;
using AktarOtomasyon.Stok.Interface;
using AktarOtomasyon.Stok.Service;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Siparis.Service
{
    public class SiparisService : ISiparisInterface
    {
        public string TaslakOlustur(int tedarikciId)
        {
            try
            {
                // Validation
                if (tedarikciId <= 0)
                    return "Geçersiz tedarikçi ID.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_siparis_taslak_olustur", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@tedarikci_id", SqlDbType.Int)
                        { Value = tedarikciId });
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = DBNull.Value });

                    var outputParam = new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputParam);

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
                return "Sipariş taslağı oluşturulamadı: " + ex.Message;
            }
        }

        public string SatirEkle(SiparisSatirModel satir)
        {
            try
            {
                // Validation
                if (satir == null)
                    return "Sipariş satır modeli boş olamaz.";
                if (satir.SiparisId <= 0)
                    return "Geçersiz sipariş ID.";
                if (satir.UrunId <= 0)
                    return "Geçersiz ürün ID.";
                if (satir.Miktar <= 0)
                    return "Miktar sıfırdan büyük olmalıdır.";
                if (satir.BirimFiyat < 0)
                    return "Birim fiyat negatif olamaz.";

                using (var sMan = new SqlManager())
                {
                    // Business Rule: Check if order status allows modifications
                    var cmdCheck = sMan.CreateCommand(
                        "SELECT durum FROM siparis WHERE siparis_id = @siparis_id",
                        CommandType.Text);
                    cmdCheck.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = satir.SiparisId });

                    var dt = sMan.ExecuteQuery(cmdCheck);
                    if (dt.Rows.Count == 0)
                        return "Sipariş bulunamadı.";

                    var durum = dt.Rows[0]["durum"].ToString();
                    if (durum == "TAMAMLANDI" || durum == "IPTAL")
                        return string.Format("'{0}' durumundaki siparişe satır eklenemez.", durum);

                    // Add line
                    var cmd = sMan.CreateCommand("sp_siparis_satir_ekle", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = satir.SiparisId });
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                        { Value = satir.UrunId });
                    cmd.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                        { Value = satir.Miktar, Precision = 18, Scale = 2 });
                    cmd.Parameters.Add(new SqlParameter("@birim_fiyat", SqlDbType.Decimal)
                        { Value = satir.BirimFiyat, Precision = 18, Scale = 2 });

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
                return "Sipariş satırı eklenemedi: " + ex.Message;
            }
        }

        public List<SiparisSatirModel> SatirListele(int siparisId)
        {
            var result = new List<SiparisSatirModel>();

            try
            {
                if (siparisId <= 0)
                    return result;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_siparis_satir_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = siparisId });

                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new SiparisSatirModel
                        {
                            SatirId = Convert.ToInt32(row["satir_id"]),
                            SiparisId = Convert.ToInt32(row["siparis_id"]),
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            UrunAdi = row["urun_adi"].ToString(),
                            Miktar = Convert.ToDecimal(row["miktar"]),
                            BirimFiyat = Convert.ToDecimal(row["birim_fiyat"]),
                            Tutar = Convert.ToDecimal(row["tutar"]),
                            TeslimMiktar = Convert.ToDecimal(row["teslim_miktar"])
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

        public List<SiparisModel> Listele()
        {
            var result = new List<SiparisModel>();

            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_siparis_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@durum", SqlDbType.NVarChar, 20)
                        { Value = DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);

                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new SiparisModel
                        {
                            SiparisId = Convert.ToInt32(row["siparis_id"]),
                            SiparisNo = row["siparis_no"].ToString(),
                            TedarikciId = Convert.ToInt32(row["tedarikci_id"]),
                            TedarikciAdi = row["tedarikci_adi"].ToString(),
                            SiparisTarih = Convert.ToDateTime(row["siparis_tarih"]),
                            BeklenenTeslimTarih = row["beklenen_teslim_tarih"] != DBNull.Value
                                ? Convert.ToDateTime(row["beklenen_teslim_tarih"])
                                : (DateTime?)null,
                            Durum = row["durum"].ToString(),
                            ToplamTutar = Convert.ToDecimal(row["toplam_tutar"])
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

        public string DurumGuncelle(int siparisId, string durum)
        {
            try
            {
                // Validation
                if (siparisId <= 0)
                    return "Geçersiz sipariş ID.";
                if (string.IsNullOrWhiteSpace(durum))
                    return "Durum zorunludur.";

                durum = durum.ToUpper();

                // Valid statuses
                var validStatuses = new[] { "TASLAK", "GONDERILDI", "KISMI", "TAMAMLANDI", "IPTAL" };
                if (!validStatuses.Contains(durum))
                    return "Geçersiz durum. İzin verilenler: " + string.Join(", ", validStatuses);

                using (var sMan = new SqlManager())
                {
                    // Get current status
                    var cmdCheck = sMan.CreateCommand(
                        "SELECT durum FROM siparis WHERE siparis_id = @siparis_id",
                        CommandType.Text);
                    cmdCheck.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = siparisId });

                    var dt = sMan.ExecuteQuery(cmdCheck);
                    if (dt.Rows.Count == 0)
                        return "Sipariş bulunamadı.";

                    var currentDurum = dt.Rows[0]["durum"].ToString();

                    // Business Rule: Validate status transitions
                    var validationError = ValidateDurumGecisi(currentDurum, durum);
                    if (validationError != null)
                        return validationError;

                    // Update status
                    var cmd = sMan.CreateCommand("sp_siparis_durum_guncelle", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = siparisId });
                    cmd.Parameters.Add(new SqlParameter("@durum", SqlDbType.NVarChar, 20)
                        { Value = durum });

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
                return "Sipariş durumu güncellenemedi: " + ex.Message;
            }
        }

        private string ValidateDurumGecisi(string currentDurum, string newDurum)
        {
            // Rule 1: Cannot change from TAMAMLANDI or IPTAL
            if (currentDurum == "TAMAMLANDI" || currentDurum == "IPTAL")
                return string.Format("'{0}' durumundaki siparişin durumu değiştirilemez.", currentDurum);

            // Rule 2: Cannot go back to TASLAK from other statuses
            if (newDurum == "TASLAK" && currentDurum != "TASLAK")
                return "Sipariş durumu TASLAK'a geri alınamaz.";

            return null; // Valid transition
        }

        public string TaslakOlusturKritikStoktan(int tedarikciId)
        {
            try
            {
                // Validation
                if (tedarikciId <= 0)
                    return "Geçersiz tedarikçi ID.";

                using (var sMan = new SqlManager())
                {
                    sMan.BeginTransaction();
                    try
                    {
                        // 1. Create order draft
                        var cmdSiparis = sMan.CreateCommand("sp_siparis_taslak_olustur", CommandType.StoredProcedure);
                        cmdSiparis.Parameters.Add(new SqlParameter("@tedarikci_id", SqlDbType.Int)
                            { Value = tedarikciId });
                        cmdSiparis.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                            { Value = DBNull.Value });

                        var outputParam = new SqlParameter("@siparis_id", SqlDbType.Int)
                            { Direction = ParameterDirection.Output };
                        cmdSiparis.Parameters.Add(outputParam);

                        sMan.ExecuteNonQuery(cmdSiparis);
                        var siparisId = (int)outputParam.Value;

                        // 2. Get critical stock items
                        var stokService = new StokService();
                        var kritikStoklar = stokService.KritikListele();

                        if (kritikStoklar.Count == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Kritik stok seviyesinde ürün bulunamadı.";
                        }

                        // 3. Filter by tedarikci_id
                        var cmdFilter = sMan.CreateCommand(
                            "SELECT urun_id FROM urun WHERE tedarikci_id = @tedarikci_id",
                            CommandType.Text);
                        cmdFilter.Parameters.Add(new SqlParameter("@tedarikci_id", SqlDbType.Int)
                            { Value = tedarikciId });

                        var dtTedarikciUrunler = sMan.ExecuteQuery(cmdFilter);
                        var tedarikciUrunIds = new HashSet<int>();
                        foreach (DataRow row in dtTedarikciUrunler.Rows)
                        {
                            tedarikciUrunIds.Add(Convert.ToInt32(row["urun_id"]));
                        }

                        var filteredStoklar = kritikStoklar
                            .Where(s => tedarikciUrunIds.Contains(s.UrunId))
                            .ToList();

                        if (filteredStoklar.Count == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Bu tedarikçiye ait kritik stokta ürün bulunamadı.";
                        }

                        // 4. Add order lines for each critical product
                        int satirSayisi = 0;
                        foreach (var kritikStok in filteredStoklar)
                        {
                            // Calculate order quantity
                            decimal hedefStok = (kritikStok.MinStok > 0 ? kritikStok.MinStok : kritikStok.MevcutStok * 2);
                            decimal siparisMiktar = hedefStok - kritikStok.MevcutStok;

                            if (siparisMiktar <= 0)
                                continue; // Skip if no order needed

                            // Use default price
                            decimal birimFiyat = 0;

                            var cmdSatir = sMan.CreateCommand("sp_siparis_satir_ekle", CommandType.StoredProcedure);
                            cmdSatir.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                                { Value = siparisId });
                            cmdSatir.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                                { Value = kritikStok.UrunId });
                            cmdSatir.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                                { Value = siparisMiktar, Precision = 18, Scale = 2 });
                            cmdSatir.Parameters.Add(new SqlParameter("@birim_fiyat", SqlDbType.Decimal)
                                { Value = birimFiyat, Precision = 18, Scale = 2 });

                            sMan.ExecuteNonQuery(cmdSatir);
                            satirSayisi++;
                        }

                        if (satirSayisi == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Sipariş oluşturulacak ürün bulunamadı.";
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
                return "Kritik stoktan sipariş oluşturulamadı: " + ex.Message;
            }
        }

        public string TeslimAl(int siparisId)
        {
            try
            {
                // Validation
                if (siparisId <= 0)
                    return "Geçersiz sipariş ID.";

                using (var sMan = new SqlManager())
                {
                    sMan.BeginTransaction();
                    try
                    {
                        // 1. Check order status
                        var cmdCheck = sMan.CreateCommand(
                            "SELECT durum FROM siparis WHERE siparis_id = @siparis_id",
                            CommandType.Text);
                        cmdCheck.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                            { Value = siparisId });

                        var dt = sMan.ExecuteQuery(cmdCheck);
                        if (dt.Rows.Count == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Sipariş bulunamadı.";
                        }

                        var durum = dt.Rows[0]["durum"].ToString();
                        if (durum == "TASLAK")
                        {
                            sMan.RollbackTransaction();
                            return "TASLAK durumundaki sipariş teslim alınamaz. Önce 'GONDERILDI' durumuna alınız.";
                        }
                        if (durum == "TAMAMLANDI")
                        {
                            sMan.RollbackTransaction();
                            return "Sipariş zaten tamamlanmış.";
                        }
                        if (durum == "IPTAL")
                        {
                            sMan.RollbackTransaction();
                            return "İptal edilmiş sipariş teslim alınamaz.";
                        }

                        // 2. Get order lines
                        var cmdSatirlar = sMan.CreateCommand("sp_siparis_satir_listele", CommandType.StoredProcedure);
                        cmdSatirlar.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                            { Value = siparisId });

                        var dtSatirlar = sMan.ExecuteQuery(cmdSatirlar);

                        if (dtSatirlar.Rows.Count == 0)
                        {
                            sMan.RollbackTransaction();
                            return "Siparişte satır bulunamadı.";
                        }

                        // 3. Create stock movements for each line
                        var stokService = new StokService();

                        foreach (DataRow row in dtSatirlar.Rows)
                        {
                            var satirId = Convert.ToInt32(row["satir_id"]);
                            var urunId = Convert.ToInt32(row["urun_id"]);
                            var miktar = Convert.ToDecimal(row["miktar"]);
                            var mevcutTeslimMiktar = Convert.ToDecimal(row["teslim_miktar"]);

                            // Calculate quantity to receive (full delivery)
                            decimal teslimMiktar = miktar - mevcutTeslimMiktar;

                            if (teslimMiktar <= 0)
                                continue; // Already fully delivered

                            // Create stock movement
                            var hareketModel = new Stok.Interface.StokHareketModel
                            {
                                UrunId = urunId,
                                HareketTip = "GIRIS",
                                Miktar = teslimMiktar,
                                ReferansTip = "SIPARIS",
                                ReferansId = siparisId,
                                Aciklama = string.Format("Sipariş teslim alımı (Satır: {0})", satirId),
                                KullaniciId = null
                            };

                            var stokError = stokService.HareketEkleInternal(sMan, hareketModel);
                            if (stokError != null)
                            {
                                sMan.RollbackTransaction();
                                return string.Format("Stok hareketi oluşturulamadı: {0}", stokError);
                            }

                            // Update teslim_miktar
                            var cmdUpdate = sMan.CreateCommand(
                                "UPDATE siparis_satir SET teslim_miktar = @teslim_miktar WHERE satir_id = @satir_id",
                                CommandType.Text);
                            cmdUpdate.Parameters.Add(new SqlParameter("@teslim_miktar", SqlDbType.Decimal)
                                { Value = miktar, Precision = 18, Scale = 2 });
                            cmdUpdate.Parameters.Add(new SqlParameter("@satir_id", SqlDbType.Int)
                                { Value = satirId });

                            sMan.ExecuteNonQuery(cmdUpdate);
                        }

                        // 4. Update order status to TAMAMLANDI
                        var cmdDurum = sMan.CreateCommand("sp_siparis_durum_guncelle", CommandType.StoredProcedure);
                        cmdDurum.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                            { Value = siparisId });
                        cmdDurum.Parameters.Add(new SqlParameter("@durum", SqlDbType.NVarChar, 20)
                            { Value = "TAMAMLANDI" });

                        sMan.ExecuteNonQuery(cmdDurum);

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
                return "Sipariş teslim alınamadı: " + ex.Message;
            }
        }

        public SiparisModel TaslakOlusturDetayli(int tedarikciId)
        {
            try
            {
                // Validation
                if (tedarikciId <= 0)
                    return null;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_siparis_taslak_olustur", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@tedarikci_id", SqlDbType.Int)
                        { Value = tedarikciId });
                    cmd.Parameters.Add(new SqlParameter("@kullanici_id", SqlDbType.Int)
                        { Value = DBNull.Value });

                    var outputParam = new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(outputParam);

                    sMan.ExecuteNonQuery(cmd);

                    var siparisId = (int)outputParam.Value;

                    // Get and return the created order
                    return Getir(siparisId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public SiparisModel Getir(int siparisId)
        {
            try
            {
                if (siparisId <= 0)
                    return null;

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_siparis_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = siparisId });

                    var dt = sMan.ExecuteQuery(cmd);

                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new SiparisModel
                    {
                        SiparisId = Convert.ToInt32(row["siparis_id"]),
                        SiparisNo = row["siparis_no"].ToString(),
                        TedarikciId = Convert.ToInt32(row["tedarikci_id"]),
                        TedarikciAdi = row["tedarikci_adi"].ToString(),
                        SiparisTarih = Convert.ToDateTime(row["siparis_tarih"]),
                        BeklenenTeslimTarih = row["beklenen_teslim_tarih"] != DBNull.Value
                            ? Convert.ToDateTime(row["beklenen_teslim_tarih"])
                            : (DateTime?)null,
                        Durum = row["durum"].ToString(),
                        ToplamTutar = Convert.ToDecimal(row["toplam_tutar"]),
                        Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : string.Empty
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GuncelleHeader(SiparisModel model)
        {
            try
            {
                // Validation
                if (model == null)
                    return "Sipariş modeli boş olamaz.";
                if (model.SiparisId <= 0)
                    return "Geçersiz sipariş ID.";
                if (model.TedarikciId <= 0)
                    return "Geçersiz tedarikçi ID.";

                using (var sMan = new SqlManager())
                {
                    // Check if order exists and is TASLAK
                    var cmdCheck = sMan.CreateCommand(
                        "SELECT durum FROM siparis WHERE siparis_id = @siparis_id",
                        CommandType.Text);
                    cmdCheck.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = model.SiparisId });

                    var dt = sMan.ExecuteQuery(cmdCheck);
                    if (dt.Rows.Count == 0)
                        return "Sipariş bulunamadı.";

                    var durum = dt.Rows[0]["durum"].ToString();
                    if (durum != "TASLAK")
                        return "Sadece TASLAK durumundaki siparişler güncellenebilir.";

                    // Update header
                    var cmd = sMan.CreateCommand("sp_siparis_header_guncelle", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@siparis_id", SqlDbType.Int)
                        { Value = model.SiparisId });
                    cmd.Parameters.Add(new SqlParameter("@tedarikci_id", SqlDbType.Int)
                        { Value = model.TedarikciId });
                    cmd.Parameters.Add(new SqlParameter("@siparis_tarih", SqlDbType.DateTime)
                        { Value = model.SiparisTarih });
                    cmd.Parameters.Add(new SqlParameter("@beklenen_teslim_tarih", SqlDbType.DateTime)
                        { Value = model.BeklenenTeslimTarih ?? (object)DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, 500)
                        { Value = model.Aciklama ?? (object)DBNull.Value });

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
                return "Sipariş güncellenemedi: " + ex.Message;
            }
        }

        public string SatirSil(int satirId)
        {
            try
            {
                // Validation
                if (satirId <= 0)
                    return "Geçersiz satır ID.";

                using (var sMan = new SqlManager())
                {
                    // Check if order allows modifications
                    var cmdCheck = sMan.CreateCommand(
                        @"SELECT s.durum
                          FROM siparis s
                          INNER JOIN siparis_satir ss ON s.siparis_id = ss.siparis_id
                          WHERE ss.satir_id = @satir_id",
                        CommandType.Text);
                    cmdCheck.Parameters.Add(new SqlParameter("@satir_id", SqlDbType.Int)
                        { Value = satirId });

                    var dt = sMan.ExecuteQuery(cmdCheck);
                    if (dt.Rows.Count == 0)
                        return "Satır bulunamadı.";

                    var durum = dt.Rows[0]["durum"].ToString();
                    if (durum == "TAMAMLANDI" || durum == "IPTAL")
                        return string.Format("'{0}' durumundaki siparişten satır silinemez.", durum);

                    // Delete line
                    var cmd = sMan.CreateCommand("sp_siparis_satir_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@satir_id", SqlDbType.Int)
                        { Value = satirId });

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
                return "Sipariş satırı silinemedi: " + ex.Message;
            }
        }

        public string SatirGuncelle(SiparisSatirModel satir)
        {
            try
            {
                // Validation
                if (satir == null)
                    return "Sipariş satır modeli boş olamaz.";
                if (satir.SatirId <= 0)
                    return "Geçersiz satır ID.";
                if (satir.UrunId <= 0)
                    return "Geçersiz ürün ID.";
                if (satir.Miktar <= 0)
                    return "Miktar sıfırdan büyük olmalıdır.";
                if (satir.BirimFiyat < 0)
                    return "Birim fiyat negatif olamaz.";

                using (var sMan = new SqlManager())
                {
                    // Check if order allows modifications
                    var cmdCheck = sMan.CreateCommand(
                        @"SELECT s.durum
                          FROM siparis s
                          INNER JOIN siparis_satir ss ON s.siparis_id = ss.siparis_id
                          WHERE ss.satir_id = @satir_id",
                        CommandType.Text);
                    cmdCheck.Parameters.Add(new SqlParameter("@satir_id", SqlDbType.Int)
                        { Value = satir.SatirId });

                    var dt = sMan.ExecuteQuery(cmdCheck);
                    if (dt.Rows.Count == 0)
                        return "Satır bulunamadı.";

                    var durum = dt.Rows[0]["durum"].ToString();
                    if (durum == "TAMAMLANDI" || durum == "IPTAL")
                        return string.Format("'{0}' durumundaki siparişte satır güncellenemez.", durum);

                    // Update line
                    var cmd = sMan.CreateCommand("sp_siparis_satir_guncelle", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@satir_id", SqlDbType.Int)
                        { Value = satir.SatirId });
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                        { Value = satir.UrunId });
                    cmd.Parameters.Add(new SqlParameter("@miktar", SqlDbType.Decimal)
                        { Value = satir.Miktar, Precision = 18, Scale = 2 });
                    cmd.Parameters.Add(new SqlParameter("@birim_fiyat", SqlDbType.Decimal)
                        { Value = satir.BirimFiyat, Precision = 18, Scale = 2 });

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
                return "Sipariş satırı güncellenemedi: " + ex.Message;
            }
        }
    }
}

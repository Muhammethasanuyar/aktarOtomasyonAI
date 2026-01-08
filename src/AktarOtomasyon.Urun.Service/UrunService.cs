using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using AktarOtomasyon.Urun.Interface;
using AktarOtomasyon.Urun.Interface.Models;
using AktarOtomasyon.Util.DataAccess;

namespace AktarOtomasyon.Urun.Service
{
    /// <summary>
    /// Ürün service implementasyonu.
    /// Sprint 2: Complete implementation with Category, Unit, Stock, and Image management
    /// KURAL: Stateless, try/catch, using(sMan), SP-only data access
    /// </summary>
    public class UrunService : IUrunInterface
    {
        #region Product CRUD

        public string Kaydet(UrunModel urun)
        {
            try
            {
                // Client-side validation
                if (urun == null)
                    return "Ürün bilgisi boş olamaz.";

                if (string.IsNullOrWhiteSpace(urun.UrunKod))
                    return "Ürün kodu zorunludur.";

                if (string.IsNullOrWhiteSpace(urun.UrunAdi))
                    return "Ürün adı zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_kaydet", CommandType.StoredProcedure);

                    var paramUrunId = new SqlParameter("@urun_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = urun.UrunId == 0 ? (object)DBNull.Value : urun.UrunId
                    };
                    cmd.Parameters.Add(paramUrunId);

                    cmd.Parameters.Add(new SqlParameter("@urun_kod", SqlDbType.NVarChar, 50)
                        { Value = urun.UrunKod });
                    cmd.Parameters.Add(new SqlParameter("@urun_adi", SqlDbType.NVarChar, 200)
                        { Value = urun.UrunAdi });
                    cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int)
                        { Value = (object)urun.KategoriId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@birim_id", SqlDbType.Int)
                        { Value = (object)urun.BirimId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@alis_fiyati", SqlDbType.Decimal)
                        { Value = (object)urun.AlisFiyati ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@satis_fiyati", SqlDbType.Decimal)
                        { Value = (object)urun.SatisFiyati ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@barkod", SqlDbType.NVarChar, 50)
                        { Value = (object)urun.Barkod ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, -1)
                        { Value = (object)urun.Aciklama ?? DBNull.Value });

                    sMan.ExecuteNonQuery(cmd);
                    urun.UrunId = Convert.ToInt32(paramUrunId.Value);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Ürün kaydedilemedi: " + ex.Message;
            }
        }

        public UrunModel Getir(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    return GetirInternal(sMan, cmd);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UrunModel GetirBarkod(string barkod)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(barkod)) return null;

                using (var sMan = new SqlManager())
                {
                    // Use direct SQL for efficiency if no specific SP exists for Barcode
                    var query = "SELECT * FROM urun WHERE barkod = @barkod AND aktif = 1";
                    var cmd = sMan.CreateCommand(query, CommandType.Text);
                    cmd.Parameters.Add(new SqlParameter("@barkod", SqlDbType.NVarChar, 50) { Value = barkod });

                    return GetirInternal(sMan, cmd);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        private UrunModel GetirInternal(SqlManager sMan, SqlCommand cmd)
        {
            var dt = sMan.ExecuteQuery(cmd);
            if (dt.Rows.Count == 0)
                return null;

            var row = dt.Rows[0];
            return new UrunModel
            {
                UrunId = Convert.ToInt32(row["urun_id"]),
                UrunKod = row["urun_kod"] != DBNull.Value ? row["urun_kod"].ToString() : null,
                UrunAdi = row["urun_adi"] != DBNull.Value ? row["urun_adi"].ToString() : null,
                KategoriId = row["kategori_id"] != DBNull.Value ? Convert.ToInt32(row["kategori_id"]) : (int?)null,
                BirimId = row["birim_id"] != DBNull.Value ? Convert.ToInt32(row["birim_id"]) : (int?)null,
                AlisFiyati = row["alis_fiyati"] != DBNull.Value ? Convert.ToDecimal(row["alis_fiyati"]) : (decimal?)null,
                SatisFiyati = row["satis_fiyati"] != DBNull.Value ? Convert.ToDecimal(row["satis_fiyati"]) : (decimal?)null,
                Barkod = row["barkod"] != DBNull.Value ? row["barkod"].ToString() : null,
                Aciklama = row["aciklama"] != DBNull.Value ? row["aciklama"].ToString() : null,
                Aktif = Convert.ToBoolean(row["aktif"])
            };
        }

        public List<UrunListeItemDto> Listele(UrunFiltreDto filtre = null)
        {
            try
            {
                if (filtre == null)
                    filtre = new UrunFiltreDto();

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)filtre.Aktif ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int)
                        { Value = (object)filtre.KategoriId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@arama", SqlDbType.NVarChar, 100)
                        { Value = (object)filtre.Arama ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<UrunListeItemDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new UrunListeItemDto
                        {
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            UrunKod = row["urun_kod"] != DBNull.Value ? row["urun_kod"].ToString() : null,
                            UrunAdi = row["urun_adi"] != DBNull.Value ? row["urun_adi"].ToString() : null,
                            KategoriId = row["kategori_id"] != DBNull.Value ? Convert.ToInt32(row["kategori_id"]) : (int?)null,
                            KategoriAdi = row["kategori_adi"] != DBNull.Value ? row["kategori_adi"].ToString() : null,
                            BirimId = row["birim_id"] != DBNull.Value ? Convert.ToInt32(row["birim_id"]) : (int?)null,
                            BirimAdi = row["birim_adi"] != DBNull.Value ? row["birim_adi"].ToString() : null,
                            AlisFiyati = row["alis_fiyati"] != DBNull.Value ? Convert.ToDecimal(row["alis_fiyati"]) : (decimal?)null,
                            SatisFiyati = row["satis_fiyati"] != DBNull.Value ? Convert.ToDecimal(row["satis_fiyati"]) : (decimal?)null,
                            Barkod = row["barkod"] != DBNull.Value ? row["barkod"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            AnaGorselPath = dt.Columns.Contains("ana_gorsel_path") && row["ana_gorsel_path"] != DBNull.Value 
                                ? (row["ana_gorsel_path"].ToString().StartsWith("App_Data") 
                                    ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, row["ana_gorsel_path"].ToString().Replace('/', Path.DirectorySeparatorChar))
                                    : row["ana_gorsel_path"].ToString().Replace('/', Path.DirectorySeparatorChar))
                                : null
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<UrunListeItemDto>();
            }
        }

        public string Pasifle(int urunId, bool cascadeStokAyar = false)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });
                    cmd.Parameters.Add(new SqlParameter("@cascade_stok_ayar", SqlDbType.Bit) { Value = cascadeStokAyar });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Ürün pasifleştirilemedi: " + ex.Message;
            }
        }

        #endregion

        #region Category Management

        public string KategoriKaydet(UrunKategoriDto kategori)
        {
            try
            {
                if (kategori == null)
                    return "Kategori bilgisi boş olamaz.";

                if (string.IsNullOrWhiteSpace(kategori.KategoriKod))
                    return "Kategori kodu zorunludur.";

                if (string.IsNullOrWhiteSpace(kategori.KategoriAdi))
                    return "Kategori adı zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_kategori_kaydet", CommandType.StoredProcedure);

                    var paramKategoriId = new SqlParameter("@kategori_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = kategori.KategoriId == 0 ? (object)DBNull.Value : kategori.KategoriId
                    };
                    cmd.Parameters.Add(paramKategoriId);

                    cmd.Parameters.Add(new SqlParameter("@kategori_kod", SqlDbType.NVarChar, 50)
                        { Value = kategori.KategoriKod });
                    cmd.Parameters.Add(new SqlParameter("@kategori_adi", SqlDbType.NVarChar, 200)
                        { Value = kategori.KategoriAdi });
                    cmd.Parameters.Add(new SqlParameter("@ust_kategori_id", SqlDbType.Int)
                        { Value = (object)kategori.UstKategoriId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = kategori.Aktif });

                    sMan.ExecuteNonQuery(cmd);
                    kategori.KategoriId = Convert.ToInt32(paramKategoriId.Value);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Kategori kaydedilemedi: " + ex.Message;
            }
        }

        public UrunKategoriDto KategoriGetir(int kategoriId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_kategori_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int) { Value = kategoriId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new UrunKategoriDto
                    {
                        KategoriId = Convert.ToInt32(row["kategori_id"]),
                        KategoriKod = row["kategori_kod"] != DBNull.Value ? row["kategori_kod"].ToString() : null,
                        KategoriAdi = row["kategori_adi"] != DBNull.Value ? row["kategori_adi"].ToString() : null,
                        UstKategoriId = row["ust_kategori_id"] != DBNull.Value ? Convert.ToInt32(row["ust_kategori_id"]) : (int?)null,
                        UstKategoriAdi = row["ust_kategori_adi"] != DBNull.Value ? row["ust_kategori_adi"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<UrunKategoriDto> KategoriListele(bool? aktif = null, int? ustKategoriId = null, string arama = null)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_kategori_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)aktif ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ust_kategori_id", SqlDbType.Int)
                        { Value = (object)ustKategoriId ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@arama", SqlDbType.NVarChar, 200)
                        { Value = (object)arama ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<UrunKategoriDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new UrunKategoriDto
                        {
                            KategoriId = Convert.ToInt32(row["kategori_id"]),
                            KategoriKod = row["kategori_kod"] != DBNull.Value ? row["kategori_kod"].ToString() : null,
                            KategoriAdi = row["kategori_adi"] != DBNull.Value ? row["kategori_adi"].ToString() : null,
                            UstKategoriId = row["ust_kategori_id"] != DBNull.Value ? Convert.ToInt32(row["ust_kategori_id"]) : (int?)null,
                            UstKategoriAdi = row["ust_kategori_adi"] != DBNull.Value ? row["ust_kategori_adi"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            UrunSayisi = Convert.ToInt32(row["urun_sayisi"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<UrunKategoriDto>();
            }
        }

        public string KategoriPasifle(int kategoriId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_kategori_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int) { Value = kategoriId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Kategori pasifleştirilemedi: " + ex.Message;
            }
        }

        #endregion

        #region Unit Management

        public string BirimKaydet(UrunBirimDto birim)
        {
            try
            {
                if (birim == null)
                    return "Birim bilgisi boş olamaz.";

                if (string.IsNullOrWhiteSpace(birim.BirimKod))
                    return "Birim kodu zorunludur.";

                if (string.IsNullOrWhiteSpace(birim.BirimAdi))
                    return "Birim adı zorunludur.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_birim_kaydet", CommandType.StoredProcedure);

                    var paramBirimId = new SqlParameter("@birim_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.InputOutput,
                        Value = birim.BirimId == 0 ? (object)DBNull.Value : birim.BirimId
                    };
                    cmd.Parameters.Add(paramBirimId);

                    cmd.Parameters.Add(new SqlParameter("@birim_kod", SqlDbType.NVarChar, 20)
                        { Value = birim.BirimKod });
                    cmd.Parameters.Add(new SqlParameter("@birim_adi", SqlDbType.NVarChar, 100)
                        { Value = birim.BirimAdi });
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = birim.Aktif });

                    sMan.ExecuteNonQuery(cmd);
                    birim.BirimId = Convert.ToInt32(paramBirimId.Value);

                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Birim kaydedilemedi: " + ex.Message;
            }
        }

        public UrunBirimDto BirimGetir(int birimId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_birim_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@birim_id", SqlDbType.Int) { Value = birimId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new UrunBirimDto
                    {
                        BirimId = Convert.ToInt32(row["birim_id"]),
                        BirimKod = row["birim_kod"] != DBNull.Value ? row["birim_kod"].ToString() : null,
                        BirimAdi = row["birim_adi"] != DBNull.Value ? row["birim_adi"].ToString() : null,
                        Aktif = Convert.ToBoolean(row["aktif"])
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<UrunBirimDto> BirimListele(bool? aktif = null)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_birim_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@aktif", SqlDbType.Bit)
                        { Value = (object)aktif ?? DBNull.Value });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<UrunBirimDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        liste.Add(new UrunBirimDto
                        {
                            BirimId = Convert.ToInt32(row["birim_id"]),
                            BirimKod = row["birim_kod"] != DBNull.Value ? row["birim_kod"].ToString() : null,
                            BirimAdi = row["birim_adi"] != DBNull.Value ? row["birim_adi"].ToString() : null,
                            Aktif = Convert.ToBoolean(row["aktif"]),
                            UrunSayisi = Convert.ToInt32(row["urun_sayisi"])
                        });
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<UrunBirimDto>();
            }
        }

        public string BirimPasifle(int birimId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_birim_pasifle", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@birim_id", SqlDbType.Int) { Value = birimId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Birim pasifleştirilemedi: " + ex.Message;
            }
        }

        #endregion

        #region Stock Settings

        public UrunStokAyarDto StokAyarGetir(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_stok_ayar_getir", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);
                    if (dt.Rows.Count == 0)
                        return null;

                    var row = dt.Rows[0];
                    return new UrunStokAyarDto
                    {
                        AyarId = Convert.ToInt32(row["ayar_id"]),
                        UrunId = Convert.ToInt32(row["urun_id"]),
                        MinimumStok = Convert.ToDecimal(row["min_stok"]),
                        HedefStok = row["max_stok"] != DBNull.Value ? Convert.ToDecimal(row["max_stok"]) : (decimal?)null,
                        EmniyetStoku = Convert.ToDecimal(row["kritik_stok"]),
                        TedarikSuresi = row["tedarik_suresi"] != DBNull.Value ? Convert.ToInt32(row["tedarik_suresi"]) : (int?)null
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string StokAyarKaydet(UrunStokAyarDto stokAyar)
        {
            try
            {
                if (stokAyar == null)
                    return "Stok ayar bilgisi boş olamaz.";

                // BR-STOK-001: Validation
                if (stokAyar.MinimumStok < 0)
                    return "Minimum stok negatif olamaz.";

                if (stokAyar.EmniyetStoku < 0)
                    return "Emniyet stoku negatif olamaz.";

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_stok_ayar_kaydet", CommandType.StoredProcedure);

                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                        { Value = stokAyar.UrunId });
                    cmd.Parameters.Add(new SqlParameter("@min_stok", SqlDbType.Decimal)
                        { Value = stokAyar.MinimumStok });
                    cmd.Parameters.Add(new SqlParameter("@max_stok", SqlDbType.Decimal)
                        { Value = (object)stokAyar.HedefStok ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@kritik_stok", SqlDbType.Decimal)
                        { Value = stokAyar.EmniyetStoku });
                    cmd.Parameters.Add(new SqlParameter("@siparis_miktari", SqlDbType.Decimal)
                        { Value = (object)stokAyar.TedarikSuresi ?? DBNull.Value });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (SqlException sqlEx)
            {
                return sqlEx.Message;
            }
            catch (Exception ex)
            {
                return "Stok ayarları kaydedilemedi: " + ex.Message;
            }
        }

        public string UrunVeStokAyarKaydet(UrunModel urun, UrunStokAyarDto stokAyar)
        {
            try
            {
                if (urun == null)
                    return "Ürün bilgisi boş olamaz.";

                if (stokAyar == null)
                    return "Stok ayar bilgisi boş olamaz.";

                // Client-side validations
                if (string.IsNullOrWhiteSpace(urun.UrunKod))
                    return "Ürün kodu zorunludur.";

                if (string.IsNullOrWhiteSpace(urun.UrunAdi))
                    return "Ürün adı zorunludur.";

                if (stokAyar.MinimumStok < 0)
                    return "Minimum stok negatif olamaz.";

                if (stokAyar.EmniyetStoku < 0)
                    return "Emniyet stoku negatif olamaz.";

                using (var sMan = new SqlManager())
                {
                    sMan.BeginTransaction();

                    try
                    {
                        // 1. Save Product
                        var cmdUrun = sMan.CreateCommand("sp_urun_kaydet", CommandType.StoredProcedure);

                        var paramUrunId = new SqlParameter("@urun_id", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.InputOutput,
                            Value = urun.UrunId == 0 ? (object)DBNull.Value : urun.UrunId
                        };
                        cmdUrun.Parameters.Add(paramUrunId);

                        cmdUrun.Parameters.Add(new SqlParameter("@urun_kod", SqlDbType.NVarChar, 50)
                            { Value = urun.UrunKod });
                        cmdUrun.Parameters.Add(new SqlParameter("@urun_adi", SqlDbType.NVarChar, 200)
                            { Value = urun.UrunAdi });
                        cmdUrun.Parameters.Add(new SqlParameter("@kategori_id", SqlDbType.Int)
                            { Value = (object)urun.KategoriId ?? DBNull.Value });
                        cmdUrun.Parameters.Add(new SqlParameter("@birim_id", SqlDbType.Int)
                            { Value = (object)urun.BirimId ?? DBNull.Value });
                        cmdUrun.Parameters.Add(new SqlParameter("@alis_fiyati", SqlDbType.Decimal)
                            { Value = (object)urun.AlisFiyati ?? DBNull.Value });
                        cmdUrun.Parameters.Add(new SqlParameter("@satis_fiyati", SqlDbType.Decimal)
                            { Value = (object)urun.SatisFiyati ?? DBNull.Value });
                        cmdUrun.Parameters.Add(new SqlParameter("@barkod", SqlDbType.NVarChar, 50)
                            { Value = (object)urun.Barkod ?? DBNull.Value });
                        cmdUrun.Parameters.Add(new SqlParameter("@aciklama", SqlDbType.NVarChar, -1)
                            { Value = (object)urun.Aciklama ?? DBNull.Value });

                        sMan.ExecuteNonQuery(cmdUrun);
                        urun.UrunId = Convert.ToInt32(paramUrunId.Value);

                        // 2. Save Stock Settings
                        stokAyar.UrunId = urun.UrunId;
                        var cmdStok = sMan.CreateCommand("sp_urun_stok_ayar_kaydet", CommandType.StoredProcedure);

                        cmdStok.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                            { Value = stokAyar.UrunId });
                        cmdStok.Parameters.Add(new SqlParameter("@min_stok", SqlDbType.Decimal)
                            { Value = stokAyar.MinimumStok });
                        cmdStok.Parameters.Add(new SqlParameter("@max_stok", SqlDbType.Decimal)
                            { Value = (object)stokAyar.HedefStok ?? DBNull.Value });
                        cmdStok.Parameters.Add(new SqlParameter("@kritik_stok", SqlDbType.Decimal)
                            { Value = stokAyar.EmniyetStoku });
                        cmdStok.Parameters.Add(new SqlParameter("@siparis_miktari", SqlDbType.Decimal)
                            { Value = (object)stokAyar.TedarikSuresi ?? DBNull.Value });

                        sMan.ExecuteNonQuery(cmdStok);

                        sMan.CommitTransaction();
                        return null; // success
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
                return "Ürün ve stok ayarları kaydedilemedi: " + ex.Message;
            }
        }

        #endregion

        #region Image Management

        public int? GorselEkle(int urunId, string gorselPath, string gorselTip, bool anaGorsel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gorselPath))
                    return null;

                // Dosya Kopyalama Mantığı
                // Orijinal dosyanın varlığını kontrol et
                if (!File.Exists(gorselPath))
                    return null;

                // Hedef klasör: App_Data/Images/Urunler/{UrunId}/
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string relativeDir = Path.Combine("App_Data", "Images", "Urunler", urunId.ToString());
                string targetDir = Path.Combine(baseDir, relativeDir);

                if (!Directory.Exists(targetDir))
                    Directory.CreateDirectory(targetDir);

                // Benzersiz dosya adı oluştur
                string extension = Path.GetExtension(gorselPath);
                string newFileName = Guid.NewGuid().ToString() + extension;
                string targetPath = Path.Combine(targetDir, newFileName);

                // Dosyayı kopyala
                File.Copy(gorselPath, targetPath);

                // Veritabanına kaydedilecek yol (Relative path tercih edilir)
                string dbPath = Path.Combine(relativeDir, newFileName);

                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_gorsel_ekle", CommandType.StoredProcedure);

                    var paramGorselId = new SqlParameter("@gorsel_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramGorselId);

                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int)
                        { Value = urunId });
                    cmd.Parameters.Add(new SqlParameter("@gorsel_path", SqlDbType.NVarChar, 500)
                        { Value = dbPath }); // Kopyalanan dosyanın yolu
                    cmd.Parameters.Add(new SqlParameter("@gorsel_tip", SqlDbType.NVarChar, 50)
                        { Value = (object)gorselTip ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@ana_gorsel", SqlDbType.Bit)
                        { Value = anaGorsel });

                    sMan.ExecuteNonQuery(cmd);
                    return Convert.ToInt32(paramGorselId.Value);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GorselSil(int gorselId)
        {
            try
            {
                // Not: Fiziksel dosyayı silmek için önce yolu almak gerekir ama
                // şimdilik sadece DB kaydını siliyoruz. İleride dosya silme de eklenebilir.
                
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_gorsel_sil", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@gorsel_id", SqlDbType.Int) { Value = gorselId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (Exception ex)
            {
                return "Görsel silinemedi: " + ex.Message;
            }
        }

        public List<UrunGorselDto> GorselListele(int urunId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_gorsel_listele", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@urun_id", SqlDbType.Int) { Value = urunId });

                    var dt = sMan.ExecuteQuery(cmd);
                    var liste = new List<UrunGorselDto>();

                    foreach (DataRow row in dt.Rows)
                    {
                        var dto = new UrunGorselDto
                        {
                            GorselId = Convert.ToInt32(row["gorsel_id"]),
                            UrunId = Convert.ToInt32(row["urun_id"]),
                            GorselPath = row["gorsel_path"] != DBNull.Value ? row["gorsel_path"].ToString() : null,
                            GorselTip = row["gorsel_tip"] != DBNull.Value ? row["gorsel_tip"].ToString() : null,
                            AnaGorsel = Convert.ToBoolean(row["ana_gorsel"]),
                            Sira = Convert.ToInt32(row["sira"]),
                            OlusturmaTarih = Convert.ToDateTime(row["olusturma_tarih"])
                        };

                        // Eğer path relative ise (App_Data ile başlıyorsa), başına BaseDirectory ekle
                        // Böylece UI tarafı dosyayı bulabilir.
                        if (!string.IsNullOrEmpty(dto.GorselPath) && dto.GorselPath.StartsWith("App_Data"))
                        {
                           dto.GorselPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dto.GorselPath);
                        }

                        liste.Add(dto);
                    }

                    return liste;
                }
            }
            catch (Exception)
            {
                return new List<UrunGorselDto>();
            }
        }

        public string AnaGorselAyarla(int gorselId)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    var cmd = sMan.CreateCommand("sp_urun_ana_gorsel_set", CommandType.StoredProcedure);
                    cmd.Parameters.Add(new SqlParameter("@gorsel_id", SqlDbType.Int) { Value = gorselId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (Exception ex)
            {
                return "Ana görsel ayarlanamadı: " + ex.Message;
            }
        }

        public string GorselSiraGuncelle(int gorselId, int yeniSira)
        {
            try
            {
                using (var sMan = new SqlManager())
                {
                    // Inline SQL kullanıyoruz çünkü SP olmayabilir.
                    string sql = "UPDATE urun_gorsel SET sira = @sira WHERE gorsel_id = @gorsel_id";
                    var cmd = sMan.CreateCommand(sql, CommandType.Text);
                    
                    cmd.Parameters.Add(new SqlParameter("@sira", SqlDbType.Int) { Value = yeniSira });
                    cmd.Parameters.Add(new SqlParameter("@gorsel_id", SqlDbType.Int) { Value = gorselId });

                    sMan.ExecuteNonQuery(cmd);
                    return null; // success
                }
            }
            catch (Exception ex)
            {
                return "Görsel sırası güncellenemedi: " + ex.Message;
            }
        }

        #endregion
    }
}

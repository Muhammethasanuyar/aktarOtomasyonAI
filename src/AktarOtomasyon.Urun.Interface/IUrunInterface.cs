using System.Collections.Generic;
using AktarOtomasyon.Urun.Interface.Models;

namespace AktarOtomasyon.Urun.Interface
{
    /// <summary>
    /// Ürün işlemleri interface.
    /// Sprint 2: Extended with Category, Unit, Stock Settings, and Image management
    /// </summary>
    public interface IUrunInterface
    {
        #region Product CRUD

        /// <summary>
        /// Ürün kaydeder (yeni veya güncelleme).
        /// </summary>
        string Kaydet(UrunModel urun);

        /// <summary>
        /// Ürünu ID ile getirir.
        /// </summary>
        UrunModel Getir(int urunId);

        /// <summary>
        /// Ürünü Barkod ile getirir.
        /// </summary>
        UrunModel GetirBarkod(string barkod);

        /// <summary>
        /// Ürünleri listeler/arar.
        /// </summary>
        List<UrunListeItemDto> Listele(UrunFiltreDto filtre = null);

        /// <summary>
        /// Ürünü pasifler.
        /// </summary>
        string Pasifle(int urunId, bool cascadeStokAyar = false);

        #endregion

        #region Category Management

        /// <summary>
        /// Kategori kaydeder (yeni veya güncelleme).
        /// </summary>
        string KategoriKaydet(UrunKategoriDto kategori);

        /// <summary>
        /// Kategori getirir.
        /// </summary>
        UrunKategoriDto KategoriGetir(int kategoriId);

        /// <summary>
        /// Kategori listeler.
        /// </summary>
        List<UrunKategoriDto> KategoriListele(bool? aktif = null, int? ustKategoriId = null, string arama = null);

        /// <summary>
        /// Kategoriyi pasifler.
        /// </summary>
        string KategoriPasifle(int kategoriId);

        #endregion

        #region Unit Management

        /// <summary>
        /// Birim kaydeder (yeni veya güncelleme).
        /// </summary>
        string BirimKaydet(UrunBirimDto birim);

        /// <summary>
        /// Birim getirir.
        /// </summary>
        UrunBirimDto BirimGetir(int birimId);

        /// <summary>
        /// Birim listeler.
        /// </summary>
        List<UrunBirimDto> BirimListele(bool? aktif = null);

        /// <summary>
        /// Birimi pasifler.
        /// </summary>
        string BirimPasifle(int birimId);

        #endregion

        #region Stock Settings

        /// <summary>
        /// Stok ayarlarını getirir.
        /// </summary>
        UrunStokAyarDto StokAyarGetir(int urunId);

        /// <summary>
        /// Stok ayarlarını kaydeder.
        /// </summary>
        string StokAyarKaydet(UrunStokAyarDto stokAyar);

        /// <summary>
        /// Ürün ve stok ayarlarını birlikte kaydeder (TRANSACTION).
        /// </summary>
        string UrunVeStokAyarKaydet(UrunModel urun, UrunStokAyarDto stokAyar);

        #endregion

        #region Image Management

        /// <summary>
        /// Ürüne görsel ekler.
        /// </summary>
        int? GorselEkle(int urunId, string gorselPath, string gorselTip, bool anaGorsel);

        /// <summary>
        /// Görseli siler.
        /// </summary>
        string GorselSil(int gorselId);

        /// <summary>
        /// Ürün görsellerini listeler.
        /// </summary>
        List<UrunGorselDto> GorselListele(int urunId);

        /// <summary>
        /// Ana görseli ayarlar.
        /// </summary>
        string AnaGorselAyarla(int gorselId);

        /// <summary>
        /// Görsel sırasını günceller (Sürükle-Bırak için).
        /// </summary>
        string GorselSiraGuncelle(int gorselId, int yeniSira);

        #endregion
    }
}

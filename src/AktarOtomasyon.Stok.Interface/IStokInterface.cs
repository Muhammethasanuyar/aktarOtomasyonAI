using System.Collections.Generic;

namespace AktarOtomasyon.Stok.Interface
{
    /// <summary>
    /// Stok işlemleri interface.
    /// </summary>
    public interface IStokInterface
    {
        /// <summary>
        /// Stok hareketi ekler.
        /// </summary>
        string HareketEkle(StokHareketModel hareket);

        /// <summary>
        /// Ürünün stok durumunu getirir.
        /// </summary>
        decimal DurumGetir(int urunId);

        /// <summary>
        /// Kritik stok seviyesindeki ürünleri listeler.
        /// </summary>
        List<StokKritikModel> KritikListele();

        /// <summary>
        /// Tüm stok hareketlerini listeler.
        /// </summary>
        List<StokHareketListeDto> HareketListele();

        /// <summary>
        /// Belirli tarih aralığındaki satış (CIKIS) raporunu getirir.
        /// </summary>
        List<SatisRaporuDto> SatisRaporuGetir(System.DateTime baslangic, System.DateTime bitis);
        
        /// <summary>
        /// Ürünün stok durumu detaylarını getirir.
        /// </summary>
        StokDurumDto StokDurumGetir(int urunId);
    }

    public class StokDurumDto
    {
        public int UrunId { get; set; }
        public decimal MevcutStok { get; set; }
        public decimal KritikStok { get; set; }
    }

    public class StokKritikModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal MevcutStok { get; set; }
        public decimal MinStok { get; set; }
    }

    public class StokHareketListeDto
    {
        public int HareketId { get; set; }
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string HareketTip { get; set; }
        public decimal Miktar { get; set; }
        public System.DateTime HareketTarih { get; set; }
        public string Aciklama { get; set; }
        public decimal OncekiBakiye { get; set; }
        public decimal SonrakiBakiye { get; set; }
    }

    /// <summary>
    /// Satış (CIKIS) raporu modeli.
    /// </summary>
    public class SatisRaporuDto
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal ToplamMiktar { get; set; }
        public int IslemSayisi { get; set; }
    }

    public class SatisRaporuRequest
    {
        public System.DateTime BaslangicTarih { get; set; }
        public System.DateTime BitisTarih { get; set; }
    }
}

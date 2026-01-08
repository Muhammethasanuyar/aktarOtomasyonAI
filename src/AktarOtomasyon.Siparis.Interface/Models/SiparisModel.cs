using System;

namespace AktarOtomasyon.Siparis.Interface
{
    public class SiparisModel
    {
        public int SiparisId { get; set; }
        public string SiparisNo { get; set; }
        public int TedarikciId { get; set; }
        public string TedarikciAdi { get; set; }
        public DateTime SiparisTarih { get; set; }
        public DateTime? BeklenenTeslimTarih { get; set; }
        public DateTime? TeslimTarih { get; set; }
        public string Durum { get; set; }
        public decimal ToplamTutar { get; set; }
        public string Aciklama { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime OlusturmaTarih { get; set; }
    }

    public class SiparisSatirModel
    {
        public int SatirId { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public decimal TeslimMiktar { get; set; }
    }
}

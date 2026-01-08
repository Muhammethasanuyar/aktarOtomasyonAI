using System;

namespace AktarOtomasyon.Ai.Interface
{
    public class AiIcerikModel
    {
        public int IcerikId { get; set; }
        public int UrunId { get; set; }
        public string Icerik { get; set; }
        public string Durum { get; set; } // Taslak, OnayBekliyor, Aktif
        public string Kaynaklar { get; set; }
        public string SablonKod { get; set; }
        public string Provider { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime OlusturmaTarih { get; set; }
        public int? OnaylayanKullaniciId { get; set; }
        public DateTime? OnayTarih { get; set; }
    }

    public class AiIcerikVersiyonModel
    {
        public int VersiyonId { get; set; }
        public int IcerikId { get; set; }
        public int VersiyonNo { get; set; }
        public string Icerik { get; set; }
        public DateTime OlusturmaTarih { get; set; }
    }

    public class AiSablonModel
    {
        public int SablonId { get; set; }
        public string SablonKod { get; set; }
        public string SablonAdi { get; set; }
        public string PromptSablonu { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime OlusturmaTarih { get; set; }
        public DateTime? GuncellemeTarih { get; set; }
    }

    public class AiSablonListModel
    {
        public int SablonId { get; set; }
        public string SablonKod { get; set; }
        public string SablonAdi { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
    }

    public class AiUrunBilgiModel
    {
        public int UrunId { get; set; }
        public string UrunKod { get; set; }
        public string UrunAdi { get; set; }
        public string KategoriAdi { get; set; }
        public string BirimAdi { get; set; }
        public decimal? SatisFiyati { get; set; }
        public string Aciklama { get; set; }
    }
}

using System;

namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// Bildirim modeli.
    /// </summary>
    public class BildirimModel
    {
        public int BildirimId { get; set; }
        public string BildirimTip { get; set; } // STOK_KRITIK, SIPARIS_GELEN, AI_ONAY_BEKLIYOR
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string ReferansTip { get; set; }
        public int? ReferansId { get; set; }
        public bool Okundu { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime OlusturmaTarih { get; set; }
        public DateTime? OkunmaTarih { get; set; }
    }
}

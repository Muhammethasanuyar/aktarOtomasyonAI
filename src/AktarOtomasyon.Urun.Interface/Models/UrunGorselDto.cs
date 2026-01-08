using System;

namespace AktarOtomasyon.Urun.Interface.Models
{
    /// <summary>
    /// Ürün görsel DTO - Image management için
    /// </summary>
    public class UrunGorselDto
    {
        public int GorselId { get; set; }
        public int UrunId { get; set; }
        public string GorselPath { get; set; }
        public string GorselTip { get; set; }
        public bool AnaGorsel { get; set; }
        public int Sira { get; set; }
        public DateTime OlusturmaTarih { get; set; }
    }
}

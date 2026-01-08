using System;

namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// Tedarikçi bilgilerini içeren model sınıfı.
    /// </summary>
    public class TedarikciModel
    {
        public int TedarikciId { get; set; }
        public string TedarikciKod { get; set; }
        public string TedarikciAdi { get; set; }
        public string Yetkili { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public bool Aktif { get; set; }
    }
}

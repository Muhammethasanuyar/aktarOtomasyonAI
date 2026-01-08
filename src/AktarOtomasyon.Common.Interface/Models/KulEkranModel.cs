namespace AktarOtomasyon.Common.Interface
{
    /// <summary>
    /// kul_ekran tablosu modeli.
    /// SP: sp_kul_ekran_getir
    /// </summary>
    public class KulEkranModel
    {
        public int EkranId { get; set; }
        public string EkranKod { get; set; }
        public string MenudekiAdi { get; set; }
        public string FormAdi { get; set; }
        public string Modul { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
    }
}

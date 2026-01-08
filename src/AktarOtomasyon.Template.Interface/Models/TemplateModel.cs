namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template entity model
    /// </summary>
    public class TemplateModel
    {
        public int TemplateId { get; set; }
        public string TemplateKod { get; set; }
        public string TemplateAdi { get; set; }
        public string Modul { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public int? AktifVersionId { get; set; }
        public int? AktifVersionNo { get; set; }
    }
}

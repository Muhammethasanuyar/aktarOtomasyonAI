using System;

namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template list item DTO for grid/list views
    /// </summary>
    public class TemplateListeItemDto
    {
        public int TemplateId { get; set; }
        public string TemplateKod { get; set; }
        public string TemplateAdi { get; set; }
        public string Modul { get; set; }
        public string Aciklama { get; set; }
        public bool Aktif { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? AktifVersionId { get; set; }
        public int? AktifVersionNo { get; set; }
    }
}

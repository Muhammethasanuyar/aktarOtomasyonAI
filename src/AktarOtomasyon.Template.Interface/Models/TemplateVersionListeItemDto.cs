using System;

namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template version list item DTO
    /// </summary>
    public class TemplateVersionListeItemDto
    {
        public int TemplateVersionId { get; set; }
        public int TemplateId { get; set; }
        public int VersionNo { get; set; }
        public string Durum { get; set; }
        public string DosyaAdi { get; set; }
        public long? DosyaBoyut { get; set; }
        public string Sha256 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool IsActive { get; set; }
    }
}

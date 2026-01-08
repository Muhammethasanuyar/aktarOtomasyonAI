using System;

namespace AktarOtomasyon.Template.Interface.Models
{
    /// <summary>
    /// Template version entity model
    /// </summary>
    public class TemplateVersionModel
    {
        public int TemplateVersionId { get; set; }
        public int TemplateId { get; set; }
        public int VersionNo { get; set; }
        public string Durum { get; set; } // DRAFT, APPROVED, ARCHIVED
        public string DosyaAdi { get; set; }
        public string DosyaYolu { get; set; }
        public string MimeType { get; set; }
        public long? DosyaBoyut { get; set; }
        public string Sha256 { get; set; }
        public string Notlar { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public int? ApprovedBy { get; set; }
        public bool IsActive { get; set; }
    }
}

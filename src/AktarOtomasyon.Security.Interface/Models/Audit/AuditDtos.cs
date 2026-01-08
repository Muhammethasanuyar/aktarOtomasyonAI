using System;

namespace AktarOtomasyon.Security.Interface.Models.Audit
{
    /// <summary>
    /// Filter criteria for audit log listing
    /// </summary>
    public class AuditListeFilterDto
    {
        public string Entity { get; set; }
        public string Action { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public int Top { get; set; }

        public AuditListeFilterDto()
        {
            Top = 1000; // Default: last 1000 records
        }
    }

    /// <summary>
    /// Audit log list item for grid display
    /// </summary>
    public class AuditListeItemDto
    {
        public int AuditId { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
    }

    /// <summary>
    /// Audit log detail with JSON payload
    /// </summary>
    public class AuditDetayDto
    {
        public int AuditId { get; set; }
        public string Entity { get; set; }
        public int EntityId { get; set; }
        public string Action { get; set; }
        public string DetailJson { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
    }
}

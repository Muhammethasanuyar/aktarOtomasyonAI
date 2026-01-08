using System;

namespace AktarOtomasyon.Audit.Interface.Models
{
    public class AuditListeItemDto
    {
        public int AuditId { get; set; }
        public string Entity { get; set; }
        public int? EntityId { get; set; }
        public string Action { get; set; }
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string AdSoyad { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

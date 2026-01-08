using System;

namespace AktarOtomasyon.Audit.Interface.Models
{
    public class AuditFiltre
    {
        public string Entity { get; set; }
        public string Action { get; set; }
        public int? KullaniciId { get; set; }
        public DateTime? BaslangicTarih { get; set; }
        public DateTime? BitisTarih { get; set; }
        public int Top { get; set; }

        public AuditFiltre()
        {
            Top = 1000;
        }
    }
}

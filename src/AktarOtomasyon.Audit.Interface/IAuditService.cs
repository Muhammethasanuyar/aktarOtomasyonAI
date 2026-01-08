using System;
using System.Collections.Generic;
using AktarOtomasyon.Audit.Interface.Models;

namespace AktarOtomasyon.Audit.Interface
{
    public interface IAuditService
    {
        List<AuditListeItemDto> AuditListele(AuditFiltre filtre);
        AuditDetayDto AuditGetir(int auditId);
    }
}

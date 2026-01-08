using System.Collections.Generic;
using AktarOtomasyon.Security.Interface.Models.Audit;

namespace AktarOtomasyon.Security.Interface
{
    /// <summary>
    /// Audit service interface for viewing audit logs
    /// Read-only operations (audit log writes handled by SPs)
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Lists audit log entries with filters
        /// </summary>
        /// <param name="filtre">Filter criteria (entity, action, user, date range, top N)</param>
        /// <returns>List of audit log entries with user info</returns>
        List<AuditListeItemDto> AuditListele(AuditListeFilterDto filtre);

        /// <summary>
        /// Gets audit log detail with JSON payload
        /// </summary>
        /// <param name="auditId">Audit log ID</param>
        /// <returns>Full audit log record with JSON detail</returns>
        AuditDetayDto AuditGetir(int auditId);
    }
}

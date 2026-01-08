/*
    sp_audit_listele

    Purpose: List audit log entries with filters
    Read-only: For viewing audit trail

    Parameters:
        @entity            - Filter by entity (NULL for all)
        @action            - Filter by action (NULL for all)
        @kullanici_id      - Filter by user who performed action (NULL for all)
        @baslangic_tarih   - Start date filter (NULL for all)
        @bitis_tarih       - End date filter (NULL for all)
        @top               - Limit number of records (default 1000)

    Returns:
        List of audit log entries with user info (most recent first)

    Performance:
        - Uses indexes: IX_audit_log_entity_action, IX_audit_log_created_at, IX_audit_log_created_by
        - TOP parameter prevents excessive results
        - ORDER BY created_at DESC (most recent first)
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_audit_listele', 'P') IS NOT NULL
    DROP PROCEDURE sp_audit_listele
GO

CREATE PROCEDURE [dbo].[sp_audit_listele]
    @entity NVARCHAR(100) = NULL,
    @action NVARCHAR(50) = NULL,
    @kullanici_id INT = NULL,
    @baslangic_tarih DATETIME = NULL,
    @bitis_tarih DATETIME = NULL,
    @top INT = 1000
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@top)
        a.audit_id,
        a.entity,
        a.entity_id,
        a.action,
        a.detail_json,
        a.created_at,
        a.created_by,
        -- User info (LEFT JOIN in case user deleted)
        k.kullanici_adi,
        k.ad_soyad
    FROM audit_log a
    LEFT JOIN kullanici k ON a.created_by = k.kullanici_id
    WHERE (@entity IS NULL OR a.entity = @entity)
        AND (@action IS NULL OR a.action = @action)
        AND (@kullanici_id IS NULL OR a.created_by = @kullanici_id)
        AND (@baslangic_tarih IS NULL OR a.created_at >= @baslangic_tarih)
        AND (@bitis_tarih IS NULL OR a.created_at <= @bitis_tarih)
    ORDER BY a.created_at DESC
END
GO

PRINT 'Created sp_audit_listele'
GO

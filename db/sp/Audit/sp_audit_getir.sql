/*
    sp_audit_getir

    Purpose: Get audit log detail by ID
    Returns full record with JSON detail

    Parameters:
        @audit_id - Audit log ID to retrieve

    Returns:
        Single audit log record with user info and JSON detail
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_audit_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_audit_getir
GO

CREATE PROCEDURE [dbo].[sp_audit_getir]
    @audit_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
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
    WHERE a.audit_id = @audit_id
END
GO

PRINT 'Created sp_audit_getir'
GO

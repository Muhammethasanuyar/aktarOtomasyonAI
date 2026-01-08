/*
    sp_rol_getir

    Purpose: Get role by ID for editing

    Parameters:
        @rol_id - Role ID to retrieve

    Returns:
        Single role record
*/

USE [AktarOtomasyon]
GO

IF OBJECT_ID('sp_rol_getir', 'P') IS NOT NULL
    DROP PROCEDURE sp_rol_getir
GO

CREATE PROCEDURE [dbo].[sp_rol_getir]
    @rol_id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        rol_id,
        rol_adi,
        aciklama,
        aktif,
        created_at,
        created_by,
        updated_at,
        updated_by
    FROM rol
    WHERE rol_id = @rol_id
END
GO

PRINT 'Created sp_rol_getir'
GO

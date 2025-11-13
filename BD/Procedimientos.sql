USE PiscinaDB;
GO

IF OBJECT_ID('SP_LOGIN', 'P') IS NOT NULL
    DROP PROCEDURE SP_LOGIN;
GO

ALTER PROCEDURE SP_LOGIN
(
    @Documento VARCHAR(50),
    @Clave VARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.IdUsuario,
        u.Documento,
        u.Clave,
        u.Estado,
        u.IdRol,
        u.NombreCompleto,
        r.Descripcion AS DescripcionRol
    FROM Usuario u
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    WHERE u.Documento = @Documento
      AND u.Clave = @Clave
      AND u.Estado = 1;
END
GO

CREATE PROCEDURE [dbo].DetalleUsuario
    @idUsuario  UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        NombreUsuario,
        CorreoElectronico,
        FechaCreacion,
        FechaModificacion,
        UsuarioCrea,
        UsuarioModifica,
        TELEFONO,
        DIRECCION
    FROM dbo.Usuarios
    WHERE Id = @idUsuario;
END
GO
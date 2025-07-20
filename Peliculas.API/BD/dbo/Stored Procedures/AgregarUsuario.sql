CREATE PROCEDURE [dbo].[AgregarUsuario]
    @NombreUsuario      VARCHAR(MAX),
    @PasswordHash       VARCHAR(MAX),
    @CorreoElectronico  VARCHAR(MAX),
    @IdEstado int 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Id AS UNIQUEIDENTIFIER = NEWID();

    BEGIN TRAN

    -- Insertar nuevo usuario
    INSERT INTO [dbo].[Usuarios] (
        [Id],
        [NombreUsuario],
        [PasswordHash],
        [CorreoElectronico],
        estado_id
    )
    VALUES (
        @Id,
        @NombreUsuario,
        @PasswordHash,
        @CorreoElectronico,
        @IdEstado
    );

    -- Insertar relación con perfil 2
    INSERT INTO [dbo].[PerfilesxUsuario] (
        [IdUsuario],
        [IdPerfil]
    )
    VALUES (
        @Id,
        2
    );

    COMMIT TRAN
END
GO
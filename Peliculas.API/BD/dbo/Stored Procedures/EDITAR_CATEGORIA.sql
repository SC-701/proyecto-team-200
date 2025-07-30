CREATE PROCEDURE EDITAR_CATEGORIA
  @IdCategoria UNIQUEIDENTIFIER,
  @Nombre NVARCHAR(255),
  @Descripcion NVARCHAR(255),
  @FechaCreacion DATE,
  @EstadoId INT
AS
BEGIN
  SET NOCOUNT ON;

  BEGIN TRANSACTION;

  UPDATE [dbo].[CATEGORIAS]
  SET
    NOMBRE = @Nombre,
    DESCRIPCION = @Descripcion,
    FECHA_CREACION = @FechaCreacion,
    ESTADO_ID = @EstadoId
  WHERE CATEGORIAS_ID = @IdCategoria;

  COMMIT TRANSACTION;

  SELECT @IdCategoria AS Resultado;
END;

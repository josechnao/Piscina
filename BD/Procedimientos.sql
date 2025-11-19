USE PiscinaDB;
GO

-------------------------
----PROCEDIMIENTO PARA LOGUEO
-------------------------
IF OBJECT_ID('SP_LOGIN', 'P') IS NOT NULL
    DROP PROCEDURE SP_LOGIN;
GO

CREATE PROCEDURE SP_LOGIN
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
-------------------------
----PROCEDIMIENTO PARA FORMULARIO DE USUARIO
-------------------------

CREATE PROCEDURE SP_LISTAR_USUARIOS
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.IdUsuario,
        u.Documento,
        u.NombreCompleto,
        u.Clave,
        u.Estado,
        r.IdRol,
        r.Descripcion AS Rol
    FROM Usuario u
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    ORDER BY u.IdUsuario ASC;
END
GO

CREATE PROCEDURE SP_GUARDAR_USUARIO
(
    @IdUsuario INT,
    @Documento VARCHAR(50),
    @NombreCompleto VARCHAR(100),
    @Clave VARCHAR(100),
    @IdRol INT,
    @Estado BIT,
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF (@IdUsuario = 0)
    BEGIN
        -- INSERTAR
        INSERT INTO Usuario (Documento, NombreCompleto, Clave, IdRol, Estado)
        VALUES (@Documento, @NombreCompleto, @Clave, @IdRol, @Estado);

        SET @Resultado = SCOPE_IDENTITY(); -- devolvemos el ID generado
    END
    ELSE
    BEGIN
        -- EDITAR
        UPDATE Usuario
        SET Documento = @Documento,
            NombreCompleto = @NombreCompleto,
            Clave = @Clave,
            IdRol = @IdRol,
            Estado = @Estado
        WHERE IdUsuario = @IdUsuario;

        SET @Resultado = @IdUsuario; -- devolvemos el mismo ID
    END
END;
GO
CREATE PROCEDURE SP_ELIMINAR_USUARIO
(
    @IdUsuario INT,
    @Resultado BIT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Usuario WHERE IdUsuario = @IdUsuario;

    SET @Resultado = 1;
END;
GO
-------------------------
----PROCEDIMIENTO PARA FORMULARIO DE PROVEEDORES
-------------------------
CREATE PROCEDURE SP_LISTARPROVEEDORES
AS
BEGIN
    SELECT 
        P.IdProveedor,
        P.Nombre,
        P.Documento,
        P.Telefono,
        P.Correo,
        P.Estado,
        CASE WHEN P.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS EstadoTexto
    FROM Proveedor P
END
GO

CREATE PROCEDURE SP_GUARDARPROVEEDOR
(
    @IdProveedor INT,
    @Nombre VARCHAR(150),
    @Documento VARCHAR(20),
    @Telefono VARCHAR(20),
    @Correo VARCHAR(100),
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY

        IF (@IdProveedor = 0)
        BEGIN
            INSERT INTO Proveedor(Nombre, Documento, Telefono, Correo, Estado)
            VALUES (@Nombre, @Documento, @Telefono, @Correo, @Estado);

            SET @Resultado = SCOPE_IDENTITY();
            SET @Mensaje = 'Proveedor registrado correctamente.';
        END
        ELSE
        BEGIN
            UPDATE Proveedor
            SET 
                Nombre = @Nombre,
                Documento = @Documento,
                Telefono = @Telefono,
                Correo = @Correo,
                Estado = @Estado
            WHERE IdProveedor = @IdProveedor;

            SET @Resultado = @IdProveedor;
            SET @Mensaje = 'Proveedor actualizado correctamente.';
        END

    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH;
END
GO



CREATE PROCEDURE SP_ELIMINARPROVEEDOR
(
    @IdProveedor INT,
    @Resultado   INT OUTPUT,
    @Mensaje     VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY

        UPDATE Proveedor
        SET Estado = 0
        WHERE IdProveedor = @IdProveedor;

        SET @Resultado = 1;
        SET @Mensaje = 'Proveedor eliminado correctamente.';

    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH;

END
GO

-------------------------
----PROCEDIMIENTO PARA FORMULARIO DE CATEGORIA
-------------------------

CREATE PROC SP_LISTARCATEGORIA
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdCategoria,
        Descripcion,
        Estado,
        IIF(Estado = 1, 'Activo', 'Inactivo') AS EstadoValor
    FROM Categoria;
END;
GO


CREATE PROC SP_REGISTRARCATEGORIA
(
    @Descripcion VARCHAR(100),
    @Estado BIT,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Resultado = 0;
    SET @Mensaje = '';

    -- Validación de duplicados
    IF EXISTS (SELECT 1 FROM Categoria WHERE Descripcion = @Descripcion)
    BEGIN
        SET @Mensaje = 'La categoría ya existe.';
        RETURN;
    END

    INSERT INTO Categoria (Descripcion, Estado)
    VALUES (@Descripcion, @Estado);

    SET @Resultado = SCOPE_IDENTITY();
END;
GO


CREATE PROC SP_EDITARCATEGORIA
(
    @IdCategoria INT,
    @Descripcion VARCHAR(100),
    @Estado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Resultado = 0;
    SET @Mensaje = '';

    -- Validación de duplicados
    IF EXISTS (
        SELECT 1 
        FROM Categoria 
        WHERE Descripcion = @Descripcion
        AND IdCategoria <> @IdCategoria
    )
    BEGIN
        SET @Mensaje = 'Ya existe otra categoría con esa descripción.';
        RETURN;
    END

    UPDATE Categoria
    SET Descripcion = @Descripcion,
        Estado = @Estado
    WHERE IdCategoria = @IdCategoria;

    IF @@ROWCOUNT > 0
        SET @Resultado = 1;
    ELSE
        SET @Mensaje = 'No se encontró la categoría.';
END;
GO


CREATE PROC SP_CAMBIARESTADOCATEGORIA
(
    @IdCategoria INT,
    @NuevoEstado BIT,
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Resultado = 0;
    SET @Mensaje = '';

    UPDATE Categoria
    SET Estado = @NuevoEstado
    WHERE IdCategoria = @IdCategoria;

    IF @@ROWCOUNT > 0
        SET @Resultado = 1;
    ELSE
        SET @Mensaje = 'No se encontró la categoría.';
END;
GO

-------------------------
----PROCEDIMIENTO PARA PRODUCTOS
-------------------------


IF EXISTS(SELECT * FROM sys.objects WHERE name = 'SP_LISTARPRODUCTOS')
    DROP PROCEDURE SP_LISTARPRODUCTOS;
GO

CREATE PROCEDURE SP_LISTARPRODUCTOS
AS
BEGIN
    SELECT 
        p.IdProducto,
        p.Codigo,
        p.Nombre,
        p.Descripcion,
        p.IdCategoria,
        c.Descripcion AS Categoria,
        p.Stock,
        p.PrecioCompra,
        p.PrecioVenta,
        p.Estado AS EstadoValor,
        CASE WHEN p.Estado = 1 THEN 'Activo' ELSE 'Inactivo' END AS Estado
    FROM Producto p
    INNER JOIN Categoria c ON c.IdCategoria = p.IdCategoria;
END
GO


IF EXISTS(SELECT * FROM sys.objects WHERE name = 'SP_REGISTRARPRODUCTO')
    DROP PROCEDURE SP_REGISTRARPRODUCTO;
GO

CREATE PROCEDURE SP_REGISTRARPRODUCTO
(
    @Codigo      VARCHAR(50),
    @Nombre      VARCHAR(150),
    @Descripcion VARCHAR(150),
    @IdCategoria INT,
    @Estado      BIT
)
AS
BEGIN
    INSERT INTO Producto
        (Codigo, Nombre, Descripcion, IdCategoria, Estado)
    VALUES
        (@Codigo, @Nombre, @Descripcion, @IdCategoria, @Estado);
END
GO


IF EXISTS(SELECT * FROM sys.objects WHERE name = 'SP_EDITARPRODUCTO')
    DROP PROCEDURE SP_EDITARPRODUCTO;
GO

CREATE PROCEDURE SP_EDITARPRODUCTO
(
    @IdProducto  INT,
    @Codigo      VARCHAR(50),
    @Nombre      VARCHAR(150),
    @Descripcion VARCHAR(150),
    @IdCategoria INT,
    @Estado      BIT
)
AS
BEGIN
    UPDATE Producto
    SET
        Codigo      = @Codigo,
        Nombre      = @Nombre,
        Descripcion = @Descripcion,
        IdCategoria = @IdCategoria,
        Estado      = @Estado
    WHERE IdProducto = @IdProducto;
END
GO

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'SP_CAMBIARESTADO_PRODUCTO')
    DROP PROCEDURE SP_CAMBIARESTADO_PRODUCTO;
GO

CREATE PROCEDURE SP_CAMBIARESTADO_PRODUCTO
(
    @IdProducto INT,
    @Estado     BIT
)
AS
BEGIN
    UPDATE Producto
    SET Estado = @Estado
    WHERE IdProducto = @IdProducto;
END
GO

-------------------------
----PROCEDIMIENTO PARA COMPRA
-------------------------

CREATE PROCEDURE SP_ObtenerCorrelativoCompra
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UltimoNumero INT;

    -- Buscar el correlativo del año actual
    SELECT @UltimoNumero = UltimoNumero
    FROM CorrelativoCompra
    WHERE YEAR(FechaActualizacion) = YEAR(GETDATE());

    -- Si no existe → crear uno nuevo
    IF @UltimoNumero IS NULL
    BEGIN
        INSERT INTO CorrelativoCompra (UltimoNumero, FechaActualizacion)
        VALUES (1, GETDATE());

        SELECT 1 AS UltimoNumero;
    END
    ELSE
    BEGIN
        SELECT @UltimoNumero AS UltimoNumero;
    END
END
GO

CREATE PROCEDURE SP_IncrementarCorrelativoCompra
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CorrelativoCompra
    SET UltimoNumero = UltimoNumero + 1,
        FechaActualizacion = GETDATE();
END
GO



CREATE PROCEDURE SP_RegistrarCompra
(
    @IdUsuario INT,
    @IdProveedor INT,
    @TipoDocumento VARCHAR(20),
    @NumeroDocumento VARCHAR(50),
    @NumeroCorrelativo INT,
    @MontoTotal DECIMAL(10,2),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Compra(IdUsuario, IdProveedor, TipoDocumento, NumeroDocumento, NumeroCorrelativo, MontoTotal)
    VALUES (@IdUsuario, @IdProveedor, @TipoDocumento, @NumeroDocumento, @NumeroCorrelativo, @MontoTotal);

    SET @Resultado = SCOPE_IDENTITY();
END
GO


CREATE PROCEDURE SP_RegistrarDetalleCompra
(
    @IdCompra INT,
    @IdProducto INT,
    @PrecioCompra DECIMAL(10,2),
    @PrecioVenta DECIMAL(10,2),
    @Cantidad INT,
    @Subtotal DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DetalleCompra (IdCompra, IdProducto, PrecioCompra, PrecioVenta, Cantidad, SubTotal)
    VALUES (@IdCompra, @IdProducto, @PrecioCompra, @PrecioVenta, @Cantidad, @Subtotal);
END
GO


CREATE PROCEDURE SP_ActualizarStockProducto
(
    @IdProducto INT,
    @Cantidad INT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Producto
    SET Stock = Stock + @Cantidad
    WHERE IdProducto = @IdProducto;
END
GO


CREATE PROCEDURE SP_ActualizarPreciosProducto
(
    @IdProducto INT,
    @PrecioCompra DECIMAL(10,2),
    @PrecioVenta DECIMAL(10,2)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Producto
    SET PrecioCompra = @PrecioCompra,
        PrecioVenta = @PrecioVenta
    WHERE IdProducto = @IdProducto;
END
GO

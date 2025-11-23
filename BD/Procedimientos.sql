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

-------------------------
----PROCEDIMIENTO PARA ENTRADAS
-------------------------

CREATE PROCEDURE SP_ListarEntradaTipo
AS
BEGIN
    SELECT IdEntradaTipo, Descripcion, PrecioBase, Estado
    FROM EntradaTipo
    WHERE Estado = 1
    ORDER BY IdEntradaTipo;
END
GO

CREATE PROCEDURE SP_ActualizarPreciosEntrada
(
    @IdEntradaTipo INT,
    @NuevoPrecio DECIMAL(10,2)
)
AS
BEGIN
    UPDATE EntradaTipo
    SET PrecioBase = @NuevoPrecio
    WHERE IdEntradaTipo = @IdEntradaTipo;
END
GO

-------------------------
----PROCEDIMIENTO PARA PROMOCIONES
-------------------------







-------------------------
----PROCEDIMIENTO PARA MANTENEDOR
-------------------------

CREATE PROCEDURE SP_GET_NEGOCIO
AS
BEGIN
    SELECT 
        IdNegocio,
        NombreNegocio,
        Direccion,
        Ciudad,
        Telefono,
        Logo
    FROM Negocio
    WHERE IdNegocio = 1;
END
GO

CREATE PROCEDURE SP_UPDATE_NEGOCIO
(
    @NombreNegocio VARCHAR(150),
    @Direccion VARCHAR(200),
    @Ciudad VARCHAR(150),
    @Telefono VARCHAR(20),
    @Logo VARBINARY(MAX)
)
AS
BEGIN
    UPDATE Negocio
    SET 
        NombreNegocio = @NombreNegocio,
        Direccion     = @Direccion,
        Ciudad        = @Ciudad,
        Telefono      = @Telefono,
        Logo          = @Logo
    WHERE IdNegocio = 1;
END
GO

-------------------------
----PROCEDIMIENTO PARA VENTAS
-------------------------
/* ===========================
   TIPOS DE TABLA PARA VENTAS
   =========================== */

IF TYPE_ID('TVP_DetalleEntrada') IS NOT NULL
    DROP TYPE TVP_DetalleEntrada;
GO

CREATE TYPE TVP_DetalleEntrada AS TABLE
(
    IdEntradaTipo   INT         NOT NULL,
    Cantidad        INT         NOT NULL,
    PrecioUnitario  DECIMAL(10,2) NOT NULL
);
GO


IF TYPE_ID('TVP_DetalleProducto') IS NOT NULL
    DROP TYPE TVP_DetalleProducto;
GO

CREATE TYPE TVP_DetalleProducto AS TABLE
(
    IdProducto      INT         NOT NULL,
    Cantidad        INT         NOT NULL,
    PrecioUnitario  DECIMAL(10,2) NOT NULL
);
GO

/* ==========================================
   SP: OBTENER CORRELATIVO PARA NUMERO VENTA
   ========================================== */

IF OBJECT_ID('SP_ObtenerCorrelativoVenta', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerCorrelativoVenta;
GO

CREATE PROCEDURE SP_ObtenerCorrelativoVenta
(
    @NumeroVenta VARCHAR(50) OUTPUT,
    @Resultado   BIT OUTPUT,
    @Mensaje     VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE 
        @IdCorrelativo  INT,
        @UltimoNumero   INT,
        @CantidadDigitos INT,
        @Prefijo        VARCHAR(10);

    SELECT TOP 1
        @IdCorrelativo   = IdCorrelativo,
        @UltimoNumero    = UltimoNumero,
        @CantidadDigitos = CantidadDigitos,
        @Prefijo         = ISNULL(Prefijo,'')
    FROM Correlativo
    WHERE Estado = 1
    ORDER BY IdCorrelativo;

    IF @IdCorrelativo IS NULL
    BEGIN
        SET @Resultado = 0;
        SET @Mensaje = 'No existe configuración de correlativo activa.';
        RETURN;
    END

    SET @UltimoNumero = @UltimoNumero + 1;

    UPDATE Correlativo
    SET UltimoNumero = @UltimoNumero
    WHERE IdCorrelativo = @IdCorrelativo;

    DECLARE @NumeroStr VARCHAR(50);
    SET @NumeroStr = RIGHT(REPLICATE('0', @CantidadDigitos) + CAST(@UltimoNumero AS VARCHAR(20)), @CantidadDigitos);

    SET @NumeroVenta = CASE WHEN @Prefijo = '' THEN @NumeroStr ELSE @Prefijo + @NumeroStr END;

    SET @Resultado = 1;
    SET @Mensaje = 'OK';
END
GO

/* ==========================================
   SP: MODULO DE PROMOCION 2X1
   ========================================== */

CREATE PROCEDURE SP_OBTENER_PROMO
AS
BEGIN
    SELECT TOP 1 
        IdPromocion,
        Estado,
        Categoria,
        UsuarioModifico,
        FechaActualizacion
    FROM Promocion;
END
GO

CREATE PROCEDURE SP_ACTUALIZAR_PROMO
(
    @Estado BIT,
    @Categoria VARCHAR(20),
    @UsuarioModifico INT
)
AS
BEGIN
    UPDATE Promocion
    SET Estado = @Estado,
        Categoria = @Categoria,
        UsuarioModifico = @UsuarioModifico,
        FechaActualizacion = GETDATE()
    WHERE IdPromocion = 1;   -- porque solo habrá 1 registro
END
GO
/* ==============================================
     SP: PARA BUSCAR CLIENTE PARTE DEL MODULO DE VENTA
   ============================================== */
CREATE PROCEDURE SP_BUSCARCLIENTE_POR_DNI
@DNI VARCHAR(20)
AS
BEGIN
    SELECT TOP 1 
        IdCliente,
        DNI,
        NombreCompleto,
        Telefono
    FROM Cliente
    WHERE DNI = @DNI;
END
GO



/* ==============================================
   EJECUTAR TODOS ESTOS DROP PROCEDURE, NO USAREMOS MAS ESOS PROCEDIMIENTOS
   ============================================== */

IF OBJECT_ID('SP_RegistrarVentaPiscina', 'P') IS NOT NULL
    DROP PROCEDURE SP_RegistrarVentaPiscina;
GO



IF OBJECT_ID('SP_ListarEntradaTipoActivas','P') IS NOT NULL
    DROP PROCEDURE SP_ListarEntradaTipoActivas;
GO


IF OBJECT_ID('SP_ListarProductosActivosVenta','P') IS NOT NULL
    DROP PROCEDURE SP_ListarProductosActivosVenta;
GO

IF OBJECT_ID('PromocionVigencia', 'U') IS NOT NULL DROP TABLE PromocionVigencia;
IF OBJECT_ID('PromocionLimite', 'U') IS NOT NULL DROP TABLE PromocionLimite;
IF OBJECT_ID('PromocionCondicion', 'U') IS NOT NULL DROP TABLE PromocionCondicion;
IF OBJECT_ID('Promocion', 'U') IS NOT NULL DROP TABLE Promocion;
GO

IF OBJECT_ID('SP_RegistrarPromocion', 'P') IS NOT NULL DROP PROCEDURE SP_RegistrarPromocion;
IF OBJECT_ID('SP_ListarPromociones', 'P') IS NOT NULL DROP PROCEDURE SP_ListarPromociones;
IF OBJECT_ID('SP_EliminarPromocion', 'P') IS NOT NULL DROP PROCEDURE SP_EliminarPromocion;
IF OBJECT_ID('SP_PROMO_ACTIVA', 'P') IS NOT NULL DROP PROCEDURE SP_PROMO_ACTIVA;
IF OBJECT_ID('SP_PROMO_ACTIVA_COMPLETA', 'P') IS NOT NULL DROP PROCEDURE SP_PROMO_ACTIVA_COMPLETA;
GO
/* ==============================================
     SP: PARA REGISTRAR VENTAS
   ============================================== */


CREATE TYPE DetalleEntradaType AS TABLE
(
    IdEntradaTipo INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    PrecioAplicado DECIMAL(10,2),
    SubTotal DECIMAL(10,2)
);
GO


CREATE TYPE DetalleProductoType AS TABLE
(
    IdProducto INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    SubTotal DECIMAL(10,2)
);
GO


CREATE PROCEDURE SP_REGISTRAR_VENTA
(
    @IdUsuario INT,
    @IdCliente INT = NULL,
    @MetodoPago VARCHAR(20),
    @IdCajaTurno INT,
    @MontoTotal DECIMAL(10,2),
    
    @DetalleEntradas dbo.DetalleEntradaType READONLY,
    @DetalleProductos dbo.DetalleProductoType READONLY,

    @NumeroVentaGenerado VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdVenta INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        ----------------------------------------------------
        -- 1. GENERAR CORRELATIVO
        ----------------------------------------------------
        DECLARE @Ultimo INT, @Digitos INT, @Nuevo INT, @Prefijo VARCHAR(20);

        SELECT TOP 1 
              @Ultimo = UltimoNumero,
              @Digitos = CantidadDigitos,
              @Prefijo = Prefijo
        FROM Correlativo
        WHERE Estado = 1;

        SET @Nuevo = @Ultimo + 1;

        DECLARE @NumeroFormateado VARCHAR(50);
        SET @NumeroFormateado = 
            ISNULL(@Prefijo,'') + RIGHT(REPLICATE('0', @Digitos) + CAST(@Nuevo AS VARCHAR(20)), @Digitos);

        -- Actualizar correlativo
        UPDATE Correlativo
        SET UltimoNumero = @Nuevo
        WHERE Estado = 1;

        SET @NumeroVentaGenerado = @NumeroFormateado;


        ----------------------------------------------------
        -- 2. INSERTAR VENTA
        ----------------------------------------------------
        INSERT INTO Venta(IdUsuario, IdCliente, NumeroVenta, MontoTotal, MetodoPago, IdCajaTurno)
        VALUES(@IdUsuario, @IdCliente, @NumeroVentaGenerado, @MontoTotal, @MetodoPago, @IdCajaTurno);

        SET @IdVenta = SCOPE_IDENTITY();


        ----------------------------------------------------
        -- 3. INSERTAR DETALLE DE ENTRADAS
        ----------------------------------------------------
        INSERT INTO DetalleVentaEntrada
        (
            IdVenta,
            IdEntradaTipo,
            Cantidad,
            PrecioUnitario,
            PrecioAplicado,
            SubTotal
        )
        SELECT 
            @IdVenta,
            IdEntradaTipo,
            Cantidad,
            PrecioUnitario,
            PrecioAplicado,
            SubTotal
        FROM @DetalleEntradas;


        ----------------------------------------------------
        -- 4. INSERTAR DETALLE DE PRODUCTOS + DESCONTAR STOCK
        ----------------------------------------------------
        INSERT INTO DetalleVentaProducto
        (
            IdVenta,
            IdProducto,
            Cantidad,
            PrecioUnitario,
            SubTotal
        )
        SELECT @IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal
        FROM @DetalleProductos;

        -- Descontar stock
        UPDATE p
        SET p.Stock = p.Stock - d.Cantidad
        FROM Producto p
        INNER JOIN @DetalleProductos d ON p.IdProducto = d.IdProducto;


        ----------------------------------------------------
        -- 5. FINALIZAR
        ----------------------------------------------------
        COMMIT TRANSACTION;
        RETURN 1;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @NumeroVentaGenerado = '';
        RETURN 0;
    END CATCH
END;
GO

CREATE PROCEDURE SP_CAJA_OBTENER_ACTIVA
(
    @IdUsuario INT
)
AS
BEGIN
    SELECT TOP 1 *
    FROM CajaTurno
    WHERE IdUsuario = @IdUsuario AND Estado = 1;
END;
GO

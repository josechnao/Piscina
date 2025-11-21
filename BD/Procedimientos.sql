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

DROP PROCEDURE IF EXISTS SP_RegistrarPromocion;
GO

CREATE PROCEDURE SP_RegistrarPromocion
(
    @TipoPromo VARCHAR(20),              -- '2x1' / 'Descuento'
    @IdEntradaTipo INT,
    @Porcentaje DECIMAL(5,2) = NULL,     -- solo si es descuento
    @TipoCondicion VARCHAR(30),          -- texto
    @CantidadCondicion INT = NULL,       
    @TipoLimite VARCHAR(20),             -- texto
    @CantidadLimite INT = NULL, 
    @TipoVigencia VARCHAR(20),           -- texto
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL,
    @FechaDia DATE = NULL,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1) Insertar cabecera promoción
        INSERT INTO Promocion(TipoPromo, IdEntradaTipo, Porcentaje, Estado)
        VALUES(@TipoPromo, @IdEntradaTipo, @Porcentaje, 1);

        DECLARE @IdPromocion INT = SCOPE_IDENTITY();

        -- 2) Insertar condición
        INSERT INTO PromocionCondicion(IdPromocion, TipoCondicion, Cantidad)
        VALUES(@IdPromocion, @TipoCondicion, @CantidadCondicion);

        -- 3) Insertar límite
        INSERT INTO PromocionLimite(IdPromocion, TipoLimite, CantidadLimite, CantidadUsada)
        VALUES(@IdPromocion, @TipoLimite, @CantidadLimite, 0);

        -- 4) Insertar vigencia
        INSERT INTO PromocionVigencia(IdPromocion, TipoVigencia, FechaInicio, FechaFin, FechaDia)
        VALUES(@IdPromocion, @TipoVigencia, @FechaInicio, @FechaFin, @FechaDia);

        SET @Resultado = 1;
        SET @Mensaje = 'Promoción registrada correctamente';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



DROP PROCEDURE IF EXISTS SP_ListarPromociones;
GO

CREATE PROCEDURE SP_ListarPromociones
AS
BEGIN
    SELECT
        P.IdPromocion,
        P.TipoPromo,
        ISNULL(ET.Descripcion, 'Todos') AS Categoria,
        P.Porcentaje,
        P.Estado,
        C.TipoCondicion,
        C.Cantidad AS CantidadCondicion,
        L.TipoLimite,
        L.CantidadLimite,
        L.CantidadUsada,
        V.TipoVigencia,
        V.FechaInicio,
        V.FechaFin,
        V.FechaDia
    FROM Promocion P
    LEFT JOIN EntradaTipo ET ON ET.IdEntradaTipo = P.IdEntradaTipo
    INNER JOIN PromocionCondicion C ON C.IdPromocion = P.IdPromocion
    INNER JOIN PromocionLimite L ON L.IdPromocion = P.IdPromocion
    INNER JOIN PromocionVigencia V ON V.IdPromocion = P.IdPromocion;
END
GO

EXEC SP_ListarPromociones;



DROP PROCEDURE IF EXISTS SP_EliminarPromocion;
GO

CREATE PROCEDURE SP_EliminarPromocion
(
    @IdPromocion INT
)
AS
BEGIN
    DELETE FROM PromocionVigencia WHERE IdPromocion = @IdPromocion;
    DELETE FROM PromocionLimite WHERE IdPromocion = @IdPromocion;
    DELETE FROM PromocionCondicion WHERE IdPromocion = @IdPromocion;
    DELETE FROM Promocion WHERE IdPromocion = @IdPromocion;
END
GO

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

/* ==============================================
   SP: REGISTRAR VENTA PISCINA (ENTRADAS + SNACKS)
   ============================================== */

IF OBJECT_ID('SP_RegistrarVentaPiscina', 'P') IS NOT NULL
    DROP PROCEDURE SP_RegistrarVentaPiscina;
GO

CREATE PROCEDURE SP_RegistrarVentaPiscina
(
    @IdUsuario          INT,
    @IdCliente          INT,
    @MetodoPago         VARCHAR(20),   -- 'EFECTIVO','QR','CORTESIA'
    @IdCajaTurno        INT,
    @DetalleEntradas    TVP_DetalleEntrada READONLY,
    @DetalleProductos   TVP_DetalleProducto READONLY,
    @NumeroVenta        VARCHAR(50) OUTPUT,
    @PromoParcial       BIT OUTPUT,   -- 1 = alguna promo solo se aplicó parcialmente
    @Resultado          BIT OUTPUT,
    @Mensaje            VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @IdVenta INT;
    DECLARE @MontoTotal DECIMAL(10,2) = 0;

    SET @PromoParcial = 0;
    SET @Resultado = 0;
    SET @Mensaje = '';

    BEGIN TRY
        BEGIN TRAN;

        /* 0) VALIDACIONES BÁSICAS */

        IF NOT EXISTS (SELECT 1 FROM @DetalleEntradas) 
           AND NOT EXISTS (SELECT 1 FROM @DetalleProductos)
        BEGIN
            SET @Mensaje = 'No hay ítems en la venta.';
            ROLLBACK TRAN;
            RETURN;
        END

        /* 1) VALIDAR CONFLICTOS DE PROMOCIONES */
        DECLARE @GlobalActivas INT;

        SELECT @GlobalActivas = COUNT(*)
        FROM Promocion P
        WHERE P.Estado = 1
          AND P.IdEntradaTipo IS NULL;

        IF @GlobalActivas > 1
        BEGIN
            SET @Mensaje = 'Existen múltiples promociones globales activas. Desactive una antes de continuar.';
            ROLLBACK TRAN;
            RETURN;
        END

        -- Conflictos por categoría (más de una promo activa por tipo de entrada)
        IF EXISTS (
            SELECT IdEntradaTipo
            FROM Promocion
            WHERE Estado = 1 AND IdEntradaTipo IS NOT NULL
            GROUP BY IdEntradaTipo
            HAVING COUNT(*) > 1
        )
        BEGIN
            SET @Mensaje = 'Existen múltiples promociones activas para una misma categoría de entrada. Desactive una antes de continuar.';
            ROLLBACK TRAN;
            RETURN;
        END


        /* 2) OBTENER NUMERO DE VENTA */

        DECLARE @ResCorr BIT, @MsgCorr VARCHAR(500), @NumVenta VARCHAR(50);

        EXEC SP_ObtenerCorrelativoVenta
             @NumeroVenta = @NumVenta OUTPUT,
             @Resultado   = @ResCorr OUTPUT,
             @Mensaje     = @MsgCorr OUTPUT;

        IF @ResCorr = 0
        BEGIN
            SET @Mensaje = @MsgCorr;
            ROLLBACK TRAN;
            RETURN;
        END

        SET @NumeroVenta = @NumVenta;


        /* 3) PROCESAR ENTRADAS: PROMOS, LIMITES, VIGENCIA */

        -- Temp con info de promo candidata para cada línea de entrada
        CREATE TABLE #TmpEntrada (
            IdEntradaTipo   INT,
            Cantidad        INT,
            PrecioUnitario  DECIMAL(10,2),
            IdPromocion     INT NULL,
            TipoPromo       VARCHAR(20) NULL,
            Porcentaje      DECIMAL(5,2) NULL,
            TipoCondicion   VARCHAR(30) NULL,
            CantidadCond    INT NULL,
            TipoLimite      VARCHAR(20) NULL,
            CantidadLimite  INT NULL,
            CantidadUsada   INT NULL,
            TipoVigencia    VARCHAR(20) NULL,
            FechaInicio     DATE NULL,
            FechaFin        DATE NULL,
            FechaDia        DATE NULL
        );

        INSERT INTO #TmpEntrada (IdEntradaTipo, Cantidad, PrecioUnitario,
                                 IdPromocion, TipoPromo, Porcentaje,
                                 TipoCondicion, CantidadCond, TipoLimite,
                                 CantidadLimite, CantidadUsada,
                                 TipoVigencia, FechaInicio, FechaFin, FechaDia)
        SELECT
            de.IdEntradaTipo,
            de.Cantidad,
            de.PrecioUnitario,
            p.IdPromocion,
            p.TipoPromo,
            p.Porcentaje,
            pc.TipoCondicion,
            pc.Cantidad,
            pl.TipoLimite,
            pl.CantidadLimite,
            pl.CantidadUsada,
            pv.TipoVigencia,
            pv.FechaInicio,
            pv.FechaFin,
            pv.FechaDia
        FROM @DetalleEntradas de
        OUTER APPLY (
            -- Promo candidata: primero global, si no hay o no aplica, por categoría
            SELECT TOP 1 P.*
            FROM Promocion P
            LEFT JOIN PromocionVigencia PV ON PV.IdPromocion = P.IdPromocion
            WHERE 
                P.Estado = 1
                AND (
                        (P.IdEntradaTipo IS NULL) OR
                        (P.IdEntradaTipo = de.IdEntradaTipo)
                    )
                AND (
                        PV.TipoVigencia IS NULL
                        OR PV.TipoVigencia = 'SIN_FECHA'
                        OR (PV.TipoVigencia = 'SOLO_DIA' 
                            AND PV.FechaDia = CONVERT(DATE, GETDATE()))
                        OR (PV.TipoVigencia = 'RANGO'
                            AND CONVERT(DATE, GETDATE()) BETWEEN PV.FechaInicio AND PV.FechaFin)
                    )
            ORDER BY 
                CASE WHEN P.IdEntradaTipo IS NULL THEN 0 ELSE 1 END DESC  -- global manda
        ) p
        LEFT JOIN PromocionCondicion pc ON pc.IdPromocion = p.IdPromocion
        LEFT JOIN PromocionLimite   pl ON pl.IdPromocion = p.IdPromocion
        LEFT JOIN PromocionVigencia pv ON pv.IdPromocion = p.IdPromocion;


        -- Tabla final para insertar detalle de entradas
        CREATE TABLE #DetEntradaFinal (
            IdEntradaTipo   INT,
            Cantidad        INT,
            PrecioUnitario  DECIMAL(10,2),
            PrecioAplicado  DECIMAL(10,2),
            SubTotal        DECIMAL(10,2),
            IdPromocion     INT NULL
        );

        DECLARE 
            @IdEntradaTipo   INT,
            @Cant            INT,
            @PrecioUnit      DECIMAL(10,2),
            @IdProm          INT,
            @TipoPromo       VARCHAR(20),
            @Porc            DECIMAL(5,2),
            @TipoCond        VARCHAR(30),
            @CantCond        INT,
            @TipoLimite      VARCHAR(20),
            @CantLimite      INT,
            @CantUsada       INT,
            @TipoVig         VARCHAR(20),
            @FIni            DATE,
            @FFin            DATE,
            @FDia            DATE,
            @CantPromo       INT,
            @CantNormal      INT,
            @BaseAcum        INT,
            @TotalAcum       INT,
            @PagoPromo       DECIMAL(10,2),
            @PrecioAplicado  DECIMAL(10,2),
            @SubTotalFila    DECIMAL(10,2);

        DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
            SELECT IdEntradaTipo, Cantidad, PrecioUnitario,
                   IdPromocion, TipoPromo, Porcentaje,
                   TipoCondicion, CantidadCond,
                   TipoLimite, CantidadLimite, ISNULL(CantidadUsada,0),
                   TipoVigencia, FechaInicio, FechaFin, FechaDia
            FROM #TmpEntrada;

        OPEN cur;
        FETCH NEXT FROM cur INTO 
            @IdEntradaTipo, @Cant, @PrecioUnit,
            @IdProm, @TipoPromo, @Porc,
            @TipoCond, @CantCond,
            @TipoLimite, @CantLimite, @CantUsada,
            @TipoVig, @FIni, @FFin, @FDia;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @CantPromo = 0;
            SET @CantNormal = @Cant;

            -- Si no hay promo o método es CORTESIA, no procesamos nada de promo
            IF @IdProm IS NOT NULL AND @MetodoPago <> 'CORTESIA'
            BEGIN
                -- 3.1 Validar condición (versión simplificada pero funcional)
                SET @BaseAcum = 0;

                IF @TipoCond = 'ACUMULA_DIA'
                BEGIN
                    SELECT @BaseAcum = ISNULL(SUM(dve.Cantidad),0)
                    FROM DetalleVentaEntrada dve
                    INNER JOIN Venta v ON v.IdVenta = dve.IdVenta
                    WHERE 
                        dve.IdPromocion = @IdProm
                        AND v.IdCliente = @IdCliente
                        AND CONVERT(DATE, v.FechaRegistro) = CONVERT(DATE, GETDATE());
                END
                ELSE IF @TipoCond = 'ACUMULA_VIGENCIA'
                BEGIN
                    SELECT @BaseAcum = ISNULL(SUM(dve.Cantidad),0)
                    FROM DetalleVentaEntrada dve
                    INNER JOIN Venta v ON v.IdVenta = dve.IdVenta
                    WHERE 
                        dve.IdPromocion = @IdProm
                        AND v.IdCliente = @IdCliente
                        AND (@FIni IS NULL OR CONVERT(DATE,v.FechaRegistro) >= @FIni)
                        AND (@FFin IS NULL OR CONVERT(DATE,v.FechaRegistro) <= @FFin);
                END

                SET @TotalAcum = @BaseAcum + @Cant;

                IF (@TipoCond = 'MINIMO_COMPRA' AND @Cant < @CantCond)
                    OR (@TipoCond IN ('ACUMULA_DIA','ACUMULA_VIGENCIA') AND @TotalAcum < @CantCond)
                BEGIN
                    -- No cumple condición -> sin promo
                    SET @IdProm = NULL;
                END
                ELSE
                BEGIN
                    -- 3.2 Validar límite
                    IF @TipoLimite = 'CANTIDAD_MAXIMA' AND @CantLimite IS NOT NULL
                    BEGIN
                        DECLARE @Restantes INT = @CantLimite - @CantUsada;

                        IF @Restantes <= 0
                        BEGIN
                            -- Ya no quedan promos
                            SET @IdProm = NULL;
                        END
                        ELSE IF @Cant <= @Restantes
                        BEGIN
                            SET @CantPromo = @Cant;
                            SET @CantNormal = 0;
                            SET @CantUsada = @CantUsada + @CantPromo;
                        END
                        ELSE
                        BEGIN
                            -- Aplica solo a parte de la cantidad
                            SET @CantPromo = @Restantes;
                            SET @CantNormal = @Cant - @Restantes;
                            SET @CantUsada = @CantUsada + @CantPromo;
                            SET @PromoParcial = 1;
                        END
                    END
                    ELSE
                    BEGIN
                        -- Sin límite
                        SET @CantPromo = @Cant;
                        SET @CantNormal = 0;
                    END

                    -- Actualizar CantidadUsada en tabla de límite
                    IF @IdProm IS NOT NULL AND @TipoLimite = 'CANTIDAD_MAXIMA' AND @CantPromo > 0
                    BEGIN
                        UPDATE PromocionLimite
                        SET CantidadUsada = CantidadUsada + @CantPromo
                        WHERE IdPromocion = @IdProm;

                        -- Desactivar promo si se llegó al límite
                        UPDATE P
                        SET P.Estado = 0
                        FROM Promocion P
                        INNER JOIN PromocionLimite PL ON PL.IdPromocion = P.IdPromocion
                        WHERE P.IdPromocion = @IdProm
                          AND PL.CantidadLimite IS NOT NULL
                          AND PL.CantidadUsada >= PL.CantidadLimite;
                    END
                END
            END

            -- 3.3 Insertar fila(s) en #DetEntradaFinal

            -- Parte con promo
            IF @CantPromo > 0 AND @IdProm IS NOT NULL AND @MetodoPago <> 'CORTESIA'
            BEGIN
                IF @TipoPromo = 'Descuento'
                BEGIN
                    SET @PrecioAplicado = @PrecioUnit * (1 - (@Porc / 100.0));
                    SET @SubTotalFila   = @PrecioAplicado * @CantPromo;
                END
                ELSE IF @TipoPromo = '2x1'
                BEGIN
                    DECLARE @Paga INT = CEILING(@CantPromo / 2.0);
                    SET @PrecioAplicado = (@PrecioUnit * @Paga) / @CantPromo;
                    SET @SubTotalFila   = @PrecioUnit * @Paga;
                END
                ELSE
                BEGIN
                    SET @PrecioAplicado = @PrecioUnit;
                    SET @SubTotalFila   = @PrecioUnit * @CantPromo;
                END

                INSERT INTO #DetEntradaFinal (IdEntradaTipo, Cantidad, PrecioUnitario, PrecioAplicado, SubTotal, IdPromocion)
                VALUES (@IdEntradaTipo, @CantPromo, @PrecioUnit, @PrecioAplicado, @SubTotalFila, @IdProm);

                SET @MontoTotal = @MontoTotal + @SubTotalFila;
            END

            -- Parte sin promo (o toda la fila si no aplica)
            IF @CantNormal > 0
            BEGIN
                SET @PrecioAplicado = @PrecioUnit;
                SET @SubTotalFila   = @PrecioUnit * @CantNormal;

                INSERT INTO #DetEntradaFinal (IdEntradaTipo, Cantidad, PrecioUnitario, PrecioAplicado, SubTotal, IdPromocion)
                VALUES (@IdEntradaTipo, @CantNormal, @PrecioUnit, @PrecioAplicado, @SubTotalFila, NULL);

                SET @MontoTotal = @MontoTotal + @SubTotalFila;
            END

            FETCH NEXT FROM cur INTO 
                @IdEntradaTipo, @Cant, @PrecioUnit,
                @IdProm, @TipoPromo, @Porc,
                @TipoCond, @CantCond,
                @TipoLimite, @CantLimite, @CantUsada,
                @TipoVig, @FIni, @FFin, @FDia;
        END

        CLOSE cur;
        DEALLOCATE cur;


        /* 4) PROCESAR PRODUCTOS (SIN PROMO, SOLO CORTESIA) */

        CREATE TABLE #DetProductoFinal (
            IdProducto      INT,
            Cantidad        INT,
            PrecioUnitario  DECIMAL(10,2),
            SubTotal        DECIMAL(10,2)
        );

        DECLARE 
            @IdProd INT,
            @CantProd INT,
            @PrecioProd DECIMAL(10,2);

        DECLARE curp CURSOR LOCAL FAST_FORWARD FOR
            SELECT IdProducto, Cantidad, PrecioUnitario
            FROM @DetalleProductos;

        OPEN curp;
        FETCH NEXT FROM curp INTO @IdProd, @CantProd, @PrecioProd;

        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @MetodoPago = 'CORTESIA'
                SET @SubTotalFila = 0;
            ELSE
                SET @SubTotalFila = @PrecioProd * @CantProd;

            INSERT INTO #DetProductoFinal (IdProducto, Cantidad, PrecioUnitario, SubTotal)
            VALUES (@IdProd, @CantProd, @PrecioProd, @SubTotalFila);

            SET @MontoTotal = @MontoTotal + @SubTotalFila;

            FETCH NEXT FROM curp INTO @IdProd, @CantProd, @PrecioProd;
        END

        CLOSE curp;
        DEALLOCATE curp;


        /* 5) CORTESIA: OVERRIDE TOTAL A 0 (PERO STOCK IGUAL SE DESCARGA) */

        IF @MetodoPago = 'CORTESIA'
        BEGIN
            SET @MontoTotal = 0;

            UPDATE #DetEntradaFinal
            SET PrecioAplicado = 0, SubTotal = 0;

            UPDATE #DetProductoFinal
            SET SubTotal = 0;
        END


        /* 6) INSERTAR VENTA */

        INSERT INTO Venta (IdUsuario, IdCliente, NumeroVenta, MontoTotal, MetodoPago, FechaRegistro, IdCajaTurno)
        VALUES (@IdUsuario, @IdCliente, @NumeroVenta, @MontoTotal, @MetodoPago, GETDATE(), @IdCajaTurno);

        SET @IdVenta = SCOPE_IDENTITY();


        /* 7) INSERTAR DETALLE ENTRADAS */

        INSERT INTO DetalleVentaEntrada
            (IdVenta, IdEntradaTipo, Cantidad, PrecioUnitario, PrecioAplicado, SubTotal, IdPromocion)
        SELECT
            @IdVenta,
            IdEntradaTipo,
            Cantidad,
            PrecioUnitario,
            PrecioAplicado,
            SubTotal,
            IdPromocion
        FROM #DetEntradaFinal;


        /* 8) INSERTAR DETALLE PRODUCTOS + ACTUALIZAR STOCK */

        INSERT INTO DetalleVentaProducto
            (IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal)
        SELECT
            @IdVenta,
            IdProducto,
            Cantidad,
            PrecioUnitario,
            SubTotal
        FROM #DetProductoFinal;

        -- Descontar stock
        UPDATE P
        SET P.Stock = P.Stock - D.Cantidad
        FROM Producto P
        INNER JOIN #DetProductoFinal D ON D.IdProducto = P.IdProducto;


        /* 9) FINALIZAR */

        SET @Resultado = 1;
        SET @Mensaje = 'Venta registrada correctamente';

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO


IF OBJECT_ID('SP_ListarEntradaTipoActivas','P') IS NOT NULL
    DROP PROCEDURE SP_ListarEntradaTipoActivas;
GO

CREATE PROCEDURE SP_ListarEntradaTipoActivas
AS
BEGIN
    SELECT 
        IdEntradaTipo,
        Descripcion,
        PrecioBase,
        Estado
    FROM EntradaTipo
    WHERE Estado = 1;
END
GO

IF OBJECT_ID('SP_ListarProductosActivosVenta','P') IS NOT NULL
    DROP PROCEDURE SP_ListarProductosActivosVenta;
GO

CREATE PROCEDURE SP_ListarProductosActivosVenta
AS
BEGIN
    SELECT 
        p.IdProducto,
        p.Nombre,
        p.Descripcion,
        p.PrecioVenta,
        p.Stock,
        c.Descripcion AS Categoria
    FROM Producto p
    INNER JOIN Categoria c ON c.IdCategoria = p.IdCategoria
    WHERE p.Estado = 1;
END
GO

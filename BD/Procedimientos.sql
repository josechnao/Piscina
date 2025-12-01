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
    @Logo VARBINARY(MAX),
    @Resultado BIT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Negocio
        SET NombreNegocio = @NombreNegocio,
            Direccion = @Direccion,
            Ciudad = @Ciudad,
            Telefono = @Telefono,
            Logo = @Logo
        WHERE IdNegocio = 1;

        SET @Resultado = 1;
        SET @Mensaje = 'Datos actualizados correctamente';
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
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

/* ==========================================
   SP: MODULO DE CAJATURNO
   ========================================== */

CREATE PROCEDURE SP_VERIFICAR_CAJA_ABIERTA
(
    @IdUsuario       INT,
    @IdCajaTurno     INT OUTPUT,
    @EstadoCaja      BIT OUTPUT,      -- 1 = abierta, 0 = cerrada/no existe
    @Mensaje         VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        @IdCajaTurno = IdCajaTurno,
        @EstadoCaja = Estado
    FROM CajaTurno
    WHERE IdUsuario = @IdUsuario
      AND Estado = 1 -- abierta
    ORDER BY FechaApertura DESC;

    IF (@IdCajaTurno IS NULL)
    BEGIN
        SET @IdCajaTurno = 0;
        SET @EstadoCaja = 0;
        SET @Mensaje = 'No existe una caja abierta.';
    END
    ELSE
    BEGIN
        SET @Mensaje = 'Caja abierta encontrada.';
    END
END;
GO


CREATE PROCEDURE SP_ABRIR_CAJA
(
    @IdUsuario       INT,
    @MontoInicial    DECIMAL(10,2),
    @IdCajaTurno     INT OUTPUT,
    @Mensaje         VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Verificar si ya tiene caja abierta
    IF EXISTS (SELECT 1 FROM CajaTurno 
               WHERE IdUsuario = @IdUsuario AND Estado = 1)
    BEGIN
        SET @IdCajaTurno = 0;
        SET @Mensaje = 'El usuario ya tiene una caja abierta.';
        RETURN;
    END

    -- 2. Insertar nueva caja
    INSERT INTO CajaTurno (IdUsuario, MontoInicial, Estado)
    VALUES (@IdUsuario, @MontoInicial, 1);

    SET @IdCajaTurno = SCOPE_IDENTITY();
    SET @Mensaje = 'Caja abierta correctamente.';
END;
GO


CREATE PROCEDURE SP_OBTENER_CAJA_ACTIVA
(
    @IdUsuario INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdCajaTurno,
        IdUsuario,
        MontoInicial,
        MontoFinal,
        TotalVentas,
        TotalGastos,
        Diferencia,
        FechaApertura,
        FechaCierre,
        Observacion,
        Estado
    FROM CajaTurno
    WHERE IdUsuario = @IdUsuario
      AND Estado = 1      -- solo caja activa
    ORDER BY FechaApertura DESC;
END;
GO

CREATE PROCEDURE SP_CERRAR_CAJA
(
    @IdCajaTurno INT,
    @MontoFinal  DECIMAL(10,2),
    @Observacion VARCHAR(250) = NULL,
    @Mensaje     VARCHAR(100) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdUsuario     INT;
    DECLARE @FechaApertura DATETIME;
    DECLARE @MontoInicial  DECIMAL(10,2);
    DECLARE @TotalVentas   DECIMAL(10,2);
    DECLARE @TotalGastos   DECIMAL(10,2);
    DECLARE @Diferencia    DECIMAL(10,2);

    -- 1. Obtener datos base
    SELECT 
        @IdUsuario     = IdUsuario,
        @FechaApertura = FechaApertura,
        @MontoInicial  = MontoInicial
    FROM CajaTurno
    WHERE IdCajaTurno = @IdCajaTurno;

    IF (@IdUsuario IS NULL)
    BEGIN
        SET @Mensaje = 'Caja no encontrada.';
        RETURN;
    END

    -- 2. Calcular ventas del turno (necesito tu confirmación para personalizar)
    /*
    SELECT @TotalVentas = ISNULL(SUM(TotalPagar), 0)
    FROM Venta
    WHERE IdUsuario = @IdUsuario
      AND FechaRegistro >= @FechaApertura
      AND FechaRegistro <= GETDATE();
    */

    SET @TotalVentas = 0; -- temporal

    -- 3. Calcular gastos del turno
    /*
    SELECT @TotalGastos = ISNULL(SUM(Monto), 0)
    FROM Gasto
    WHERE IdUsuario = @IdUsuario
      AND FechaRegistro >= @FechaApertura
      AND FechaRegistro <= GETDATE();
    */

    SET @TotalGastos = 0; -- temporal

    -- 4. Calcular diferencia
    DECLARE @Teorico DECIMAL(10,2);
    SET @Teorico = @MontoInicial + @TotalVentas - @TotalGastos;

    SET @Diferencia = @MontoFinal - @Teorico;

    -- 5. Actualizar caja
    UPDATE CajaTurno
    SET 
        MontoFinal  = @MontoFinal,
        TotalVentas = @TotalVentas,
        TotalGastos = @TotalGastos,
        Diferencia  = @Diferencia,
        Observacion = @Observacion,
        FechaCierre = GETDATE(),
        Estado = 0
    WHERE IdCajaTurno = @IdCajaTurno;

    SET @Mensaje = 'Caja cerrada correctamente.';
END;
GO

/* ==========================================
   SP: MODULO DE GASTOS
   ========================================== */


CREATE PROCEDURE SP_REGISTRAR_GASTO
(
    @IdCategoriaGasto INT,
    @IdUsuario INT,
    @IdCajaTurno INT = NULL,   -- Cajero envía turno / Admin envía NULL
    @Monto DECIMAL(10,2),
    @Descripcion VARCHAR(250),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Gasto
        (IdCategoriaGasto, IdUsuario, IdCajaTurno, Monto, Descripcion, FechaRegistro, Estado)
        VALUES
        (@IdCategoriaGasto, @IdUsuario, @IdCajaTurno, @Monto, @Descripcion, GETDATE(), 1);

        SET @Resultado = SCOPE_IDENTITY(); -- Devolver Id insertado
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH

END;

GO

CREATE PROCEDURE SP_EDITAR_GASTO
(
    @IdGasto INT,
    @IdCategoriaGasto INT,
    @Monto DECIMAL(10,2),
    @Descripcion VARCHAR(250),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Gasto
        SET 
            IdCategoriaGasto = @IdCategoriaGasto,
            Monto = @Monto,
            Descripcion = @Descripcion
        WHERE IdGasto = @IdGasto;

        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH

END;

GO

CREATE PROCEDURE SP_CAMBIAR_ESTADO_GASTO
(
    @IdGasto INT,
    @Estado BIT,
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        UPDATE Gasto
        SET Estado = @Estado
        WHERE IdGasto = @IdGasto;

        SET @Resultado = 1;
    END TRY
    BEGIN CATCH
        SET @Resultado = 0;
    END CATCH

END;

GO

CREATE PROCEDURE SP_LISTAR_GASTOS_ADMIN
AS
BEGIN
    SELECT 
        G.IdGasto,
        G.IdCategoriaGasto,
        C.Descripcion AS Categoria,
        G.IdUsuario,
        U.NombreCompleto AS Usuario,
        R.Descripcion AS RolDescripcion,
        R.IdRol,                           -- ← AGREGADO
        G.IdCajaTurno,
        G.Monto,
        G.Descripcion,
        G.FechaRegistro,
        G.Estado
    FROM Gasto G
    INNER JOIN CategoriaGasto C ON G.IdCategoriaGasto = C.IdCategoriaGasto
    INNER JOIN Usuario U ON G.IdUsuario = U.IdUsuario
    INNER JOIN Rol R ON U.IdRol = R.IdRol      -- ← necesario para IdRol
    ORDER BY G.FechaRegistro DESC;
END
GO


GO

CREATE PROCEDURE SP_LISTAR_CATEGORIA_GASTO
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdCategoriaGasto,
        Descripcion,
        Estado
    FROM CategoriaGasto
    WHERE Estado = 1
    ORDER BY Descripcion;
END;
GO

CREATE PROCEDURE SP_LISTAR_GASTOS_CAJERO
(
    @IdCajaTurno INT
)
AS
BEGIN
    SELECT 
        G.IdGasto,
        G.IdCategoriaGasto,
        C.Descripcion AS Categoria,
        G.IdUsuario,
        U.NombreCompleto AS Usuario,
        R.Descripcion AS RolDescripcion,
        R.IdRol,                          -- ← AGREGADO
        G.IdCajaTurno,
        G.Monto,
        G.Descripcion,
        G.FechaRegistro,
        G.Estado
    FROM Gasto G
    INNER JOIN CategoriaGasto C ON G.IdCategoriaGasto = C.IdCategoriaGasto
    INNER JOIN Usuario U ON G.IdUsuario = U.IdUsuario
    INNER JOIN Rol R ON U.IdRol = R.IdRol         -- ← AGREGADO
    WHERE G.IdCajaTurno = @IdCajaTurno
    ORDER BY G.FechaRegistro DESC;
END
GO



CREATE PROCEDURE SP_FILTRAR_GASTOS_ADMIN
(
    @Descripcion NVARCHAR(200) = NULL,
    @IdCategoriaGasto INT = 0,
    @IdRol INT = 0,
    @FechaDesde DATETIME = NULL,
    @FechaHasta DATETIME = NULL
)
AS
BEGIN
    SELECT 
        G.IdGasto,
        G.IdCategoriaGasto,
        C.Descripcion AS Categoria,
        G.IdUsuario,
        U.NombreCompleto AS Usuario,
        R.Descripcion AS RolDescripcion,
        R.IdRol,                        -- ← AGREGADO
        G.IdCajaTurno,
        G.Monto,
        G.Descripcion,
        G.FechaRegistro,
        G.Estado
    FROM Gasto G
    INNER JOIN CategoriaGasto C ON G.IdCategoriaGasto = C.IdCategoriaGasto
    INNER JOIN Usuario U ON G.IdUsuario = U.IdUsuario
    INNER JOIN Rol R ON U.IdRol = R.IdRol
    WHERE 
        (@Descripcion IS NULL OR G.Descripcion LIKE '%' + @Descripcion + '%')
        AND (@IdCategoriaGasto = 0 OR G.IdCategoriaGasto = @IdCategoriaGasto)
        AND (@IdRol = 0 OR R.IdRol = @IdRol)
        AND (@FechaDesde IS NULL OR G.FechaRegistro >= @FechaDesde)
        AND (@FechaHasta IS NULL OR G.FechaRegistro <= @FechaHasta)
    ORDER BY G.FechaRegistro DESC;
END
GO

/* ==========================================
   SP: MODULO DE VENTAS
   ========================================== */

CREATE PROCEDURE SP_LISTAR_ENTRADASTIPO_ACTIVAS
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdEntradaTipo,
        Descripcion,
        PrecioBase,
        Estado
    FROM EntradaTipo
    WHERE Estado = 1;
END
GO


CREATE PROCEDURE SP_LISTAR_PRODUCTOS_ACTIVOS
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.IdProducto,
        P.Codigo,
        P.Nombre,
        P.Descripcion,
        P.IdCategoria,
        C.Descripcion AS Categoria,
        P.PrecioCompra,
        P.PrecioVenta,
        P.Stock,
        P.Estado
    FROM Producto P
    INNER JOIN Categoria C ON P.IdCategoria = C.IdCategoria
    WHERE P.Estado = 1;
END
GO


CREATE PROCEDURE SP_LISTAR_PRODUCTOS_POR_CATEGORIA
(
    @IdCategoria INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        P.IdProducto,
        P.Codigo,
        P.Nombre,
        P.Descripcion,
        P.IdCategoria,
        C.Descripcion AS Categoria,
        P.PrecioVenta,
        P.Stock,
        P.Estado
    FROM Producto P
    INNER JOIN Categoria C ON P.IdCategoria = C.IdCategoria
    WHERE 
        P.Estado = 1
        AND P.IdCategoria = @IdCategoria;
END
GO


CREATE PROCEDURE SP_OBTENER_PROMO_ACTIVA
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdPromocion,
        Estado,
        Categoria,
        UsuarioModifico,
        FechaActualizacion
    FROM Promocion
    WHERE Estado = 1; -- Solo la promo activa
END
GO

CREATE TYPE TVP_DetalleVentaEntrada AS TABLE
(
    IdEntradaTipo INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    PrecioAplicado DECIMAL(10,2),
    SubTotal DECIMAL(10,2)
)
GO

CREATE TYPE TVP_DetalleVentaProducto AS TABLE
(
    IdProducto INT,
    Cantidad INT,
    PrecioUnitario DECIMAL(10,2),
    SubTotal DECIMAL(10,2)
)
GO

CREATE PROCEDURE SP_REGISTRAR_VENTA_COMPLETA
(
    @IdUsuario INT,
    @IdCliente INT = NULL,
    @MetodoPago VARCHAR(20),
    @IdCajaTurno INT,
    @DetalleEntradas TVP_DetalleVentaEntrada READONLY,
    @DetalleProductos TVP_DetalleVentaProducto READONLY,
    @Resultado INT OUTPUT,
    @Mensaje VARCHAR(500) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE 
            @UltimoNumero INT,
            @CantidadDigitos INT,
            @Prefijo VARCHAR(10),
            @NuevoNumero INT,
            @NumeroVenta VARCHAR(50);

        -- Obtener correlativo activo
        SELECT 
            @UltimoNumero = UltimoNumero,
            @CantidadDigitos = CantidadDigitos,
            @Prefijo = Prefijo
        FROM Correlativo
        WHERE Estado = 1;

        -- Incrementar número
        SET @NuevoNumero = @UltimoNumero + 1;

        -- Generar código final: V00001, V00002, ...
        SET @NumeroVenta = @Prefijo +
            RIGHT(REPLICATE('0', @CantidadDigitos) + CAST(@NuevoNumero AS VARCHAR(10)), @CantidadDigitos);

        -- Insertar cabecera de venta
        INSERT INTO Venta(IdUsuario, IdCliente, NumeroVenta, MetodoPago, MontoTotal, FechaRegistro, IdCajaTurno)
        VALUES
        (
            @IdUsuario,
            @IdCliente,
            @NumeroVenta,
            @MetodoPago,
            (
                -- SUMA TOTAL (entradas + snacks)
                (SELECT ISNULL(SUM(SubTotal),0) FROM @DetalleEntradas) +
                (SELECT ISNULL(SUM(SubTotal),0) FROM @DetalleProductos)
            ),
            GETDATE(),
            @IdCajaTurno
        );

        DECLARE @IdVentaGenerada INT = SCOPE_IDENTITY();

        -- Insertar detalle de ENTRADAS
        INSERT INTO DetalleVentaEntrada(IdVenta, IdEntradaTipo, Cantidad, PrecioUnitario, PrecioAplicado, SubTotal)
        SELECT @IdVentaGenerada, IdEntradaTipo, Cantidad, PrecioUnitario, PrecioAplicado, SubTotal
        FROM @DetalleEntradas;

        -- Insertar detalle de SNACKS
        INSERT INTO DetalleVentaProducto(IdVenta, IdProducto, Cantidad, PrecioUnitario, SubTotal)
        SELECT @IdVentaGenerada, IdProducto, Cantidad, PrecioUnitario, SubTotal
        FROM @DetalleProductos;

        -- Actualizar STOCK por cada producto vendido
        UPDATE P
        SET P.Stock = P.Stock - D.Cantidad
        FROM Producto P
        INNER JOIN @DetalleProductos D ON P.IdProducto = D.IdProducto;

        -- Actualizar correlativo
        UPDATE Correlativo
        SET UltimoNumero = @NuevoNumero
        WHERE Estado = 1;

        -- Éxito
        SET @Resultado = @IdVentaGenerada;
        SET @Mensaje = @NumeroVenta; -- devolvemos el numero de venta

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO

EXEC SP_OBTENER_PROMO;
SELECT * FROM Producto WHERE Estado = 1;

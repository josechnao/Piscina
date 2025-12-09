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


CREATE PROCEDURE SP_LISTAR_CATEGORIAS_ACTIVAS
AS
BEGIN
    SELECT IdCategoria, Descripcion
    FROM Categoria
    WHERE Estado = 1
END


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
   SP: MODULO DE GASTO
   ========================================== */

CREATE PROCEDURE SP_REGISTRAR_GASTO
(
    @IdCategoriaGasto INT,
    @IdUsuario INT,
    @IdCajaTurno INT = NULL,
    @Monto DECIMAL(10,2),
    @Descripcion VARCHAR(200),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Gasto(IdCategoriaGasto, IdUsuario, IdCajaTurno, Monto, Descripcion)
    VALUES(@IdCategoriaGasto, @IdUsuario, @IdCajaTurno, @Monto, @Descripcion);

    SET @Resultado = SCOPE_IDENTITY();
END;
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
    ORDER BY Descripcion ASC;
END;
GO


CREATE PROCEDURE SP_EDITAR_GASTO
(
    @IdGasto INT,
    @IdCategoriaGasto INT,
    @Monto DECIMAL(10,2),
    @Descripcion VARCHAR(200),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Gasto
    SET IdCategoriaGasto = @IdCategoriaGasto,
        Monto = @Monto,
        Descripcion = @Descripcion
    WHERE IdGasto = @IdGasto;

    SET @Resultado = 1;
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

    UPDATE Gasto SET Estado = @Estado WHERE IdGasto = @IdGasto;

    SET @Resultado = 1;
END;
GO

CREATE PROCEDURE SP_LISTAR_GASTOS_CAJERO
(
    @IdCajaTurno INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        g.IdGasto,
        g.IdCategoriaGasto,
        cg.Descripcion AS Categoria,
        g.IdUsuario,
        u.NombreCompleto AS Usuario,
        r.Descripcion AS RolDescripcion,
        g.IdCajaTurno,
        g.Monto,
        g.Descripcion,
        g.FechaRegistro,
        g.Estado
    FROM Gasto g
    INNER JOIN CategoriaGasto cg ON g.IdCategoriaGasto = cg.IdCategoriaGasto
    INNER JOIN Usuario u ON g.IdUsuario = u.IdUsuario
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    WHERE g.IdCajaTurno = @IdCajaTurno
    ORDER BY g.FechaRegistro DESC;
END;
GO

CREATE PROCEDURE SP_LISTAR_GASTOS_ADMIN
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        g.IdGasto,
        g.IdCategoriaGasto,
        cg.Descripcion AS Categoria,
        g.IdUsuario,
        u.NombreCompleto AS Usuario,
        r.Descripcion AS RolDescripcion,
        g.IdCajaTurno,
        g.Monto,
        g.Descripcion,
        g.FechaRegistro,
        g.Estado
    FROM Gasto g
    INNER JOIN CategoriaGasto cg ON g.IdCategoriaGasto = cg.IdCategoriaGasto
    INNER JOIN Usuario u ON g.IdUsuario = u.IdUsuario
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    ORDER BY g.FechaRegistro DESC;
END;
GO

CREATE PROCEDURE SP_FILTRAR_GASTOS_CAJERO
(
    @IdCajaTurno INT,
    @Descripcion VARCHAR(200) = NULL,
    @IdCategoriaGasto INT = 0
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        g.IdGasto,
        g.IdCategoriaGasto,
        cg.Descripcion AS Categoria,
        g.IdUsuario,
        u.NombreCompleto AS Usuario,
        r.Descripcion AS RolDescripcion,
        g.IdCajaTurno,
        g.Monto,
        g.Descripcion,
        g.FechaRegistro,
        g.Estado
    FROM Gasto g
    INNER JOIN CategoriaGasto cg ON g.IdCategoriaGasto = cg.IdCategoriaGasto
    INNER JOIN Usuario u ON g.IdUsuario = u.IdUsuario
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    WHERE 
        g.IdCajaTurno = @IdCajaTurno
        AND (@Descripcion IS NULL OR g.Descripcion LIKE '%' + @Descripcion + '%')
        AND (@IdCategoriaGasto = 0 OR g.IdCategoriaGasto = @IdCategoriaGasto)
    ORDER BY g.FechaRegistro DESC;
END;
GO

ALTER PROCEDURE SP_LISTAR_GASTOS_ADMIN
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        g.IdGasto,
        g.IdCategoriaGasto,
        cg.Descripcion AS Categoria,
        g.IdUsuario,
        u.NombreCompleto AS Usuario,
        u.IdRol,                       -- ← AGREGADO
        r.Descripcion AS RolDescripcion,
        g.IdCajaTurno,
        g.Monto,
        g.Descripcion,
        g.FechaRegistro,
        g.Estado
    FROM Gasto g
    INNER JOIN CategoriaGasto cg ON g.IdCategoriaGasto = cg.IdCategoriaGasto
    INNER JOIN Usuario u ON g.IdUsuario = u.IdUsuario
    INNER JOIN Rol r ON u.IdRol = r.IdRol
    ORDER BY g.FechaRegistro DESC;
END;
GO

EXEC SP_LISTAR_GASTOS_ADMIN


/* ==========================================
   SP: MODULO DE CAJATURNO
   ========================================== */

CREATE PROCEDURE SP_VERIFICAR_CAJA_ABIERTA
(
    @IdUsuario INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        IdCajaTurno,
        MontoInicial,
        FechaApertura,
        Estado
    FROM CajaTurno
    WHERE IdUsuario = @IdUsuario
      AND Estado = 1
    ORDER BY IdCajaTurno DESC;
END;
GO

CREATE PROCEDURE SP_ABRIR_CAJA
(
    @IdUsuario INT,
    @MontoInicial DECIMAL(18,2),
    @Resultado INT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO CajaTurno(IdUsuario, MontoInicial)
    VALUES (@IdUsuario, @MontoInicial);

    SET @Resultado = SCOPE_IDENTITY();
END;
GO


CREATE PROCEDURE SP_RESUMEN_CAJA_TURNO
(
    @IdCajaTurno INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        -- Monto de apertura
        (SELECT MontoInicial 
         FROM CajaTurno 
         WHERE IdCajaTurno = @IdCajaTurno) AS MontoInicial,

        -- Ventas válidas del turno (sin cortesia)
        (SELECT ISNULL(SUM(MontoTotal), 0) 
         FROM Venta 
         WHERE IdCajaTurno = @IdCajaTurno
           AND MetodoPago <> 'CORTESIA') AS TotalVentas,

        -- Gastos del turno
        (SELECT ISNULL(SUM(Monto), 0) 
         FROM Gasto 
         WHERE IdCajaTurno = @IdCajaTurno) AS TotalGastos;
END;
GO



CREATE PROCEDURE SP_CERRAR_CAJA
(
    @IdCajaTurno INT,
    @MontoFinal DECIMAL(18,2),
    @TotalVentas DECIMAL(18,2),
    @TotalGastos DECIMAL(18,2),
    @Diferencia DECIMAL(18,2),
    @Observacion VARCHAR(250),
    @Resultado BIT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE CajaTurno SET
        FechaCierre = GETDATE(),
        Estado = 0,
        MontoFinal = @MontoFinal,
        TotalVentas = @TotalVentas,
        TotalGastos = @TotalGastos,
        Diferencia = @Diferencia,
        Observacion = @Observacion
    WHERE IdCajaTurno = @IdCajaTurno;

    SET @Resultado = 1;
END;
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
    SELECT  
        P.IdProducto,
        P.Nombre,
        P.Descripcion,
        P.PrecioVenta,
        P.Stock,
        P.IdCategoria,              -- 🔥 AGREGAR ESTO
        C.Descripcion AS Categoria  -- Texto
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

CREATE PROCEDURE SP_REGISTRAR_VENTA
(
    @IdUsuario          INT,
    @IdCajaTurno        INT = NULL,
    @DNI                VARCHAR(20),
    @NombreCompleto     VARCHAR(150),
    @Telefono           VARCHAR(20),
    @MetodoPago         VARCHAR(20),
    @MontoTotal         DECIMAL(10,2),
    @Detalle            XML,              -- XML con entradas y productos

    @Resultado          BIT OUTPUT,
    @Mensaje            VARCHAR(500) OUTPUT,
    @IdVentaGenerado    INT OUTPUT,
    @NumeroVentaGenerado VARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @Resultado = 0;
    SET @Mensaje = '';
    SET @IdVentaGenerado = 0;
    SET @NumeroVentaGenerado = '';

    BEGIN TRY
        BEGIN TRAN;

        ----------------------------------------------------------------
        -- 1) CLIENTE: buscar por DNI, si no existe se crea, si existe se actualiza
        ----------------------------------------------------------------
        DECLARE @IdCliente INT;

        SELECT @IdCliente = IdCliente
        FROM Cliente
        WHERE DNI = @DNI;

        IF @IdCliente IS NULL
        BEGIN
            INSERT INTO Cliente (DNI, NombreCompleto, Telefono)
            VALUES (@DNI, @NombreCompleto, @Telefono);

            SET @IdCliente = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            UPDATE Cliente
            SET NombreCompleto = @NombreCompleto,
                Telefono       = @Telefono
            WHERE IdCliente = @IdCliente;
        END

        ----------------------------------------------------------------
        -- 2) CORRELATIVO: obtener y armar NumeroVenta (TCK-000123, etc.)
        ----------------------------------------------------------------
        DECLARE @IdCorrelativo INT,
                @UltimoNumero INT,
                @CantidadDigitos INT,
                @Prefijo VARCHAR(10);

        SELECT TOP 1
            @IdCorrelativo   = IdCorrelativo,
            @UltimoNumero    = UltimoNumero,
            @CantidadDigitos = CantidadDigitos,
            @Prefijo         = Prefijo
        FROM Correlativo
        WHERE Estado = 1
        ORDER BY IdCorrelativo;

        IF @IdCorrelativo IS NULL
        BEGIN
            SET @Mensaje = 'No se encontró configuración de correlativo.';
            ROLLBACK TRAN;
            RETURN;
        END

        DECLARE @NuevoNumero INT = @UltimoNumero + 1;
        DECLARE @NumeroVenta VARCHAR(50);

        -- Ej: si CantidadDigitos = 6 => 000123
        SET @NumeroVenta = @Prefijo +
                           RIGHT(REPLICATE('0', @CantidadDigitos) + CAST(@NuevoNumero AS VARCHAR(20)),
                                 @CantidadDigitos);

        ----------------------------------------------------------------
        -- 3) VENTA
        ----------------------------------------------------------------

        -- Si es admin, permitirá registrar venta sin caja abierta
        IF @IdCajaTurno = 0
            SET @IdCajaTurno = NULL;

        INSERT INTO Venta
        (
            IdUsuario,
            IdCliente,
            NumeroVenta,
            MontoTotal,
            MetodoPago,
            IdCajaTurno
            -- FechaRegistro usa default GETDATE()
        )
        VALUES
        (
            @IdUsuario,
            @IdCliente,
            @NumeroVenta,
            @MontoTotal,
            @MetodoPago,
            @IdCajaTurno
        );

        DECLARE @IdVenta INT = SCOPE_IDENTITY();

        ----------------------------------------------------------------
        -- 4) DETALLE ENTRADAS
        --   <Entrada IdEntradaTipo="" Cantidad="" PrecioUnitario="" PrecioAplicado="" SubTotal="" EsPromo="0|1" />
        ----------------------------------------------------------------
        INSERT INTO DetalleVentaEntrada
        (
            IdVenta,
            IdEntradaTipo,
            Cantidad,
            PrecioUnitario,
            PrecioAplicado,
            SubTotal,
            EsPromo
        )
        SELECT
            @IdVenta,
            X.N.value('@IdEntradaTipo','int'),
            X.N.value('@Cantidad','int'),
            X.N.value('@PrecioUnitario','decimal(10,2)'),
            X.N.value('@PrecioAplicado','decimal(10,2)'),
            X.N.value('@SubTotal','decimal(10,2)'),
            X.N.value('@EsPromo','bit')
        FROM @Detalle.nodes('/Detalles/Entrada') AS X(N);

        ----------------------------------------------------------------
        -- 5) DETALLE PRODUCTOS
        --   <Producto IdProducto="" Cantidad="" PrecioUnitario="" SubTotal="" />
        ----------------------------------------------------------------
        INSERT INTO DetalleVentaProducto
        (
            IdVenta,
            IdProducto,
            Cantidad,
            PrecioUnitario,
            SubTotal
        )
        SELECT
            @IdVenta,
            X.N.value('@IdProducto','int'),
            X.N.value('@Cantidad','int'),
            X.N.value('@PrecioUnitario','decimal(10,2)'),
            X.N.value('@SubTotal','decimal(10,2)')
        FROM @Detalle.nodes('/Detalles/Producto') AS X(N);

        ----------------------------------------------------------------
        -- 6) ACTUALIZAR STOCK PRODUCTOS
        ----------------------------------------------------------------
        UPDATE P
        SET P.Stock = P.Stock - D.Cantidad
        FROM Producto P
        INNER JOIN DetalleVentaProducto D
            ON P.IdProducto = D.IdProducto
        WHERE D.IdVenta = @IdVenta;

        ----------------------------------------------------------------
        -- 7) ACTUALIZAR CORRELATIVO
        ----------------------------------------------------------------
        UPDATE Correlativo
        SET UltimoNumero = @NuevoNumero
        WHERE IdCorrelativo = @IdCorrelativo;

        ----------------------------------------------------------------
        -- FIN OK
        ----------------------------------------------------------------
        COMMIT TRAN;

        SET @Resultado = 1;
        SET @IdVentaGenerado = @IdVenta;
        SET @NumeroVentaGenerado = @NumeroVenta;
        SET @Mensaje = 'Venta registrada correctamente.';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRAN;

        SET @Resultado = 0;
        SET @Mensaje = ERROR_MESSAGE();
    END CATCH
END
GO



CREATE PROCEDURE SP_LISTAR_PERMISOS_POR_ROL
(
    @IdRol INT
)
AS
BEGIN
    SELECT P.NombreMenu, P.NombreFormulario
    FROM RolPermiso RP
    INNER JOIN Permiso P ON RP.IdPermiso = P.IdPermiso
    WHERE RP.IdRol = @IdRol;
END;
GO

-------------------------
----PROCEDIMIENTO PARA REPORTE VENTA
-------------------------

CREATE PROCEDURE SP_REPORTE_VENTAS_GENERAL
(
    @FechaDesde DATE,
    @FechaHasta DATE,
    @MetodoPago VARCHAR(20) = NULL  -- NULL = todos
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        v.IdVenta,
        v.NumeroVenta,
        v.FechaRegistro,
        u.NombreCompleto AS Cajero,
        v.MetodoPago,
        v.MontoTotal
    FROM Venta v
    INNER JOIN Usuario u ON v.IdUsuario = u.IdUsuario
    WHERE 
        CONVERT(DATE, v.FechaRegistro) >= @FechaDesde
        AND CONVERT(DATE, v.FechaRegistro) <= @FechaHasta
        AND (@MetodoPago IS NULL OR v.MetodoPago = @MetodoPago)
    ORDER BY v.FechaRegistro DESC;
END;
GO


ALTER PROCEDURE SP_REPORTE_VENTA_DETALLE
(
    @IdVenta INT
)
AS
BEGIN
    SET NOCOUNT ON;

    /* ============================================================
       1) ENCABEZADO — Venta, Cliente y Usuario
       ============================================================ */
    SELECT 
        v.IdVenta,
        v.NumeroVenta,
        v.FechaRegistro,
        v.MetodoPago,
        v.MontoTotal,

        c.NombreCompleto AS ClienteNombre,
        c.DNI AS ClienteDNI,
        c.Telefono AS ClienteTelefono,

        u.NombreCompleto AS Cajero
    FROM Venta v
    INNER JOIN Cliente c ON v.IdCliente = c.IdCliente
    INNER JOIN Usuario u ON v.IdUsuario = u.IdUsuario
    WHERE v.IdVenta = @IdVenta;


    /* ============================================================
       2) DETALLE (Entradas + Productos unificados)
       ============================================================ */

    SELECT *
    FROM
    (
        /* ----------------------
           Detalle de ENTRADAS
           ---------------------- */
        SELECT
            'Entrada' AS TipoItem,              -- Tipo genérico
            'Entrada' AS NombreItem,            -- Nombre fijo para columna "Producto/Entrada"
            et.Descripcion AS DescripcionItem,  -- Adulto / Adolescente / Niño / Bebé
            de.PrecioUnitario,
            de.Cantidad,
            de.SubTotal,
            v.MetodoPago
        FROM DetalleVentaEntrada de
        INNER JOIN EntradaTipo et ON de.IdEntradaTipo = et.IdEntradaTipo
        INNER JOIN Venta v ON de.IdVenta = v.IdVenta
        WHERE de.IdVenta = @IdVenta

        UNION ALL

        /* ----------------------
           Detalle de PRODUCTOS
           ---------------------- */
        SELECT
            'Producto' AS TipoItem,
            p.Nombre AS NombreItem,             -- Nombre comercial del producto
            p.Descripcion AS DescripcionItem,
            dp.PrecioUnitario,
            dp.Cantidad,
            dp.SubTotal,
            v.MetodoPago
        FROM DetalleVentaProducto dp
        INNER JOIN Producto p ON dp.IdProducto = p.IdProducto
        INNER JOIN Venta v ON dp.IdVenta = v.IdVenta
        WHERE dp.IdVenta = @IdVenta
    ) AS DetalleFinal
    ORDER BY NombreItem;
END;
GO

-------------------------
----PROCEDIMIENTO PARA REPORTE COMPRAS
-------------------------

CREATE PROCEDURE SP_REPORTE_LISTAR_PROVEEDORES
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IdProveedor,
        Nombre
    FROM Proveedor
    WHERE Estado = 1
    ORDER BY Nombre;
END;
GO

CREATE PROCEDURE SP_REPORTE_LISTAR_COMPRAS
(
    @FechaInicio DATE,
    @FechaFin DATE,
    @IdProveedor INT = 0,
    @DocumentoProveedor VARCHAR(20) = '',
    @NumeroDocumento VARCHAR(50) = '',
    @NumeroCorrelativo VARCHAR(20) = ''
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.IdCompra,
        c.NumeroCorrelativo,
        p.Nombre AS Proveedor,
        p.Documento AS DocumentoProveedor,
        c.NumeroDocumento,
        CONVERT(VARCHAR(19), c.FechaRegistro, 120) AS Fecha,
        c.MontoTotal AS TotalCompra,
        u.NombreCompleto AS UsuarioNombre
    FROM Compra c
    INNER JOIN Proveedor p ON c.IdProveedor = p.IdProveedor
    INNER JOIN Usuario u ON c.IdUsuario = u.IdUsuario
    WHERE
        CONVERT(DATE, c.FechaRegistro) BETWEEN @FechaInicio AND @FechaFin
        AND (@IdProveedor = 0 OR c.IdProveedor = @IdProveedor)
        AND (p.Documento LIKE '%' + @DocumentoProveedor + '%')
        AND (c.NumeroDocumento LIKE '%' + @NumeroDocumento + '%')
        AND (CAST(c.NumeroCorrelativo AS VARCHAR(20)) LIKE '%' + @NumeroCorrelativo + '%')
    ORDER BY c.FechaRegistro DESC;
END;
GO



CREATE PROCEDURE SP_REPORTE_DETALLE_COMPRA
(
    @IdCompra INT
)
AS
BEGIN
    SET NOCOUNT ON;

    -- =============================
    -- 1) ENCABEZADO DEL MODAL
    -- =============================
    SELECT
        p.Nombre AS ProveedorNombre,
        p.Documento AS DocumentoProveedor,
        p.Telefono AS TelefonoProveedor,
        c.NumeroCorrelativo,
        c.NumeroDocumento,
        CONVERT(VARCHAR(19), c.FechaRegistro, 120) AS Fecha,
        u.NombreCompleto AS UsuarioNombre,
        c.MontoTotal
    FROM Compra c
    INNER JOIN Proveedor p ON c.IdProveedor = p.IdProveedor
    INNER JOIN Usuario u ON c.IdUsuario = u.IdUsuario
    WHERE c.IdCompra = @IdCompra;


    -- =============================
    -- 2) DETALLE DE LA COMPRA
    -- =============================
    SELECT
        pr.Nombre AS Producto,
        pr.Descripcion,
        dc.Cantidad,
        dc.PrecioCompra,
        dc.PrecioVenta,
        dc.SubTotal
    FROM DetalleCompra dc
    INNER JOIN Producto pr ON dc.IdProducto = pr.IdProducto
    WHERE dc.IdCompra = @IdCompra;
END;
GO

-------------------------
----PROCEDIMIENTO PARA REPORTE CAJA TURNO
-------------------------

ALTER PROCEDURE SP_REPORTE_CAJATURNO_RESUMEN
(
    @FechaDesde DATE,
    @FechaHasta DATE,
    @IdUsuario INT = 0
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.NombreCompleto AS Cajero,
        ct.IdCajaTurno,
        ct.FechaApertura,
        ct.FechaCierre,
        ct.MontoInicial,
        ct.MontoFinal,

        COUNT(v.IdVenta) AS TotalVentas,

        SUM(CASE WHEN v.MetodoPago = 'CORTESIA' THEN 0 ELSE ISNULL(v.MontoTotal,0) END) AS VentasSumaTotal,

        COUNT(g.IdGasto) AS TotalGastos,

        SUM(CASE WHEN g.Estado = 1 THEN ISNULL(g.Monto,0) ELSE 0 END) AS GastoTotalSuma,

        CONCAT(
            'Efectivo: ', SUM(CASE WHEN v.MetodoPago = 'EFECTIVO' THEN 1 ELSE 0 END),
            ' | QR: ', SUM(CASE WHEN v.MetodoPago = 'QR' THEN 1 ELSE 0 END),
            ' | Cortesía: ', SUM(CASE WHEN v.MetodoPago = 'CORTESIA' THEN 1 ELSE 0 END)
        ) AS MetodoPagoResumen,

        /* Cálculo corregido */
        (
            ct.MontoFinal -
            (
                ct.MontoInicial +
                SUM(CASE WHEN v.MetodoPago = 'EFECTIVO' THEN ISNULL(v.MontoTotal,0) ELSE 0 END) -
                SUM(CASE WHEN g.Estado = 1 THEN ISNULL(g.Monto,0) ELSE 0 END)
            )
        ) AS Diferencia,

        ct.Observacion

    FROM CajaTurno ct
    INNER JOIN Usuario u ON u.IdUsuario = ct.IdUsuario
    LEFT JOIN Venta v ON v.IdCajaTurno = ct.IdCajaTurno
    LEFT JOIN Gasto g ON g.IdCajaTurno = ct.IdCajaTurno

    WHERE 
        CONVERT(DATE, ct.FechaApertura) BETWEEN @FechaDesde AND @FechaHasta
        AND (@IdUsuario = 0 OR ct.IdUsuario = @IdUsuario)

    GROUP BY u.NombreCompleto,
             ct.IdCajaTurno,
             ct.FechaApertura,
             ct.FechaCierre,
             ct.MontoInicial,
             ct.MontoFinal,
             ct.Observacion

    ORDER BY ct.FechaApertura DESC;
END
GO




ALTER PROCEDURE SP_REPORTE_CAJATURNO_DETALLE_TURNO
(
    @IdCajaTurno INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.NombreCompleto AS Cajero,
        ct.IdCajaTurno,
        ct.FechaApertura,
        ct.FechaCierre,
        ct.MontoInicial,
        ct.MontoFinal,

        /* Cantidad de ventas */
        COUNT(v.IdVenta) AS TotalVentas,

        /* Suma de ventas sin cortesía */
        SUM(
            CASE 
                WHEN v.MetodoPago = 'CORTESIA' THEN 0 
                ELSE ISNULL(v.MontoTotal,0) 
            END
        ) AS VentasSumaTotal,

        /* Número de gastos */
        COUNT(g.IdGasto) AS TotalGastos,

        /* Suma de gastos activos */
        SUM(
            CASE 
                WHEN g.Estado = 1 THEN ISNULL(g.Monto,0)
                ELSE 0
            END
        ) AS GastoTotalSuma,

        /* Resumen de métodos de pago */
        CONCAT(
            'Efectivo: ', SUM(CASE WHEN v.MetodoPago = 'EFECTIVO' THEN 1 ELSE 0 END),
            ' | QR: ', SUM(CASE WHEN v.MetodoPago = 'QR' THEN 1 ELSE 0 END),
            ' | Cortesía: ', SUM(CASE WHEN v.MetodoPago = 'CORTESIA' THEN 1 ELSE 0 END)
        ) AS MetodoPagoResumen,

        /* Cálculo corregido */
        (ct.MontoFinal - 
            (
                ct.MontoInicial +
                SUM(CASE WHEN v.MetodoPago = 'EFECTIVO' THEN ISNULL(v.MontoTotal,0) ELSE 0 END) -
                SUM(CASE WHEN g.Estado = 1 THEN ISNULL(g.Monto,0) ELSE 0 END)
            )
        ) AS Diferencia,

        ct.Observacion

    FROM CajaTurno ct
    INNER JOIN Usuario u ON u.IdUsuario = ct.IdUsuario
    LEFT JOIN Venta v ON v.IdCajaTurno = ct.IdCajaTurno
    LEFT JOIN Gasto g ON g.IdCajaTurno = ct.IdCajaTurno

    WHERE ct.IdCajaTurno = @IdCajaTurno

    GROUP BY u.NombreCompleto,
             ct.IdCajaTurno,
             ct.FechaApertura,
             ct.FechaCierre,
             ct.MontoInicial,
             ct.MontoFinal,
             ct.Observacion;
END
GO


CREATE PROCEDURE SP_REPORTE_CAJATURNO_VENTAS
(
    @IdCajaTurno INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.IdVenta,
        v.NumeroVenta AS NroTicket,
        v.MontoTotal,
        v.MetodoPago,
        v.FechaRegistro
    FROM Venta v
    WHERE v.IdCajaTurno = @IdCajaTurno
    ORDER BY v.FechaRegistro ASC;
END
GO



CREATE PROCEDURE SP_REPORTE_CAJATURNO_GASTOS
(
    @IdCajaTurno INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        cg.Descripcion AS Categoria,
        g.Descripcion,
        g.Monto,
        g.FechaRegistro,
        g.Estado
    FROM Gasto g
    INNER JOIN CategoriaGasto cg ON cg.IdCategoriaGasto = g.IdCategoriaGasto
    WHERE g.IdCajaTurno = @IdCajaTurno
    ORDER BY g.FechaRegistro ASC;
END
GO


SELECT IdCajaTurno, MontoInicial, MontoFinal,
       (MontoInicial + 
        (SELECT SUM(CASE WHEN MetodoPago = 'EFECTIVO' THEN MontoTotal ELSE 0 END) 
         FROM Venta WHERE IdCajaTurno = ct.IdCajaTurno) -
        (SELECT SUM(CASE WHEN Estado = 1 THEN Monto ELSE 0 END)
         FROM Gasto WHERE IdCajaTurno = ct.IdCajaTurno)
       ) AS SaldoCorrecto
FROM CajaTurno ct
ORDER BY IdCajaTurno;

-------------------------
----PROCEDIMIENTO PARA REPORTE GENERAL
-------------------------

CREATE PROCEDURE SP_RESUMEN_FINANCIERO_GENERAL
(
    @FechaDesde DATE,
    @FechaHasta DATE
)
AS
BEGIN
    SET NOCOUNT ON;

    /* ==========================================================
       1) INGRESOS TOTALES (EXCLUYE MÉTODO CORTESÍA)
       ========================================================== */
    SELECT 
        ISNULL(SUM(MontoTotal), 0) AS IngresosTotales
    INTO #Ingresos
    FROM Venta
    WHERE 
        CONVERT(DATE, FechaRegistro) BETWEEN @FechaDesde AND @FechaHasta
        AND MetodoPago <> 'CORTESIA';



    /* ==========================================================
       2) PÉRDIDAS POR CORTESÍAS
       (SUMA DE TODAS LAS VENTAS CON MÉTODO 'CORTESIA')
       ========================================================== */
    SELECT 
        ISNULL(SUM(MontoTotal), 0) AS PerdidasCortesias
    INTO #Perdidas
    FROM Venta
    WHERE 
        CONVERT(DATE, FechaRegistro) BETWEEN @FechaDesde AND @FechaHasta
        AND MetodoPago = 'CORTESIA';



    /* ==========================================================
       3) EGRESOS COMPRAS (NO TIENE ESTADO → SE SUMAN TODAS)
       ========================================================== */
    SELECT 
        ISNULL(SUM(MontoTotal), 0) AS EgresosCompras
    INTO #Compras
    FROM Compra
    WHERE 
        CONVERT(DATE, FechaRegistro) BETWEEN @FechaDesde AND @FechaHasta;



    /* ==========================================================
       4) EGRESOS GASTOS (SOLO ESTADO = 1)
       ========================================================== */
    SELECT 
        ISNULL(SUM(Monto), 0) AS EgresosGastos
    INTO #Gastos
    FROM Gasto
    WHERE 
        CONVERT(DATE, FechaRegistro) BETWEEN @FechaDesde AND @FechaHasta
        AND Estado = 1;



    /* ==========================================================
       5) RESULTADO FINAL EN UNA SOLA FILA
       ========================================================== */
    SELECT  
        (SELECT IngresosTotales     FROM #Ingresos)  AS IngresosTotales,
        (SELECT PerdidasCortesias   FROM #Perdidas)  AS PerdidasCortesias,
        (SELECT EgresosCompras      FROM #Compras)   AS EgresosCompras,
        (SELECT EgresosGastos       FROM #Gastos)    AS EgresosGastos;

END
GO


CREATE PROCEDURE SP_BUSCAR_CLIENTE_POR_DNI
(
    @DNI VARCHAR(20)
)
AS
BEGIN
    SELECT TOP 1 IdCliente, DNI, NombreCompleto, Telefono
    FROM Cliente
    WHERE DNI = @DNI;
END
GO

/* OPCIONAL: si quieres eliminar la BD primero, descomenta esto
IF DB_ID('PiscinaDB') IS NOT NULL
BEGIN
    ALTER DATABASE PiscinaDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE PiscinaDB;
END
GO
*/

CREATE DATABASE PiscinaDB;
GO

USE PiscinaDB;
GO

/* ===========================
   1. SEGURIDAD
   =========================== */

CREATE TABLE Rol (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Permiso (
    IdPermiso INT IDENTITY(1,1) PRIMARY KEY,
    NombreMenu VARCHAR(100) NOT NULL,
    NombreFormulario VARCHAR(100) NOT NULL
);
GO

CREATE TABLE RolPermiso (
    IdRolPermiso INT IDENTITY(1,1) PRIMARY KEY,
    IdRol INT NOT NULL,
    IdPermiso INT NOT NULL,
    CONSTRAINT FK_RolPermiso_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    CONSTRAINT FK_RolPermiso_Permiso FOREIGN KEY (IdPermiso) REFERENCES Permiso(IdPermiso)
);
GO

CREATE TABLE Usuario (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Documento VARCHAR(20) NOT NULL,
    NombreCompleto VARCHAR(150) NOT NULL,
    Clave VARCHAR(100) NOT NULL,
    IdRol INT NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Usuario_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol)
);
GO

/* ===========================
   2. CONFIGURACIÓN
   =========================== */

CREATE TABLE Negocio (
    IdNegocio INT IDENTITY(1,1) PRIMARY KEY,
    NombreNegocio VARCHAR(150) NOT NULL,
    Documento VARCHAR(20) NULL,
    Direccion VARCHAR(200) NULL,
    Telefono VARCHAR(20) NULL,
    RutaLogo VARCHAR(250) NULL
);
GO

CREATE TABLE Correlativo (
    IdCorrelativo INT IDENTITY(1,1) PRIMARY KEY,
    UltimoNumero INT NOT NULL,
    CantidadDigitos INT NOT NULL,
    Gestion INT NULL,
    Prefijo VARCHAR(10) NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

/* ===========================
   3. CLIENTES
   =========================== */

CREATE TABLE Cliente (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
    DNI VARCHAR(20) NOT NULL,
    NombreCompleto VARCHAR(150) NOT NULL,
    Telefono VARCHAR(20) NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
);
GO

/* ===========================
   4. ENTRADAS
   =========================== */

CREATE TABLE EntradaTipo (
    IdEntradaTipo INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,   -- Adulto / Niño / Mayor, etc.
    PrecioBase DECIMAL(10,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE EntradaPromocion (
    IdPromocion INT IDENTITY(1,1) PRIMARY KEY,
    IdEntradaTipo INT NOT NULL,
    DescripcionPromo VARCHAR(200) NOT NULL,
    TipoPromocion TINYINT NOT NULL,      -- 1=2x1, 2=%, 3=desc fijo, 4=precio especial
    Valor DECIMAL(10,2) NULL,            -- % o monto según TipoPromocion
    MaxCantidad INT NULL,                -- NULL = ilimitado
    FechaInicio DATETIME NOT NULL,
    FechaFin DATETIME NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_EntradaPromocion_EntradaTipo 
        FOREIGN KEY (IdEntradaTipo) REFERENCES EntradaTipo(IdEntradaTipo)
);
GO

/* ===========================
   5. INVENTARIO (SNACKS)
   =========================== */

CREATE TABLE Categoria (
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Producto (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(150) NOT NULL,
    IdCategoria INT NOT NULL,
    PrecioCompra DECIMAL(10,2) NOT NULL DEFAULT 0,
    PrecioVenta DECIMAL(10,2) NOT NULL DEFAULT 0,
    Stock INT NOT NULL DEFAULT 0,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Producto_Categoria FOREIGN KEY (IdCategoria) REFERENCES Categoria(IdCategoria)
);
GO

/* ===========================
   6. PROVEEDORES Y COMPRAS
   =========================== */

CREATE TABLE Proveedor (
    IdProveedor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(150) NOT NULL,
    Documento VARCHAR(20) NULL,
    Telefono VARCHAR(20) NULL,
    Correo VARCHAR(100) NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Compra (
    IdCompra INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdProveedor INT NOT NULL,
    NumeroDocumento VARCHAR(50) NULL,
    MontoTotal DECIMAL(10,2) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Compra_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Compra_Proveedor FOREIGN KEY (IdProveedor) REFERENCES Proveedor(IdProveedor)
);
GO

CREATE TABLE DetalleCompra (
    IdDetalleCompra INT IDENTITY(1,1) PRIMARY KEY,
    IdCompra INT NOT NULL,
    IdProducto INT NOT NULL,
    PrecioCompra DECIMAL(10,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Cantidad INT NOT NULL,
    SubTotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleCompra_Compra FOREIGN KEY (IdCompra) REFERENCES Compra(IdCompra),
    CONSTRAINT FK_DetalleCompra_Producto FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

/* ===========================
   7. CAJA POR TURNO
   =========================== */

CREATE TABLE CajaTurno (
    IdCajaTurno INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    MontoInicial DECIMAL(10,2) NOT NULL DEFAULT 0,
    MontoFinal DECIMAL(10,2) NULL,
    FechaApertura DATETIME NOT NULL DEFAULT GETDATE(),
    FechaCierre DATETIME NULL,
    Observacion VARCHAR(250) NULL,
    Estado BIT NOT NULL DEFAULT 1,   -- 1 = Abierto, 0 = Cerrado (o al revés, como definas)
    CONSTRAINT FK_CajaTurno_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO

/* ===========================
   8. VENTAS
   =========================== */

CREATE TABLE Venta (
    IdVenta INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdCliente INT NULL,              -- puede ser NULL si no se desea registrar cliente
    NumeroVenta VARCHAR(50) NOT NULL,
    MontoTotal DECIMAL(10,2) NOT NULL,
    MetodoPago VARCHAR(20) NOT NULL, -- 'EFECTIVO', 'QR', etc.
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    IdCajaTurno INT NOT NULL,
    CONSTRAINT FK_Venta_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_Venta_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente),
    CONSTRAINT FK_Venta_CajaTurno FOREIGN KEY (IdCajaTurno) REFERENCES CajaTurno(IdCajaTurno)
);
GO

CREATE TABLE DetalleVentaEntrada (
    IdDetalleEntrada INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdEntradaTipo INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,  -- precio normal
    PrecioAplicado DECIMAL(10,2) NOT NULL,  -- precio luego de promo (si aplica)
    SubTotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVentaEntrada_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    CONSTRAINT FK_DetalleVentaEntrada_EntradaTipo FOREIGN KEY (IdEntradaTipo) REFERENCES EntradaTipo(IdEntradaTipo)
);
GO

CREATE TABLE DetalleVentaProducto (
    IdDetalleProducto INT IDENTITY(1,1) PRIMARY KEY,
    IdVenta INT NOT NULL,
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    SubTotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_DetalleVentaProducto_Venta FOREIGN KEY (IdVenta) REFERENCES Venta(IdVenta),
    CONSTRAINT FK_DetalleVentaProducto_Producto FOREIGN KEY (IdProducto) REFERENCES Producto(IdProducto)
);
GO

/* ===========================
   9. GASTOS
   =========================== */

CREATE TABLE CategoriaGasto (
    IdCategoriaGasto INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Gasto (
    IdGasto INT IDENTITY(1,1) PRIMARY KEY,
    IdCategoriaGasto INT NOT NULL,
    Descripcion VARCHAR(200) NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario INT NOT NULL,
    CONSTRAINT FK_Gasto_CategoriaGasto FOREIGN KEY (IdCategoriaGasto) REFERENCES CategoriaGasto(IdCategoriaGasto),
    CONSTRAINT FK_Gasto_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);
GO

USE PiscinaDB;
GO

/* ===========================
   1) ROLES
   =========================== */
INSERT INTO Rol (Descripcion) VALUES ('Administrador');
INSERT INTO Rol (Descripcion) VALUES ('Cajero');
GO


/* ===========================
   2) CORRELATIVO DE VENTA
   =========================== */
INSERT INTO Correlativo (UltimoNumero, CantidadDigitos, Gestion, Prefijo, Estado)
VALUES (0, 6, YEAR(GETDATE()), 'TCK-', 1);
GO


/* ===========================
   3) USUARIO ADMINISTRADOR
   =========================== */
INSERT INTO Usuario (Documento, NombreCompleto, Clave, IdRol)
VALUES ('1', 'Administrador General', '1', 1);
GO


INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Bebidas', 1),
       ('Postres', 1);
GO


/* ===========================
   4) TIPOS DE ENTRADA
   =========================== */
INSERT INTO EntradaTipo (Descripcion, PrecioBase, Estado)
VALUES 
('Adulto', 20, 1),
('Adolescente', 15, 1),
('Niño', 10, 1),
('Bebé', 0, 1);
GO


/* ===========================
   5) PROMOCIÓN (PLANTILLA)
   =========================== */
INSERT INTO Promocion (Estado, Categoria, UsuarioModifico)
VALUES (0, 'Adulto', 1);
GO


/* ===========================
   6) CATEGORÍAS DE GASTO
   =========================== */
INSERT INTO CategoriaGasto (Descripcion, Estado)
VALUES
('Servicios Básicos', 1),
('Limpieza y Mantenimiento', 1),
('Compra de Útiles', 1),
('Comida del Personal', 1),
('Reparaciones', 1),
('Transporte', 1),
('Emergencias', 1),
('Otros', 1);
GO


/* ===========================
   7) NEGOCIO (DATOS GENERALES)
   =========================== */
INSERT INTO Negocio (IdNegocio, NombreNegocio, Direccion, Ciudad, Telefono, Logo)
VALUES (1, 'Agua Vida', 'Barrio Mariscal', 'San Julian', '00000000', NULL);
GO


/* ===========================
   8) PERMISOS
   =========================== */

-- Permisos disponibles
INSERT INTO Permiso (NombreMenu, NombreFormulario)
VALUES
('Ventas', 'frmVentas'),
('Gastos', 'frmGastos'),
('Reportes', 'frmReportes'),
('Compras', 'frmCompras'),
('Usuarios', 'frmUsuarios'),
('Productos', 'frmProductos'),
('Proveedores', 'frmProveedores'),
('Mantenedor', 'frmMantenedor'),
('EntradasPromo', 'frmEntradaPromo');
GO


/* ===========================
   RELACIÓN ROLES → PERMISOS
   =========================== */

DECLARE @idCajero INT = (SELECT IdRol FROM Rol WHERE Descripcion = 'Cajero');
DECLARE @idAdmin INT = (SELECT IdRol FROM Rol WHERE Descripcion = 'Administrador');

-- Permisos del CAJERO
INSERT INTO RolPermiso (IdRol, IdPermiso)
SELECT @idCajero, IdPermiso
FROM Permiso
WHERE NombreMenu IN ('Ventas', 'Gastos');

-- Todos los permisos para ADMIN
INSERT INTO RolPermiso (IdRol, IdPermiso)
SELECT @idAdmin, IdPermiso
FROM Permiso;
GO

USE PiscinaDB;
GO

----------------------------------------------------------------
----1) INSERTAMOS ROLES
----------------------------------------------------------------
INSERT INTO Rol (Descripcion) VALUES ('Administrador');
INSERT INTO Rol (Descripcion) VALUES ('Cajero');

----------------------------------------------------------------
----1) INSERTAMOS PLANTILLA PARA CORRELATIVO VENTA
----------------------------------------------------------------
INSERT INTO Correlativo (UltimoNumero, CantidadDigitos, Gestion, Prefijo, Estado)
VALUES (0, 6, YEAR(GETDATE()), 'TCK-', 1);


----------------------------------------------------------------
----1) INSERTAMOS USUARIOS
----------------------------------------------------------------
INSERT INTO Usuario (Documento, NombreCompleto, Clave,IdRol)
VALUES ('1', 'Administrador General', '1',1);


----------------------------------------------------------------
----1) INSERTAMOS ENTRADAS
----------------------------------------------------------------
INSERT INTO EntradaTipo (Descripcion, PrecioBase, Estado)
VALUES 
('Adulto', 20, 1),
('Adolescente', 15, 1),
('Niño', 10, 1),
('Bebé', 0, 1);


----------------------------------------------------------------
----1) INSERTAMOS PROMOCION
----------------------------------------------------------------
INSERT INTO Promocion (Estado, Categoria, UsuarioModifico)
VALUES (0, 'Adulto', 1);  



----------------------------------------------------------------
----1) INSERTAMOS CATEGORIA
----------------------------------------------------------------
INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Bebidas', 1);

INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Postres', 1);


INSERT INTO CategoriaGasto (Descripcion, Estado) VALUES
('Servicios Básicos', 1),
('Limpieza y Mantenimiento', 1),
('Compra de Útiles', 1),
('Comida del Personal', 1),
('Reparaciones', 1),
('Transporte', 1),
('Emergencias', 1),
('Otros', 1);
select * from CategoriaGasto;
----------------------------------------------------------------
----1) INSERTAMOS NEGOCIO
----------------------------------------------------------------
INSERT INTO Negocio (IdNegocio, NombreNegocio, Direccion, Ciudad, Telefono, Logo)
VALUES (1, 'Agua Vida', 'Barrio Mariscal', 'San Julian', '00000000', NULL);

----------------------------------------------------------------
----1) INSERTAMOS PERMISOS
----------------------------------------------------------------

INSERT INTO Permiso (NombreMenu, NombreFormulario)
VALUES
('Ventas', 'frmVentas'),
('Gastos', 'frmGastos'),
('Reportes', 'frmReportes'),
('Compras', 'frmCompras'),
('Usuarios', 'frmUsuarios'),
('Productos', 'frmProductos'),
('Proveedores', 'frmProveedores'),
('Mantenedor', 'frmMantenedor');

INSERT INTO Permiso (NombreMenu, NombreFormulario)
VALUES ('EntradasPromo', 'frmEntradaPromo');


DECLARE @idCajero INT = (SELECT IdRol FROM Rol WHERE Descripcion = 'Cajero');

INSERT INTO RolPermiso (IdRol, IdPermiso)
SELECT @idCajero, IdPermiso
FROM Permiso
WHERE NombreMenu IN ('Ventas', 'Gastos');


DECLARE @idAdmin INT = (SELECT IdRol FROM Rol WHERE Descripcion = 'Administrador');

INSERT INTO RolPermiso (IdRol, IdPermiso)
SELECT @idAdmin, IdPermiso FROM Permiso;



DECLARE @idAdmin INT = (SELECT IdRol FROM Rol WHERE Descripcion = 'Administrador');

INSERT INTO RolPermiso (IdRol, IdPermiso)
SELECT @idAdmin, IdPermiso FROM Permiso;




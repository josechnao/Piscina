USE PiscinaDB;
GO

select * from Promocion;
SELECT * FROM Producto WHERE Estado = 1;
exec SP_LISTAR_PRODUCTOS_ACTIVOS;


INSERT INTO Rol (Descripcion) VALUES ('Administrador');
INSERT INTO Rol (Descripcion) VALUES ('Cajero');

INSERT INTO Usuario (Documento, NombreCompleto, Clave,IdRol)
VALUES ('1', 'Administrador General', '1',1);

INSERT INTO Promocion (Estado, Categoria, UsuarioModifico)
VALUES (0, 'Adulto', 1);   -- 1 = IdUsuario admin o cualquiera que tengas

INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Bebidas', 1);

INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Postres', 1);

INSERT INTO Proveedor (Nombre, Documento, Telefono, Correo, Estado)
VALUES ('Coca Cola Bolivia', '12345678', '70010010', 'contacto@cocacola.com', 1);

INSERT INTO Proveedor (Nombre, Documento, Telefono, Correo, Estado)
VALUES ('Embotelladora La Plazuela', '87654321', '72030303', 'ventas@laplazuela.com', 1);

INSERT INTO Proveedor (Nombre, Documento, Telefono, Correo, Estado)
VALUES ('Distribuidora Santa Fe', '99887766', '76050505', 'contacto@santafe.com', 1);

INSERT INTO Producto (Codigo, Nombre, Descripcion, IdCategoria, Estado)
VALUES ('P001', 'Coca Cola 600ml', 'Bebida gaseosa', 1, 1);

INSERT INTO EntradaTipo (Descripcion, PrecioBase, Estado)
VALUES 
('Adulto', 20, 1),
('Adolescente', 15, 1),
('Niño', 10, 1),
('Bebé', 0, 1);

INSERT INTO Cliente (DNI, NombreCompleto, Telefono)
VALUES 
('123', 'Carlos Mendoza', '70012345'),
('98765432', 'María López', '76543210'),
('55667788', 'Jorge Ramírez', '78965412');


INSERT INTO Negocio (IdNegocio, NombreNegocio, Direccion, Ciudad, Telefono, Logo)
VALUES (1, 'Agua Vida', 'Barrio Mariscal', 'San Julian', '00000000', NULL);


INSERT INTO CategoriaGasto (Descripcion, Estado) VALUES
('Servicios Básicos', 1),
('Limpieza y Mantenimiento', 1),
('Compra de Útiles', 1),
('Comida del Personal', 1),
('Reparaciones', 1),
('Transporte', 1),
('Emergencias', 1),
('Otros', 1);

USE PiscinaDB;
GO


INSERT INTO Rol (Descripcion) VALUES ('Administrador');

INSERT INTO Usuario (Documento, NombreCompleto, Clave,IdRol)
VALUES ('1', 'Administrador General', '1',1);

select * from Producto;

INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Bebidas', 1);

INSERT INTO Categoria (Descripcion, Estado)
VALUES ('Postres', 1);

INSERT INTO Producto (Codigo, Nombre, Descripcion, IdCategoria, Estado)
VALUES ('P001', 'Coca Cola 600ml', 'Bebida gaseosa', 1, 1);

INSERT INTO EntradaTipo (Descripcion, PrecioBase, Estado)
VALUES 
('Adulto', 20, 1),
('Adolescente', 15, 1),
('Niño', 10, 1),
('Bebé', 0, 1);

SELECT * FROM EntradaTipo;



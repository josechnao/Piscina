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

USE PiscinaDB;
GO


INSERT INTO Rol (Descripcion) VALUES ('Administrador');

INSERT INTO Usuario (Documento, NombreCompleto, Clave,IdRol)
VALUES ('1', 'Administrador General', '1',1);

select * from Rol;
select * from Usuario;
EXEC SP_LOGIN '1', '1';   -- usa un documento y clave válidos

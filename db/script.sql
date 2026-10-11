IF DB_ID('Lab7') IS NULL CREATE DATABASE Lab7;
GO
USE Lab7;
GO
IF OBJECT_ID('Persona') IS NULL
CREATE TABLE Persona(
  IdPersona INT IDENTITY PRIMARY KEY,
  Nombre    NVARCHAR(50)  NOT NULL,
  Apellido  NVARCHAR(50)  NOT NULL,
  Dni       CHAR(8)       NOT NULL,
  Email     NVARCHAR(100) NOT NULL,
  Telefono  NVARCHAR(15)  NOT NULL,
  Estado    BIT NOT NULL DEFAULT 1);
IF OBJECT_ID('Producto') IS NULL
CREATE TABLE Producto(
  IdProducto INT IDENTITY PRIMARY KEY,
  Nombre     NVARCHAR(100) NOT NULL,
  Precio     DECIMAL(10,2) NOT NULL,
  Stock      INT NOT NULL,
  Estado     BIT NOT NULL DEFAULT 1);
GO

IF NOT EXISTS (SELECT 1 FROM Persona)
INSERT INTO Persona(Nombre,Apellido,Dni,Email,Telefono) VALUES
 (N'Juan',N'Pérez','45678901',N'juan.perez@mail.com',N'987654321'),
 (N'María',N'García','45678902',N'maria.garcia@mail.com',N'987654322'),
 (N'Luis',N'Torres','45678903',N'luis.torres@mail.com',N'987654323'),
 (N'Ana',N'Quispe','45678904',N'ana.quispe@mail.com',N'987654324'),
 (N'Carlos',N'Ramos','45678905',N'carlos.ramos@mail.com',N'987654325'),
 (N'Lucía',N'Flores','45678906',N'lucia.flores@mail.com',N'987654326');
IF NOT EXISTS (SELECT 1 FROM Producto)
INSERT INTO Producto(Nombre,Precio,Stock) VALUES
 (N'Laptop',2500.00,10),(N'Mouse inalámbrico',45.50,50),(N'Teclado mecánico',180.00,25),
 (N'Monitor 24"',650.00,15),(N'Audífonos',120.90,30),(N'Disco SSD 1TB',320.00,20);
GO

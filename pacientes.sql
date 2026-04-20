-- ============================================================
--  EstadisticaInferencial — Script de base de datos
--  SQL Server  |  40 pacientes  |  incluye columna Genero
-- ============================================================

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EstadisticaInferencial')
BEGIN
    CREATE DATABASE EstadisticaInferencial;
END
GO

USE EstadisticaInferencial;
GO

-- Crear la tabla si no existe
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Pacientes')
BEGIN
    CREATE TABLE Pacientes (
        Id                  INT           NOT NULL PRIMARY KEY,
        Nombre              VARCHAR(100)  NOT NULL,
        Genero              CHAR(1)       NULL,        -- 'M' = Hombre, 'F' = Mujer
        Edad                INT           NULL,
        PesoKg              DECIMAL(5,2)  NULL,
        EstaturaM           DECIMAL(4,2)  NULL,
        Imc                 DECIMAL(5,2)  NULL,
        TglMgDl             INT           NULL,
        PresionSistolica    INT           NULL,
        PresionDiastolica   INT           NULL,
        Padecimientos       VARCHAR(200)  NULL,
        Fecha               DATE          NULL
    );
END
GO

-- Insertar datos (solo si la tabla está vacía)
IF NOT EXISTS (SELECT TOP 1 1 FROM Pacientes)
BEGIN
    INSERT INTO Pacientes (Id, Nombre, Genero, Edad, PesoKg, EstaturaM, Imc, TglMgDl, PresionSistolica, PresionDiastolica, Padecimientos, Fecha) VALUES
        (1,  'Juan Perez',        'M', 45, 82.00, 1.75, 26.78, 180, 130, 85, 'Hipertension',    '2026-03-01'),
        (2,  'Maria Lopez',       'F', 34, 65.00, 1.62, 24.77, 140, 120, 80, 'Ninguno',         '2026-03-01'),
        (3,  'Carlos Ramirez',    'M', 50, 90.00, 1.70, 31.14, 210, 140, 90, 'Diabetes',        '2026-03-02'),
        (4,  'Ana Gomez',         'F', 29, 58.00, 1.60, 22.66, 120, 110, 70, 'Ninguno',         '2026-03-02'),
        (5,  'Luis Fernandez',    'M', 60, 85.00, 1.68, 30.12, 200, 150, 95, 'Hipertension',    '2026-03-03'),
        (6,  'Sofia Morales',     'F', 41, 70.00, 1.65, 25.71, 160, 125, 82, 'Colesterol alto', '2026-03-03'),
        (7,  'Pedro Castillo',    'M', 38, 78.00, 1.72, 26.37, 150, 118, 76, 'Ninguno',         '2026-03-04'),
        (8,  'Laura Vargas',      'F', 47, 74.00, 1.66, 26.85, 190, 135, 88, 'Hipertension',    '2026-03-04'),
        (9,  'Diego Herrera',     'M', 33, 92.00, 1.80, 28.40, 170, 128, 84, 'Obesidad',        '2026-03-05'),
        (10, 'Elena Rojas',       'F', 55, 68.00, 1.58, 27.24, 210, 145, 92, 'Diabetes',        '2026-03-05'),
        (11, 'Jose Castro',       'M', 44, 80.00, 1.73, 26.73, 175, 130, 85, 'Hipertension',    '2026-03-06'),
        (12, 'Patricia Solis',    'F', 36, 62.00, 1.61, 23.92, 135, 118, 75, 'Ninguno',         '2026-03-06'),
        (13, 'Ricardo Mendez',    'M', 52, 88.00, 1.69, 30.81, 220, 142, 90, 'Diabetes',        '2026-03-07'),
        (14, 'Daniela Cruz',      'F', 27, 55.00, 1.59, 21.76, 115, 108, 70, 'Ninguno',         '2026-03-07'),
        (15, 'Marco Jimenez',     'M', 48, 84.00, 1.74, 27.74, 185, 134, 86, 'Hipertension',    '2026-03-08'),
        (16, 'Andrea Navarro',    'F', 31, 60.00, 1.63, 22.58, 140, 116, 74, 'Ninguno',         '2026-03-08'),
        (17, 'Rafael Vega',       'M', 57, 91.00, 1.71, 31.12, 230, 150, 96, 'Diabetes',        '2026-03-09'),
        (18, 'Lucia Herrera',     'F', 42, 67.00, 1.64, 24.91, 165, 124, 82, 'Colesterol alto', '2026-03-09'),
        (19, 'Gabriel Pineda',    'M', 39, 76.00, 1.70, 26.30, 155, 120, 78, 'Ninguno',         '2026-03-10'),
        (20, 'Valeria Campos',    'F', 46, 73.00, 1.67, 26.18, 195, 138, 88, 'Hipertension',    '2026-03-10'),
        (21, 'Andres Molina',     'M', 35, 89.00, 1.82, 26.87, 168, 126, 83, 'Obesidad',        '2026-03-11'),
        (22, 'Natalia Salas',     'F', 53, 69.00, 1.60, 26.95, 205, 144, 91, 'Diabetes',        '2026-03-11'),
        (23, 'Miguel Araya',      'M', 40, 81.00, 1.74, 26.75, 172, 129, 84, 'Hipertension',    '2026-03-12'),
        (24, 'Paula Quesada',     'F', 28, 57.00, 1.58, 22.83, 118, 110, 72, 'Ninguno',         '2026-03-12'),
        (25, 'Esteban Rojas',     'M', 62, 86.00, 1.69, 30.11, 210, 148, 94, 'Hipertension',    '2026-03-13'),
        (26, 'Camila Mora',       'F', 33, 64.00, 1.63, 24.09, 142, 117, 76, 'Ninguno',         '2026-03-13'),
        (27, 'Hector Salazar',    'M', 51, 93.00, 1.72, 31.44, 225, 152, 97, 'Diabetes',        '2026-03-14'),
        (28, 'Monica Brenes',     'F', 45, 71.00, 1.65, 26.08, 176, 130, 85, 'Colesterol alto', '2026-03-14'),
        (29, 'Adrian Soto',       'M', 37, 79.00, 1.73, 26.40, 158, 122, 80, 'Ninguno',         '2026-03-15'),
        (30, 'Veronica Chaves',   'F', 49, 75.00, 1.66, 27.22, 198, 136, 89, 'Hipertension',    '2026-03-15'),
        (31, 'Cristian Vargas',   'M', 34, 91.00, 1.81, 27.78, 169, 127, 83, 'Obesidad',        '2026-03-16'),
        (32, 'Daniel Rojas',      'M', 54, 70.00, 1.59, 27.69, 207, 146, 92, 'Diabetes',        '2026-03-16'),
        (33, 'Oscar Delgado',     'M', 41, 83.00, 1.75, 27.10, 174, 131, 86, 'Hipertension',    '2026-03-17'),
        (34, 'Silvia Murillo',    'F', 30, 59.00, 1.62, 22.48, 139, 115, 74, 'Ninguno',         '2026-03-17'),
        (35, 'Julio Acosta',      'M', 63, 87.00, 1.70, 30.10, 215, 149, 95, 'Hipertension',    '2026-03-18'),
        (36, 'Paola Fonseca',     'F', 32, 63.00, 1.61, 24.30, 141, 118, 76, 'Ninguno',         '2026-03-18'),
        (37, 'Fernando Coto',     'M', 56, 94.00, 1.73, 31.41, 232, 151, 98, 'Diabetes',        '2026-03-19'),
        (38, 'Karina Segura',     'F', 44, 72.00, 1.66, 26.13, 177, 129, 84, 'Colesterol alto', '2026-03-19'),
        (39, 'Leonardo Rivas',    'M', 38, 77.00, 1.72, 26.03, 156, 121, 79, 'Ninguno',         '2026-03-20'),
        (40, 'Melissa Porras',    'F', 47, 74.00, 1.67, 26.53, 192, 135, 88, 'Hipertension',    '2026-03-20');
END
GO

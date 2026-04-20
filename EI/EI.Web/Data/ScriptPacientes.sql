-- Script SQL Server para la base de datos EstadisticaInferencial
-- Ejecutar en SQL Server Management Studio o sqlcmd

USE [master];
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'EstadisticaInferencial')
BEGIN
    CREATE DATABASE [EstadisticaInferencial];
END
GO

USE [EstadisticaInferencial];
GO

IF OBJECT_ID('[dbo].[Pacientes]', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Pacientes] (
        [Id]                 INT            NOT NULL IDENTITY(1,1),
        [Nombre]             VARCHAR(100)   NOT NULL,
        [Edad]               INT            NULL,
        [PesoKg]             DECIMAL(5,2)   NULL,
        [EstaturaM]          DECIMAL(4,2)   NULL,
        [Imc]                DECIMAL(6,4)   NULL,
        [TglMgDl]            INT            NULL,
        [PresionSistolica]   INT            NULL,
        [PresionDiastolica]  INT            NULL,
        [Padecimientos]      VARCHAR(200)   NULL,
        [Fecha]              DATE           NULL,
        [Genero]             CHAR(1)        NULL,   -- 'M' = Hombre, 'F' = Mujer
        CONSTRAINT [PK_Pacientes] PRIMARY KEY ([Id])
    );
END
GO

-- Insertar datos solo si la tabla está vacía
IF NOT EXISTS (SELECT 1 FROM [dbo].[Pacientes])
BEGIN
    INSERT INTO [dbo].[Pacientes] ([Nombre],[Edad],[PesoKg],[EstaturaM],[Imc],[TglMgDl],[PresionSistolica],[PresionDiastolica],[Padecimientos],[Fecha],[Genero]) VALUES
        ('Juan Perez',       45, 82.0,  1.75, 26.7755, 180, 130,  85, 'Hipertension',    '2026-03-01', 'M'),
        ('Maria Lopez',      34, 65.0,  1.62, 24.7676, 140, 120,  80, 'Ninguno',         '2026-03-01', 'F'),
        ('Carlos Ramirez',   50, 90.0,  1.70, 31.1419, 210, 140,  90, 'Diabetes',        '2026-03-02', 'M'),
        ('Ana Gomez',        29, 58.0,  1.60, 22.6563, 120, 110,  70, 'Ninguno',         '2026-03-02', 'F'),
        ('Luis Fernandez',   60, 85.0,  1.68, 30.1162, 200, 150,  95, 'Hipertension',    '2026-03-03', 'M'),
        ('Sofia Morales',    41, 70.0,  1.65, 25.7117, 160, 125,  82, 'Colesterol alto', '2026-03-03', 'F'),
        ('Pedro Castillo',   38, 78.0,  1.72, 26.3656, 150, 118,  76, 'Ninguno',         '2026-03-04', 'M'),
        ('Laura Vargas',     47, 74.0,  1.66, 26.8544, 190, 135,  88, 'Hipertension',    '2026-03-04', 'F'),
        ('Diego Herrera',    33, 92.0,  1.80, 28.3951, 170, 128,  84, 'Obesidad',        '2026-03-05', 'M'),
        ('Elena Rojas',      55, 68.0,  1.58, 27.2392, 210, 145,  92, 'Diabetes',        '2026-03-05', 'F'),
        ('Jose Castro',      44, 80.0,  1.73, 26.7299, 175, 130,  85, 'Hipertension',    '2026-03-06', 'M'),
        ('Patricia Solis',   36, 62.0,  1.61, 23.9188, 135, 118,  75, 'Ninguno',         '2026-03-06', 'F'),
        ('Ricardo Mendez',   52, 88.0,  1.69, 30.8112, 220, 142,  90, 'Diabetes',        '2026-03-07', 'M'),
        ('Daniela Cruz',     27, 55.0,  1.59, 21.7555, 115, 108,  70, 'Ninguno',         '2026-03-07', 'F'),
        ('Marco Jimenez',    48, 84.0,  1.74, 27.7447, 185, 134,  86, 'Hipertension',    '2026-03-08', 'M'),
        ('Andrea Navarro',   31, 60.0,  1.63, 22.5827, 140, 116,  74, 'Ninguno',         '2026-03-08', 'F'),
        ('Rafael Vega',      57, 91.0,  1.71, 31.1207, 230, 150,  96, 'Diabetes',        '2026-03-09', 'M'),
        ('Lucia Herrera',    42, 67.0,  1.64, 24.9108, 165, 124,  82, 'Colesterol alto', '2026-03-09', 'F'),
        ('Gabriel Pineda',   39, 76.0,  1.70, 26.2976, 155, 120,  78, 'Ninguno',         '2026-03-10', 'M'),
        ('Valeria Campos',   46, 73.0,  1.67, 26.1752, 195, 138,  88, 'Hipertension',    '2026-03-10', 'F'),
        ('Andres Molina',    35, 89.0,  1.82, 26.8687, 168, 126,  83, 'Obesidad',        '2026-03-11', 'M'),
        ('Natalia Salas',    53, 69.0,  1.60, 26.9531, 205, 144,  91, 'Diabetes',        '2026-03-11', 'F'),
        ('Miguel Araya',     40, 81.0,  1.74, 26.7539, 172, 129,  84, 'Hipertension',    '2026-03-12', 'M'),
        ('Paula Quesada',    28, 57.0,  1.58, 22.8329, 118, 110,  72, 'Ninguno',         '2026-03-12', 'F'),
        ('Esteban Rojas',    62, 86.0,  1.69, 30.1110, 210, 148,  94, 'Hipertension',    '2026-03-13', 'M'),
        ('Camila Mora',      33, 64.0,  1.63, 24.0882, 142, 117,  76, 'Ninguno',         '2026-03-13', 'F'),
        ('Hector Salazar',   51, 93.0,  1.72, 31.4359, 225, 152,  97, 'Diabetes',        '2026-03-14', 'M'),
        ('Monica Brenes',    45, 71.0,  1.65, 26.0790, 176, 130,  85, 'Colesterol alto', '2026-03-14', 'F'),
        ('Adrian Soto',      37, 79.0,  1.73, 26.3958, 158, 122,  80, 'Ninguno',         '2026-03-15', 'M'),
        ('Veronica Chaves',  49, 75.0,  1.66, 27.2173, 198, 136,  89, 'Hipertension',    '2026-03-15', 'F'),
        ('Cristian Vargas',  34, 91.0,  1.81, 27.7769, 169, 127,  83, 'Obesidad',        '2026-03-16', 'M'),
        ('Daniel Rojas',     54, 70.0,  1.59, 27.6888, 207, 146,  92, 'Diabetes',        '2026-03-16', 'M'),
        ('Oscar Delgado',    41, 83.0,  1.75, 27.1020, 174, 131,  86, 'Hipertension',    '2026-03-17', 'M'),
        ('Silvia Murillo',   30, 59.0,  1.62, 22.4813, 139, 115,  74, 'Ninguno',         '2026-03-17', 'F'),
        ('Julio Acosta',     63, 87.0,  1.70, 30.1038, 215, 149,  95, 'Hipertension',    '2026-03-18', 'M'),
        ('Paola Fonseca',    32, 63.0,  1.61, 24.3046, 141, 118,  76, 'Ninguno',         '2026-03-18', 'F'),
        ('Fernando Coto',    56, 94.0,  1.73, 31.4077, 232, 151,  98, 'Diabetes',        '2026-03-19', 'M'),
        ('Karina Segura',    44, 72.0,  1.66, 26.1286, 177, 129,  84, 'Colesterol alto', '2026-03-19', 'F'),
        ('Leonardo Rivas',   38, 77.0,  1.72, 26.0276, 156, 121,  79, 'Ninguno',         '2026-03-20', 'M'),
        ('Melissa Porras',   47, 74.0,  1.67, 26.5338, 192, 135,  88, 'Hipertension',    '2026-03-20', 'F');
END
GO

CREATE TABLE [dbo].[Categoria]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nome] NVARCHAR(50) NULL, 
    [DataCriacao] DATETIME NOT NULL, 
    [DataModificacao] DATETIME NOT NULL
)

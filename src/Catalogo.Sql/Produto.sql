CREATE TABLE [dbo].[Produto]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Nome] VARCHAR(100) NOT NULL, 
    [DataCriacao] DATETIME NOT NULL DEFAULT GetDate(), 
    [DataModificacao] DATETIME NOT NULL DEFAULT GetDate(), 
    [Preco] DECIMAL(18, 2) NULL
)

CREATE TABLE [dbo].[Readings]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Depth numeric(10,2),
	Temperature numeric(5,2),
	MagX numeric(10,4),
	MagY numeric(10,4),
	MagZ numeric(10,4)
)

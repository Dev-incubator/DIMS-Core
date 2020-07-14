CREATE TABLE [dbo].[Direction]
(
	[DirectionId] INT IDENTITY NOT NULL, 
    [Name] NCHAR(25) NOT NULL, 
    [Description] NVARCHAR(255) NULL,
    CONSTRAINT PK_Direction_DirectionId PRIMARY KEY(DirectionId),
    CONSTRAINT CK_Direction_Name CHECK([Name]!=''),
    CONSTRAINT UQ_Direction_Name UNIQUE([Name])
)

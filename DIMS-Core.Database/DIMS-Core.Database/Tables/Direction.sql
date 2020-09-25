CREATE TABLE [dbo].[Direction]
(
	[DirectionId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(15) NOT NULL, 
    [Description] NVARCHAR(250) NULL
)

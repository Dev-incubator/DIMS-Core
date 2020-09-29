CREATE TABLE [dbo].[Direction]
(
    [DirectionId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL
)

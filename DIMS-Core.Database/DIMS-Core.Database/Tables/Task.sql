CREATE TABLE [dbo].[Task]
(
    [TaskId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [StartDate] DATETIME NOT NULL, 
    [DeadlineDate] DATETIME NOT NULL
)

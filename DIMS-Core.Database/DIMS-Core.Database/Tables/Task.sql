CREATE TABLE [dbo].[Task]
(
	[TaskId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Description] NVARCHAR(250) NULL, 
    [StartDate] DATETIME NOT NULL, 
    [DeadlineDate] DATETIME NOT NULL
)

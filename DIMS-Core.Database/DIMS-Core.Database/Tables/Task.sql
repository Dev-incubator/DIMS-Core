CREATE TABLE [dbo].[Task]
(
	[TaskId] INT IDENTITY NOT NULL, 
    [Name] NCHAR(30) NOT NULL, 
    [Description] NVARCHAR(100) NULL, 
    [StartDate] DATE NOT NULL, 
    [DeadlineDate] DATE NOT NULL
    CONSTRAINT PK_Task_Task_TaskId PRIMARY KEY(TaskId)
    CONSTRAINT UQ_Task_Name UNIQUE([Name])
)

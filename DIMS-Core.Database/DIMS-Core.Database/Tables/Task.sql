CREATE TABLE [dbo].[Task]
(
	[TaskId] INT NOT NULL, 
    [Name] NCHAR(25) NULL, 
    [Description] NVARCHAR(100) NULL, 
    [StartDate] DATE NULL, 
    [DeadlineDate] DATE NULL
    CONSTRAINT PK_Task_Task_TaskId PRIMARY KEY(TaskId)
    CONSTRAINT UQ_Task_Name UNIQUE([Name])
)

CREATE TABLE [dbo].[TaskState]
(
	[StateId] INT IDENTITY NOT NULL, 
    [StateName] NCHAR(30) NOT NULL
	CONSTRAINT PK_TaskState_StateId PRIMARY KEY(StateId),
	CONSTRAINT UQ_TaskState_StateName UNIQUE(StateName)
)

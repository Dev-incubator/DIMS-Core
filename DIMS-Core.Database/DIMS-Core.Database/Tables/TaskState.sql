CREATE TABLE [dbo].[TaskState]
(
	[StateId] INT NOT NULL, 
    [StateName] NCHAR(25) NULL
	CONSTRAINT PK_TaskState_StateId PRIMARY KEY(StateId),
	CONSTRAINT UQ_TaskState_StateName UNIQUE(StateName)
)

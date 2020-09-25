CREATE TABLE [dbo].[TaskState]
(
	[StateId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [StateName] NVARCHAR(30) NOT NULL
)

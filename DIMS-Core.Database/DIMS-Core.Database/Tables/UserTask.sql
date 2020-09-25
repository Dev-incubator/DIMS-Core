CREATE TABLE [dbo].[UserTask]
(
	[UserTaskId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [TaskId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [StateId] INT NOT NULL,
    FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([TaskId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]),
    FOREIGN KEY ([StateId]) REFERENCES [dbo].[TaskState] ([StateId]),
)

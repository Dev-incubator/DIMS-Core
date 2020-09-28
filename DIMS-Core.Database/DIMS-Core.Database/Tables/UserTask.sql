CREATE TABLE [dbo].[UserTask]
(
	[UserTaskId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [TaskId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [StateId] INT NOT NULL,
    FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([TaskId]) on delete cascade on update cascade,
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]) on delete cascade on update cascade,
    FOREIGN KEY ([StateId]) REFERENCES [dbo].[TaskState] ([StateId]) on delete cascade on update cascade,
)

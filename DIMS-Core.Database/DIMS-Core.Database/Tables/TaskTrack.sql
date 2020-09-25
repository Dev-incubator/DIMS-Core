CREATE TABLE [dbo].[TaskTrack]
(
	[TaskTrackId] INT NOT NULL PRIMARY KEY, 
    [UserTaskId] INT NOT NULL, 
    [TrackDate] DATETIME NOT NULL, 
    [TrackNote] NVARCHAR(250) NULL,
    FOREIGN KEY ([UserTaskId]) REFERENCES [dbo].[UserTask] ([UserTaskId])
)

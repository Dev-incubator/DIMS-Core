CREATE TABLE [dbo].[TaskTrack]
(
    [TaskTrackId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [UserTaskId] INT NOT NULL, 
    [TrackDate] DATETIME NOT NULL, 
    [TrackNote] NVARCHAR(255) NULL,
    FOREIGN KEY ([UserTaskId]) REFERENCES [dbo].[UserTask] ([UserTaskId]) on delete cascade on update cascade
)

CREATE TABLE [dbo].[TaskTrack]
(
	[TaskTrackId] INT IDENTITY NOT NULL, 
    [UserTaskId] INT NOT NULL, 
    [TrackDate] DATE NOT NULL, 
    [TrackNote] NVARCHAR(100) NULL
    CONSTRAINT PK_TaskTrack_TaskTrackId PRIMARY KEY(TaskTrackId),
    CONSTRAINT FK_TaskTrack_To_UserTask FOREIGN KEY(UserTaskId) REFERENCES UserTask(UserTaskId) ON DELETE CASCADE ON UPDATE CASCADE
)

CREATE TABLE [dbo].[TaskTrack]
(
	[TaskTrackId] INT NOT NULL, 
    [UserTaskId] INT NOT NULL, 
    [TrackDate] DATE NULL, 
    [TrackNote] NVARCHAR(50) NULL
    CONSTRAINT PK_TaskTrack_TaskTrackId PRIMARY KEY(TaskTrackId),
    CONSTRAINT FK_TaskTrack_To_UserTask FOREIGN KEY(UserTaskId) REFERENCES UserTask(UserTaskId)
)

﻿CREATE TABLE [dbo].[UserTask]
(
	[UserTaskId] INT IDENTITY NOT NULL, 
    [TaskId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [StateId] INT NOT NULL CONSTRAINT DF_UserTask_StateId DEFAULT 1
    CONSTRAINT PK_UserTask_UserTaskId PRIMARY KEY(UserTaskId)
    CONSTRAINT FK_UserTask_To_Task FOREIGN KEY(TaskId) REFERENCES Task(TaskId) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_UserTask_To_UserProfile FOREIGN KEY(UserId) REFERENCES UserProfile(UserId) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT FK_UserTask_To_TaskState FOREIGN KEY(StateId) REFERENCES TaskState(StateId) ON DELETE SET NULL ON UPDATE CASCADE
)

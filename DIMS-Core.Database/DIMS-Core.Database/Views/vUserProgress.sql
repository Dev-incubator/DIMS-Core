﻿CREATE VIEW [dbo].[vUserProgress]
	AS SELECT
		UserProfile.UserId,
		UserTask.TaskId,
		TaskTrack.TaskTrackId,
		(UserProfile.[Name] + ' ' + UserProfile.LastName) as UserName,
		Task.[Name] as TaskName,
		TaskTrack.TrackNote,
		FORMAT(TaskTrack.TrackDate, 'dd/MM/yyyy hh:mm:ss') as TrackDate
	FROM UserProfile
		JOIN UserTask on UserProfile.UserId = UserTask.UserId
		JOIN TaskTrack on UserTask.TaskId = TaskTrack.UserTaskId
		JOIN Task on UserTask.TaskId = Task.TaskId
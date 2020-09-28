CREATE VIEW [dbo].[vUserTrack]
	AS SELECT
		UserTask.UserId,
		UserTask.TaskId,
		TaskTrack.TaskTrackId,
		Task.[Name] as TaskName,
		TaskTrack.TrackNote,
		TaskTrack.TrackDate
	FROM UserTask
		JOIN Task on UserTask.TaskId = Task.TaskId
		JOIN TaskTrack on UserTask.UserTaskId = TaskTrack.UserTaskId
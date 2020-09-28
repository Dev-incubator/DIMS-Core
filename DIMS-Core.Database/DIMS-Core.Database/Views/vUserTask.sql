CREATE VIEW [dbo].[vUserTask]
	AS SELECT
		UserTask.UserId,
		UserTask.TaskId,
		Task.[Name] as TaskName,
		Task.[Description],
		Task.StartDate,
		Task.DeadlineDate,
		TaskState.StateName as 'State'
	FROM UserTask
		JOIN Task ON UserTask.TaskId = Task.TaskId
		JOIN TaskState on UserTask.StateId = TaskState.StateId
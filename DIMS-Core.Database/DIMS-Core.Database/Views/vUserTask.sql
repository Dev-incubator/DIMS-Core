CREATE VIEW [dbo].[vUserTask]
	AS SELECT
		UserTask.UserId,
		UserTask.TaskId,
		Task.[Name] as TaskName,
		Task.[Description],
		FORMAT(Task.StartDate, 'dd/MM/yyyy hh:mm') as StartDate,
		FORMAT(Task.DeadlineDate, 'dd/MM/yyyy hh:mm') as DeadlineDate,
		TaskState.StateName as 'State'
	FROM UserTask
		JOIN Task ON UserTask.TaskId = Task.TaskId
		JOIN TaskState on UserTask.StateId = TaskState.StateId
CREATE VIEW [dbo].[vTask]
	AS SELECT 
		Task.TaskId,
		Task.[Name],
		Task.[Description],
		Task.StartDate,
		Task.DeadlineDate
	FROM [Task]
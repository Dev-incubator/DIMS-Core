CREATE VIEW [dbo].[vTask]
	AS SELECT 
		Task.TaskId,
		Task.[Name],
		Task.[Description],
		FORMAT(Task.StartDate, 'dd.MM.yyyy hh:mm:ss') as StartDate,
		FORMAT(Task.DeadlineDate, 'dd.MM.yyyy hh:mm:ss') as DeadlineDate
	FROM [Task]
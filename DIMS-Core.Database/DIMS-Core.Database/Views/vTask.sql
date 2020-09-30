CREATE VIEW [dbo].[vTask] 
    AS SELECT 
        TaskId,
        Name,
        Description,
        StartDate,
        DeadlineDate
    FROM [Task]
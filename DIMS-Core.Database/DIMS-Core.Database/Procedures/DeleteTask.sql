CREATE PROCEDURE [dbo].[DeleteTask]
	@TaskId int
AS
	DELETE FROM Task
	WHERE TaskId = @TaskId
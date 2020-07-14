CREATE PROCEDURE [dbo].[DeleteTask]
	@id int
AS
	DELETE FROM Task
	WHERE TaskId = @id

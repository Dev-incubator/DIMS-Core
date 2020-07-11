CREATE PROCEDURE [dbo].[SetUserTaskAsSuccess]
	@userId int,
	@taskId int
AS
UPDATE UserTask
SET StateId =2
WHERE UserId=@userId AND TaskId=@taskId
CREATE PROCEDURE [dbo].[SetUserTaskAsFail]
	@UserId int,
	@TaskId int
AS
    UPDATE UserTask SET StateId = 3
    WHERE UserId = @UserId AND TaskId = @TaskId
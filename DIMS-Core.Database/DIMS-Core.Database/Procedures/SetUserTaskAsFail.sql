CREATE PROCEDURE [dbo].[SetUserTaskAsFail]
    @UserId int,
    @TaskId int
AS
    UPDATE UserTask SET StateId = 4
    WHERE UserId = @UserId AND TaskId = @TaskId
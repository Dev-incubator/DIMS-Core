CREATE PROCEDURE [dbo].[SetUserTaskAsActive]
    @UserId int,
    @TaskId int
AS
    UPDATE UserTask SET StateId = 1
    WHERE UserId = @UserId AND TaskId = @TaskId

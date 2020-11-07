CREATE PROCEDURE [dbo].[SetUserTaskAsPause]
    @UserId int,
    @TaskId int
AS
    UPDATE UserTask SET StateId = 2
    WHERE UserId = @UserId AND TaskId = @TaskId

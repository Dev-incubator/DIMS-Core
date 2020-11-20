CREATE PROCEDURE [dbo].[SetUserTaskState]
    @UserId int,
    @TaskId int,
    @State int
AS
    UPDATE UserTask SET StateId = @State
    WHERE UserId = @UserId AND TaskId = @TaskId

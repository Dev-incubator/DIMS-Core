﻿CREATE PROCEDURE [dbo].[SetUserTaskAsSuccess]
    @UserId int,
    @TaskId int
AS
    UPDATE UserTask SET StateId = 3
    WHERE UserId = @UserId AND TaskId = @TaskId

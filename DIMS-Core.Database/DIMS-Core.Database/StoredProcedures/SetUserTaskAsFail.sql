CREATE PROCEDURE [dbo].[SetUserTaskAsFail]
	@userId int,
	@taskId int
AS
UPDATE UserTask
SET StateId =3
WHERE UserId=@userId AND TaskId=@taskId

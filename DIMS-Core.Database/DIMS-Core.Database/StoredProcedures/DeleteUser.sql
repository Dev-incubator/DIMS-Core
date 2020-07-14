CREATE PROCEDURE [dbo].[DeleteUser]
	@id int
AS
	DELETE FROM UserProfile
	WHERE UserId = @id

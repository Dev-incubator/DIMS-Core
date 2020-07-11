CREATE PROCEDURE [dbo].[DeleteUserById]
	@id int
AS
	DELETE FROM UserProfile
	WHERE UserId = @id

CREATE PROCEDURE [dbo].[DeleteUser]
    @UserId int
AS
    Delete from UserProfile where UserId = @UserId;

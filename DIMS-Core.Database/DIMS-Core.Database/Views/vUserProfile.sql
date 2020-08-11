CREATE VIEW [dbo].[vUserProfile] AS
SELECT 
UserProfile.UserId,
(UserProfile.FirstName + ' ' + UserProfile.LastName) as FullName,
UserProfile.Email,
Direction.[Name] as Direction,
UserProfile.Sex,
UserProfile.Education,
(DATEDIFF(YEAR, UserProfile.BirthDate, GETDATE())) as Age,
UserProfile.UniversityAverageScore,
UserProfile.MathScore,
UserProfile.[Address],
UserProfile.MobilePhone,
UserProfile.Skype,
UserProfile.StartDate
FROM UserProfile INNER JOIN Direction ON UserProfile.DirectionId=Direction.DirectionId


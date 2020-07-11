CREATE VIEW [dbo].[vUserProfile] AS
SELECT 
UserProfile.UserId as UserId,
(UserProfile.[Name] + ' ' + UserProfile.LastName) as FullName,
UserProfile.Email as Email,
Direction.[Name] as Direction,
UserProfile.Sex as Sex,
UserProfile.Education as Education,
(DATEDIFF(YEAR, UserProfile.BirthDate, GETDATE())) as Age,
UserProfile.UniversityAverageScore as UniversityAverageScore,
UserProfile.MathScore,
UserProfile.[Address],
UserProfile.MobilePhone,
UserProfile.Skype,
UserProfile.StartDate
FROM UserProfile INNER JOIN Direction ON UserProfile.DirectionId=Direction.DirectionId


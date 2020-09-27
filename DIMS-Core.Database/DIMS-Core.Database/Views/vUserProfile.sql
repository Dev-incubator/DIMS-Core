CREATE VIEW [dbo].[vUserProfile] 
	AS SELECT 
		UserProfile.UserId,
		(UserProfile.[Name] + ' ' + UserProfile.LastName) as FullName,
		UserProfile.Email,
		Direction.[Name] as Direction,
		UserProfile.Sex,
		UserProfile.Education,
		((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),UserProfile.BirthOfDate,112))/10000) AS Age,
		UserProfile.UniversityAverageScore,
		UserProfile.MathScore,
		UserProfile.[Address],
		UserProfile.MobilePhone,
		UserProfile.Skype,
		FORMAT(UserProfile.StartDate, 'dd.MM.yyyy', 'en-US') as UserProfile
	FROM UserProfile INNER JOIN Direction ON UserProfile.DirectionId=Direction.DirectionId
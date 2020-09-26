CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [DirectionId] INT NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(255) NOT NULL, 
    [Sex] TINYINT NOT NULL, 
    [Education] NVARCHAR(255) NOT NULL, 
    [BirthOfDate] DATETIME NOT NULL, 
    [UniversityAverageScore] FLOAT NOT NULL, 
    [MathScore] FLOAT NOT NULL, 
    [Address] NVARCHAR(255) NOT NULL, 
    [MobilePhone] NVARCHAR(255) NOT NULL, 
    [Skype] NVARCHAR(255) NOT NULL, 
    [StartDate] DATETIME NOT NULL,
    FOREIGN KEY ([DirectionId]) REFERENCES [dbo].[Direction] ([DirectionId])
)

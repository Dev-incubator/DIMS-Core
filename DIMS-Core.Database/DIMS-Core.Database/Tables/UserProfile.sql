CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [DirectionId] INT NOT NULL, 
    [Name] NVARCHAR(30) NOT NULL, 
    [Email] NVARCHAR(30) NOT NULL, 
    [LastName] NVARCHAR(30) NOT NULL, 
    [Sex] NVARCHAR(2) NOT NULL, 
    [Education] NVARCHAR(30) NOT NULL, 
    [BirthOfDate] DATETIME NOT NULL, 
    [UniversityAverageScore] FLOAT NOT NULL, 
    [MathScore] FLOAT NOT NULL, 
    [Address] NVARCHAR(30) NOT NULL, 
    [MobilePhone] NVARCHAR(30) NOT NULL, 
    [Skype] NVARCHAR(30) NOT NULL, 
    [StartDate] DATETIME NOT NULL,
    FOREIGN KEY ([DirectionId]) REFERENCES [dbo].[Direction] ([DirectionId])
)

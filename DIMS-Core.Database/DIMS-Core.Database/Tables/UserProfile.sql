CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT Identity(1,1) NOT NULL PRIMARY KEY, 
    [DirectionId] INT NOT NULL, 
    [Name] NVARCHAR(64) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [LastName] NVARCHAR(64) NOT NULL, 
    [Sex] TINYINT NOT NULL DEFAULT 0, 
    [Education] NVARCHAR(128) NULL, 
    [BirthOfDate] DATETIME NULL, 
    [UniversityAverageScore] FLOAT NULL, 
    [MathScore] FLOAT NULL, 
    [Address] NVARCHAR(255) NULL, 
    [MobilePhone] NVARCHAR(16) NULL, 
    [Skype] NVARCHAR(255) NULL, 
    [StartDate] DATETIME NULL,
    FOREIGN KEY ([DirectionId]) REFERENCES [dbo].[Direction] ([DirectionId])
)

CREATE TABLE [dbo].[UserProfile]
(
	[UserId] INT IDENTITY, 
    [DirectionId] INT NULL, 
    [Name] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Sex] NCHAR(25) NULL, 
    [Education] NVARCHAR(50) CONSTRAINT DF_UserProfile_Education  DEFAULT 'Not indicated', 
    [BirthDate] DATE NULL, 
    [UniversityAverageScore] FLOAT NULL, 
    [MathScore] FLOAT NULL, 
    [Address] NVARCHAR(50) NULL, 
    [MobilePhone] NCHAR(25) NULL, 
    [Skype] NVARCHAR(50) NULL, 
    [StartDate] DATE NULL
    CONSTRAINT PK_UserProfile_UserId PRIMARY KEY (UserId),
    CONSTRAINT UQ_UserProfile_Email UNIQUE(Email),
    CONSTRAINT FK_UserProfile_To_Direction FOREIGN KEY(DirectionId) REFERENCES Direction(DirectionId) ON DELETE SET NULL ON UPDATE CASCADE

)

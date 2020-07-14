/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
PRINT('SEEDING SEED DATA')
INSERT Direction VALUES 
('.Net', '.Net description text'),
('Java', 'Java description text'),
('Front-end', 'Front-end description text'),
('Salesforce', 'Salesfoce description text')
GO

INSERT INTO UserProfile (DirectionId, [Name], [LastName], BirthDate, Email) VALUES
(1,'Vasya', 'Pupkin', CAST('2000-03-20' as datetime), 'Vasya@mail.ru'),
(2,'Vanya', 'Kryvoi', CAST('1999-03-20' as datetime), 'Vanya@gmail.com'),
(3,'Petya', 'Dlinny', CAST('2001-03-20' as datetime), 'Petya@yandex.ru'),
(4,'Lesha', 'Medlenny', CAST('1996-03-20' as datetime), 'Lesha@mail.ru')
GO

INSERT TaskState VALUES
('Active'),
('Success'),
('Fail')

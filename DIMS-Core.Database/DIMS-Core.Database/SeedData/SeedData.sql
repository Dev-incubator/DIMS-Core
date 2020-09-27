PRINT('SEEDING SEED DATA')
INSERT Direction VALUES 
    ('-', 'I haven''t decided, I need a consultation'),
    ('FRONTEND', 'Front-end description text'),
    ('JAVA', '.Net description text'),
    ('.NET', 'Java description text'),
    ('SALESFORCE', 'Front-end description text')
GO

INSERT INTO UserProfile (DirectionId, [Name], [LastName], BirthOfDate, Email, Skype, Sex, UniversityAverageScore, MathScore, Education, [Address], MobilePhone, StartDate) VALUES
    (3,'Elisey', 'Butko', '1999-20-08', 'elisey.butko@gmail.com', 'elisey.butko', 1, 9.0, 41, 'Высшее образование', 'Минск, ул. Макаёнка 21, 32', '+375(29)600-20-85', '2020-20-08'),
    (3,'Mark', 'Davydov', '2000-20-03', 'mark.davydov@gmail.com', 'mark.davydov', 1, 7.4, 52, 'Среднее профессиональное образование', 'Минск, ул. Дачная 15', '+375(29)175-51-75', '2020-01-09'),
    (2,'Peter', 'Doronin', '2001-01-05', 'peter.doronin@gmail.com', 'peter2001', 1, 5.8, 83, 'Высшее образование', 'Минск, ул. Дружбы 5, 100', '+375(44)555-20-96', '2020-30-07'),
    (1,'Harry', 'Soloviev', '1999-11-09', 'harry.soloviev@gmail.com', 'harrysoloviev', 1, 7.0, 80, 'Высшее образование', 'Минск, ул. Михалово 1, 50', '+375(25)444-06-08', '2020-01-08'),
    (5,'Peter', 'Doronin', '1996-10-01', 'peter.doronin@gmail.com', 'doronin.peter', 1, 7.1, 90, 'Высшее образование', 'Минск, ул. Макаёнка 70, 32', '+375(25)359-98-75', '2020-29-07'),
    (4,'Bogdan', 'Nazarov', '1995-13-02', 'bogdan.nazarov@gmail.com', 'nazarov', 1, 9.3, 49, 'Незаконченное высшее образование', 'Минск, ул. Юбилейная 21, 32', '+375(29)904-57-79', '2020-25-09')
GO

INSERT INTO TaskState (StateName) VALUES
    ('Active'),
    ('Pause'),
    ('Success'),
    ('Fail')
GO

INSERT INTO Task ([Name], [Description], StartDate, DeadlineDate) VALUES
    ('The Death Valley :: .Net', 'Starting mission', '2020-25-09 13:48:13','2020-02-10 13:48:13')
GO

INSERT INTO UserTask (TaskId, UserId, StateId) VALUES
    (1, 5, 1)
GO

INSERT INTO TaskTrack (UserTaskId, TrackDate, TrackNote) VALUES
    (1, '2020-25-09 13:48:13','Start task')
GO
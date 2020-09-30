PRINT('SEEDING SEED DATA')
INSERT Direction VALUES 
    ('-', 'I haven''t decided, I need a consultation'),
    ('FRONTEND', 'Front-end description text'),
    ('JAVA', '.Net description text'),
    ('.NET', 'Java description text'),
    ('SALESFORCE', 'Front-end description text')
GO

INSERT INTO UserProfile (DirectionId, Name, LastName, BirthOfDate, Email, Skype, Sex, UniversityAverageScore, MathScore, Education, Address, MobilePhone, StartDate) VALUES
    (3,'Elisey', 'Butko', '1999-20-08', 'elisey.butko@gmail.com', 'elisey.butko', 1, 9.0, 4.1, 'Высшее образование', 'Минск, ул. Макаёнка 21, 32', '375296002085', '2020-20-08'),
    (3,'Mark', 'Davydov', '2000-20-03', 'mark.davydov@gmail.com', 'mark.davydov', 1, 7.4, 5.2, 'Среднее профессиональное образование', 'Минск, ул. Дачная 15', '375291755175', '2020-01-09'),
    (2,'Peter', 'Doronin', '2001-01-05', 'peter.doronin@gmail.com', 'peter2001', 1, 5.8, 8.3, 'Высшее образование', 'Минск, ул. Дружбы 5, 100', '375445552096', '2020-30-07'),
    (1,'Harry', 'Soloviev', '1999-11-09', 'harry.soloviev@gmail.com', 'harrysoloviev', 1, 7.0, 8.0, 'Высшее образование', 'Минск, ул. Михалово 1, 50', '375254440608', '2020-01-08'),
    (5,'Peter', 'Doronin', '1996-10-01', 'peter.doronin@gmail.com', 'doronin.peter', 1, 7.1, 9.0, 'Высшее образование', 'Минск, ул. Макаёнка 70, 32', '375253599875', '2020-29-07'),
    (4,'Bogdan', 'Nazarov', '1995-13-02', 'bogdan.nazarov@gmail.com', 'bogdan.nazarov@gmail.com', 1, 9.3, 4.9, 'Незаконченное высшее образование', 'Минск, ул. Юбилейная 21, 32', '375299045779', '2020-25-09')
GO

INSERT INTO TaskState (StateName) VALUES
    ('Active'),
    ('Pause'),
    ('Success'),
    ('Fail')
GO

INSERT INTO Task (Name, Description, StartDate, DeadlineDate) VALUES
    ('The Death Valley :: .Net', 'Starting mission', '2020-25-09 13:48:13','2020-02-10 13:48:13')
GO

INSERT INTO UserTask (TaskId, UserId, StateId) VALUES
    (1, 5, 1)
GO

INSERT INTO TaskTrack (UserTaskId, TrackDate, TrackNote) VALUES
    (1, '2020-25-09 13:48:13','Start task')
GO
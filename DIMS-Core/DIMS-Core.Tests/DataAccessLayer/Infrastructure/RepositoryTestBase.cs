using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Enums;
using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using EntityTask = DIMS_Core.DataAccessLayer.Entities.Task;

namespace DIMS_Core.Tests.Infrastructure
{
    [TestFixture]
    public class RepositoryTestBase : IDisposable
    {
        protected readonly DIMSCoreDatabaseContext context;

        
        public RepositoryTestBase()
        {
            var options = new DbContextOptionsBuilder<DIMSCoreDatabaseContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                   .Options;

            context = new DIMSCoreDatabaseContext(options);
            context.Database.EnsureCreated();

            Seed(context);
        }

        private void Seed(DIMSCoreDatabaseContext context)
        {
            #region Seed Directions
            var directions = new[]
               {
                    new Direction {
                        DirectionId = 1,
                        Name = "-",
                        Description = "I haven't decided, I need a consultation"
                    },
                    new Direction {
                        DirectionId = 2,
                        Name = "FRONTEND",
                        Description = "Front-end description text"
                    },
                    new Direction {
                        DirectionId = 3,
                        Name = "JAVA",
                        Description = "Java description text"
                    },
                    new Direction {
                        DirectionId = 4,
                        Name = ".NET",
                        Description = ".Net description text"
                    },
                    new Direction {
                        DirectionId = 5,
                        Name = "SALESFORCE",
                        Description = "Front-end description text"
                    }
                };

            context.Direction.AddRange(directions);
            #endregion

            #region Seed UserProfiles
            var userProfiles = new[]
            {
                    new UserProfile {
                        UserId = 1,
                        Name = "Elisey",
                        LastName = "Butko",
                        Email = "elisey.butko@gmail.com",
                        DirectionId = 3,
                        Direction = directions[2],
                        Sex = Sex.Female,
                        Education = "БГУИР",
                        BirthOfDate = new DateTime(2000,08,20),
                        UniversityAverageScore = 9.0,
                        MathScore = 4.1,
                        Address = "Минск, ул. Макаёнка 21, 32",
                        MobilePhone = "375296002085",
                        Skype = "elisey.butko",
                        StartDate = new DateTime(2020,08,20)
                    },
                    new UserProfile {
                        UserId = 2,
                        Name = "Mark",
                        LastName = "Davydov",
                        Email = "mark.davydov@gmail.com",
                        DirectionId = 2,
                        Direction = directions[1],
                        Sex = Sex.Female,
                        Education = "БГУ",
                        BirthOfDate = new DateTime(1995,08,20),
                        UniversityAverageScore = 8.5,
                        MathScore = 6.1,
                        Address = "Минск, ул. Дачная 15",
                        MobilePhone = "375291755175",
                        Skype = "mark.davydov",
                        StartDate = new DateTime(2020,09,01)
                    },
                    new UserProfile {
                        UserId = 3,
                        Name = "Peter",
                        LastName = "Doronin",
                        Email = "peter.doronin@gmail.com",
                        DirectionId = 4,
                        Direction = directions[3],
                        Sex = Sex.Female,
                        Education = "БНТУ",
                        BirthOfDate = new DateTime(2000,08,20),
                        UniversityAverageScore = 7.5,
                        MathScore = 8.1,
                        Address = "Минск, ул. Дружбы 5, 100",
                        MobilePhone = "375445552096",
                        Skype = "peter2000",
                        StartDate = new DateTime(2020,07,30),
                    },
                };

            context.UserProfile.AddRange(userProfiles);
            #endregion

            #region Seed Tasks
            var tasks = new[]
            {
                new EntityTask
                {
                    TaskId = 1,
                    Name = "Create database",
                    Description = "Description to create database",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                },
                new EntityTask
                {
                    TaskId = 2,
                    Name = "Write CRUD operations for Users",
                    Description = "Write create, read, update and delete operations for Users",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                },
                new EntityTask
                {
                    TaskId = 3,
                    Name = "Write CRUD operations for Tasks",
                    Description = "Write create, read, update and delete operations for Tasks",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                },
            };

            context.Task.AddRange(tasks);
            #endregion

            #region Seed TaskState
            var taskStates = new[]
                {
                    new TaskState
                    {
                        StateId = 1,
                        StateName = "In development"
                    },
                    new TaskState
                    {
                        StateId = 2,
                        StateName = "Design in progress"
                    },
                    new TaskState
                    {
                        StateId = 3,
                        StateName = "Ready to estimate"
                    },
                    new TaskState
                    {
                        StateId = 4,
                        StateName = "Design complete"
                    },
                    new TaskState
                    {
                        StateId = 5,
                        StateName = "Done"
                    },
                    new TaskState
                    {
                        StateId = 6,
                        StateName = "Ready to development"
                    }
                };
            context.TaskState.AddRange(taskStates);
            #endregion

            #region Seed VUserProfiles
            var vUserProfiles = new VUserProfile[3];
            for (int i = 0; i < 3; i++)
            {
                vUserProfiles[i] = new VUserProfile
                {
                    UserId = userProfiles[i].UserId,
                    FullName = userProfiles[i].Name + " " + userProfiles[i].LastName,
                    Email = userProfiles[i].Email,
                    Direction = userProfiles[i].Direction.Name,
                    Sex = userProfiles[i].Sex,
                    Education = userProfiles[i].Education,
                    Age = GetFullAge(userProfiles[i].BirthOfDate),
                    UniversityAverageScore = userProfiles[i].UniversityAverageScore,
                    MathScore = userProfiles[i].MathScore,
                    Address = userProfiles[i].Address,
                    MobilePhone = userProfiles[i].MobilePhone,
                    Skype = userProfiles[i].Skype,
                    StartDate = userProfiles[i].StartDate
                };
            };

            context.VUserProfile.AddRange(vUserProfiles);
            #endregion

            #region Seed VUserTasks
            var vUserTasks = new[]
            {
                new VUserTask {
                    TaskId = 1,
                    TaskName = "Create database",
                    Description = "Description to create database",
                    UserId = 1,
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04),
                    State = "In work"
                },
                new VUserTask {
                    TaskId = 2,
                    TaskName = "Write CRUD operations for Users",
                    Description = "Write create, read, update and delete operations for Users",
                    UserId = 1,
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04),
                    State = "In work"
                },
                 new VUserTask {
                    TaskId = 3,
                    TaskName = "Write CRUD operations for Tasks",
                    Description = "Write create, read, update and delete operations for Tasks",
                    UserId = 2,
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04),
                    State = "In work"
                }
            };

            context.VUserTask.AddRange(vUserTasks);
            #endregion

            #region Seed VTaskRepository
            var vTaskRepositories = new[]{
                new VTask{
                    TaskId = 1,
                    Name = "Write CRUD operations for Users",
                    Description = "Write create, read, update and delete operations for Users",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                },
                 new VTask{
                    TaskId = 2,
                    Name = "Create database",
                    Description = "Description to create database",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                },
                 new VTask{
                    TaskId = 3,
                    Name = "Write CRUD operations for Tasks",
                    Description = "Write create, read, update and delete operations for Tasks",
                    StartDate = new DateTime(2020,07,30),
                    DeadlineDate = new DateTime(2020, 12,04)
                }
            };
            context.VTask.AddRange(vTaskRepositories);
            #endregion

            #region Seed VUserTrack
            var vUserTracks = new[]{
                new VUserTrack{
                    TaskTrackId = 1,
                    UserId = 1,
                    TaskId = 1,
                    TaskName = "Create database",
                    TrackDate = new DateTime(2020, 12,04),
                    TrackNote = "Create table UserProfile"
                },
                new VUserTrack{
                    TaskTrackId = 2,
                    UserId = 1,
                    TaskId = 1,
                    TaskName = "Create database",
                    TrackDate = new DateTime(2020, 12,04),
                    TrackNote = "Create table Tasks"
                },
                new VUserTrack{
                    TaskTrackId = 3,
                    UserId = 1,
                    TaskId = 1,
                    TaskName = "Create database",
                    TrackDate = new DateTime(2020, 12,04),
                    TrackNote = "Create table TaskState"
                }
            };
            context.VUserTrack.AddRange(vUserTracks);
            #endregion

            context.SaveChanges();
        }

        private int? GetFullAge(DateTime? birthdate)
        {
            if (birthdate.HasValue)
            {
                var age = DateTime.Today.Year - birthdate.Value.Year;
                if (birthdate > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
            else return null;
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

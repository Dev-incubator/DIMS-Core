using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace DIMS_Core.Tests.DataAccessLayer.Infrastructure
{
    [TestFixture]
    public class RepositoryTestBase : IDisposable
    {
        protected DIMSCoreDatabaseContext Context { get; set; }
        protected UserProfile NewUserProfile { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<DIMSCoreDatabaseContext>()
                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                       .Options;
            Context = new DIMSCoreDatabaseContext(options);
            Context.Database.EnsureCreated();
            
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
            Context.Direction.AddRange(directions);
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
            Context.UserProfile.AddRange(userProfiles);
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
            Context.VUserProfile.AddRange(vUserProfiles);
            #endregion

            NewUserProfile = CreateNewUserProfile(4);

            Context.SaveChanges();
        }

        protected UserProfile CreateNewUserProfile(int newId)
        {
            UserProfile userProfile = new UserProfile()
            {
                UserId = newId,
                Name = "Harry",
                LastName = "Soloviev",
                Email = "harry.soloviev@gmail.com",
                DirectionId = 1,
                Direction = Context.Direction.Find(1),
                Sex = Sex.Female,
                Education = "БГУИР",
                BirthOfDate = new DateTime(1999, 09, 11),
                UniversityAverageScore = 7,
                MathScore = 8,
                Address = "Минск, ул. Михалово 1, 50",
                MobilePhone = "375254440608",
                Skype = "harrysoloviev",
                StartDate = new DateTime(2020, 09, 25)
            };
            return userProfile;
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
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

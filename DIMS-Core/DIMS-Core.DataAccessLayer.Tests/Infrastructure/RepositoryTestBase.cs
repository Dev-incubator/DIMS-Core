﻿using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Tests.Infrastructure
{
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
            SeedVUserProfiles(context);
            context.SaveChanges();
        }

        private void SeedVUserProfiles(DIMSCoreDatabaseContext context)
        {
            var vUserProfiles = new[]
            {
                new VUserProfile {
                    UserId = 1,
                    FullName = "Elisey Butko",
                    Email = "elisey.butko@gmail.com",
                    Direction = "JAVA",
                    Sex = Enums.Sex.Female,
                    Education = "БГУИР",
                    Age = 21,
                    UniversityAverageScore = 9.0,
                    MathScore = 4.1,
                    Address = "Минск, ул. Макаёнка 21, 32",
                    MobilePhone = "375296002085",
                    Skype = "elisey.butko",
                    StartDate = new System.DateTime(2020,08,20)
                },
                new VUserProfile {
                    UserId = 2,
                    FullName = "Mark Davydov",
                    Email = "mark.davydov@gmail.com",
                    Direction = "FRONTEND",
                    Sex = Enums.Sex.Female,
                    Education = "БГУ",
                    Age = 25,
                    UniversityAverageScore = 8.5,
                    MathScore = 6.1,
                    Address = "Минск, ул. Дачная 15",
                    MobilePhone = "375291755175",
                    Skype = "mark.davydov",
                    StartDate = new System.DateTime(2020,09,01)
                },
                new VUserProfile {
                    UserId = 3,
                    FullName = "Peter Doronin",
                    Email = "peter.doronin@gmail.com",
                    Direction = ".NET",
                    Sex = Enums.Sex.Female,
                    Education = "БНТУ",
                    Age = 20,
                    UniversityAverageScore = 7.5,
                    MathScore = 8.1,
                    Address = "Минск, ул. Дружбы 5, 100",
                    MobilePhone = "375445552096",
                    Skype = "peter2000",
                    StartDate = new System.DateTime(2020,07,30)
                },
            };

            context.VUserProfile.AddRange(vUserProfiles);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

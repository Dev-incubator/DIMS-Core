using System;
using System.Collections.Generic;
using System.Text;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using DIMS_Core.Tests.DAL.Mocks;

namespace DIMS_Core.Tests.DAL
{
    public class DirectoryRepositoryTest
    {
        [Test]
        public void GetAllFromRepository()
        {
            var repo = new Mock<IDirectionRepository>();
            var result = repo.Object.GetAll();
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public void DeleteExistingElement(int id)
        {
            var repo = new Mock<IDirectionRepository>();
            var task = repo.Object.DeleteAsync(id);
            Assert.IsTrue(task.IsCompleted);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public void GetByIdAsync(int id)
        {
            var repo = new Mock<IDirectionRepository>();
            var task = repo.Object.GetByIdAsync(id);
            Assert.IsTrue(task.IsCompletedSuccessfully);
        }

        [Test]
        [TestCase(-300)]
        public void GetByIdNotExisting(int id)
        {
            var repo = new Mock<IDirectionRepository>();
            var task = repo.Object.GetByIdAsync(id);
            task.Wait();
            var res = task.Result;
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task GetByIdExisting(int id)
        {
            var list = new List<Direction>() 
            {
                new Direction { DirectionId = 1, Name = ".net", Description = "none" },
                new Direction { DirectionId = 2, Name = "java", Description = "none" },
                new Direction { DirectionId = 3, Name = "front-end", Description = "none" },
                new Direction { DirectionId = 4, Name = "salesforce", Description = "none" }
            };
            var DbSetDirectionMock = MockHelper.CreateDbSetMock<Direction>(list);
            var DbMock = new Mock<DIMSCoreDatabaseContext>();
            //DbMock.Setup(db=>db.)
            DbMock.Setup(db => db.Set<Direction>()).Returns(DbSetDirectionMock.Object);
            //DbMock.Setup(db => db.Direction).Returns(DbSetDirectionMock.Object);

            var repo = new DirectionRepository(DbMock.Object);
            var res = await repo.GetByIdAsync(id);
            Assert.AreEqual(list.ElementAt(id - 1), res);
        }
    }
}

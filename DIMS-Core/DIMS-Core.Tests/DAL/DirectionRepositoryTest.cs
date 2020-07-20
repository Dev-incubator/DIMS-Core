using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DAL.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DIMS_Core.Tests.DAL
{
    public class DirectionRepositoryTest
    {
        List<Direction> list;
        Mock<DIMSCoreDataBaseContext> DbMock;
        Mock<DbSet<Direction>> DbSetDirectionMock;

        public DirectionRepositoryTest()
        {
            list = new List<Direction>()
            {
                new Direction { DirectionId = 1, Name = ".net", Description = "none" },
                new Direction { DirectionId = 2, Name = "java", Description = "none" },
                new Direction { DirectionId = 3, Name = "front-end", Description = "none" },
                new Direction { DirectionId = 4, Name = "salesforce", Description = "none" }
            };
            DbSetDirectionMock = MockHelper.CreateDbSetMock<Direction>(list);
            DbMock = new Mock<DIMSCoreDataBaseContext>();
            DbMock.Setup(db => db.Set<Direction>()).Returns(DbSetDirectionMock.Object);
            DbMock.Setup(db => db.Direction).Returns(DbSetDirectionMock.Object);
        }

        [Test]
        public void GetAllFromRepository()
        {
            var repo = new DirectionRepository(DbMock.Object);
            var res = repo.GetAll();
            Assert.IsTrue(res.Count() == 4);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateNewEntity()
        {
            Direction direction = new Direction { Name = "C++", Description = "none" };
            var repo = new DirectionRepository(DbMock.Object);
            await repo.CreateAsync(direction);
            DbSetDirectionMock.Verify(db => db.AddAsync(It.IsAny<Direction>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task DeleteExistingElement(int id)
        {
            DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return list.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(DbMock.Object);
            await repo.DeleteAsync(id);
            DbSetDirectionMock.Verify(m => m.Remove(It.IsAny<Direction>()), Times.Once);
        }

        [Test]
        [TestCase(-300)]
        public async System.Threading.Tasks.Task GetByIdNotExisting(int id)
        {
            DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return list.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(DbMock.Object);
            var res = await repo.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task GetByIdExisting(int id)
        {
            DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return list.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(DbMock.Object);
            var res = await repo.GetByIdAsync(id);
            Assert.AreEqual(list.ElementAt(id - 1), res);
        }
    }
}

using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
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
        private readonly Mock<DIMSCoreDatabaseContext> _DbMock;
        private readonly Mock<DbSet<Direction>> _DbSetDirectionMock;
        private readonly List<Direction> _DbSetList;

        public DirectionRepositoryTest()
        {
            _DbSetList = new List<Direction>()
            {
                new Direction { DirectionId = 1, Name = ".net", Description = "none" },
                new Direction { DirectionId = 2, Name = "java", Description = "none" },
                new Direction { DirectionId = 3, Name = "front-end", Description = "none" },
                new Direction { DirectionId = 4, Name = "salesforce", Description = "none" }
            };

            _DbSetDirectionMock = MockHelper.CreateDbSetMock<Direction>(_DbSetList);
            _DbMock = new Mock<DIMSCoreDatabaseContext>();
            _DbMock.Setup(db => db.Set<Direction>()).Returns(_DbSetDirectionMock.Object);
            _DbMock.Setup(db => db.Direction).Returns(_DbSetDirectionMock.Object);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateNewEntity()
        {
            Direction direction = new Direction { Name = "C++", Description = "none" };
            var repo = new DirectionRepository(_DbMock.Object);
            await repo.CreateAsync(direction);
            _DbSetDirectionMock.Verify(db => db.AddAsync(It.IsAny<Direction>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task DeleteExistingElement(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return _DbSetList.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(_DbMock.Object);
            await repo.DeleteAsync(id);
            _DbSetDirectionMock.Verify(m => m.Remove(It.IsAny<Direction>()), Times.Once);
        }

        [Test]
        public void GetAllFromRepository()
        {
            var repo = new DirectionRepository(_DbMock.Object);
            var res = repo.GetAll();
            Assert.IsTrue(res.Count() == 4);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task GetByIdExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return _DbSetList.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(_DbMock.Object);
            var res = await repo.GetByIdAsync(id);
            Assert.AreEqual(_DbSetList.ElementAt(id - 1), res);
        }

        [Test]
        [TestCase(-300)]
        [TestCase(100)]
        public async System.Threading.Tasks.Task GetByIdNotExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => { return _DbSetList.SingleOrDefault(d => d.DirectionId == id); });
            var repo = new DirectionRepository(_DbMock.Object);
            var res = await repo.GetByIdAsync(id);
            Assert.IsNull(res);
        }
    }
}
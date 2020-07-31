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
using Task = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.DAL
{
    public class DirectionRepositoryTest
    {
        private Mock<DIMSCoreDatabaseContext> _DbMock;
        private Mock<DbSet<Direction>> _DbSetDirectionMock;
        private List<Direction> _DbSetList;
        private IDirectionRepository directionRepository;

        public DirectionRepositoryTest()
        {
            InitializeDbMock();
            directionRepository = new DirectionRepository(_DbMock.Object);
        }

        [Test]
        public async Task CreateNewEntity()
        {
            Direction direction = new Direction
            {
                DirectionId = 5,
                Name = "C++",
                Description = "none"
            };

            await directionRepository.CreateAsync(direction);
            Assert.NotNull(_DbSetDirectionMock.Object.FirstOrDefault(d=>d.DirectionId==direction.DirectionId));
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            await directionRepository.DeleteAsync(id);
            _DbSetDirectionMock.Verify(m => m.Remove(It.IsAny<Direction>()), Times.Once);
        }

        [Test]
        public void GetAllFromRepository()
        {
            var res = directionRepository.GetAll();
            Assert.IsTrue(res.Count() == _DbSetList.Count);
        }

        [Test]
        [TestCase(1)]
        public async Task GetByIdExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            var res = await directionRepository.GetByIdAsync(id);
            Assert.IsTrue(res.DirectionId == id);
        }

        [Test]
        [TestCase(-300)]
        [TestCase(100)]
        public async Task GetByIdNotExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            var res = await directionRepository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        private void InitializeDbMock()
        {
            _DbSetList = new List<Direction>()
            {
                new Direction
                {
                    DirectionId = 1,
                    Name = ".net",
                    Description = "none"
                },
                new Direction
                {
                    DirectionId = 2,
                    Name = "java",
                    Description = "none"
                },
                new Direction
                {
                    DirectionId = 3,
                    Name = "front-end",
                    Description = "none"
                },
                new Direction
                {
                    DirectionId = 4,
                    Name = "salesforce",
                    Description = "none"
                }
            };
            _DbSetDirectionMock = MockHelper.CreateDbSetMock<Direction>(_DbSetList);
            _DbMock = new Mock<DIMSCoreDatabaseContext>();
            _DbMock.Setup(db => db.Set<Direction>()).Returns(_DbSetDirectionMock.Object);
            _DbMock.Setup(db => db.Direction).Returns(_DbSetDirectionMock.Object);
        }
    }
}
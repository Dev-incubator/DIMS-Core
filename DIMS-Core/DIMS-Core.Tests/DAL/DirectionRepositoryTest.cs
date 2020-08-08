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
            Assert.NotNull(_DbSetDirectionMock.Object.FirstOrDefault(d => d.DirectionId == direction.DirectionId));
        }

        [Test]
        [TestCase(-1)]
        public void DeleteNotExistingElement(int id)
        {
            int prevCount = _DbSetDirectionMock.Object.Count();
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            Assert.DoesNotThrowAsync(async () => await directionRepository.DeleteAsync(id));
            Assert.AreEqual(prevCount, _DbSetDirectionMock.Object.Count());
        }

        [Test]
        public void GetAllFromRepository()
        {
            var res = directionRepository.GetAll();
            Assert.AreEqual(_DbSetDirectionMock.Object.Count(), res.Count());
        }

        [Test]
        [TestCase(2)]
        public async Task GetByIdExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            var res = await directionRepository.GetByIdAsync(id);
            Assert.AreEqual(id, res.DirectionId);
        }

        [Test]
        [TestCase(-1)]
        public async Task GetByIdNotExisting(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            var res = await directionRepository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            _DbSetDirectionMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.DirectionId == id));
            await directionRepository.DeleteAsync(id);
            Assert.IsNull(await _DbSetDirectionMock.Object.FindAsync(id));
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
        }
    }
}
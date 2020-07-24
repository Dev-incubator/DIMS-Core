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
using Task = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.DAL
{
    public class TaskStateRepositoryTest
    {
        private List<TaskState> _DbSetList;
        private Mock<DIMSCoreDatabaseContext> _DbMock;
        private Mock<DbSet<TaskState>> _DbSetTaskStateMock;

        [Test]
        public void GetAllFromRepository()
        {
            InitializeDbWithThreeObjects();
            var repository = new TaskStateRepository(_DbMock.Object);
            var res = repository.GetAll();
            Assert.IsTrue(res.Count() == 3);
        }

        [Test]
        public async Task CreateNewEntity()
        {
            InitializeDbWithThreeObjects();
            TaskState TaskState = new TaskState
            {
                StateName = "Other state"
            };
            var repository = new TaskStateRepository(_DbMock.Object);
            await repository.CreateAsync(TaskState);
            _DbSetTaskStateMock.Verify(db => db.AddAsync(It.IsAny<TaskState>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var repository = new TaskStateRepository(_DbMock.Object);
            await repository.DeleteAsync(id);
            _DbSetTaskStateMock.Verify(m => m.Remove(It.IsAny<TaskState>()), Times.Once);
        }

        [Test]
        [TestCase(-300)]
        public async Task GetByIdNotExisting(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var repository = new TaskStateRepository(_DbMock.Object);
            var res = await repository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async Task GetByIdExisting(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var repository = new TaskStateRepository(_DbMock.Object);
            var res = await repository.GetByIdAsync(id);
            Assert.AreEqual(_DbSetList.ElementAt(id - 1), res);
        }

        private void InitializeDbWithThreeObjects()
        {
            _DbSetList = new List<TaskState>()
            {
                new TaskState
                {
                    StateId = 1,
                    StateName = "Active"
                },
                new TaskState
                {
                    StateId = 2,
                    StateName = "Success"
                },
                new TaskState
                {
                    StateId = 3,
                    StateName = "Fail"
                },
            };
            _DbSetTaskStateMock = MockHelper.CreateDbSetMock<TaskState>(_DbSetList);
            _DbMock = new Mock<DIMSCoreDatabaseContext>();
            _DbMock.Setup(db => db.Set<TaskState>()).Returns(_DbSetTaskStateMock.Object);
            _DbMock.Setup(db => db.TaskState).Returns(_DbSetTaskStateMock.Object);
        }
    }
}
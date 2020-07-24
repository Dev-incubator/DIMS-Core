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
    public class TaskStateRepositoryTest
    {
        private List<TaskState> _DbSetList;
        private Mock<DIMSCoreDatabaseContext> _DbMock;
        private Mock<DbSet<TaskState>> _DbSetTaskStateMock;
        private ITaskStateRepository taskStateRepository;

        public TaskStateRepositoryTest()
        {
            InitializeDbMock();
            taskStateRepository = new TaskStateRepository(_DbMock.Object);
        }

        [Test]
        public void GetAllFromRepository()
        {
            var res = taskStateRepository.GetAll();
            Assert.IsTrue(res.Count() == _DbSetList.Count);
        }

        [Test]
        public async Task CreateNewEntity()
        {
            TaskState TaskState = new TaskState
            {
                StateName = "Other state"
            };

            await taskStateRepository.CreateAsync(TaskState);
            _DbSetTaskStateMock.Verify(db => db.AddAsync(It.IsAny<TaskState>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            await taskStateRepository.DeleteAsync(id);
            _DbSetTaskStateMock.Verify(m => m.Remove(It.IsAny<TaskState>()), Times.Once);
        }

        [Test]
        [TestCase(-300)]
        public async Task GetByIdNotExisting(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var res = await taskStateRepository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async Task GetByIdExisting(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var res = await taskStateRepository.GetByIdAsync(id);
            Assert.IsTrue(res.StateId == id);
        }

        private void InitializeDbMock()
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
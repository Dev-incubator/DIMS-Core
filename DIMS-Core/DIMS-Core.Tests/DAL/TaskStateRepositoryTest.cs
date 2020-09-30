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
            Assert.AreEqual(_DbSetTaskStateMock.Object.Count(), res.Count());
        }

        [Test]
        public async Task CreateNewEntity()
        {
            TaskState TaskState = new TaskState
            {
                StateId = 5,
                StateName = "Other state"
            };

            await taskStateRepository.CreateAsync(TaskState);
            Assert.NotNull(_DbSetTaskStateMock.Object.FirstOrDefault(ts => ts.StateId == TaskState.StateId));
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            await taskStateRepository.DeleteAsync(id);
            Assert.IsNull(_DbSetTaskStateMock.Object.FirstOrDefault(ts => ts.StateId == id));
        }

        [Test]
        [TestCase(-1)]
        public void DeleteNotExistingElement(int id)
        {
            int prevCount = _DbSetTaskStateMock.Object.Count();
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            Assert.DoesNotThrowAsync(async () => await taskStateRepository.DeleteAsync(id));
            Assert.AreEqual(prevCount, _DbSetTaskStateMock.Object.Count());
        }

        [Test]
        [TestCase(2)]
        public async Task GetByIdExisting(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var res = await taskStateRepository.GetByIdAsync(id);
            Assert.AreEqual(id, res.StateId);
        }

        [Test]
        [TestCase(-1)]
        public async Task GetByIdNotExisting(int id)
        {
            _DbSetTaskStateMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.StateId == id));
            var res = await taskStateRepository.GetByIdAsync(id);
            Assert.IsNull(res);
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
        }
    }
}
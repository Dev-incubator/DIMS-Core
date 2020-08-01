using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DAL.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Task = System.Threading.Tasks.Task;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;

namespace DIMS_Core.Tests.DAL
{
    public class TaskRepositoryTest
    {
        private List<TaskEntity> _DbSetList;
        private Mock<DIMSCoreDatabaseContext> _DbMock;
        private Mock<DbSet<TaskEntity>> _DbSetTaskMock;
        private ITaskRepository taskRepository;

        public TaskRepositoryTest()
        {
            InitializeDbMock();
            taskRepository = new TaskRepository(_DbMock.Object);
        }

        [Test]
        public void GetAllFromRepository()
        {
            var res = taskRepository.GetAll();
            Assert.AreEqual(_DbSetTaskMock.Object.Count(), res.Count());
        }

        [Test]
        public async Task CreateNewEntity()
        {
            TaskEntity Task = new TaskEntity
            {
                TaskId = 5,
                Name = "Write report",
                Description = "none",
                StartDate = DateTime.Parse("23.07.2020"),
                DeadlineDate = DateTime.Parse("24.07.2020")
            };

            await taskRepository.CreateAsync(Task);
            Assert.NotNull(_DbSetTaskMock.Object.FirstOrDefault(t => t.TaskId == Task.TaskId));
        }

        [Test]
        [TestCase(1)]
        public async Task DeleteExistingElement(int id)
        {
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            await taskRepository.DeleteAsync(id);
            Assert.IsNull(await _DbSetTaskMock.Object.FindAsync(id));
        }

        [Test]
        [TestCase(-1)]
        public void DeleteNotExistingElement(int id)
        {
            int prevCount = _DbSetTaskMock.Object.Count();
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            Assert.DoesNotThrowAsync(async () => await taskRepository.DeleteAsync(id));
            Assert.AreEqual(prevCount, _DbSetTaskMock.Object.Count());
        }

        [Test]
        [TestCase(1)]
        public async Task GetByIdExisting(int id)
        {
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            var res = await taskRepository.GetByIdAsync(id);
            Assert.AreEqual(id, res.TaskId);
        }

        [Test]
        [TestCase(-1)]
        public async Task GetByIdNotExisting(int id)
        {
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            var res = await taskRepository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        private void InitializeDbMock()
        {
            _DbSetList = new List<TaskEntity>()
            {
                new TaskEntity
                {
                    TaskId=1,
                    Name="Create Unit tests",
                    Description="none",
                    StartDate=DateTime.Parse("20.07.2020"),
                    DeadlineDate=DateTime.Parse("22.07.2020")
                },
                new TaskEntity
                {
                    TaskId=2,
                    Name="Implement DAL",
                    Description="none",
                    StartDate=DateTime.Parse("19.07.2020"),
                    DeadlineDate=DateTime.Parse("27.07.2020")
                },
                new TaskEntity
                {
                    TaskId=3,
                    Name="Implement BLL",
                    Description="none",
                    StartDate=DateTime.Parse("23.07.2020"),
                    DeadlineDate=DateTime.Parse("30.07.2020")
                }
            };
            _DbSetTaskMock = MockHelper.CreateDbSetMock<TaskEntity>(_DbSetList);
            _DbMock = new Mock<DIMSCoreDatabaseContext>();
            _DbMock.Setup(db => db.Set<TaskEntity>()).Returns(_DbSetTaskMock.Object);
            _DbMock.Setup(db => db.Task).Returns(_DbSetTaskMock.Object);
        }
    }
}
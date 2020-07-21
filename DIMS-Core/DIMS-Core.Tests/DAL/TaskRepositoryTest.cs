using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DAL.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DIMS_Core.Tests.DAL
{
    public class TaskRepositoryTest
    {
        private List<Task> _DbSetList;
        private Mock<DIMSCoreDatabaseContext> _DbMock;
        private Mock<DbSet<Task>> _DbSetTaskMock;

        [Test]
        public void GetAllFromRepository()
        {
            InitializeDbWithThreeObjects();
            var repository = new TaskRepository(_DbMock.Object);
            var res = repository.GetAll();
            Assert.IsTrue(res.Count() == 3);
        }

        [Test]
        public async System.Threading.Tasks.Task CreateNewEntity()
        {
            InitializeDbWithThreeObjects();
            Task Task = new Task
            {
                Name = "Write report",
                Description = "none",
                StartDate = DateTime.Parse("23.07.2020"),
                DeadlineDate = DateTime.Parse("24.07.2020")
            };
            var repository = new TaskRepository(_DbMock.Object);
            await repository.CreateAsync(Task);
            _DbSetTaskMock.Verify(db => db.AddAsync(It.IsAny<Task>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task DeleteExistingElement(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            var repository = new TaskRepository(_DbMock.Object);
            await repository.DeleteAsync(id);
            _DbSetTaskMock.Verify(m => m.Remove(It.IsAny<Task>()), Times.Once);
        }

        [Test]
        [TestCase(-300)]
        public async System.Threading.Tasks.Task GetByIdNotExisting(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            var repository = new TaskRepository(_DbMock.Object);
            var res = await repository.GetByIdAsync(id);
            Assert.IsNull(res);
        }

        [Test]
        [TestCase(1)]
        public async System.Threading.Tasks.Task GetByIdExisting(int id)
        {
            InitializeDbWithThreeObjects();
            _DbSetTaskMock.Setup(m => m.FindAsync(It.IsAny<int>())).ReturnsAsync(() => _DbSetList.SingleOrDefault(d => d.TaskId == id));
            var repository = new TaskRepository(_DbMock.Object);
            var res = await repository.GetByIdAsync(id);
            Assert.AreEqual(_DbSetList.ElementAt(id - 1), res);
        }

        private void InitializeDbWithThreeObjects()
        {
            _DbSetList = new List<Task>()
            {
                new Task
                {
                    TaskId=1,
                    Name="Create Unit tests",
                    Description="none",
                    StartDate=DateTime.Parse("20.07.2020"),
                    DeadlineDate=DateTime.Parse("22.07.2020")
                },
                new Task
                {
                    TaskId=2,
                    Name="Implement DAL",
                    Description="none",
                    StartDate=DateTime.Parse("19.07.2020"),
                    DeadlineDate=DateTime.Parse("27.07.2020")
                },
                new Task
                {
                    TaskId=3,
                    Name="Implement BLL",
                    Description="none",
                    StartDate=DateTime.Parse("23.07.2020"),
                    DeadlineDate=DateTime.Parse("30.07.2020")
                }
            };
            _DbSetTaskMock = MockHelper.CreateDbSetMock<Task>(_DbSetList);
            _DbMock = new Mock<DIMSCoreDatabaseContext>();
            _DbMock.Setup(db => db.Set<Task>()).Returns(_DbSetTaskMock.Object);
            _DbMock.Setup(db => db.Task).Returns(_DbSetTaskMock.Object);
        }
    }
}
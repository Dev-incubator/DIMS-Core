using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TaskThread = System.Threading.Tasks.Task;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using System;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class TaskRepositoryTests : RepositoryTestBase
    {
        private TaskRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new TaskRepository(context);
        }

        [Test]
        public void GetAll_ShouldReturn_AllTasks()
        {
            int countTasks = context.Task.Count();
            var result = repository.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetById_ShouldReturn_Task()
        {
            int getId = 2;
            const string returnName = "Write CRUD operations for Users";
            var result = await repository.GetById(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread Create_ShouldCreate_Task()
        {
            int newId = 4;
            var newTask = new TaskEntity()
            {
                TaskId = 4,
                Name = "Write CRUD operations for Directions",
                Description = "Write create, read, update and delete operations for Directions",
                StartDate = new DateTime(2020, 07, 30),
                DeadlineDate = new DateTime(2020, 12, 04)
            };
            await repository.Create(newTask);
            context.SaveChanges();
            var result = await repository.GetById(newId);         
            Assert.That(newTask, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread Update_ShouldUpdate_TaskName()
        {
            int updateId = 1;
            const string newName = "Create MainDatabase";
            var updateTask = await repository.GetById(updateId);  
            updateTask.Name = newName;
            repository.Update(updateTask);                            
            context.SaveChanges();
            var result = await repository.GetById(updateId);            
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread Delete_ShouldDelete_Task()
        {
            int deleteId = 3;
            await repository.Delete(deleteId);                          
            context.SaveChanges();
            var result = await repository.GetById(deleteId);   
            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

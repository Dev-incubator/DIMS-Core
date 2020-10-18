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

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class TaskRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            int countTasks = 3;
            var query = new TaskRepository(context);
            var result = query.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread ShouldReturnById()
        {
            int getId = 2;
            string returnName = "Write CRUD operations for Users";
            var query = new TaskRepository(context);
            var result = await query.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread ShouldAdd()
        {
            int newId = 4;
            var query = new TaskRepository(context);
            var newTask = new TaskEntity()
            {
                TaskId = 4,
                Name = "Write CRUD operations for Directions",
                Description = "Write create, read, update and delete operations for Directions",
                StartDate = new DateTime(2020, 07, 30),
                DeadlineDate = new DateTime(2020, 12, 04)
            };
            await query.CreateAsync(newTask);
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);         
            Assert.That(newTask, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread ShouldUpdate()
        {
            int updateId = 1;
            string newName = "Create MainDatabase";
            var query = new TaskRepository(context);
            var updateTask = await query.GetByIdAsync(updateId);  
            updateTask.Name = newName;
            query.Update(updateTask);                            
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread ShouldDelete()
        {
            int deleteId = 3;
            var query = new TaskRepository(context);
            await query.DeleteAsync(deleteId);                          
            context.SaveChanges();
            TaskEntity result = await query.GetByIdAsync(deleteId);   
            Assert.IsNull(result);
        }
    }
}

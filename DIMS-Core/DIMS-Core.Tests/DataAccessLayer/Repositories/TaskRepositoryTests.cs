using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Xunit;
using TaskThread = System.Threading.Tasks.Task;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using System;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    class TaskRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            int countTasks = 3;
            var query = new TaskRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.AreEqual(countTasks, result.Count());
        }

        [Test]
        public async TaskThread ShouldReturnById()
        {
            // Arrange
            int getId = 2;
            string returnName = "Write CRUD operations for Users";
            var query = new TaskRepository(context);

            // Act
            var result = await query.GetByIdAsync(getId);

            //Assert
            Assert.AreEqual(returnName, result.Name);
        }

        [Test]
        public async TaskThread ShouldAdd()
        {
            // Arrange
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

            // Act
            await query.CreateAsync(newTask);                      //add
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);               //get

            //Assert
            Assert.AreEqual(newTask, result);
        }

        [Test]
        public async TaskThread ShouldUpdate()
        {
            // Arrange
            int updateId = 1;
            string newName = "Create MainDatabase";

            var query = new TaskRepository(context);
            var updateTask = await query.GetByIdAsync(updateId);  //get
            updateTask.Name = newName;

            // Act
            query.Update(updateTask);                            //update
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            //get

            //Assert
            Assert.AreEqual(newName, result.Name);
        }

        [Test]
        public async TaskThread ShouldDelete()
        {
            // Arrange
            int deleteId = 3;
            var query = new TaskRepository(context);

            // Act
            await query.DeleteAsync(deleteId);                          //delete
            context.SaveChanges();
            TaskEntity result = await query.GetByIdAsync(deleteId);    //get

            //Assert
            Assert.IsNull(result);
        }
    }
}

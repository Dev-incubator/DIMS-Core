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
        private TaskRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new TaskRepository(Context);
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countTasks = Context.Task.Count();
            var result = query.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 2;
            const string returnName = "Write CRUD operations for Users";
            var result = await query.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByNegativeId_ValueIsNull()
        {
            int getId = -1;
            var result = await query.GetByIdAsync(getId);
            Assert.That(result, Is.Null);
        }

        [Test]
        public async TaskThread CreateAsync_CreatingWithNotExistingId_CreatedSuccessfull()
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
            await query.CreateAsync(newTask);
            Context.SaveChanges();
            var result = await query.GetByIdAsync(newId);         
            Assert.That(newTask, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread CreateAsync_TryAddNull_NothingCreated()
        {
            int countDirectionsBeforeAdding = Context.Task.Count();
            await query.CreateAsync(null);
            Context.SaveChanges();
            Assert.That(countDirectionsBeforeAdding, Is.EqualTo(Context.Task.Count()));
        }

        [Test]
        public async TaskThread Update_UpdateNameByExistingId_NameUpdated()
        {
            int updateId = 1;
            const string newName = "Create MainDatabase";
            var updateTask = await query.GetByIdAsync(updateId);  
            updateTask.Name = newName;
            query.Update(updateTask);                            
            Context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread Delete_DeleteByExistingId_DeletedItemEqualsNull()
        {
            int deleteId = 3;
            await query.DeleteAsync(deleteId);                          
            Context.SaveChanges();
            var result = await query.GetByIdAsync(deleteId);   
            Assert.IsNull(result);
        }

        [Test]
        public async TaskThread Delete_WithNegativeExistingIdShouldNo_ThrowError()
        {
            int deleteId = -3;
            await query.DeleteAsync(deleteId);
            Assert.That(query.DeleteAsync(deleteId), Throws.Nothing);
        }

        [Test]
        public async TaskThread Delete_WithNotExistingIdShouldNo_ThrowError()
        {
            int deleteId = 33;
            await query.DeleteAsync(deleteId);
            Assert.That(query.DeleteAsync(deleteId), Throws.Nothing);
        }
    }
}

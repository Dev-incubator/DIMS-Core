using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class TaskStateRepositoryTest : RepositoryTestBase
    {
        private TaskStateRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new TaskStateRepository(Context);
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countTasksStates = Context.TaskState.Count();
            var result = repository.GetAll();
            Assert.That(countTasksStates, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 2;
            const string returnName = "Design in progress";
            var result = await repository.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread CreateAsync_CreatingWithNotExistingId_CreatedSuccessfull()
        {
            int newId = 7;
            var newTaskState = new TaskState()
            {
                StateId = 7,
                StateName = "On check",
            };
            await repository.CreateAsync(newTaskState);          
            Context.SaveChanges();
            var result = await repository.GetByIdAsync(newId);             
            Assert.That(newTaskState, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread Update_UpdateNameByExistingId_NameUpdated()
        {
            int updateId = 1;
            const string newName = "In progress";
            var updateTask = await repository.GetByIdAsync(updateId);  
            updateTask.StateName = newName;
            repository.Update(updateTask);                          
            Context.SaveChanges();
            var result = await repository.GetByIdAsync(updateId);        
            Assert.That(newName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread Delete_DeleteByExistingId_DeletedItemEqualsNull()
        {
            int deleteId = 3;
            await repository.DeleteAsync(deleteId);                          
            Context.SaveChanges();
            TaskState result = await repository.GetByIdAsync(deleteId);     
            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

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
            repository = new TaskStateRepository(context);
        }

        [Test]
        public void GetAll_ShouldReturn_AllTaskStates()
        {
            int countTasksStates = context.TaskState.Count();
            var result = repository.GetAll();
            Assert.That(countTasksStates, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetById_ShouldReturn_Task()
        {
            int getId = 2;
            const string returnName = "Design in progress";
            var result = await repository.GetById(getId);
            Assert.That(returnName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread Create_ShouldCreate_TaskState()
        {
            int newId = 7;
            var newTaskState = new TaskState()
            {
                StateId = 7,
                StateName = "On check",
            };
            await repository.Create(newTaskState);          
            context.SaveChanges();
            var result = await repository.GetById(newId);             
            Assert.That(newTaskState, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread Update_ShouldUpdate_StateName()
        {
            int updateId = 1;
            const string newName = "In progress";
            var updateTask = await repository.GetById(updateId);  
            updateTask.StateName = newName;
            repository.Update(updateTask);                          
            context.SaveChanges();
            var result = await repository.GetById(updateId);        
            Assert.That(newName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread Delete_ShouldDelete_TaskState()
        {
            int deleteId = 3;
            await repository.Delete(deleteId);                          
            context.SaveChanges();
            TaskState result = await repository.GetById(deleteId);     
            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

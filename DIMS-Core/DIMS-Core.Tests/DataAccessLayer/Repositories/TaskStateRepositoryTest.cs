using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class TaskStateRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            int countTasksStates = 6;
            var query = new TaskStateRepository(context);
            var result = query.GetAll();
            Assert.That(countTasksStates, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread ShouldReturnById()
        {
            int getId = 2;
            string returnName = "Design in progress";
            var query = new TaskStateRepository(context);
            var result = await query.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread ShouldAdd()
        {
            int newId = 7;
            var query = new TaskStateRepository(context);
            var newTaskState = new TaskState()
            {
                StateId = 7,
                StateName = "On check",
            };
            await query.CreateAsync(newTaskState);          
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);             
            Assert.That(newTaskState, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread ShouldUpdate()
        {
            int updateId = 1;
            string newName = "In progress";
            var query = new TaskStateRepository(context);
            var updateTask = await query.GetByIdAsync(updateId);  
            updateTask.StateName = newName;
            query.Update(updateTask);                          
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);        
            Assert.That(newName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread ShouldDelete()
        {
            int deleteId = 3;
            var query = new TaskStateRepository(context);
            await query.DeleteAsync(deleteId);                          
            context.SaveChanges();
            TaskState result = await query.GetByIdAsync(deleteId);     
            Assert.IsNull(result);
        }
    }
}

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
            // Arrange
            int countTasksStates = 6;
            var query = new TaskStateRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.That(countTasksStates, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread ShouldReturnById()
        {
            // Arrange
            int getId = 2;
            string returnName = "Design in progress";
            var query = new TaskStateRepository(context);

            // Act
            var result = await query.GetByIdAsync(getId);

            //Assert
            Assert.That(returnName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread ShouldAdd()
        {
            // Arrange
            int newId = 7;
            var query = new TaskStateRepository(context);
            var newTaskState = new TaskState()
            {
                StateId = 7,
                StateName = "On check",
            };

            // Act
            await query.CreateAsync(newTaskState);                      //add
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);               //get

            //Assert
            Assert.That(newTaskState, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread ShouldUpdate()
        {
            // Arrange
            int updateId = 1;
            string newName = "In progress";

            var query = new TaskStateRepository(context);
            var updateTask = await query.GetByIdAsync(updateId);  //get
            updateTask.StateName = newName;

            // Act
            query.Update(updateTask);                            //update
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            //get

            //Assert
            Assert.That(newName, Is.EqualTo(result.StateName));
        }

        [Test]
        public async TaskThread ShouldDelete()
        {
            // Arrange
            int deleteId = 3;
            var query = new TaskStateRepository(context);

            // Act
            await query.DeleteAsync(deleteId);                          //delete
            context.SaveChanges();
            TaskState result = await query.GetByIdAsync(deleteId);      //get

            //Assert
            Assert.IsNull(result);
        }
    }
}

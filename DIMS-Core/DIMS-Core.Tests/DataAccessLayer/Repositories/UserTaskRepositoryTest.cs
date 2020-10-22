using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DataAccessLayer.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class UserTaskRepositoryTest : RepositoryTestBase
    {
        private UserTaskRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new UserTaskRepository(Context);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturn_Count()
        {
            int expected = Context.UserTask.Count();

            var actual = repository.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }


        [Test]
        public async Task GetByIdAsync_WithCorrectIdShouldReturn_UserTask()
        {
            int getId = 1;
            var expected = Context.UserTask.Find(getId);

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }


        [Test]
        public async Task GetByIdAsync_WithInvalidIdShouldReturn_Null()
        {
            int getId = 5;

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task GetByIdAsync_WithNegativeIdShouldReturn_Null()
        {
            int getId = -1;

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WithCorrectIdShouldReturn_UserTask()
        {
            int newId = 4;
            var expected = CreateNewUserTask(newId);

            await repository.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(newId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void CreateAsync_WithNullEntryShouldReturn_NotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await repository.CreateAsync(null));
        }


        [Test]
        public async Task CreateAsync_WithNegativeIdShouldReturn_Null()
        {
            int newId = -1;
            var expected = CreateNewUserTask(newId);

            await repository.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(newId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_WithCorrectIdShouldReturn_UserTask()
        {
            int updateId = 1;
            var newUserTaskState = Context.TaskState.Find(2);
            var expected = Context.UserTask.Find(updateId);
            expected.State = newUserTaskState;

            repository.Update(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task Update_WithInvalidIdShouldReturn_Null()
        {
            int getId = 1;
            int updateId = 5;
            var expected = Context.TaskState.Find(2);
            var updateUserTask = Context.UserTask.Find(getId);
            updateUserTask.State = expected;

            repository.Update(updateUserTask);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_WithNegativeIdShouldReturn_Null()
        {
            int updateId = -1;
            int getId = 1;
            var expected = Context.TaskState.Find(2);
            var updateUserTask = Context.UserTask.Find(getId);
            updateUserTask.State = expected;

            repository.Update(updateUserTask);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithCorrectIdShouldReturn_Null()
        {
            int deleteId = 3;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithInvalidIdShouldReturn_Null()
        {
            int deleteId = 5;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithNegativeIdShouldReturn_Null()
        {
            int deleteId = -1;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }
    }
}

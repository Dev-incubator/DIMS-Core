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
        private UserTaskRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new UserTaskRepository(Context);
        }

        [Test]
        public void GetAll_ActualCount()
        {
            // Arrange
            int expected = Context.UserTask.Count();

            // Act
            var actual = query.GetAll();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }


        [Test]
        public async Task GetByIdAsync_Id1_UserTask()
        {
            // Arrange
            int getId = 1;
            var expected = Context.UserTask.Find(getId);

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }


        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            // Arrange
            int getId = 5;

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task CreateAsync_Id4_NewUserTask()
        {
            // Arrange
            int newId = 4;
            var expected = CreateNewUserTask(newId);

            // Act
            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void CreateAsync_Null_NotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await query.CreateAsync(null));
        }

        [Test]
        public async Task Update_Id1_NewState()
        {
            // Arrange
            int updateId = 1;
            var expected = Context.TaskState.Find(2);
            var updateUserTask = Context.UserTask.Find(updateId);
            updateUserTask.State = expected;

            // Act
            query.Update(updateUserTask);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual.State));
        }

        [Test]
        public async Task Update_Id5_IsNull()
        {
            // Arrange
            int getId = 1;
            int updateId = 5;
            var expected = Context.TaskState.Find(2);
            var updateUserTask = Context.UserTask.Find(getId);
            updateUserTask.State = expected;

            // Act
            query.Update(updateUserTask);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id3_IsNull()
        {
            // Arrange
            int deleteId = 3;

            // Act
            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id5_IsNull()
        {
            // Arrange
            int deleteId = 5;

            // Act
            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(actual);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }

    }
}

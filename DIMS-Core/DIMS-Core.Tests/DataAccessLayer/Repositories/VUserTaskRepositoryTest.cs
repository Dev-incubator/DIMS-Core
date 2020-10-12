using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class VUserTaskRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAllSearch()
        {
            // Arrange
            int countUserTasks = 3;
            var query = new VUserTaskRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.Equals(countUserTasks, result.Count());
        }

        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            int countUserTasks = 3;
            var query = new VUserTaskRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.Equals(countUserTasks, result.Count());
        }

        [Test]
        public void ShouldReturnById()
        {
            // Arrange
            int getId = 1;
            string returnTaskName = "Create database";
            var query = new VUserTaskRepository(context);

            // Act
            var result = query.GetByIdAsync(getId);

            //Assert
            Assert.Equals(returnTaskName, result.Result.TaskName);
        }
    }
}

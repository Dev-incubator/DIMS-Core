using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VUserTaskRepositoryTest : RepositoryTestBase
    {
        private VUserTaskRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new VUserTaskRepository(Context);
        }

        [Test]
        public void GetAll_ShouldReturn_VUserTasks()
        {
            int countUserTasks = Context.VUserTask.Count();
            var result = repository.GetAll();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetById_ShouldReturn_VUserTask()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = repository.GetById(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.TaskName));
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

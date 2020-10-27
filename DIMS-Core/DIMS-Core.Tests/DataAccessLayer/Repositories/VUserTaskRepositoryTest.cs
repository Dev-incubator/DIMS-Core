using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VUserTaskRepositoryTest : RepositoryTestBase
    {
        private VUserTaskRepository repository;

        [OneTimeSetUp]
        public void InitQuery()
        {
            repository = new VUserTaskRepository(Context);
        }

        [Test]
        public void Search_SearchAllItems_GetActualCountOfItems()
        {
            int countUserTasks = Context.VUserTask.Count();
            var result = repository.Search();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countUserTasks = Context.VUserTask.Count();
            var result = repository.GetAll();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = await repository.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.TaskName));
        }

        [Test]
        public async Task GetById_WithNegativeIdShouldReturn_Null()
        {
            int getId = -1;
            var result = await repository.GetByIdAsync(getId);
            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void ClearRepository()
        {
            repository.Dispose();
        }
    }
}

using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VTaskRepositoryTests : RepositoryTestBase
    {
        private VTaskRepository query;
        
        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new VTaskRepository(Context);
        }

        [Test]
        public void Search_SearchAllItems_GetActualCountOfItems()
        {
            int countTasks = Context.VTask.Count();
            var result = query.Search();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countTasks = Context.VTask.Count(); ;
            var result = query.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.Name));
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

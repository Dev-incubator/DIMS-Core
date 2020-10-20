using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VTaskRepositoryTests : RepositoryTestBase
    {
        private readonly VTaskRepository query;

        private VTaskRepositoryTests()
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
        public async Task GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = await query.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Name));
        }

        [Test]
        public async Task GetByIdAsync_GetItemByNegativeId_ValueIsNull()
        {
            int getId = -1;
            var result = await query.GetByIdAsync(getId);
            Assert.That(result, Is.Null);
        }
    }
}

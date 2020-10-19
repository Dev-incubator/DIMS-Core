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
        private readonly VUserTaskRepository query;

        private VUserTaskRepositoryTest()
        {
            query = new VUserTaskRepository(Context);
        }

        [Test]
        public void Search_SearchAllItems_GetActualCountOfItems()
        {
            int countUserTasks = Context.VUserTask.Count();
            var result = query.Search();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countUserTasks = Context.VUserTask.Count();
            var result = query.GetAll();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.TaskName));
        }

        [Test]
        public void GetByIdAsync_GetItemByNegativeId_ValueIsNull()
        {
            int getId = -1;
            var result = query.GetByIdAsync(getId);
            Assert.That(result, Is.Null);
        }
    }
}

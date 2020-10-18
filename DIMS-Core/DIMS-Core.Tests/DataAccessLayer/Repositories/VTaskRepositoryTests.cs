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
        private readonly VTaskRepository query;

        private VTaskRepositoryTests()
        {
            query = new VTaskRepository(context);
        }

        [Test]
        public void ShouldReturnAllSearch()
        {
            int countTasks = 3;
            var result = query.Search();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnAll()
        {
            int countTasks = 3;
            var result = query.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnById()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.Name));
        }
    }
}

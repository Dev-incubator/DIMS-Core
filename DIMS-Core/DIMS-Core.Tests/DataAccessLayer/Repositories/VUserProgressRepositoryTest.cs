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
    class VUserProgressRepositoryTest : RepositoryTestBase
    {
        private VUserProgressRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new VUserProgressRepository(Context);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturn_Count()
        {
            int expected = Context.VUserProgress.Count();

            var actual = repository.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public void Search_ShouldReturn_Count()
        {
            int expected = Context.VUserProgress.Count();

            var actual = repository.Search();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_WithCorrectIdShouldReturn_VUserProgress()
        {
            int getId = 1;
            var expected = Context.VUserProgress.Find(getId);

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
    }
}

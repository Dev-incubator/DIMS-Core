using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DataAccessLayer.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class VUserProfileRepositoryTests : RepositoryTestBase
    {
        private VUserProfileRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new VUserProfileRepository(Context);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturn_Count()
        {
            int expected = Context.UserProfile.Count();

            var actual = repository.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public void Search_ShouldReturn_Count()
        {
            int expected = Context.UserProfile.Count();

            var actual = repository.Search();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync__WithCorrectIdShouldReturn_VUserProfile()
        {
            int getId = 1;
            var expected = Context.VUserProfile.Find(getId);

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

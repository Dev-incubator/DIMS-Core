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
        private VUserProfileRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new VUserProfileRepository(Context);
        }

        [Test]
        public void GetAll_ActualCount()
        {
            int expected = Context.UserProfile.Count();

            var actual = query.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public void Search_ActualCount()
        {
            int expected = Context.UserProfile.Count();

            var actual = query.Search();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_VUserProfile()
        {
            int getId = 1;
            var expected = Context.VUserProfile.Find(getId);

            var actual = await query.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            int getId = 5;

            var actual = await query.GetByIdAsync(getId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            int getId = -1;

            var actual = await query.GetByIdAsync(getId);

            Assert.IsNull(actual);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

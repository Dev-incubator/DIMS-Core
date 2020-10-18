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
            // Arrange
            int expected = Context.UserProfile.Count();

            // Act
            var actual = query.GetAll();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public void Search_ActualCount()
        {
            // Arrange
            int expected = Context.UserProfile.Count();

            // Act
            var actual = query.Search();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_VUserProfile()
        {
            // Arrange
            int getId = 1;
            var expected = Context.VUserProfile.Find(getId);

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            // Arrange
            int getId = 5;

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            // Arrange
            int getId = -1;

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.IsNull(actual);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

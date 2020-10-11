using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Tests
{
    public class VUserProfileRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAllSearch()
        {
            // Arrange
            int countUsers = 3;
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.AreEqual(countUsers, result.Count());
        }

        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            int countUsers = 3;
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.AreEqual(countUsers, result.Count());
        }

        [Test]
        public async Task ShouldReturnById()
        {
            // Arrange
            int getId = 1;
            var query = new VUserProfileRepository(context);

            // Act
            var result = await query.GetByIdAsync(getId);

            //Assert
            Assert.AreEqual("Elisey Butko", result.FullName);
        }
    }
}

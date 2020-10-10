using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using NUnit.Framework;

namespace DIMS_Core.DataAccessLayer.Tests
{
    public class VUserProfileRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void ShouldReturnById()
        {
            // Arrange
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.GetByIdAsync(1);

            //Assert
            Assert.AreEqual("Elisey Butko", result.Result.FullName);
        }
    }
}

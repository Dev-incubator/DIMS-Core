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
        private VUserProgressRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new VUserProgressRepository(Context);
        }

        [Test]
        public void GetAll_ActualCount()
        {
            // Arrange
            int expected = Context.VUserProgress.Count();

            // Act
            var actual = query.GetAll();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }


        [Test]
        public void Search_ActualCount()
        {
            // Arrange
            int expected = Context.VUserProgress.Count();

            // Act
            var actual = query.Search();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_VUserProgress()
        {
            // Arrange
            int getId = 1;
            var expected = Context.VUserProgress.Find(getId);

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
    }
}

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
            int expected = Context.VUserProgress.Count();

            var actual = query.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }


        [Test]
        public void Search_ActualCount()
        {
            int expected = Context.VUserProgress.Count();

            var actual = query.Search();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_VUserProgress()
        {
            int getId = 1;
            var expected = Context.VUserProgress.Find(getId);

            var actual = await query.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            int getId = 5;

            var actual = await query.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            int getId = -1;

            var actual = await query.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }
    }
}

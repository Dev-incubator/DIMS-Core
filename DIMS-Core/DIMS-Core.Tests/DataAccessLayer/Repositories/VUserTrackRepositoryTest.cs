using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class VUserTrackRepositoryTest : RepositoryTestBase
    {
        private readonly VUserTrackRepository query;

        private VUserTrackRepositoryTest()
        {
            query = new VUserTrackRepository(context);
        }

        [Test]
        public void ShouldReturnAllSearch()
        {
            int countUserTracks = 3;
            var result = query.Search();
            Assert.That(countUserTracks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnAll()
        {
            int countUserTracks = 3;
            var result = query.GetAll();
            Assert.That(countUserTracks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnById()
        {
            int getId = 1;
            const string returnTrackNote = "Create table UserProfile";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTrackNote, Is.EqualTo(result.Result.TrackNote));
        }
    } 
}

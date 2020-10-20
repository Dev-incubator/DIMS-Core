using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VUserTrackRepositoryTest : RepositoryTestBase
    {
        private readonly VUserTrackRepository query;

        private VUserTrackRepositoryTest()
        {
            query = new VUserTrackRepository(Context);
        }

        [Test]
        public void Search_SearchAllItems_GetActualCountOfItems()
        {
            int countUserTracks = 3;
            var result = query.Search();
            Assert.That(countUserTracks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countUserTracks = 3;
            var result = query.GetAll();
            Assert.That(countUserTracks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 1;
            const string returnTrackNote = "Create table UserProfile";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTrackNote, Is.EqualTo(result.Result.TrackNote));
        }

        [Test]
        public async Task GetById_WithNegativeIdShouldReturn_Null()
        {
            int getId = -1;
            var result = await query.GetByIdAsync(getId);
            Assert.That(result, Is.Null);
        }
    } 
}

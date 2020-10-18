using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class DirectionRepositoryTest : RepositoryTestBase
    {
        private readonly DirectionRepository query;

        private DirectionRepositoryTest()
        {
            query = new DirectionRepository(Context);
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countDirections = Context.Direction.Count();
            var result = query.GetAll();
            Assert.That(countDirections, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 2;
            const string returnName = "FRONTEND";
            var result = await query.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread CreateAsync_CreatingWithNotExistingId_CreatedSuccessfull()
        {
            int newId = 4;
            var newDirection = new Direction()
            {
                DirectionId = 6,
                Name = "Javascript",
                Description = "1+'0' = 10",
            };
            await query.CreateAsync(newDirection);                      
            Context.SaveChanges();
            var result = await query.GetByIdAsync(newId);               
            Assert.That(newDirection, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread Update_UpdateNameByExistingId_NameWasUpdated()
        {
            int updateId = 1;
            const string newName = "---";
            var updateDirection = await query.GetByIdAsync(updateId);  
            updateDirection.Name = newName;
            query.Update(updateDirection);                            
            Context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);        
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread DeleteAsync_DeleteByExistingId_DeletedItemEqualsNull()
        {
            int deleteId = 3;
            await query.DeleteAsync(deleteId);                     
            Context.SaveChanges();
            Direction result = await query.GetByIdAsync(deleteId);    
            Assert.IsNull(result);
        }
    }
}

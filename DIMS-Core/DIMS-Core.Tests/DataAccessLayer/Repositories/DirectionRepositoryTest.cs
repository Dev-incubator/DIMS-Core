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
        private readonly DirectionRepository repository;

        private DirectionRepositoryTest()
        {
            repository = new DirectionRepository(Context);
        }

        [Test]
        public void GetAll_GetAllItems_GetActualCountOfItems()
        {
            int countDirections = Context.Direction.Count();
            var result = repository.GetAll();
            Assert.That(countDirections, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByExistingId_ItemFound()
        {
            int getId = 2;
            const string returnName = "FRONTEND";
            var result = await repository.GetByIdAsync(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread GetByIdAsync_GetItemByNegativeId_ValueIsNull()
        {
            int getId = -1;
            var result = await repository.GetByIdAsync(getId);
            Assert.IsNull(result);
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
            await repository.CreateAsync(newDirection);                      
            Context.SaveChanges();
            var result = await repository.GetByIdAsync(newId);               
            Assert.That(newDirection, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread CreateAsync_TryAddNull_NothingCreated()
        {
            int countDirectionsBeforeAdding = Context.Direction.Count();
            await repository.CreateAsync(null);
            Context.SaveChanges();
            Assert.That(countDirectionsBeforeAdding, Is.EqualTo(Context.Direction.Count()));
        }

        [Test]
        public async TaskThread Update_UpdateNameByExistingId_NameWasUpdated()
        {
            int updateId = 1;
            const string newName = "---";
            var updateDirection = await repository.GetByIdAsync(updateId);  
            updateDirection.Name = newName;
            repository.Update(updateDirection);                            
            Context.SaveChanges();
            var result = await repository.GetByIdAsync(updateId);        
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread DeleteAsync_DeleteByExistingId_DeletedItemEqualsNull()
        {
            int deleteId = 3;
            await repository.DeleteAsync(deleteId);                     
            Context.SaveChanges();
            Direction result = await repository.GetByIdAsync(deleteId);    
            Assert.That(result, Is.Null);
        }

        [Test]
        public async TaskThread Delete_WithNegativeExistingIdShouldNo_ThrowError()
        {
            int deleteId = -3;
            await repository.DeleteAsync(deleteId);
            Assert.That(repository.DeleteAsync(deleteId),Throws.Nothing);
        }

        [Test]
        public async TaskThread Delete_WithNotExistingIdShouldNo_ThrowError()
        {
            int deleteId = 33;
            await repository.DeleteAsync(deleteId);
            Assert.That(repository.DeleteAsync(deleteId), Throws.Nothing);
        }
    }
}

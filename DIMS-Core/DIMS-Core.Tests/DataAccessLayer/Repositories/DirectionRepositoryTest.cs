using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class DirectionRepositoryTest : RepositoryTestBase
    {
        private DirectionRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new DirectionRepository(context);
        }

        [Test]
        public void GetAll_ShouldReturn_AllDirections()
        {
            int countDirections = context.Direction.Count();
            var result = repository.GetAll();
            Assert.That(countDirections, Is.EqualTo(result.Count()));
        }

        [Test]
        public async TaskThread GetById_ShouldReturn_Direction()
        {
            int getId = 2;
            const string returnName = "FRONTEND";
            var result = await repository.GetById(getId);
            Assert.That(returnName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread Create_ShouldCreate_Direction()
        {
            int newId = 4;
            var newDirection = new Direction()
            {
                DirectionId = 6,
                Name = "Javascript",
                Description = "1+'0' = 10",
            };
            await repository.Create(newDirection);                      
            context.SaveChanges();
            var result = await repository.GetById(newId);               
            Assert.That(newDirection, Is.EqualTo(result));
        }

        [Test]
        public async TaskThread Update_ShouldUpdate_DirectionName()
        {
            int updateId = 1;
            const string newName = "---";
            var updateDirection = await repository.GetById(updateId);  
            updateDirection.Name = newName;
            repository.Update(updateDirection);                            
            context.SaveChanges();
            var result = await repository.GetById(updateId);        
            Assert.That(newName, Is.EqualTo(result.Name));
        }

        [Test]
        public async TaskThread Delete_ShouldDelete_Direction()
        {
            int deleteId = 3;
            await repository.Delete(deleteId);                     
            context.SaveChanges();
            Direction result = await repository.GetById(deleteId);    
            Assert.That(result, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

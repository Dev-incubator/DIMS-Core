using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Xunit;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class DirectionRepositoryTest : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            int countDirections = 5;
            var query = new DirectionRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.AreEqual(countDirections, result.Count());
        }

        [Test]
        public async TaskThread ShouldReturnById()
        {
            // Arrange
            int getId = 2;
            string returnName = "FRONTEND";
            var query = new DirectionRepository(context);

            // Act
            var result = await query.GetByIdAsync(getId);

            //Assert
            Assert.AreEqual(returnName, result.Name);
        }

        [Test]
        public async TaskThread ShouldAdd()
        {
            // Arrange
            int newId = 4;
            var query = new DirectionRepository(context);
            var newDirection = new Direction()
            {
                DirectionId = 6,
                Name = "Javascript",
                Description = "1+'0' = 10",
            };

            // Act
            await query.CreateAsync(newDirection);                      //add
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);               //get

            //Assert
            Assert.AreEqual(newDirection, result);
        }

        [Test]
        public async TaskThread ShouldUpdate()
        {
            // Arrange
            int updateId = 1;
            string newName = "---";

            var query = new DirectionRepository(context);
            var updateDirection = await query.GetByIdAsync(updateId);  //get
            updateDirection.Name = newName;

            // Act
            query.Update(updateDirection);                            //update
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            //get

            //Assert
            Assert.AreEqual(newName, result.Name);
        }

        [Test]
        public async TaskThread ShouldDelete()
        {
            // Arrange
            int deleteId = 3;
            var query = new DirectionRepository(context);

            // Act
            await query.DeleteAsync(deleteId);                          //delete
            context.SaveChanges();
            Direction result = await query.GetByIdAsync(deleteId);    //get

            //Assert
            Assert.IsNull(result);
        }
    }
}

using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.DataAccessLayer.Enums;
using DIMS_Core.Tests.DataAccessLayer.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UserProfile = DIMS_Core.DataAccessLayer.Entities.UserProfile;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class UserProfileRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void ShouldReturnAll()
        {
            // Arrange
            int countUsers = 3;
            var query = new UserProfileRepository(context);

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
            string returnName = "Elisey";
            var query = new UserProfileRepository(context);

            // Act
            var result = await query.GetByIdAsync(getId);

            //Assert
            Assert.AreEqual(returnName, result.Name);
        }

        [Test]
        public async Task ShouldAdd()
        {
            // Arrange
            int newId = 4;
            var query = new UserProfileRepository(context);
            var newUserProfile = new UserProfile() {
                UserId = newId,
                Name = "Harry",
                LastName = "Soloviev",
                Email = "harry.soloviev@gmail.com",
                DirectionId = 1,
                Sex = Sex.Female,
                Education = "БГУИР",
                BirthOfDate = new DateTime(1999, 09, 11),
                UniversityAverageScore = 7,
                MathScore = 8,
                Address = "Минск, ул. Михалово 1, 50",
                MobilePhone = "375254440608",
                Skype = "harrysoloviev",
                StartDate = new DateTime(2020, 09, 25)
            };

            // Act
            await query.CreateAsync(newUserProfile);                    //add
            context.SaveChanges();
            var result = await query.GetByIdAsync(newId);               //get

            //Assert
            Assert.AreEqual(newUserProfile, result);
        }

        [Test]
        public async Task ShouldUpdate()
        {
            // Arrange
            int updateId = 1;
            string newName = "Igor";

            var query = new UserProfileRepository(context);
            var updateUserProfile = await query.GetByIdAsync(updateId);  //get
            updateUserProfile.Name = newName;

            // Act
            query.Update(updateUserProfile);                            //update
            context.SaveChanges();
            var result = await query.GetByIdAsync(updateId);            //get

            //Assert
            Assert.AreEqual(newName, result.Name);
        }

        [Test]
        public async Task ShouldDelete()
        {
            // Arrange
            int deleteId = 3;
            var query = new UserProfileRepository(context);

            // Act
            await query.DeleteAsync(deleteId);                          //delete
            context.SaveChanges();
            UserProfile result = await query.GetByIdAsync(deleteId);    //get

            //Assert
            Assert.IsNull(result);
        }
    }
}

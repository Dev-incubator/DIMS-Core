using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.DataAccessLayer.Enums;
using DIMS_Core.Tests.DataAccessLayer.Infrastructure;
using System.Linq;
using System.Linq.Dynamic.Core;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using UserProfile = DIMS_Core.DataAccessLayer.Entities.UserProfile;
using MimeKit.Cryptography;
using System.Reflection;
using System.Diagnostics;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class UserProfileRepositoryTests : RepositoryTestBase
    {
        private UserProfileRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new UserProfileRepository(Context);
        }

        [Test]
        public void GetAll_ActualCount()
        {
            // Arrange
            int expected = Context.UserProfile.Count();

            // Act
            var actual = query.GetAll();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_UserProfile()
        {
            // Arrange
            int getId = 1;
            var expected = Context.UserProfile.Find(getId);

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

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            // Arrange
            int getId = -1;

            // Act
            var actual = await query.GetByIdAsync(getId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task CreateAsync_Id4_NewUserProfile()
        {
            // Arrange
            int newId = 4;
            var expected = CreateNewUserProfile(newId);

            // Act
            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void CreateAsync_Null_NotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await query.CreateAsync(null));
        }

        [Test]
        public async Task CreateAsync_IdNegative_IsNull()
        {
            // Arrange
            int newId = -1;
            var expected = CreateNewUserProfile(newId);

            // Act
            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_Id1_NewName()
        {
            // Arrange
            int updateId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(updateId);
            updateUserProfile.Name = expected;

            // Act
            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Name));
        }

        [Test]
        public async Task Update_Id5_IsNull()
        {
            // Arrange
            int updateId = 5;
            int getId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            // Act
            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_IdNegative_IsNull()
        {
            // Arrange
            int updateId = -1;
            int getId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            // Act
            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id3_IsNull()
        {
            // Arrange
            int deleteId = 3;

            // Act
            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id5_IsNull()
        {
            // Arrange
            int deleteId = 5;

            // Act
            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_IdNegative_IsNull()
        {
            // Arrange
            int deleteId = -1;

            // Act
            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            // Assert
            Assert.IsNull(actual);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

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
        private UserProfileRepository repository;

        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new UserProfileRepository(Context);
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }

        [Test]
        public void GetAll_ShouldReturn_Count()
        {
            int expected = Context.UserProfile.Count();

            var actual = repository.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_WithCorrectIdShouldReturn_UserProfile()
        {
            int getId = 1;
            var expected = Context.UserProfile.Find(getId);

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_WithInvalidIdShouldReturn_Null()
        {
            int getId = 5;

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task GetByIdAsync_WithNegativeIdShouldReturn_Null()
        {
            int getId = -1;

            var actual = await repository.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task CreateAsync_WithCorrectIdShouldReturn_UserProfile()
        {
            int newId = 4;
            var expected = CreateNewUserProfile(newId);

            await repository.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(newId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void CreateAsync_WithNullEntryShouldReturn_NotThrow()
        {
            Assert.DoesNotThrowAsync(async () => await repository.CreateAsync(null));
        }

        [Test]
        public async Task CreateAsync_WithNegativeIdShouldReturn_Null()
        {
            int newId = -1;
            var expected = CreateNewUserProfile(newId);

            await repository.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(newId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_WithCorrectIdShouldReturn_UserProfile()
        {
            int updateId = 1;
            const string newUserProfileName = "New name";
            var expected = Context.UserProfile.Find(updateId);
            expected.Name = newUserProfileName;

            repository.Update(expected);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task Update_WithInvalidIdShouldReturn_Null()
        {
            int updateId = 5;
            int getId = 1;
            const string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            repository.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_WithNegativeIdShouldReturn_Null()
        {
            int updateId = -1;
            int getId = 1;
            const string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            repository.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithCorrectIdShouldReturn_Null()
        {
            int deleteId = 3;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithInvalidIdShouldReturn_Null()
        {
            int deleteId = 5;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WithNegativeIdShouldReturn_Null()
        {
            int deleteId = -1;

            await repository.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await repository.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }
    }
}

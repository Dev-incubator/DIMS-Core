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
            int expected = Context.UserProfile.Count();

            var actual = query.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_UserProfile()
        {
            int getId = 1;
            var expected = Context.UserProfile.Find(getId);

            var actual = await query.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            int getId = 5;

            var actual = await query.GetByIdAsync(getId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            int getId = -1;

            var actual = await query.GetByIdAsync(getId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task CreateAsync_Id4_NewUserProfile()
        {
            int newId = 4;
            var expected = CreateNewUserProfile(newId);

            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

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
            int newId = -1;
            var expected = CreateNewUserProfile(newId);

            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_Id1_NewName()
        {
            int updateId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(updateId);
            updateUserProfile.Name = expected;

            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.That(expected, Is.EqualTo(actual.Name));
        }

        [Test]
        public async Task Update_Id5_IsNull()
        {
            int updateId = 5;
            int getId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_IdNegative_IsNull()
        {
            int updateId = -1;
            int getId = 1;
            string expected = "New name";
            var updateUserProfile = Context.UserProfile.Find(getId);
            updateUserProfile.Name = expected;

            query.Update(updateUserProfile);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id3_IsNull()
        {
            int deleteId = 3;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_Id5_IsNull()
        {
            int deleteId = 5;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.IsNull(actual);
        }

        [Test]
        public async Task DeleteAsync_IdNegative_IsNull()
        {
            int deleteId = -1;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.IsNull(actual);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

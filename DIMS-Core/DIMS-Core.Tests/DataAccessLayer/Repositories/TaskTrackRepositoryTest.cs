using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.DataAccessLayer.Infrastructure;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class TaskTrackRepositoryTest : RepositoryTestBase
    {
        private TaskTrackRepository query;

        [OneTimeSetUp]
        public void InitQuery()
        {
            query = new TaskTrackRepository(Context);
        }

        [Test]
        public void GetAll_ActualCount()
        {
            int expected = Context.TaskTrack.Count();

            var actual = query.GetAll();

            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_TaskTrack()
        {
            int getId = 1;
            var expected = Context.TaskTrack.Find(getId);

            var actual = await query.GetByIdAsync(getId);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public async Task GetByIdAsync_Id5_IsNull()
        {
            int getId = 5;

            var actual = await query.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task GetByIdAsync_IdNegative_IsNull()
        {
            int getId = -1;

            var actual = await query.GetByIdAsync(getId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task CreateAsync_Id4_NewTaskTrack()
        {
            int newId = 4;
            var expected = CreateNewTaskTrack(newId);

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
            var expected = CreateNewTaskTrack(newId);

            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_Id1_NewTrackNote()
        {
            int updateId = 1;
            const string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(updateId);
            updateTaskTrack.TrackNote = expected;

            query.Update(updateTaskTrack);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.That(expected, Is.EqualTo(actual.TrackNote));
        }

        [Test]
        public async Task Update_Id5_IsNull()
        {
            int updateId = 5;
            int getId = 1;
            string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(getId);
            updateTaskTrack.TrackNote = expected;

            query.Update(updateTaskTrack);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task Update_IdNegative_IsNull()
        {
            int updateId = -1;
            int getId = 1;
            string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(getId);
            updateTaskTrack.TrackNote = expected;

            query.Update(updateTaskTrack);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_Id3_IsNull()
        {
            int deleteId = 3;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_Id5_IsNull()
        {
            int deleteId = 5;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_IdNegative_IsNull()
        {
            int deleteId = -1;

            await query.DeleteAsync(deleteId);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(deleteId);

            Assert.That(actual, Is.Null);
        }

        [OneTimeTearDown]
        public void CleanupQuery()
        {
            query.Dispose();
        }
    }
}

﻿using DIMS_Core.DataAccessLayer.Repositories;
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
            // Arrange
            int expected = Context.TaskTrack.Count();

            // Act
            var actual = query.GetAll();

            // Assert
            Assert.That(expected, Is.EqualTo(actual.Count()));
        }

        [Test]
        public async Task GetByIdAsync_Id1_TaskTrack()
        {
            // Arrange
            int getId = 1;
            var expected = Context.TaskTrack.Find(getId);

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
        public async Task CreateAsync_Id4_NewTaskTrack()
        {
            // Arrange
            int newId = 4;
            var expected = CreateNewTaskTrack(newId);

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
            var expected = CreateNewTaskTrack(newId);

            // Act
            await query.CreateAsync(expected);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(newId);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public async Task Update_Id1_NewTrackNote()
        {
            // Arrange
            int updateId = 1;
            string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(updateId);
            updateTaskTrack.TrackNote = expected;

            // Act
            query.Update(updateTaskTrack);
            Context.SaveChanges();
            var actual = await query.GetByIdAsync(updateId);

            // Assert
            Assert.That(expected, Is.EqualTo(actual.TrackNote));
        }

        [Test]
        public async Task Update_Id5_IsNull()
        {
            // Arrange
            int updateId = 5;
            int getId = 1;
            string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(getId);
            updateTaskTrack.TrackNote = expected;

            // Act
            query.Update(updateTaskTrack);
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
            string expected = "New track note";
            var updateTaskTrack = Context.TaskTrack.Find(getId);
            updateTaskTrack.TrackNote = expected;

            // Act
            query.Update(updateTaskTrack);
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
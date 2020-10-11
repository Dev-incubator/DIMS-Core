﻿using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class VUserTrackRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public void ShouldReturnAllSearch()
        {
            // Arrange
            int countUserTracks = 3;
            var query = new VUserTrackRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.Equals(countUserTracks, result.Count());
        }

        [Fact]
        public void ShouldReturnAll()
        {
            // Arrange
            int countUserTracks = 3;
            var query = new VUserTrackRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.Equals(countUserTracks, result.Count());
        }

        [Fact]
        public void ShouldReturnById()
        {
            // Arrange
            int getId = 1;
            string returnTrackNote = "Create table UserProfile";
            var query = new VUserTrackRepository(context);

            // Act
            var result = query.GetByIdAsync(getId);

            //Assert
            Assert.Equals(returnTrackNote, result.Result.TrackNote);
        }
    } 
}

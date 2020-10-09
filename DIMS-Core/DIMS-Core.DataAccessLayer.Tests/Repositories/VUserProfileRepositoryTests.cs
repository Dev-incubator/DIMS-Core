using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.DataAccessLayer.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace DIMS_Core.DataAccessLayer.Tests
{
    public class VUserProfileRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public void ShouldReturnAll()
        {
            // Arrange
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ShouldReturnById()
        {
            // Arrange
            var query = new VUserProfileRepository(context);

            // Act
            var result = query.GetByIdAsync(1);

            //Assert
            Assert.Equal("Elisey Butko", result.Result.FullName);
        }
    }
}

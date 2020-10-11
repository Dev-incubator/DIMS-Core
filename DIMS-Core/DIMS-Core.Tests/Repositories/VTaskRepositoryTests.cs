using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.DataAccessLayer.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace DIMS_Core.Tests.Repositories
{
    public class VTaskRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public void ShouldReturnAll()
        {
            // Arrange
            var query = new VTaskRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.Equals(1, result.Count());
        }

        [Fact]
        public void ShouldReturnById()
        {
            // Arrange
            var query = new VTaskRepository(context);

            // Act
            var result = query.GetByIdAsync(1);

            //Assert
            Assert.Equals("Create database", result.Result.Name);
        }
    }
}

﻿using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VTaskRepositoryTests : RepositoryTestBase
    {
        [Fact]
        public void ShouldReturnAllSearch()
        {
            // Arrange
            int countTasks = 3;
            var query = new VTaskRepository(context);

            // Act
            var result = query.Search();

            //Assert
            Assert.Equals(countTasks, result.Count());
        }

        [Fact]
        public void ShouldReturnAll()
        {
            // Arrange
            int countTasks = 3;
            var query = new VTaskRepository(context);

            // Act
            var result = query.GetAll();

            //Assert
            Assert.Equals(countTasks, result.Count());
        }

        [Fact]
        public void ShouldReturnById()
        {
            // Arrange
            int getId = 1;
            string returnTaskName = "Create database";
            var query = new VTaskRepository(context);

            // Act
            var result = query.GetByIdAsync(getId);

            //Assert
            Assert.Equals(returnTaskName, result.Result.Name);
        }
    }
}
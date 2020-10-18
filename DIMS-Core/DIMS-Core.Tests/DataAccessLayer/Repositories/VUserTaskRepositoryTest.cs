﻿using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;


namespace DIMS_Core.Tests.DataAccessLayer.Repositories
{
    [TestFixture]
    public class VUserTaskRepositoryTest : RepositoryTestBase
    {
        private readonly VUserTaskRepository query;

        private VUserTaskRepositoryTest()
        {
            query = new VUserTaskRepository(context);
        }

        [Test]
        public void ShouldReturnAllSearch()
        {
            int countUserTasks = 3;
            var result = query.Search();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnAll()
        {
            int countUserTasks = 3;
            var result = query.GetAll();
            Assert.That(countUserTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void ShouldReturnById()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = query.GetByIdAsync(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.TaskName));
        }
    }
}

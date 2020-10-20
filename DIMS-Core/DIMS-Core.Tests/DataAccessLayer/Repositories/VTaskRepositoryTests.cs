﻿using DIMS_Core.DataAccessLayer.Repositories;
using DIMS_Core.Tests.Infrastructure;
using NUnit.Framework;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DIMS_Core.Tests.Repositories
{
    [TestFixture]
    public class VTaskRepositoryTests : RepositoryTestBase
    {
        private VTaskRepository repository;
        
        [OneTimeSetUp]
        public void InitRepository()
        {
            repository = new VTaskRepository(Context);
        }

        [Test]
        public void GetAll_ShouldReturn_AllVTasks()
        {
            int countTasks = Context.VTask.Count();
            var result = repository.GetAll();
            Assert.That(countTasks, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetById_ShouldReturn_VTask()
        {
            int getId = 1;
            const string returnTaskName = "Create database";
            var result = repository.GetById(getId);
            Assert.That(returnTaskName, Is.EqualTo(result.Result.Name));
        }

        [OneTimeTearDown]
        public void CleanupRepository()
        {
            repository.Dispose();
        }
    }
}

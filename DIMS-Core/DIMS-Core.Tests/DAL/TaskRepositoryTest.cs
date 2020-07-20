using System;
using System.Collections.Generic;
using System.Text;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using NUnit.Framework;
using Moq;

namespace DIMS_Core.Tests.DAL
{
    public class TaskRepositoryTest
    {
        private readonly ITaskRepository repo;
        public TaskRepositoryTest()
        {
            repo = new TaskRepository(new DIMSCoreDataBaseContext());
        }

        [Test]
        public void GetAllFromRepository()
        {
            var result = repo.GetAll();

            if (result == null)
            {
                Assert.Fail("Null returned from repository");
                return;
            }

            Assert.NotNull(result);
        }
    }
}

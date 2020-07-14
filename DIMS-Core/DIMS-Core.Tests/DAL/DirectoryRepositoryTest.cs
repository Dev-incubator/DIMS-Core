using System;
using System.Collections.Generic;
using System.Text;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Repositories;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.Tests.DAL
{
    public class DirectoryRepositoryTest
    {
        private readonly IDirectionRepository repo;
        public DirectoryRepositoryTest()
        {
            repo = new DirectionRepository(new DIMSCoreDatabaseContext());
        }

        [Test]
        public void GetAllFromRepository()
        {
            var result = repo.GetAll();
            Assert.NotNull(result);
            Assert.IsAssignableFrom(typeof(IEnumerable<Direction>), result);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public async System.Threading.Tasks.Task Delete(int id)
        {
            try
            {
                await repo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public async System.Threading.Tasks.Task GetByIdAsync(int id)
        {
            var res = await repo.GetByIdAsync(id);
            //Assert.IsNotNull(res);
            Assert.IsAssignableFrom(typeof(Direction), res);
        }
    }
}

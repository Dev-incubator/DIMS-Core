using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Tests.Infrastructure
{
    public class RepositoryTestBase : IDisposable
    {
        protected readonly DIMSCoreDatabaseContext context;

        public RepositoryTestBase()
        {
            var options = new DbContextOptionsBuilder<DIMSCoreDatabaseContext>()
                   .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                   .Options;

            context = new DIMSCoreDatabaseContext(options);
            context.Database.EnsureCreated();

            Seed(context);
        }

        private void Seed(DIMSCoreDatabaseContext context)
        {

            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}

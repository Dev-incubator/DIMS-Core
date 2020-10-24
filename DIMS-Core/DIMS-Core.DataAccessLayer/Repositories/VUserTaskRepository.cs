using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class VUserTaskRepository : Repository<VUserTask>, IVUserTaskRepository
    {
        public VUserTaskRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }
        public IQueryable<VUserTask> Search()
        {
            return GetAll();
        }
    }
}

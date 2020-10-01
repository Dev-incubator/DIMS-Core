using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class VUserTaskRepository : Repository<VUserTask>, IVUserTaskRepository
    {
        public VUserTaskRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }
    }
}

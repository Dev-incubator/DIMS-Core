using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IVTaskRepository : IRepository<VTask>
    {
        IQueryable<VTask> Search();
    }
}

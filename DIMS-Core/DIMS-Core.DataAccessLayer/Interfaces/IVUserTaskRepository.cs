using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IVUserTaskRepository : IRepository<VUserTask>
    {
        IQueryable<VUserTask> Search(UserTaskFilter filter);
    }
}

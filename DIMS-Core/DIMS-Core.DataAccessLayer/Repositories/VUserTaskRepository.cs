﻿using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Filters;
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
        public IQueryable<VUserTask> Search(UserTaskFilter filter)
        {
            var query = GetAll();

            if (filter is null)
            {
                return query;
            }

            if (filter.TaskId.HasValue && filter.TaskId.Value > 0)
            {
                query = query.Where(q => q.TaskId == filter.TaskId);
            }

            return query;
        }
    }
}

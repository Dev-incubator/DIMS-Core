﻿using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskRepository : Repository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public async Task<TaskEntity> GetWithIncludeById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await currentSet
                .Include(task => task.UserTask)
                .ThenInclude(userTask => userTask.User)
                .FirstOrDefaultAsync(x => x.TaskId == id);

            return entity;
        }
    }
}

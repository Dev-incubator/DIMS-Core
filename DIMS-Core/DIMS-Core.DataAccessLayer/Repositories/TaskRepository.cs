using DIMS_Core.DataAccessLayer.Context;
using TaskEntities = DIMS_Core.DataAccessLayer.Entities.Task;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DIMS_Core.DataAccessLayer.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskRepository : Repository<TaskEntities>, ITaskRepository
    {
        public TaskRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public async Task<TaskEntities> GetWithIncludeById(int id)
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

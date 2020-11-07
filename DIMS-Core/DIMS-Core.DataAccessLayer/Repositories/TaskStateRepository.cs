using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskStateRepository : Repository<TaskState>, ITaskStateRepository
    {
        public TaskStateRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public void SetActive(int userId, int taskId)
        {
            databaseContext.Database.ExecuteSqlRaw($"SetUserTaskAsActive {userId},{taskId}");
        }

        public void SetPause(int userId, int taskId)
        {
            databaseContext.Database.ExecuteSqlRaw($"SetUserTaskAsPause {userId},{taskId}");
        }

        public void SetSuccess(int userId, int taskId)
        {
            databaseContext.Database.ExecuteSqlRaw($"SetUserTaskAsSuccess {userId},{taskId}");
        }

        public void SetFail(int userId, int taskId)
        {
            databaseContext.Database.ExecuteSqlRaw($"SetUserTaskAsFail {userId},{taskId}");
        }
    }
}

using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskStateRepository : Repository<TaskState>, ITaskStateRepository
    {
        public TaskStateRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public void SetStatus(int userId, int taskId, int status)
        {
            string sql = string.Empty;
            switch (status)
            {
                case 1: sql = $"SetUserTaskAsActive {userId},{taskId}"; break;
                case 2: sql = $"SetUserTaskAsPause {userId},{taskId}"; break;
                case 3: sql = $"SetUserTaskAsSuccess {userId},{taskId}"; break;
                default: sql = $"SetUserTaskAsFail {userId},{taskId}"; break;
            }
            databaseContext.Database.ExecuteSqlRaw(sql);
        }
    }
}

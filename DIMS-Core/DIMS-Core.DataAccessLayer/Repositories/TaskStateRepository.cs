using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskStateEntity = DIMS_Core.DataAccessLayer.Entities.TaskState;
using TaskStateEnum = DIMS_Core.DataAccessLayer.Enums.TaskState;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class TaskStateRepository : Repository<TaskStateEntity>, ITaskStateRepository
    {
        public TaskStateRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public void SetState(int userId, int taskId, TaskStateEnum status)
        {
            string sql = string.Empty;
            switch (status)
            {
                case TaskStateEnum.Active: sql = $"SetUserTaskAsActive {userId},{taskId}"; break;
                case TaskStateEnum.Pause: sql = $"SetUserTaskAsPause {userId},{taskId}"; break;
                case TaskStateEnum.Success: sql = $"SetUserTaskAsSuccess {userId},{taskId}"; break;
                default: sql = $"SetUserTaskAsFail {userId},{taskId}"; break;
            }
            databaseContext.Database.ExecuteSqlRaw(sql);
        }
    }
}

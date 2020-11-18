using TaskStateEntity = DIMS_Core.DataAccessLayer.Entities.TaskState;
using TaskStateEnum = DIMS_Core.DataAccessLayer.Enums.TaskState;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface ITaskStateRepository : IRepository<TaskStateEntity>
    {
        void SetState(int userId, int taskId, TaskStateEnum status);
    }
}

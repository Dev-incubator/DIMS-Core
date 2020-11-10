using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface ITaskStateRepository : IRepository<TaskState>
    {
        void SetStatus(int userId, int taskId, int status);
    }
}

using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface ITaskStateRepository : IRepository<TaskState>
    {
        void SetActive(int userId, int taskId);
        void SetPause(int userId, int taskId);
        void SetSuccess(int userId, int taskId);
        void SetFail(int userId, int taskId);
    }
}

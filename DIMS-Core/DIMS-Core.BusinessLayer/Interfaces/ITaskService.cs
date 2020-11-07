using DIMS_Core.BusinessLayer.Models.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAll();

        Task<IEnumerable<MyTaskModel>> GetAllMyTask(int userId);

        Task<TaskModel> GetTask(int id);

        Task Create(TaskModel model);

        Task Delete(int id);

        Task Update(TaskModel model);

        Task SetTaskActive(int userTaskId);

        Task SetTaskPause(int userTaskId);

        Task SetTaskSuccess(int userTaskId);

        Task SetTaskFail(int userTaskId);
    }
}

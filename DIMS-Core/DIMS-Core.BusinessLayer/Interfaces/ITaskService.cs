using DIMS_Core.BusinessLayer.Models.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAll();

        Task<IEnumerable<CurrentTaskModel>> GetAllMyTask(int userId);

        Task<TaskModel> GetTask(int id);

        Task Create(TaskModel model);

        Task Delete(int id);

        Task Update(TaskModel model);

        Task SetTaskStatus(int userTaskId, int status);
    }
}

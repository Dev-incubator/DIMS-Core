using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskManager
    {
        Task CreateTask(TaskCreateModel model);

        Task UpdateTask(TaskEditModel model);

        Task DeleteTask(int id);

        Task<TaskEditModel> GetModel(int id);
    }
}
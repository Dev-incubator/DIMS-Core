using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.DataAccessLayer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAll();

        Task<TaskModel> GetTask(int id);

        Task Create(TaskModel model);

        Task Delete(int id);

        Task Update(TaskModel model);
    }
}

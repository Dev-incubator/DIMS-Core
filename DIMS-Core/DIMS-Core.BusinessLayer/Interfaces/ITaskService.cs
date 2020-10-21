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
        Task<IEnumerable<TaskModel>> SearchAsync();

        Task<TaskModel> GetTaskAsync(int id);

        Task CreateAsync(TaskModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(TaskModel model);
    }
}

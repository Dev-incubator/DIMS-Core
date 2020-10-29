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
        Task<IEnumerable<TaskModel>> Search();

        Task<TaskModel> GetTask(int id);

        Task Create(TaskModel model, List<MemberForTaskModel> AllMembers);

        Task Delete(int id);

        Task Update(TaskModel model, List<MemberForTaskModel> AllMembers);
    }
}

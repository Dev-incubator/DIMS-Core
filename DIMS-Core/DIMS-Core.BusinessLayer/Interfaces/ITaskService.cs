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

        Task<IEnumerable<MemberForTaskModel>> GetMembersAsync();

        Task<IEnumerable<MemberForTaskModel>> GetMembersForTaskAsync(int id);

        Task<TaskModel> GetTaskAsync(int id);

        Task CreateAsync(TaskModel model, List<MemberForTaskModel> members);

        Task DeleteAsync(int id);

        Task UpdateAsync(TaskModel model, List<MemberForTaskModel> members);
    }
}

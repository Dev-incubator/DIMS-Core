using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.DataAccessLayer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<VUserProfileModel>> GetAll();

        Task<UserProfileModel> GetMember(int id);

        Task<UserProfileModel> GetMemberByEmail(string email);

        Task Create(UserProfileModel model);

        Task Delete(int id);

        Task Update(UserProfileModel model);

        Task<IEnumerable<TaskModel>> GetTasksByUserId(int userId);
    }
}

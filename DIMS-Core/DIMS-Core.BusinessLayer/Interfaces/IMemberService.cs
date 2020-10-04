using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.DataAccessLayer.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<VUserProfileModel>> SearchAsync();

        Task<VUserProfileModel> GetMemberAsync(int id);

        Task CreateAsync(VUserProfileModel model);

        Task DeleteAsync(int id);

        Task UpdateAsync(VUserProfileModel model);
    }
}

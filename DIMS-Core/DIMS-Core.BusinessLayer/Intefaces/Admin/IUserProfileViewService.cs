using DIMS_Core.BusinessLayer.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces.Admin
{
    public interface IUserProfileViewService
    {
        Task<IEnumerable<VUserProfileModel>> GetAllUserProfileViews();
        Task<VUserProfileModel> GetUserProfileViewById(int userId);
        Task<VUserProfileModel> GetUserProfileViewByEmail(string email);
        Task<IEnumerable<VUserProfileModel>> GetUserProfileViewsByDirection(string direction);
    }
}

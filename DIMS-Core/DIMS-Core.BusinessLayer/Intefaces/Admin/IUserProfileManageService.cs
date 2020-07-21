using DIMS_Core.BusinessLayer.Models.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces.Admin
{
    public interface IUserProfileManageService
    {
        Task DeleteUserProfile(int userProfileId);
        Task UpdateUserProfile(UserProfileModel userModel);
        Task CreateUserProfile(UserProfileModel userModel);
    }
}

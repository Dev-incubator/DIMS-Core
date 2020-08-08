using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IVUserProfileService : IBasicCRUDService<VUserProfileModel>
    {
        Task<VUserProfileModel> GetUserProfileViewById(int userId);

        Task<VUserProfileModel> GetUserProfileViewByEmail(string email);

        Task<IEnumerable<VUserProfileModel>> GetUserProfileViewsByDirection(string direction);
    }
}
using DIMS_Core.BusinessLayer.Models.Account;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task RegistAsync(UserRegistModel model);

        Task DeleteAsync(int identityId);
    }
}
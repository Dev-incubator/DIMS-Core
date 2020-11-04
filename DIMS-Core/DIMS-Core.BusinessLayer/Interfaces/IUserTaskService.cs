using DIMS_Core.BusinessLayer.Models.UserTask;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IUserTaskService
    {
        Task<IEnumerable<UserTaskModel>> GetAllByUserId(int userId);
    }
}

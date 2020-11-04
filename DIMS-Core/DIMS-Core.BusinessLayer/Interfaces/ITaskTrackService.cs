using DIMS_Core.BusinessLayer.Models.TaskTrack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskTrackService
    {
        Task<IEnumerable<VUserTrackModel>> GetAllByUserId(int userId);

        Task Create(TaskTrackModel model);
    }
}

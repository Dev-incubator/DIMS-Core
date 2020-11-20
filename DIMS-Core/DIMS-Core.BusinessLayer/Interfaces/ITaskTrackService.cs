using DIMS_Core.BusinessLayer.Models.TaskTrack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskTrackService
    {
        Task<IEnumerable<VTaskTrackModel>> GetAllByUserId(int userId);

        Task<TaskTrackModel> GetTaskTrack(int id);

        VTaskTrackModel GetVTaskTrack(int id);

        Task Create(TaskTrackModel model);

        Task Delete(int id);

        Task Update(TaskTrackModel model);
    }
}

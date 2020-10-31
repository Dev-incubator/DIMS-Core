using DIMS_Core.BusinessLayer.Models.TaskTracks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface ITaskTracksService
    {
        Task<IEnumerable<VUserTrackModel>> GetAll();
    }
}

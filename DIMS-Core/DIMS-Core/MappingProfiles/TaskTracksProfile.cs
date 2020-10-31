using AutoMapper;
using DIMS_Core.BusinessLayer.Models.TaskTracks;
using DIMS_Core.Models.TaskTracks;

namespace DIMS_Core.MappingProfiles
{
    public class TaskTracksProfile : Profile
    {
        public TaskTracksProfile()
        {
            CreateMap<VUserTrackModel, TaskTracksViewModel>().ReverseMap();
        }
    }
}
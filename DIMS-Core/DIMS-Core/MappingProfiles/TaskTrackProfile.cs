using AutoMapper;
using DIMS_Core.BusinessLayer.Models.TaskTracks;
using DIMS_Core.Models.TaskTrack;

namespace DIMS_Core.MappingProfiles
{
    public class TaskTrackProfile : Profile
    {
        public TaskTrackProfile()
        {
            CreateMap<VUserTrackModel, TaskTrackViewModel>().ReverseMap();
        }
    }
}
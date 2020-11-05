using AutoMapper;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using DIMS_Core.Models.TaskTrack;

namespace DIMS_Core.MappingProfiles
{
    public class TaskTrackProfile : Profile
    {
        public TaskTrackProfile()
        {
            CreateMap<VTaskTrackModel, VTaskTrackViewModel>().ReverseMap();
            CreateMap<TaskTrackViewModel, TaskTrackModel>().ReverseMap();
            CreateMap<VUserTrackModel, TaskTrackViewModel>().ReverseMap();
        }
    }
}
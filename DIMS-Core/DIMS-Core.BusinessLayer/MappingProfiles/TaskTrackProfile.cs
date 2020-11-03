using AutoMapper;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class TaskTrackProfile : Profile
    {
        public TaskTrackProfile()
        {
            CreateMap<VUserTrack, VTaskTrackModel>().ReverseMap();
            CreateMap<TaskTrackModel, TaskTrack>().ReverseMap();
        }
    }
}

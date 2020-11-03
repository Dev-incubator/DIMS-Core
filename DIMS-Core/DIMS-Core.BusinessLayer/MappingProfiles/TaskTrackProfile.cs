using AutoMapper;
using DIMS_Core.BusinessLayer.Models.TaskTracks;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class TaskTrackProfile : Profile
    {
        public TaskTrackProfile()
        {
            CreateMap<VUserTrack, VUserTrackModel>().ReverseMap();
        }
    }
}

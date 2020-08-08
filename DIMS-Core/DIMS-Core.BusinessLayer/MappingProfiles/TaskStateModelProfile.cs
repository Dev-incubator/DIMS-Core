using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class TaskStateModelProfile : Profile
    {
        public TaskStateModelProfile()
        {
            CreateMap<TaskState, TaskStateModel>().ReverseMap();
        }
    }
}
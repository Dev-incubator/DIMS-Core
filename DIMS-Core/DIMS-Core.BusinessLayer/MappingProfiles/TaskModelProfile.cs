using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class TaskModelProfile : Profile
    {
        public TaskModelProfile()
        {
            CreateMap<TaskEditModel, TaskModel>().ReverseMap();
            CreateMap<TaskEditModel, TaskModel>().ReverseMap();
        }
    }
}
using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.Models.Task;

namespace DIMS_Core.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskModel, TaskViewModel>().ReverseMap();
        }
    }
}
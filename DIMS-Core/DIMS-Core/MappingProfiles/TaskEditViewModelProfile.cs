using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;

namespace DIMS_Core.MappingProfiles
{
    public class TaskEditViewModelProfile : Profile
    {
        public TaskEditViewModelProfile()
        {
            CreateMap<TaskModel, TaskEditViewModel>().ReverseMap();
        }
    }
}
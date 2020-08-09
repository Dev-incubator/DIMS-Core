using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class TaskModelProfile : Profile
    {
        public TaskModelProfile()
        {
            CreateMap<TaskEditModel, TaskModel>().ReverseMap();
            CreateMap<Task, TaskModel>().ReverseMap();
        }
    }
}
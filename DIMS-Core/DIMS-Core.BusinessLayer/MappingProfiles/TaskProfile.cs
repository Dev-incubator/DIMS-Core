using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<VTask, TaskModel>().ReverseMap();
            CreateMap<UserTaskModel, UserTask>().ReverseMap();

            CreateMap<Task, TaskModel>()
                .ForMember(x => x.SelectedMembers, x => x.MapFrom(x => x.UserTask));
            CreateMap<UserTask, int>().ConvertUsing(x => x.UserId);

            CreateMap<TaskModel, Task>()
                .ForMember(x => x.UserTask, x => x.Ignore());            
        }
    }
}

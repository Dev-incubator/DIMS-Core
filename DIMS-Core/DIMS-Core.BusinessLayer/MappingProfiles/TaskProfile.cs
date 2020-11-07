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
                .ForMember(x => x.SelectedMembers, x => x.MapFrom(y => y.UserTask));
            CreateMap<UserTask, int>().ConvertUsing(x => x.UserId);

            CreateMap<TaskModel, Task>()
                .ForMember(x => x.UserTask, x => x.Ignore());

            CreateMap<UserTask, MyTaskModel>()
                .ForMember(x => x.Status, x => x.MapFrom(y => y.State.StateName))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Task.Name))
                .ForMember(x => x.StartDate, x => x.MapFrom(y => y.Task.StartDate))
                .ForMember(x => x.DeadlineDate, x => x.MapFrom(y => y.Task.DeadlineDate));
        }
    }
}

using AutoMapper;

using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Task;
using DIMS_Core.Models.Member;

namespace DIMS_Core.MappingProfiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskModel, TaskViewModel>().ReverseMap();
            CreateMap<VUserProfileModel, MemberViewModel>().ReverseMap();
        }
    }
}
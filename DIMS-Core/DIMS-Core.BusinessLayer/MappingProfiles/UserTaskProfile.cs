using AutoMapper;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            CreateMap<UserTaskModel, UserTask>().ReverseMap();
        }
    }
}

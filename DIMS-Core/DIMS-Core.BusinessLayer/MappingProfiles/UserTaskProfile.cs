using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.Identity.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class UserTaskProfile : Profile
    {
        public UserTaskProfile()
        {
            CreateMap<UserTaskModel, UserTask>().ReverseMap();
        }
    }
}
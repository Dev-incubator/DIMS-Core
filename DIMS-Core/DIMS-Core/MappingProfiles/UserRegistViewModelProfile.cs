using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;

namespace DIMS_Core.MappingProfiles
{
    public class UserRegistViewModelProfile : Profile
    {
        public UserRegistViewModelProfile()
        {
            CreateMap<UserRegistViewModel, SignUpModel>();
            CreateMap<UserRegistViewModel, UserProfileModel>();
        }
    }
}
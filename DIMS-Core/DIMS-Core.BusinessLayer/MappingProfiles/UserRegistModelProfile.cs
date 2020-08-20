using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class UserRegistModelProfile : Profile
    {
        public UserRegistModelProfile()
        {
            CreateMap<UserRegistModel, SignUpModel>();
            CreateMap<UserRegistModel, UserProfileModel>();
        }
    }
}
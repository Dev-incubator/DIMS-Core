using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.Identity.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class UserIdentityProfile : Profile
    {
        public UserIdentityProfile()
        {
            CreateMap<SignUpModel, User>()
                .ForMember(opt => opt.Email, dist => dist.MapFrom(src => src.Email))
                .ForMember(opt => opt.UserName, dist => dist.MapFrom(src => src.Email));
        }
    }
}
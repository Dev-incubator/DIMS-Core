using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.Identity.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class SignUpModelProfile : Profile
    {
        public SignUpModelProfile()
        {
            CreateMap<User, SignUpModel>();
        }
    }
}
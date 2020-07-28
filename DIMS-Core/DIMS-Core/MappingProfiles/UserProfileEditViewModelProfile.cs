using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;

namespace DIMS_Core.MappingProfiles
{
    public class UserProfileEditViewModelProfile : Profile
    {
        public UserProfileEditViewModelProfile()
        {
            CreateMap<UserProfileModel, UserProfileEditViewModel>()
                .ForMember("FullName", dest => dest.MapFrom(src => src.LastName + " " + src.Name))
                .ForMember("FirstName", dest => dest.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
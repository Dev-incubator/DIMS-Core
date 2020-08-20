using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;

namespace DIMS_Core.MappingProfiles
{
    public class vUserProfileViewModelProfile : Profile
    {
        public vUserProfileViewModelProfile()
        {
            CreateMap<VUserProfileModel, vUserProfileViewModel>();
        }
    }
}
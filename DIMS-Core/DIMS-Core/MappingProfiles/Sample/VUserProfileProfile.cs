using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Admin;
using DIMS_Core.Models.Admin;

namespace DIMS_Core.MappingProfiles.Sample
{
    public class VUserProfileProfile : Profile
    {
        public VUserProfileProfile()
        {
            CreateMap<VUserProfileModel, VUserProfileViewModel>();
        }
    }
}
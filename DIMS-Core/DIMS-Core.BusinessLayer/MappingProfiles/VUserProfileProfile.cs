using AutoMapper;
using DIMS_Core.BusinessLayer.Models.User;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class VUserProfileProfile : Profile
    {
        public VUserProfileProfile()
        {
            CreateMap<VUserProfile, VUserProfileModel>();
        }
    }
}
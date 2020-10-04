using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;

namespace DIMS_Core.MappingProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<VUserProfileModel, MemberViewModel>().ReverseMap();
            CreateMap<AddMemberViewModel, UserProfileModel>().ReverseMap();
        }
    }
}
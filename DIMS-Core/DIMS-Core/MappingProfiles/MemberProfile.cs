using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;
using System.Linq;

namespace DIMS_Core.MappingProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<VUserProfileModel, MemberViewModel>().ReverseMap();
            CreateMap<VUserProfileModel, AddMemberViewModel>().ReverseMap();
            CreateMap<UserProfileModel, EditMemberViewModel>();
            CreateMap<UserProfileModel, MemberViewModel>().
                ForMember(x => x.FullName,
                    x => x.MapFrom(m => m.Name + " " + m.LastName));
        }
    }
}
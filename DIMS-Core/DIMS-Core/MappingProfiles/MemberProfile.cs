using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;
using System.Linq;

namespace DIMS_Core.MappingProfiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<UserProfileModel, MemberViewModel>()
                .ForMember(x => x.FullName, x => x.MapFrom(m => m.Name + " " + m.LastName));
            CreateMap<VUserProfileModel, MemberViewModel>().ReverseMap();
            CreateMap<UserProfileModel, AddMemberViewModel>().ReverseMap();
            CreateMap<UserProfileModel, EditMemberViewModel>().ReverseMap();
            CreateMap<AddMemberViewModel, SignUpModel>();
            CreateMap<UserProfileModel, DetailsMemberViewModel>()
                .ForMember(x => x.FullName, x => x.MapFrom(m => m.Name + " " + m.LastName))
                .ForMember(x => x.Direction, x => x.MapFrom(m => m.Direction.Name));

        }
    }
}
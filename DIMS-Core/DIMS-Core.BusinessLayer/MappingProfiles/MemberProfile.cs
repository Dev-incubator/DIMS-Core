using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<VUserProfileModel, VUserProfile>().ReverseMap();
        }
    }
}

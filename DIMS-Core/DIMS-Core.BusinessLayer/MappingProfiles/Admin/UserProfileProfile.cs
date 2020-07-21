using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Admin;
using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.MappingProfiles.Admin
{
    public class UserProfileProfile:Profile
    {
        public UserProfileProfile()
        {
            CreateMap<VUserProfile, VUserProfileModel>();
        }
    }
}

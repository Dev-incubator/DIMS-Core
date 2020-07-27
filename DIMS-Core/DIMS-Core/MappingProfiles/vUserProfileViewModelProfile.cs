using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.MappingProfiles
{
    public class vUserProfileViewModelProfile:Profile
    {
        public vUserProfileViewModelProfile()
        {
            CreateMap<VUserProfileModel, vUserProfileViewModelProfile>();
        }
    }
}

using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class VUserProgressModelProfile : Profile
    {
        public VUserProgressModelProfile()
        {
            CreateMap<VUserProgressModelProfile, VUserProgressModel>().ReverseMap();
        }
    }
}
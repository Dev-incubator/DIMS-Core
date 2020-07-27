using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class VUserProgressProfile : Profile
    {
        public VUserProgressProfile()
        {
            CreateMap<VUserProgressProfile, VUserProgressModel>().ReverseMap();
        }
    }
}
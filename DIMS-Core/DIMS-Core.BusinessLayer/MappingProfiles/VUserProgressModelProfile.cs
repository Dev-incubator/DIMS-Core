using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class VUserProgressModelProfile : Profile
    {
        public VUserProgressModelProfile()
        {
            CreateMap<VUserProgress, VUserProgressModel>().ReverseMap();
        }
    }
}
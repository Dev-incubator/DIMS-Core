using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class VTaskProfile : Profile
    {
        public VTaskProfile()
        {
            CreateMap<VTask, VTaskModel>().ReverseMap();
        }
    }
}
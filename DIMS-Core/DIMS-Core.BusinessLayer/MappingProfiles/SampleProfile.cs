using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.DataAccessLayer.Context;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    public class SampleProfile : Profile
    {
        public SampleProfile()
        {
            CreateMap<Sample, SampleModel>().ReverseMap();
        }
    }
}
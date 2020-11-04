using AutoMapper;
using DIMS_Core.BusinessLayer.Models.Direction;
using DIMS_Core.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.BusinessLayer.MappingProfiles
{
    internal class DirectionProfile : Profile
    {
        public DirectionProfile()
        {
            CreateMap<Direction, DirectionModel>().ReverseMap();
        }
    }
}

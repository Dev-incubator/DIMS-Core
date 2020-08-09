using AutoMapper;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models.ProgressModels;

namespace DIMS_Core.MappingProfiles
{
    public class CreateEditNoteViewModelProfile : Profile
    {
        public CreateEditNoteViewModelProfile()
        {
            CreateMap<CreateNoteViewModel, TaskTrackModel>().ReverseMap();
        }
    }
}
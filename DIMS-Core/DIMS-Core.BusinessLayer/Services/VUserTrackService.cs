using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class VUserTrackService : BasicCRUDService<VUserTrack, VUserTrackModel>, IVUserTrackService
    {
        public VUserTrackService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<VUserTrack> BaseRepository => unitOfWork.VUserTrackRepository;
    }
}
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class VUserProfileService : BasicCRUDService<VUserProfile, VUserProfileModel>, IVUserProfileService
    {
        private protected override IRepository<VUserProfile> BaseRepository => unitOfWork.VUserProfileRepository;

        public VUserProfileService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
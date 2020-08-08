using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class VUserProgressService : BasicCRUDService<VUserProgress, VUserProgressModel>, IVUserProgressService
    {
        public VUserProgressService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<VUserProgress> BaseRepository => unitOfWork.VUserProgressRepository;
    }
}
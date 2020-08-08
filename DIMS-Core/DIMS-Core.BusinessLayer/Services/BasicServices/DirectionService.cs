using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class DirectionService : BasicCRUDService<Direction, DirectionModel>, IDirectionService
    {
        public DirectionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<Direction> BaseRepository => unitOfWork.DirectionRepository;
    }
}
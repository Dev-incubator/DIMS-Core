using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class DirectionService : GenericCRUDService<Direction, DirectionModel>, IDirectionService
    {
        public DirectionService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<Direction> BaseRepository => unitOfWork.DirectionRepository;

        public async Task<IEnumerable<DirectionModel>> GetAll()
        {
            var directions = unitOfWork.DirectionRepository.GetAll();
            var directionModels = await mapper.ProjectTo<DirectionModel>(directions).ToListAsync();
            return directionModels;
        }
    }
}
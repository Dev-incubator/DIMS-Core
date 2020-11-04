using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Direction;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskThread = System.Threading.Tasks.Task;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class DirectionService : IDirectionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DirectionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async TaskThread Create(DirectionModel model)
        {
            if (model is null || model.DirectionId != 0)
            {
                return;
            }

            var direction = mapper.Map<Direction>(model);

            await unitOfWork.DirectionRepository.Create(direction);

            await unitOfWork.Save();
        }

        public async TaskThread Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.DirectionRepository.Delete(id);

            await unitOfWork.Save();
        }

        public async Task<IEnumerable<DirectionModel>> GetAll()
        {
            var directions = unitOfWork.DirectionRepository.GetAll();
            var directionModels = mapper.ProjectTo<DirectionModel>(directions);

            return await directionModels.ToListAsync();
        }

        public async Task<DirectionModel> GetDirection(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var direction = await unitOfWork.DirectionRepository.GetById(id);
            var directionModel = mapper.Map<DirectionModel>(direction);

            return directionModel;
        }

        public async TaskThread Update(DirectionModel model)
        {
            if (model is null || model.DirectionId <= 0)
            {
                return;
            }

            var direction = await unitOfWork.DirectionRepository.GetById(model.DirectionId);

            if (direction is null)
            {
                return;
            }

            var mappedDirection = mapper.Map(model, direction);

            unitOfWork.DirectionRepository.Update(mappedDirection);

            await unitOfWork.Save();
        }
    }
}

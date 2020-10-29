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

            var entity = mapper.Map<Direction>(model);

            await unitOfWork.DirectionRepository.Create(entity);

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
            var query = unitOfWork.DirectionRepository.GetAll();
            var mappedQuery = mapper.ProjectTo<DirectionModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public async Task<DirectionModel> GetDirection(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await unitOfWork.DirectionRepository.GetById(id);
            var model = mapper.Map<DirectionModel>(entity);

            return model;
        }

        public async TaskThread Update(DirectionModel model)
        {
            if (model is null || model.DirectionId <= 0)
            {
                return;
            }

            var entity = await unitOfWork.DirectionRepository.GetById(model.DirectionId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            unitOfWork.DirectionRepository.Update(mappedEntity);

            await unitOfWork.Save();
        }
    }
}

﻿using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public abstract class GenericCRUDService<TEntity, DefaultDTOModel> : IGenericCRUDService<DefaultDTOModel> where TEntity : class where DefaultDTOModel : BaseDTOModel
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        private protected abstract IRepository<TEntity> BaseRepository { get; }

        public GenericCRUDService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DefaultDTOModel> GetEntityModel(int id)
        {
            return await GetEntityModel<DefaultDTOModel>(id);
        }

        public async Task<TModel> GetEntityModel<TModel>(int id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await BaseRepository.GetByIdAsync(id);
            var model = mapper.Map<TModel>(entity);

            return model;
        }

        public async Task Create(DefaultDTOModel model)
        {
            await Create<DefaultDTOModel>(model);
        }

        public async Task Create<TModel>(TModel model)
        {
            if (model is null)
            {
                return;
            }

            var entity = mapper.Map<TEntity>(model);

            await BaseRepository.CreateAsync(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task Update(DefaultDTOModel model)
        {
            await Update<DefaultDTOModel>(model);
        }

        public async Task Update<DTOModel>(DTOModel model) where DTOModel : BaseDTOModel
        {
            if (model is null)
            {
                return;
            }

            var entity = await BaseRepository.GetByIdAsync(model.PKId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            BaseRepository.Update(mappedEntity);

            await unitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await BaseRepository.DeleteAsync(id);

            await unitOfWork.SaveAsync();
        }
    }
}
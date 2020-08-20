using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public abstract class BasicCRUDService<TEntity, DefaultDTOModel> : IBasicCRUDService<DefaultDTOModel> where TEntity : class where DefaultDTOModel : BaseDTOModel
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        private protected abstract IRepository<TEntity> BaseRepository { get; }

        public BasicCRUDService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<DefaultDTOModel> GetEntityModelAsync(int id)
        {
            return await GetEntityModelAsync<DefaultDTOModel>(id);
        }

        public async Task<TModel> GetEntityModelAsync<TModel>(int id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await BaseRepository.GetByIdAsync(id);
            var model = mapper.Map<TModel>(entity);

            return model;
        }

        public async Task CreateAsync(DefaultDTOModel model)
        {
            await CreateAsync<DefaultDTOModel>(model);
        }

        public async Task CreateAsync<TModel>(TModel model)
        {
            if (model is null)
            {
                return;
            }

            var entity = mapper.Map<TEntity>(model);

            await BaseRepository.CreateAsync(entity);

            await unitOfWork.SaveAsync();

            mapper.Map(entity, model);
        }

        public async Task UpdateAsync(DefaultDTOModel model)
        {
            await UpdateAsync<DefaultDTOModel>(model);
        }

        public async Task UpdateAsync<DTOModel>(DTOModel model) where DTOModel : BaseDTOModel
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

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await BaseRepository.DeleteAsync(id);

            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<DefaultDTOModel>> GetAllAsync()
        {
            return await GetAllAsync<DefaultDTOModel>();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var allEntities = BaseRepository.GetAll();
            var allModels = mapper.ProjectTo<T>(allEntities);

            return await allModels.ToListAsync();
        }
    }
}
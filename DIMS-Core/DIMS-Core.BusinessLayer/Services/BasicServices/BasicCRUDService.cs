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

        public async Task Create<DTOModel>(DTOModel model)
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

        public async Task<IEnumerable<DefaultDTOModel>> GetAll()
        {
            return await GetAll<DefaultDTOModel>();
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var allEntities = BaseRepository.GetAll();
            var allModels = mapper.ProjectTo<T>(allEntities);

            return await allModels.ToListAsync();
        }
    }
}
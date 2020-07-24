using AutoMapper;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.DataAccessLayer.Context;
using a = DIMS_Core.DataAccessLayer.Entities.VUserProfile;

namespace DIMS_Core.BusinessLayer.Services
{
    public abstract class GenericCRUDService<TEntity>:IGenericCRUDService where TEntity:class
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        private readonly IRepository<TEntity> baseRepository;

        public GenericCRUDService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            var property = unitOfWork.GetType().GetProperties().FirstOrDefault(p=>typeof(IRepository<TEntity>).IsAssignableFrom(p.PropertyType));
            baseRepository = (IRepository<TEntity>)property.GetValue(unitOfWork);
        }

        public Task Create<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete<TModel>(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TModel> GetEntityModel<TModel>(int id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await baseRepository.GetByIdAsync(id);
            var model = mapper.Map<TModel>(entity);

            return model;
        }

        public Task Update<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}

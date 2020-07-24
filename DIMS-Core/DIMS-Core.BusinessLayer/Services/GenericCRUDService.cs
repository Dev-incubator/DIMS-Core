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
        //string propName = nameof(unitOfWork.VUserProfileRepository);
        //var res = (IRepository<VUserProfile>)(unitOfWork.GetType().GetProperty(propName).GetValue(unitOfWork));
        //var r = res.GetAll().ToList();
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        private readonly IRepository<TEntity> repository;

        public GenericCRUDService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            var res = unitOfWork.GetType().GetProperties().FirstOrDefault(p=>typeof(IRepository<TEntity>).IsAssignableFrom(p.PropertyType));
            repository = (IRepository<TEntity>)res.GetValue(unitOfWork);
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

            var entity = await repository.GetByIdAsync(id);
            var model = mapper.Map<TModel>(entity);

            return model;
        }

        public Task Update<TModel>(TModel model)
        {
            throw new NotImplementedException();
        }
    }
}

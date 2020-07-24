using AutoMapper;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    abstract class GenericCRUDService<TEntity> where TEntity:class
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
            repository = (IRepository<TEntity>)unitOfWork.GetType().GetProperties().FirstOrDefault(p=>p.GetType()==typeof(IRepository<TEntity>)).GetValue(this.unitOfWork);
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
    }
}

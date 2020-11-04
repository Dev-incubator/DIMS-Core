using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class SampleService : ISampleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SampleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SampleModel>> Search(SampleFilter searchFilter)
        {
            var query = unitOfWork.SampleRepository.Search(searchFilter);
            var mappedQuery = mapper.ProjectTo<SampleModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public async Task<SampleModel> GetSample(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await unitOfWork.SampleRepository.GetById(id);
            var model = mapper.Map<SampleModel>(entity);

            return model;
        }

        public async Task Create(SampleModel model)
        {
            if (model is null || model.SampleId != 0)
            {
                return;
            }

            var entity = mapper.Map<Sample>(model);

            await unitOfWork.SampleRepository.Create(entity);

            await unitOfWork.Save();
        }

        public async Task Update(SampleModel model)
        {
            if (model is null || model.SampleId <= 0)
            {
                return;
            }

            var entity = await unitOfWork.SampleRepository.GetById(model.SampleId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            unitOfWork.SampleRepository.Update(mappedEntity);

            await unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.SampleRepository.Delete(id);

            await unitOfWork.Save();
        }
    }
}
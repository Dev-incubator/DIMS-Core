using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SampleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SampleModel>> SearchAsync(SampleFilter searchFilter)
        {
            if (searchFilter is null)
            {
                return Enumerable.Empty<SampleModel>();
            }

            var query = unitOfWork.SampleRepository.Search(searchFilter);
            var mappedQuery = mapper.ProjectTo<SampleModel>(query);

            return await mappedQuery.ToListAsync();
        }
    }
}
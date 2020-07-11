using DIMS_Core.Common.Enums;
using DIMS_Core.Common.Extensions;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class SampleRepository : Repository<Sample>, ISampleRepository
    {
        public SampleRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<Sample> Search(SampleFilter filter)
        {
            var query = GetAll();

            if (filter is null)
            {
                return query;
            }

            if (filter.Id.HasValue && filter.Id.Value > 0)
            {
                query = query.Where(q => q.SampleId == filter.Id);
            }

            if (!filter.Name.IsNullOrWhiteSpace())
            {
                query = query.Where(q => q.Name.Contains(filter.Name));
            }

            if (!filter.Description.IsNullOrWhiteSpace())
            {
                query = query.Where(q => q.Description.Contains(filter.Description));
            }

            query.SortAndTake(filter.SortExpression, filter.Page, filter.PageSize, q => q.SampleId, SortDirections.DESC);

            return query;
        }
    }
}
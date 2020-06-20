using DIMS_Core.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class SampleRepository : Repository<Sample>
    {
        public SampleRepository(DbContext context) : base(context)
        {
        }
    }
}
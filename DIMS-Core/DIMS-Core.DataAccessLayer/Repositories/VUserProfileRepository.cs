using System;
using System.Collections.Generic;
using System.Text;
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DIMS_Core.Common.Extensions;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class VUserProfileRepository:Repository<VUserProfile>, IVUserProfileRepository
    {
        public VUserProfileRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public async Task<VUserProfile> GetVUserProfileByEmail(string email)
        {
            if (email.IsNullOrWhiteSpace())
            {
                return null;
            }
            
            var entity = await currentSet.FirstAsync(e => e.Email == email);
            return entity;
        }

        public IQueryable<VUserProfile> GetVUserProfilesByDirection(string direction)
        {
            if (direction.IsNullOrWhiteSpace())
            {
                return null;
            }

            var query = GetAll();
            query = query.Where(o=>o.Direction==direction);
            return query;
        }
    }
}

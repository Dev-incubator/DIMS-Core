using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }

        public async Task<UserProfile> GetMemberByEmail(string email)
        {
            return await currentSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

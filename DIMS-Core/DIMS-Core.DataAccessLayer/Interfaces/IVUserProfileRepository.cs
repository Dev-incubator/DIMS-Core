using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIMS_Core.DataAccessLayer.Entities;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IVUserProfileRepository : IRepository<VUserProfile>
    {
        Task<VUserProfile> GetVUserProfileByEmail(string email);
        IQueryable<VUserProfile> GetVUserProfilesByDirection(string direction);
    }
}

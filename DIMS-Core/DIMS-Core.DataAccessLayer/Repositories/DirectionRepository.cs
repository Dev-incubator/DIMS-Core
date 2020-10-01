using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    public class DirectionRepository : Repository<Direction>, IDirectionRepository
    {
        public DirectionRepository(DIMSCoreDatabaseContext dbContext) : base(dbContext) { }
    }
}

using DIMS_Core.BusinessLayer.Models.Direction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IDirectionService
    {
        Task<IEnumerable<DirectionModel>> GetAll();

        Task<DirectionModel> GetMember(int id);

        Task Create(DirectionModel model);

        Task Delete(int id);

        Task Update(DirectionModel model);
    }
}

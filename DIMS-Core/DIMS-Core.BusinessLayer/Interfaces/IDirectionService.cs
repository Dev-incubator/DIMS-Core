using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IDirectionService : IBasicCRUDService<DirectionModel>
    {
        Task<IEnumerable<DirectionModel>> GetAll();
    }
}
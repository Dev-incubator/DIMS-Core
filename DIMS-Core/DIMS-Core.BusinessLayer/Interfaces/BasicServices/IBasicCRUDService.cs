using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IBasicCRUDService<DefaultDTOModel> where DefaultDTOModel : BaseDTOModel
    {
        Task<IEnumerable<DefaultDTOModel>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<DefaultDTOModel> GetEntityModelAsync(int id);

        Task<DTOModel> GetEntityModelAsync<DTOModel>(int id);

        Task CreateAsync(DefaultDTOModel model);

        Task CreateAsync<DTOModel>(DTOModel model);

        Task UpdateAsync(DefaultDTOModel model);

        Task UpdateAsync<DTOModel>(DTOModel model) where DTOModel : BaseDTOModel;

        Task DeleteAsync(int id);
    }
}
using DIMS_Core.BusinessLayer.Models.BaseModels;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IGenericCRUDService<DefaultDTOModel> where DefaultDTOModel : BaseDTOModel
    {
        Task<DefaultDTOModel> GetEntityModel(int id);

        Task<DTOModel> GetEntityModel<DTOModel>(int id);

        Task Create(DefaultDTOModel model);

        Task Create<DTOModel>(DTOModel model);

        Task Update(DefaultDTOModel model);

        Task Update<DTOModel>(DTOModel model) where DTOModel : BaseDTOModel;

        Task Delete(int id);
    }
}
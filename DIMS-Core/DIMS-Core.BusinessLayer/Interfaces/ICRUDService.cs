using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IGenericCRUDService
    {
        Task<DTOModel> GetEntityModel<DTOModel>(int id);
        Task Create<DTOModel>(DTOModel model);
        Task Delete<DTOModel>(int id);
        Task Update<DTOModel>(DTOModel model);
    }
}

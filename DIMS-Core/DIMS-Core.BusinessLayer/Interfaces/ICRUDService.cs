using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Interfaces
{
    public interface IGenericCRUDService
    {
        Task<TModel> GetEntityModel<TModel>(int id);
        Task Create<TModel>(TModel model);
        Task Delete<TModel>(int id);
        Task Update<TModel>(TModel model);
    }
}

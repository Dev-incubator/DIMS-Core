using DIMS_Core.DataAccessLayer.Entities;
using System.Threading.Tasks;
using TaskEntities = DIMS_Core.DataAccessLayer.Entities.Task;


namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntities>
    {
        Task<TaskEntities> GetWithIncludeById(int id);
    }
}

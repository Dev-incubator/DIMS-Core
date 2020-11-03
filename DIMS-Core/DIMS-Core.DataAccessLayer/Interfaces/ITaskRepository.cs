using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface ITaskRepository : IRepository<TaskEntity>
    {

    }
}

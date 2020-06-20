using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetByIdAsync(int id);

        Task CreateAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(int id);
    }
}
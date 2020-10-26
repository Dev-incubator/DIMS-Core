using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);

        void Update(TEntity entity);

        Task Delete(int id);
    }
}
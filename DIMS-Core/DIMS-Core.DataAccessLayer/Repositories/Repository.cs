using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext databaseContext;
        protected readonly DbSet<TEntity> currentSet;

        public Repository(DbContext context)
        {
            databaseContext = context;
            currentSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity is null)
            {
                return;
            }

            await currentSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var entity = await GetByIdAsync(id);

            if (entity is null)
            {
                return;
            }

            currentSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return currentSet.AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await currentSet.FindAsync(id);

            return entity;
        }

        public void Update(TEntity entity)
        {
            databaseContext.Entry(entity).State = EntityState.Modified;
        }

        #region Disposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    databaseContext.Dispose();
                }

                disposedValue = true;
            }
        }

        ~Repository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion Disposable
    }
}
using DIMS_Core.DataAccessLayer.Context;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DIMSCoreDatabaseContext _context;
        private ISampleRepository _sampleRepository;
        private IDirectionRepository _directionRepository;
        private ITaskRepository _taskRepository;
        private ITaskStateRepository _taskStateRepository;
        private ITaskTrackRepository _taskTrackRepository;
        private IUserProfileRepository _userProfileRepository;
        private IUserTaskRepository _userTaskRepository;
        public ISampleRepository SampleRepository => _sampleRepository ??= new SampleRepository(_context);
        public IDirectionRepository DirectionRepository => _directionRepository ??= new DirectionRepository(_context);
        public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(_context);
        public ITaskStateRepository TaskStateRepository => _taskStateRepository ??= new TaskStateRepository(_context);
        public ITaskTrackRepository TaskTrackRepository => _taskTrackRepository ??= new TaskTrackRepository(_context);
        public IUserProfileRepository UserProfileRepository => _userProfileRepository ??= new UserProfileRepository(_context);
        public IUserTaskRepository UserTaskRepository => _userTaskRepository ??= new UserTaskRepository(_context);

        public UnitOfWork(DIMSCoreDatabaseContext context)
        {
            _context = context;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region Disposable

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        ~UnitOfWork()
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
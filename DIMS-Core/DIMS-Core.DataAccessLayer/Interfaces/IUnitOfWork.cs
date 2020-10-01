using System;
using System.Threading.Tasks;

namespace DIMS_Core.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISampleRepository SampleRepository { get; }
        IDirectionRepository DirectionRepository { get; }
        ITaskRepository TaskRepository { get; }
        ITaskStateRepository TaskStateRepository { get; }
        ITaskTrackRepository TaskTrackRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IUserTaskRepository UserTaskRepository { get; }
        Task SaveAsync();
    }
}
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
        IVTaskRepository VTaskRepository { get; }
        IVUserProfileRepository VUserProfileRepository { get; }
        IVUserProgressRepository VUserProgressRepository { get; }
        IVUserTaskRepository VUserTaskRepository { get; }
        IVUserTrackRepository VUserTrackRepository { get; }
        Task SaveAsync();
    }
}
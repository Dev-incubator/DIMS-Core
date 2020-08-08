using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class TaskStateService : BasicCRUDService<TaskState, TaskStateModel>, ITaskStateService
    {
        public TaskStateService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<TaskState> BaseRepository => unitOfWork.TaskStateRepository;
    }
}
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.DataAccessLayer.Entities;
using DIMS_Core.DataAccessLayer.Interfaces;

namespace DIMS_Core.BusinessLayer.Services
{
    public class VUserTaskService : BasicCRUDService<VUserTask, VUserTaskModel>, IVUserTaskService
    {
        public VUserTaskService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private protected override IRepository<VUserTask> BaseRepository => unitOfWork.VUserTaskRepository;
    }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using System.Linq;
using TaskTrackEntity = DIMS_Core.DataAccessLayer.Entities.TaskTrack;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class TaskTrackService : ITaskTrackService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskTrackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<VTaskTrackModel>> GetAllForMember(int userId)
        {
            var taskTracks = unitOfWork.VUserTrackRepository.GetAll()
                .Where(vUserTrack => vUserTrack.UserId == userId);

            var mappedQuery = mapper.ProjectTo<VTaskTrackModel>(taskTracks);

            return await mappedQuery.ToListAsync();
        }

        public VTaskTrackModel GetVTaskTrack(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = unitOfWork.VUserTrackRepository.GetAll()
                .Where(vUserTrack => vUserTrack.TaskTrackId == id)
                .FirstOrDefault();

            var model = mapper.Map<VTaskTrackModel>(entity);

            return model;
        }

        public async Task Create(TaskTrackModel model)
        {
            if (model is null || model.TaskTrackId != 0)
            {
                return;
            }

            var entity = mapper.Map<TaskTrackEntity>(model);

            await unitOfWork.TaskTrackRepository.Create(entity);

            await unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.TaskTrackRepository.Delete(id);

            await unitOfWork.Save();
        }
    }
}

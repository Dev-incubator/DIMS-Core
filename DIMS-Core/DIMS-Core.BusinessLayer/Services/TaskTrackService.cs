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

        public async Task<IEnumerable<VTaskTrackModel>> GetAllByUserId(int userId)
        {
            var taskTracks = unitOfWork.VUserTrackRepository.GetAll()
                .Where(vUserTrack => vUserTrack.UserId == userId);

            var mappedQuery = mapper.ProjectTo<VTaskTrackModel>(taskTracks);

            return await mappedQuery.ToListAsync();
        }

        public async Task<TaskTrackModel> GetTaskTrack(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var taskTrack = await unitOfWork.TaskTrackRepository.GetById(id);
            var model = mapper.Map<TaskTrackModel>(taskTrack);

            return model;
        }

        public VTaskTrackModel GetVTaskTrack(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = unitOfWork.VUserTrackRepository.GetAll()
                .FirstOrDefault(vUserTrack => vUserTrack.TaskTrackId == id);

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

        public async Task Update(TaskTrackModel model)
        {
            if (model is null || model.TaskTrackId <= 0)
            {
                return;
            }

            var taskTrack = await unitOfWork.TaskTrackRepository.GetById(model.TaskTrackId);

            if (taskTrack is null)
            {
                return;
            }

            var entity = mapper.Map(model, taskTrack);
            unitOfWork.TaskTrackRepository.Update(entity);

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

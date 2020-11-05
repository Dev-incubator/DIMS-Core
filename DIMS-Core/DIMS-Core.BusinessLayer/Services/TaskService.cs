using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntities = DIMS_Core.DataAccessLayer.Entities.Task;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class TaskService : ITaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TaskModel>> GetAll()
        {
            var vTasks = unitOfWork.VTaskRepository.GetAll();
            var taskModels = mapper.ProjectTo<TaskModel>(vTasks);

            return await taskModels.ToListAsync();
        }

        public async Task<TaskModel> GetTask(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var task = await unitOfWork.TaskRepository.GetById(id);
            var taskModel = mapper.Map<TaskModel>(task);

            return taskModel;
        }

        public async Task Create(TaskModel model)
        {
            if (model is null || model.TaskId != 0)
            {
                return;
            }

            var task = mapper.Map<TaskEntities>(model);

            await unitOfWork.TaskRepository.Create(task);

            await unitOfWork.Save();
        }

        public async Task Update(TaskModel model)
        {
            if (model is null || model.TaskId <= 0)
            {
                return;
            }

            var task = await unitOfWork.TaskRepository.GetById(model.TaskId);

            if (task is null)
            {
                return;
            }

            var mappedTask = mapper.Map(model, task);

            unitOfWork.TaskRepository.Update(mappedTask);

            await unitOfWork.Save();
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.TaskRepository.Delete(id);

            await unitOfWork.Save();
        }

    }
}

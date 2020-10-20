using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.MappingProfiles;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TaskModel>> SearchAsync()
        {
            var query = unitOfWork.VTaskRepository.GetAll();
            var mappedQuery = mapper.ProjectTo<TaskModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public async Task<TaskModel> GetTaskAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await unitOfWork.TaskRepository.GetById(id);
            var model = mapper.Map<TaskModel>(entity);

            return model;
        }

        public async Task CreateAsync(TaskModel model)
        {
            if (model is null || model.TaskId != 0)
            {
                return;
            }

            var entity = mapper.Map<DataAccessLayer.Entities.Task>(model);

            await unitOfWork.TaskRepository.Create(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(TaskModel model)
        {
            if (model is null || model.TaskId <= 0)
            {
                return;
            }

            var entity = await unitOfWork.TaskRepository.GetById(model.TaskId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            unitOfWork.TaskRepository.Update(mappedEntity);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.TaskRepository.Delete(id);

            await unitOfWork.SaveAsync();
        }

    }
}

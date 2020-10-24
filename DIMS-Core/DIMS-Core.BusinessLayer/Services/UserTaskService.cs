using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserTaskEntities = DIMS_Core.DataAccessLayer.Entities.UserTask;

namespace DIMS_Core.BusinessLayer.Services
{
    internal class UserTaskService : IUserTaskService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserTaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskModel>> SearchAsync()
        {
            var query = unitOfWork.VUserTaskRepository.Search();
            var mappedQuery = mapper.ProjectTo<UserTaskModel>(query);

            return await mappedQuery.ToListAsync();
        }

        public async Task<UserTaskModel> GetTaskAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await unitOfWork.VUserTaskRepository.GetByIdAsync(id);
            var model = mapper.Map<UserTaskModel>(entity);

            return model;
        }

        public async Task CreateAsync(UserTaskModel model)
        {
            if (model is null || model.UserTaskId != 0)
            {
                return;
            }

            var entity = mapper.Map<UserTaskEntities>(model);

            await unitOfWork.UserTaskRepository.CreateAsync(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UserTaskModel model)
        {
            if (model is null || model.UserTaskId <= 0)
            {
                return;
            }

            var entity = await unitOfWork.UserTaskRepository.GetByIdAsync(model.TaskId);

            if (entity is null)
            {
                return;
            }

            var mappedEntity = mapper.Map(model, entity);

            unitOfWork.UserTaskRepository.Update(mappedEntity);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.UserTaskRepository.DeleteAsync(id);

            await unitOfWork.SaveAsync();
        }

    }
}

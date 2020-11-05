using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using UserTaskEntity = DIMS_Core.DataAccessLayer.Entities.UserTask;
using System.Linq;

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
            var tasks = unitOfWork.TaskRepository.GetAll();
            var mappedQuery = mapper.ProjectTo<TaskModel>(tasks);

            return await mappedQuery.ToListAsync();
        }

        public async Task<TaskModel> GetTask(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var task = await unitOfWork.TaskRepository.GetById(id);
            var model = mapper.Map<TaskModel>(task);

            return model;
        }

        public async Task Create(TaskModel model)
        {
            if (model is null || model.TaskId != 0)
            {
                return;
            }

            var task = mapper.Map<TaskEntity>(model);
            await unitOfWork.TaskRepository.Create(task);

            await unitOfWork.Save();

            if (model.SelectedMembers != null)
            {
                foreach (var userId in model.SelectedMembers)
                {
                    await CreateUserTask(task.TaskId, userId);
                }

                await unitOfWork.Save();
            }
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

            var mappedEntity = mapper.Map(model, task);
            unitOfWork.TaskRepository.Update(mappedEntity);

            if (model.SelectedMembers != null)
            {
                var selectedUserIds = model.SelectedMembers;
                var currentUserIds = task.UserTask.Select(q => q.UserId).ToArray();

                var intersectedUserIds = currentUserIds.Intersect(selectedUserIds);
                var creatingUserIds = selectedUserIds.Except(intersectedUserIds);
                var deletingUserIds = currentUserIds.Except(intersectedUserIds);

                foreach (var item in creatingUserIds)
                {
                    await CreateUserTask(task.TaskId, item);
                }

                foreach (var item in deletingUserIds)
                {
                    var userTask = task.UserTask.FirstOrDefault(q => q.UserId == item);

                    if (userTask != null)
                    {
                        await DeleteUserTask(userTask.UserTaskId);
                    }
                }
            }

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

        private async Task CreateUserTask(int taskId, int userId)
        {
            var userTask = new UserTaskEntity()
            {
                TaskId = taskId,
                UserId = userId,
                StateId = 1
            };

            await unitOfWork.UserTaskRepository.Create(userTask);
        }

        private async Task DeleteUserTask(int userTaskId)
        {
            await unitOfWork.UserTaskRepository.Delete(userTaskId);
        }
    }
}

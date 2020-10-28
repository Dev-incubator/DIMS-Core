using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using TaskEntity = DIMS_Core.DataAccessLayer.Entities.Task;
using UserTaskEntity = DIMS_Core.DataAccessLayer.Entities.UserTask;

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

        public async Task<IEnumerable<TaskModel>> Search()
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

            var task = await unitOfWork.TaskRepository.GetWithIncludeById(id);
            var model = mapper.Map<TaskModel>(task);

            return model;
        }

        public async Task Create(TaskModel model, List<MemberForTaskModel> members)
        {
            if (model is null || model.TaskId != 0)
            {
                return;
            }

            var task = mapper.Map<TaskEntity>(model);
            await unitOfWork.TaskRepository.Create(task);

            await unitOfWork.Save();

            if (members != null)
            {
                foreach (var member in members)
                {
                    if (member.Selected)
                    {
                        var userTask = new UserTaskModel()
                        {
                            TaskId = task.TaskId,
                            UserId = member.UserId,
                            StateId = 1,
                        };

                        var mappedUserTask = mapper.Map<UserTaskEntity>(userTask);
                        await unitOfWork.UserTaskRepository.Create(mappedUserTask);
                    }
                }

                await unitOfWork.Save();
            }
        }

        public async Task Update(TaskModel model, List<MemberForTaskModel> members)
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

            if (members != null)
            {
                foreach (var member in members)
                {
                    if (member.Selected && member.UserTaskId == 0)
                    {
                        await CreateUserTask(model.TaskId, member.UserId);
                    }
                    else if (!member.Selected && member.UserTaskId > 0)
                    {
                        await DeleteUserTask(member.UserTaskId);
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
            var userTask = new UserTaskModel()
            {
                TaskId = taskId,
                UserId = userId,
                StateId = 1
            };

            var mappedUserTask = mapper.Map<UserTaskEntity>(userTask);
            await unitOfWork.UserTaskRepository.Create(mappedUserTask);
        }

        private async Task DeleteUserTask(int userTaskId)
        {
            await unitOfWork.UserTaskRepository.Delete(userTaskId);
        }

    }
}

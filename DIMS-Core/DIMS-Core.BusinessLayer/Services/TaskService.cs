using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = DIMS_Core.BusinessLayer.Models.Task.TaskModel;
using MemberForTaskModel = DIMS_Core.BusinessLayer.Models.Task.MemberForTaskModel;
using TaskEntities = DIMS_Core.DataAccessLayer.Entities.Task;
using UserTask = DIMS_Core.DataAccessLayer.Entities.UserTask;
using System.Linq;
using System.Linq.Dynamic.Core;
using DIMS_Core.BusinessLayer.Models.UserTask;

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
            var vTasks = unitOfWork.VTaskRepository.Search();
            var mappedQuery = mapper.ProjectTo<TaskModel>(vTasks);

            return await mappedQuery.ToListAsync();
        }

        public async Task<IEnumerable<MemberForTaskModel>> GetMembersAsync()
        {
            var members = from u in unitOfWork.VUserProfileRepository.GetAll()
                        select new MemberForTaskModel()
                        {
                            UserTaskId = 0,
                            UserId = u.UserId,
                            FullName = u.FullName
                        };

            return await members.ToListAsync();
        }

        public async Task<IEnumerable<MemberForTaskModel>> GetMembersForTaskAsync(int id)
        {
            var members = from u in unitOfWork.VUserProfileRepository.GetAll()
                         join ut_in in unitOfWork.UserTaskRepository.GetAll().Where(x => x.TaskId == id)
                         on u.UserId equals ut_in.UserId into ut_out
                         from ut in ut_out.DefaultIfEmpty()
                         select new MemberForTaskModel()
                         {
                             UserTaskId = ut != null ? ut.UserTaskId : 0,
                             Selected = ut != null,
                             UserId = u.UserId,
                             FullName = u.FullName,
                         };

            return await members.ToListAsync();
        }

        public async Task<TaskModel> GetTaskAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var task = await unitOfWork.TaskRepository.GetByIdAsync(id);
            var model = mapper.Map<TaskModel>(task);

            return model;
        }

        public async Task CreateAsync(TaskModel model, List<MemberForTaskModel> members)
        {
            if (model is null || model.TaskId != 0)
            {
                return;
            }

            var task = mapper.Map<TaskEntities>(model);
            await unitOfWork.TaskRepository.CreateAsync(task);

            await unitOfWork.SaveAsync();

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

                        var mappedUserTask = mapper.Map<UserTask>(userTask);
                        await unitOfWork.UserTaskRepository.CreateAsync(mappedUserTask);
                    }
                }

                await unitOfWork.SaveAsync();
            }
        }

        public async Task UpdateAsync(TaskModel model, List<MemberForTaskModel> members)
        {
            if (model is null || model.TaskId <= 0)
            {
                return;
            }

            var task = await unitOfWork.TaskRepository.GetByIdAsync(model.TaskId);

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
                        var userTask = new UserTaskModel()
                        {
                            TaskId = model.TaskId,
                            UserId = member.UserId,
                            StateId = 1
                        };

                        var mappedUserTask = mapper.Map<UserTask>(userTask);
                        await unitOfWork.UserTaskRepository.CreateAsync(mappedUserTask);
                    }
                    else if (!member.Selected && member.UserTaskId > 0)
                    {
                        await unitOfWork.UserTaskRepository.DeleteAsync(member.UserTaskId);
                    }
                }
            }

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return;
            }

            await unitOfWork.TaskRepository.DeleteAsync(id);
            await unitOfWork.SaveAsync();
        }

    }
}

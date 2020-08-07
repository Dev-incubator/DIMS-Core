using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class TaskManager : ITaskManager
    {
        private readonly IMapper mapper;
        private readonly IUserTaskService userTaskService;
        private readonly ITaskService taskService;
        private readonly IVUserProfileService vUserProfileService;

        public TaskManager(IMapper mapper, IUserTaskService userTaskService, ITaskService taskService, IVUserProfileService vUserProfileService)
        {
            this.mapper = mapper;
            this.userTaskService = userTaskService;
            this.taskService = taskService;
            this.vUserProfileService = vUserProfileService;
        }

        public async Task CreateTask(TaskEditModel model)
        {
            var taskModel = mapper.Map<TaskModel>(model);
            await taskService.Create(taskModel);

            foreach (var user in model.UsersTask)
            {
                if (user.OnTask)
                {
                    UserTaskModel userTaskModel = new UserTaskModel
                    {
                        TaskId = taskModel.TaskId.Value,
                        UserId = user.UserId
                    };

                    await userTaskService.Create(userTaskModel);
                }
            }
        }

        public async Task UpdateTask(TaskEditModel model)
        {
            if (!model.TaskId.HasValue)
            {
                return;
            }

            var taskModel = mapper.Map<TaskModel>(model);
            await taskService.Update(taskModel);

            var allUserTasks = await userTaskService.GetAll();
            var UsersOnTaskBefore = allUserTasks.Where(ut => ut.TaskId == model.TaskId).Select(ut => ut.UserId);
            var UserTasksNow = model.UsersTask.Where(ut => ut.OnTask == true).Select(ut => ut.UserId);

            var usersRemovedFromTask = UsersOnTaskBefore.Except(UserTasksNow);
            var usersAddedToTask = UserTasksNow.Except(UsersOnTaskBefore);

            var userTasksToRemove = allUserTasks.Where(ut => ut.TaskId == taskModel.TaskId && usersRemovedFromTask.Any(u => u == ut.UserId));
            foreach (var ut in userTasksToRemove)
            {
                await userTaskService.Delete(ut.UserTaskId.Value);
            }

            foreach (var userId in usersAddedToTask)
            {
                UserTaskModel userTaskModel = new UserTaskModel
                {
                    TaskId = taskModel.TaskId.Value,
                    UserId = userId
                };
                await userTaskService.Create(userTaskModel);
            }
        }

        public async Task DeleteTask(int id)
        {
            await taskService.Delete(id);
        }

        public async Task<TaskEditModel> GetRawModel()
        {
            var model = new TaskEditModel();
            var allUsers = await vUserProfileService.GetAll();
            model.UsersTask = mapper.ProjectTo<UserTaskTaskMangerModel>(allUsers.AsQueryable()).ToList();
            model.StartDate = DateTime.Now;
            model.DeadlineDate = (DateTime.Now).AddDays(1);
            return model;
        }

        public async Task<TaskEditModel> GetModel(int id)
        {
            var entity = await taskService.GetEntityModel(id);
            var allUsers = await vUserProfileService.GetAll();
            var userTask = await userTaskService.GetAll();

            var model = mapper.Map<TaskEditModel>(entity);
            model.UsersTask = allUsers.Select(u => new UserTaskTaskMangerModel()
            {
                UserId = u.UserId,
                FullName = u.FullName,
                OnTask = userTask.Any(ut => ut.UserId == u.UserId && ut.TaskId == id)
            }).ToList();

            return model;
        }
    }
}
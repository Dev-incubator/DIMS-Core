using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using DIMS_Core.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIMS_Core.BusinessLayer.Services
{
    public class TaskManager:ITaskManager
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
        public async Task CreateTask(TaskCreateModel model)
        {
            var taskModel = mapper.Map<TaskModel>(model);
            await taskService.Create(taskModel);

            int taskId = taskModel.TaskId.Value;
            foreach (var userId in model.UsersAtTask)
            {
                UserTaskModel userTaskModel = new UserTaskModel
                {
                   TaskId=taskId,
                   UserId=userId.UserId
                };

                await userTaskService.Create(userTaskModel);
            }
        }

        public Task UpdateTask(TaskEditModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteTask(int id)
        {
            await taskService.Delete(id);
        }

        public async Task<TaskEditModel> GetModel(int id)
        {
            var entity = await taskService.GetEntityModel(id);
            var model = mapper.Map<TaskEditModel>(entity);

            var allUsers = await vUserProfileService.GetAll();

            var userAtTask = (from ut in entity.UserTask
                             join up in allUsers on ut.UserId equals up.UserId
                             select up).ToList();

            var allOtherUsers = allUsers.Except(userAtTask);

            model.AllOtherUsers = allOtherUsers;
            model.UsersAtTaskWas = userAtTask;

            return model;
        }
    }
}

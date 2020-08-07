using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DIMS_Core.Controllers
{
    public class TaskManagerController : Controller
    {
        IVUserProfileService vUserProfileService;
        IVTaskService vTaskService;
        IVUserProgressService vUserProgressService;
        ITaskManager taskManager;
        IMapper mapper;

        public TaskManagerController( IMapper mapper, IVUserProgressService vUserProgressService, IVTaskService vTaskService,
            IVUserProfileService vUserProfileService, ITaskManager taskManager)
        {
            this.mapper = mapper;
            this.vUserProgressService = vUserProgressService;
            this.vTaskService = vTaskService;
            this.vUserProfileService = vUserProfileService;
            this.taskManager = taskManager;
        }

        [HttpGet]
        public async Task<IActionResult> MemberProgressGrid(int? UserId)
        {
            var userProgress = await vUserProgressService.GetEntityModel(UserId.Value);

            if (UserId is null)
            {
                return Content("User not exists");
            }
            return View(new List<VUserProgressModel> { });
        }

        public async Task<IActionResult> TasksManageGrid()
        {
            IEnumerable<VTaskModel> taskModels = await vTaskService.GetAll();
            return View(taskModels);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var model = new TaskEditModel();
            var allUsers = await vUserProfileService.GetAll();
            model.UsersTask = (mapper.ProjectTo<UserTaskTaskMangerModel>(allUsers.AsQueryable())).ToList();
            return PartialView("TaskCreateWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskEditModel model)
        {
            await taskManager.CreateTask(model);
            return RedirectToAction("TasksManageGrid");
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int TaskId)
        {
            var taskEditModel = await taskManager.GetModel(TaskId);
            return PartialView("TaskEditWindow", taskEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TaskEditModel model)
        {
            //var taskModel = mapper.Map<TaskModel>(model);
            //await taskService.Update(taskModel);
            return RedirectToAction("TasksManageGrid");
        }

        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            await taskManager.DeleteTask(TaskId);
            return RedirectToAction("TasksManageGrid");
        }
    }
}
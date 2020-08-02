using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DIMS_Core.Controllers
{
    public class TaskManagerController : Controller
    {
        IVUserProfileService vUserProfileService;
        ITaskService taskService;
        IVTaskService vTaskService;
        IVUserProgressService vUserProgressService;
        IMapper mapper;

        public TaskManagerController( IMapper mapper, IVUserProgressService vUserProgressService, ITaskService taskService, IVTaskService vTaskService,
            IVUserProfileService vUserProfileService)
        {
            this.mapper = mapper;
            this.vUserProgressService = vUserProgressService;
            this.taskService = taskService;
            this.vTaskService = vTaskService;
            this.vUserProfileService = vUserProfileService;
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
            var model = new TaskEditViewModel();
            var allMembers = await vUserProfileService.GetAll();
            var list = new MultiSelectList(allMembers, "UserId", "FullName");
            model.AllMembers = list;
            return PartialView("TaskCreateWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskEditViewModel model)
        {
            var taskModel = mapper.Map<TaskModel>(model);
            await taskService.Create(taskModel);
            return RedirectToAction("TasksManageGrid");
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int TaskId)
        {
            var taskModel = await taskService.GetEntityModel(TaskId);
            var model = mapper.Map<TaskEditViewModel>(taskModel);
            return PartialView("TaskEditWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(TaskEditViewModel model)
        {
            var taskModel = mapper.Map<TaskModel>(model);
            await taskService.Update(taskModel);
            return RedirectToAction("TasksManageGrid");
        }

        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            return RedirectToAction("TasksManageGrid");
        }
    }
}
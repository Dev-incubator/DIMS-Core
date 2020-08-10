using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Authorize]
    public class TaskManagerController : Controller
    {
        private IVTaskService vTaskService;
        private ITaskManager taskManager;

        public TaskManagerController(IVTaskService vTaskService, ITaskManager taskManager)
        {
            this.vTaskService = vTaskService;
            this.taskManager = taskManager;
        }

        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> TasksManageGrid()
        {
            IEnumerable<VTaskModel> taskModels = await vTaskService.GetAllAsync();
            return View(taskModels);
        }

        [HttpGet]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> CreateTask()
        {
            var model = await taskManager.GetRawModel();
            return PartialView("TaskCreateWindow", model);
        }

        [HttpPost]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> CreateTask(TaskEditModel model)
        {
            await taskManager.CreateTask(model);
            return RedirectToAction("TasksManageGrid");
        }

        [HttpGet]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> EditTask(int TaskId)
        {
            var taskEditModel = await taskManager.GetModel(TaskId);
            return PartialView("TaskEditWindow", taskEditModel);
        }

        [HttpPost]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> EditTask(TaskEditModel model)
        {
            await taskManager.UpdateTask(model);
            return RedirectToAction("TasksManageGrid");
        }

        [HttpGet]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            var model = await vTaskService.GetEntityModelAsync(TaskId);
            return PartialView("TaskDeleteWindow", model);
        }

        [HttpPost]
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> DeleteTask(VTaskModel model)
        {
            await taskManager.DeleteTask(model.TaskId);
            return RedirectToAction("TasksManageGrid");
        }
    }
}
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Models.TaskManagerModels;
using DIMS_Core.Models.ProgressModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    public class TaskManagerController : Controller
    {
        private IVTaskService vTaskService;
        private IVUserProgressService vUserProgressService;
        private ITaskManager taskManager;

        public TaskManagerController(IVUserProgressService vUserProgressService, IVTaskService vTaskService, ITaskManager taskManager)
        {
            this.vUserProgressService = vUserProgressService;
            this.vTaskService = vTaskService;
            this.taskManager = taskManager;
        }

        [HttpGet]
        public async Task<IActionResult> MemberProgressGrid(int UserId, string UserName)
        {
            var allVUserProgress = await vUserProgressService.GetAllAsync();
            var currentUserProgress = allVUserProgress.Where(up => up.UserId == UserId);
            var model = new MembersProgressViewModel();
            model.vUserProgressModels = currentUserProgress;
            model.UserName = UserName;
            return View(model);
        }

        public async Task<IActionResult> TasksManageGrid()
        {
            IEnumerable<VTaskModel> taskModels = await vTaskService.GetAllAsync();
            return View(taskModels);
        }

        [HttpGet]
        public async Task<IActionResult> CreateTask()
        {
            var model = await taskManager.GetRawModel();
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
            await taskManager.UpdateTask(model);
            return RedirectToAction("TasksManageGrid");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            var model = await vTaskService.GetEntityModelAsync(TaskId);
            return PartialView("TaskDeleteWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask(VTaskModel model)
        {
            await taskManager.DeleteTask(model.TaskId);
            return RedirectToAction("TasksManageGrid");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models.ProgressModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class ProgressController : Controller
    {
        private IVTaskService vTaskService { get; set; }
        private IVUserTaskService vUserTaskService { get; set; }
        private IVUserProgressService vUserProgressService { get; set; }
        private IUserTaskService userTaskService { get; set; }

        public ProgressController(IVUserTaskService vUserTaskService, IVUserProgressService vUserProgressService, IUserTaskService userTaskService,
            IVTaskService vTaskService)
        {
            this.vUserTaskService = vUserTaskService;
            this.vUserProgressService = vUserProgressService;
            this.userTaskService = userTaskService;
            this.vTaskService = vTaskService;
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

        [HttpGet]
        public async Task<IActionResult> MembersTasksManageGrid(int UserId, string UserName)
        {
            var allUserTask = await userTaskService.GetAllAsync();
            var allVUserTask = await vUserTaskService.GetAllAsync();

            var model = new MembersTasksViewModel();
            model.UserName = UserName;
            model.userTaskModels = (from vut in allVUserTask
                                    join ut in allUserTask on vut.TaskId equals ut.TaskId
                                    where ut.UserId == UserId && UserId == vut.UserId
                                    select new UserTaskViewModel
                                    {
                                        UserTaskId = ut.UserTaskId.Value,
                                        TaskName = vut.TaskName,
                                        DeadlineDate = vut.DeadlineDate,
                                        StartDate = vut.StartDate,
                                        State = vut.State
                                    }).ToList();

            return View(model);
        }

        public IActionResult TaskTracksManageGrid()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateNote(int UserTaskId)
        {
            var currentUserTask = await userTaskService.GetEntityModelAsync(UserTaskId);
            var allVTasks = await vTaskService.GetAllAsync();
            var taskName = allVTasks.First(vt=>vt.TaskId==currentUserTask.TaskId).Name;
            var model = new CreateEditNoteViewModel();
            model.TaskName = taskName;
            return PartialView("CreateNoteWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote()
        {
            return PartialView("CreateNoteWindow");
        }

        [HttpGet]
        public async Task<IActionResult> EditNote(int TaskTrackId)
        {
            return PartialView("EditNoteWindow");
        }

        [HttpPost]
        public async Task<IActionResult> EditNote()
        {
            return PartialView("EditNoteWindow");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNote(int TaskTrackId)
        {
            return View("TaskTracksManageGrid");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote()
        {
            return View("TaskTracksManageGrid");
        }

        public async Task<IActionResult> SetSuccess()
        {
            return View("MembersTasksManageGrid");
        }

        public async Task<IActionResult> SetFail()
        {
            return View("MembersTasksManageGrid");
        }
    }
}
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models.ProgressModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Authorize]
    public class ProgressController : Controller
    {
        private IVTaskService vTaskService { get; set; }
        private IVUserTaskService vUserTaskService { get; set; }
        private IVUserProgressService vUserProgressService { get; set; }
        private IUserTaskService userTaskService { get; set; }
        private ITaskTrackService taskTrackService { get; set; }
        private IVUserTrackService vUserTrackService { get; set; }
        private IMapper mapper { get; set; }

        public ProgressController(IVUserTaskService vUserTaskService, IVUserProgressService vUserProgressService, IUserTaskService userTaskService,
            IVTaskService vTaskService, ITaskTrackService taskTrackService, IVUserTrackService vUserTrackService, IMapper mapper)
        {
            this.vUserTaskService = vUserTaskService;
            this.vUserProgressService = vUserProgressService;
            this.userTaskService = userTaskService;
            this.vTaskService = vTaskService;
            this.taskTrackService = taskTrackService;
            this.vUserTrackService = vUserTrackService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize("Admin, Mentor")]
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
        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> MembersTasksManageGrid(int? UserId, string UserName) //Raw session methods, just to test, need to be rebuilt
        {
            if (UserId is null)
            {
                UserId = HttpContext.Session.GetInt32("UserId");
                UserName = HttpContext.Session.GetString("UserName");
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", UserId.Value);
                HttpContext.Session.SetString("UserName", UserName);
            }

            var allUserTask = await userTaskService.GetAllAsync();
            var allVUserTask = await vUserTaskService.GetAllAsync();

            var model = new MembersTasksViewModel();
            model.UserName = UserName;
            model.userTaskModels = (from vut in allVUserTask
                                    join ut in allUserTask on new { uid = vut.UserId, tid = vut.TaskId } equals new { uid = ut.UserId, tid = ut.TaskId }
                                    where ut.UserId == UserId
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

        [HttpGet]
        [Authorize("Member")]
        public async Task<IActionResult> TaskTracksManageGrid()
        {
            int UserId = HttpContext.Session.GetInt32("UserId").GetValueOrDefault();
            var allVUserTrack = await vUserTrackService.GetAllAsync();
            var currentVUserTrack = allVUserTrack.Where(ut => ut.UserId == UserId);
            return View(currentVUserTrack);
        }

        [HttpGet]
        [Authorize("Member")]
        public async Task<IActionResult> CreateNote(int UserTaskId)
        {
            var currentUserTask = await userTaskService.GetEntityModelAsync(UserTaskId);
            var allVTasks = await vTaskService.GetAllAsync();
            var taskName = allVTasks.First(vt => vt.TaskId == currentUserTask.TaskId).Name;
            var model = new CreateNoteViewModel();
            model.TaskName = taskName;
            return PartialView("CreateNoteWindow", model);
        }

        [HttpPost]
        [Authorize("Member")]
        public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
        {
            var trackModel = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.CreateAsync(trackModel);
            return RedirectToAction("MembersTasksManageGrid");
        }

        [HttpGet]
        [Authorize("Member")]
        public async Task<IActionResult> EditNote(int TaskTrackId)
        {
            var model = await taskTrackService.GetEntityModelAsync(TaskTrackId);
            return PartialView("EditNoteWindow", model);
        }

        [HttpPost]
        [Authorize("Member")]
        public async Task<IActionResult> EditNote(TaskTrackModel model)
        {
            await taskTrackService.UpdateAsync(model);
            return RedirectToAction("TaskTracksManageGrid");
        }

        [HttpGet]
        [Authorize("Member")]
        public async Task<IActionResult> DeleteNote(int TaskTrackId)
        {
            var model = await vUserTrackService.GetEntityModelAsync(TaskTrackId);
            return PartialView("DeleteNoteWindow", model);
        }

        [HttpPost]
        [Authorize("Member")]
        public async Task<IActionResult> DeleteNote(VUserTrackModel model)
        {
            await taskTrackService.DeleteAsync(model.TaskTrackId);
            return RedirectToAction("TaskTracksManageGrid");
        }

        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> SetSuccess()
        {
            return RedirectToAction("MembersTasksManageGrid");
        }

        [Authorize("Admin, Mentor")]
        public async Task<IActionResult> SetFail()
        {
            return RedirectToAction("MembersTasksManageGrid");
        }
    }
}
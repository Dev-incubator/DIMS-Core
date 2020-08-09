using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
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
        private ITaskTrackService taskTrackService { get; set; }
        private IVUserTrackService vUserTrackService { get; set; }
        private IMapper mapper;
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
                                    join ut in allUserTask on new { uid=vut.UserId, tid=vut.TaskId} equals new {uid=ut.UserId, tid=ut.TaskId}
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

        public async Task<IActionResult> TaskTracksManageGrid(int UserId)
        {
            var allVUserTrack = await vUserTrackService.GetAllAsync();
            var currentVUserTrack = allVUserTrack.Where(ut=>ut.UserId==UserId);
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
        public async Task<IActionResult> CreateNote(CreateEditNoteViewModel model)
        {
            var trackModel = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.CreateAsync(trackModel);
            return RedirectToAction("MembersTasksManageGrid");
        }

        [HttpGet]
        public async Task<IActionResult> EditNote(int TaskTrackId)
        {
            var trackModel = await taskTrackService.GetEntityModelAsync(TaskTrackId);
            var model = mapper.Map<CreateEditNoteViewModel>(trackModel);
            return PartialView("EditNoteWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditNote(CreateEditNoteViewModel model)
        {
            var trackModel = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.UpdateAsync(trackModel);
            return RedirectToAction("TaskTracksManageGrid");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNote(int TaskTrackId)
        {
            var model = await taskTrackService.GetEntityModelAsync(TaskTrackId);
            return PartialView("DeleteNoteWindow", model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(VUserTrackModel model)
        {
            await taskTrackService.DeleteAsync(model.TaskTrackId);
            return RedirectToAction("TaskTracksManageGrid");
        }

        public async Task<IActionResult> SetSuccess()
        {
            return RedirectToAction("MembersTasksManageGrid");
        }

        public async Task<IActionResult> SetFail()
        {
            return RedirectToAction("MembersTasksManageGrid");
        }
    }
}
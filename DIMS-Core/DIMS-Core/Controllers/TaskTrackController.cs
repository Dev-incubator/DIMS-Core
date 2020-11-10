using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using DIMS_Core.Models.TaskTrack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("task-tracks")]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService taskTrackService;
        private readonly IUserTaskService userTaskService;
        private readonly IMapper mapper;

        public TaskTrackController(
            ITaskTrackService taskTrackService, 
            IUserTaskService userTaskService, 
            IMapper mapper)
        {
            this.taskTrackService = taskTrackService;
            this.userTaskService = userTaskService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // To Do - Get the id of the current user
            int userId = 3;

            var taskTracks = await taskTrackService.GetAllByUserId(userId);
            var model = mapper.Map<IEnumerable<VTaskTrackViewModel>>(taskTracks);

            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(int userTaskId = 0, string back = null, string backAction = null)
        {
            // To Do - Get the id of the current user
            int userId = 3;

            var model = new TaskTrackViewModel
            {
                UserTaskId = userTaskId,
                TrackDate = DateTime.Now
            };

            var userTasks = await userTaskService.GetAllByUserId(userId);
            ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
            ViewBag.BackController = back;
            ViewBag.BackAction = backAction;
            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskTrackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var taskTrack = mapper.Map<TaskTrackModel>(model);

            await taskTrackService.Create(taskTrack);

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var taskTrack = taskTrackService.GetVTaskTrack(id);
            var model = mapper.Map<VTaskTrackViewModel>(taskTrack);

            return View(model);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await taskTrackService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
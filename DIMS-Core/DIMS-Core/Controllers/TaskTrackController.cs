using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.TaskTrack;
using DIMS_Core.Models.TaskTrack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.Models.Route;

namespace DIMS_Core.Controllers
{
    [Route("task-tracks")]
    [Authorize]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService taskTrackService;
        private readonly IUserTaskService userTaskService;
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public TaskTrackController(
            ITaskTrackService taskTrackService, 
            IUserTaskService userTaskService,
            IMemberService memberService,
            IMapper mapper)
        {
            this.taskTrackService = taskTrackService;
            this.userTaskService = userTaskService;
            this.memberService = memberService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);

            var taskTracks = await taskTrackService.GetAllByUserId(currentUser.UserId);
            var model = mapper.Map<IEnumerable<VTaskTrackViewModel>>(taskTracks);

            ViewBag.Route = new Route()
            {
                Controller = "task-tracks"
            };
            return View(model);
        }

        [HttpGet("details")]
        public IActionResult Details([FromQuery] Route route)
        {
            if (!route.TaskTrackId.HasValue || route.TaskTrackId.Value <= 0)
            {
                return BadRequest();
            }

            var taskTrack = taskTrackService.GetVTaskTrack(route.TaskTrackId.Value);
            var model = mapper.Map<VTaskTrackViewModel>(taskTrack);

            ViewBag.Route = route;
            return View(model);
        }

        [HttpGet("create")]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Create([FromQuery] Route route)
        {
            var model = new TaskTrackViewModel
            {
                UserTaskId = route.UserTaskId ?? 0,
                TrackDate = DateTime.Now
            };

            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
            var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);

            ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskTrackViewModel model, [FromQuery] Route route)
        {
            if (!ModelState.IsValid)
            {
                var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
                var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
                ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
                ViewBag.Route = route;
                return View(model);
            }

            var taskTrack = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.Create(taskTrack);

            return RedirectToAction("Index");
        }

        [HttpGet("edit")]
        [Authorize(Roles = "member")]
        public async Task<IActionResult> Edit([FromQuery] Route route)
        {
            if (!route.TaskTrackId.HasValue || route.TaskTrackId.Value <= 0)
            {
                return BadRequest();
            }

            var taskTrack = await taskTrackService.GetTaskTrack(route.TaskTrackId.Value);
            var model = mapper.Map<TaskTrackViewModel>(taskTrack);

            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
            var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);

            ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TaskTrackViewModel model, [FromQuery] Route route)
        {
            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);

            if (!ModelState.IsValid || model.TaskTrackId <= 0)
            {
                var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
                ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
                ViewBag.Route = route;
                return View(model);
            }

            var taskTrack = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.Update(taskTrack);

            return RedirectToAction("Index");
        }

        [HttpGet("delete")]
        [Authorize(Roles = "member")]
        public IActionResult Delete([FromQuery] Route route)
        {
            if (!route.TaskTrackId.HasValue || route.TaskTrackId.Value <= 0)
            {
                return BadRequest();
            }

            var taskTrack = taskTrackService.GetVTaskTrack(route.TaskTrackId.Value);
            var model = mapper.Map<VTaskTrackViewModel>(taskTrack);

            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([FromQuery] Route route)
        {
            if (!route.TaskTrackId.HasValue || route.TaskTrackId.Value <= 0)
            {
                return BadRequest();
            }

            await taskTrackService.Delete(route.TaskTrackId.Value);

            return RedirectToAction("Index");
        }
    }
}
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
        public async Task<IActionResult> Index()
        {
            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
            var taskTracks = await taskTrackService.GetAllByUserId(currentUser.UserId);
            var model = mapper.Map<IEnumerable<VTaskTrackViewModel>>(taskTracks);

            return View(model);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id, string back = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var taskTrack = taskTrackService.GetVTaskTrack(id);
            var model = mapper.Map<VTaskTrackViewModel>(taskTrack);

            ViewBag.BackController = back;
            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create(int userTaskId = 0, string back = null)
        {
            var model = new TaskTrackViewModel
            {
                UserTaskId = userTaskId,
                TrackDate = DateTime.Now
            };

            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
            var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
            ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
            ViewBag.BackController = back;
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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id, string back = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var taskTrack = await taskTrackService.GetTaskTrack(id);
            var model = mapper.Map<TaskTrackViewModel>(taskTrack);

            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
            var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
            ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
            ViewBag.BackController = back;
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TaskTrackViewModel model)
        {
            var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
                ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
                return View(model);
            }

            if (model.TaskTrackId <= 0)
            {
                var userTasks = await userTaskService.GetAllByUserId(currentUser.UserId);
                ViewBag.SelectListUserTasks = new SelectList(userTasks, "UserTaskId", "Task.Name");
                return View(model);
            }

            var taskTrack = mapper.Map<TaskTrackModel>(model);
            await taskTrackService.Update(taskTrack);

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
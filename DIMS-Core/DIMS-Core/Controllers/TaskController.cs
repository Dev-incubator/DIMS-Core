using System;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.Helpers;
using DIMS_Core.Models.Task;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.Models.Route;
using TaskStateEnum = DIMS_Core.DataAccessLayer.Enums.TaskState;

namespace DIMS_Core.Controllers
{
    [Route("tasks")]
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, 
            IMemberService memberService, IMapper mapper)
        {
            this.taskService = taskService;
            this.memberService = memberService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var tasks = await taskService.GetAll();
            var model = mapper.Map<IEnumerable<TaskViewModel>>(tasks);

            ViewBag.Route = new Route()
            {
                Controller = "tasks"
            };
            return View(model);
        }

        [HttpGet("current-tasks")]
        public async Task<IActionResult> CurrentTasks([FromQuery] Route route)
        {
            if (!route.UserId.HasValue || route.UserId.Value == 0)
            {
                var currentUser = await memberService.GetMemberByEmail(User.Identity.Name);
                route.UserId = currentUser.UserId;
                ViewBag.Member = currentUser;
            }
            else
            {
                ViewBag.Member = await memberService.GetMember(route.UserId.Value);
            }

            var currentTask = await taskService.GetAllMyTask(route.UserId.Value);
            var model = mapper.Map<IEnumerable<CurrentTaskViewModel>>(currentTask);

            return View(model);
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details([FromQuery] Route route)
        {
            if (!route.TaskId.HasValue || route.TaskId.Value <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(route.TaskId.Value);
            var model = mapper.Map<TaskViewModel>(task);

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            ViewBag.Route = route;
            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create([FromQuery] Route route)
        {
            var model = new TaskViewModel()
            {
                StartDate = DateTime.Now,
                DeadlineDate = DateTime.Now
            };

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskViewModel model, [FromQuery] Route route)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                ViewBag.Route = route;
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            await taskService.Create(task);

            return RedirectToAction("Index");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit([FromQuery] Route route)
        {
            if (!route.TaskId.HasValue || route.TaskId.Value <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(route.TaskId.Value);
            var model = mapper.Map<TaskViewModel>(task);

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TaskViewModel model, [FromQuery] Route route)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                ViewBag.Route = route;
                return View(model);
            }

            if (model.TaskId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                ViewBag.Route = route;
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            await taskService.Update(task);

            if (route.UserId.HasValue && route.UserId.Value > 0)
            {
                return RedirectToAction(route.BackAction, route.BackController, new Route { UserId = route.UserId });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery] Route route)
        {
            if (!route.TaskId.HasValue || route.TaskId.Value <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(route.TaskId.Value);
            var model = mapper.Map<TaskViewModel>(task);

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([FromQuery] Route route)
        {
            if (!route.TaskId.HasValue || route.TaskId.Value <= 0)
            {
                return BadRequest();
            }

            await taskService.Delete(route.TaskId.Value);

            return RedirectToAction("Index");
        }

        [HttpPost("task-state")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskStatePost([FromQuery] Route route, int userTaskId, TaskStateEnum status)
        {
            if (!route.UserId.HasValue || route.UserId.Value <= 0 || userTaskId <= 0 || status <= 0)
            {
                return BadRequest();
            }

            await taskService.SetTaskState(userTaskId, status);

            return RedirectToAction("CurrentTasks", "Task", new Route() { UserId = route.UserId.Value });
        }
    }
}
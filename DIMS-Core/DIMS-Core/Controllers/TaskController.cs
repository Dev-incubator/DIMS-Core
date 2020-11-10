using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.Helpers;
using DIMS_Core.Models.Task;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("tasks")]
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

            return View(model);
        }

        [HttpGet("current-tasks")]
        public async Task<IActionResult> CurrentTasks()
        {
            // To Do - Get the id of the current user
            int userId = 3;

            var currentTask = await taskService.GetAllMyTask(userId);
            var model = mapper.Map<IEnumerable<CurrentTaskViewModel>>(currentTask);

            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id, string back = null, string backAction = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskViewModel>(task);
            
            ViewBag.BackController = back;
            ViewBag.BackAction = backAction;
            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            return View(new TaskViewModel());
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            await taskService.Create(task);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id, string back = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskViewModel>(task);

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
            ViewBag.BackController = back;
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                return View(model);
            }

            if (model.TaskId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");
                ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            await taskService.Update(task);

            return RedirectToAction("Index");
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskViewModel>(task);

            ViewBag.AllMembers = await memberService.GetMembersViewModel(mapper);
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

            await taskService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost("task-active/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskActive(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await taskService.SetTaskActive(id);

            return RedirectToAction("MyTasks");
        }

        [HttpPost("task-pause/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskPause(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await taskService.SetTaskPause(id);

            return RedirectToAction("MyTasks");
        }

        [HttpPost("task-success/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskSuccess(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await taskService.SetTaskSuccess(id);

            return RedirectToAction("MyTasks");
        }

        [HttpPost("task-fail/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskFail(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await taskService.SetTaskFail(id);

            return RedirectToAction("MyTasks");
        }
    }
}
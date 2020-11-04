using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
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
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await taskService.GetAll();
            var model = mapper.Map<IEnumerable<TaskViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id, string back = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            ViewBag.BackController = back;

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members.GetMembersForTask(task.UserTask);                                         // Get list of members who participates in this task  

            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = mapper.Map<TaskModel>(model);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id, string back = null)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            ViewBag.BackController = back;

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members = mapper.Map<List<MemberForTaskModel>>(await memberService.Search());     // Get list of all members
            model.Members.MarkSelectedMembersForTask(task.UserTask);                                // Mark members who participates in this task

            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] TaskWithMembersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.TaskId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            await taskService.Update(task, AllMembers: model.Members);

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await taskService.GetTask(id);
            var model = mapper.Map<TaskViewModel>(dto);

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
    }
}
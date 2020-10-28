using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.Helpers;
using DIMS_Core.Models.Task;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("tasks")]
    public class TaskController : Controller
    {
        private readonly ITaskService taskService;
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMemberService memberService, IMapper mapper)
        {
            this.taskService = taskService;
            this.memberService = memberService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await taskService.Search();
            var model = mapper.Map<IEnumerable<TaskViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members.GetMembersForTask(task.UserTask);

            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var allMembers = mapper.Map<List<MemberForTaskModel>>(await memberService.Search());
            var model = new TaskWithMembersViewModel(allMembers);

            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] TaskWithMembersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var task = mapper.Map<TaskModel>(model);
            var allMembers = mapper.Map<List<MemberForTaskModel>>(model.Members);
            await taskService.Create(task, allMembers);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTask(id);
            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members = mapper.Map<List<MemberForTaskModel>>(await memberService.Search()); 
            model.Members.MarkSelectedMembersForTask(task.UserTask);

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
            var allMembers = mapper.Map<List<MemberForTaskModel>>(model.Members);
            await taskService.Update(task, allMembers);

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
            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members.GetMembersForTask(task.UserTask);

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
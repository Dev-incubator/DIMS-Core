﻿using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.DataAccessLayer.Filters;
using DIMS_Core.Models.Member;
using DIMS_Core.Models.Task;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await taskService.SearchAsync();
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

            var task = await taskService.GetTaskAsync(id);
            var members = await taskService.GetMembersForTaskAsync(id);

            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members = mapper.Map<List<MemberForTaskViewModel>>(members.Where(x => x.Selected));

            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var members = await taskService.GetMembersAsync();

            var model = new TaskWithMembersViewModel(){
                StartDate = DateTime.Now,
                DeadlineDate = DateTime.Now,
                Members = mapper.Map<List<MemberForTaskViewModel>>(members)
            };

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
            var members = mapper.Map<List<MemberForTaskModel>>(model.Members);

            await taskService.CreateAsync(task, members);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTaskAsync(id);
            var members = await taskService.GetMembersForTaskAsync(id);

            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members = mapper.Map<List<MemberForTaskViewModel>>(members);

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
            var members = mapper.Map<List<MemberForTaskModel>>(model.Members);

            await taskService.UpdateAsync(task, members);

            return RedirectToAction("Index");
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var task = await taskService.GetTaskAsync(id);
            var members = await taskService.GetMembersForTaskAsync(id);

            var model = mapper.Map<TaskWithMembersViewModel>(task);
            model.Members = mapper.Map<List<MemberForTaskViewModel>>(members.Where(x => x.Selected));

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

            await taskService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
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

            await taskService.Create(dto);

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
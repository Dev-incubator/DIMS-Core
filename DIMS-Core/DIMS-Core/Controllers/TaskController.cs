using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Task;
using DIMS_Core.BusinessLayer.Models.UserTask;
using DIMS_Core.Models.Member;
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
        private readonly IUserTaskService userTaskService;
        private readonly IMapper mapper;

        public TaskController(ITaskService taskService, IMemberService memberService, 
            IUserTaskService userTaskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.memberService = memberService;
            this.userTaskService = userTaskService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await taskService.SearchAsync();
            var model = mapper.Map<IEnumerable<TaskViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var members = await memberService.SearchAsync();

            var model = new AddTaskViewModel(){
                Members = mapper.Map<List<SelectMemberViewModel>>(members)
            };

            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dtoTask = mapper.Map<TaskModel>(model);

            await taskService.CreateAsync(dtoTask);

            if (model.Members != null)
            {
                foreach (var member in model.Members)
                {
                    if (member.Selected)
                    {
                        var dtoUserTask = new UserTaskModel()
                        {
                            TaskId = dtoTask.TaskId,
                            UserId = member.UserId,
                            StateId = 1
                        };

                        await userTaskService.CreateAsync(dtoUserTask);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await taskService.GetTaskAsync(id);
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

            await taskService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
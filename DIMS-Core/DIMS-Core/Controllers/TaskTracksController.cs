using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models.TaskTracks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("task-tracks")]
    public class TaskTracksController : Controller
    {
        private readonly ITaskTracksService taskTracksService;
        private readonly IMapper mapper;

        public TaskTracksController(ITaskTracksService taskTracksService, IMapper mapper)
        {
            this.taskTracksService = taskTracksService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            // To Do - Get the id of the current user
            int userId = 3;

            var searchResult = await taskTracksService.GetAllForMember(UserId: userId);
            var model = mapper.Map<IEnumerable<TaskTracksViewModel>>(searchResult);

            return View(model);
        }
    }
}
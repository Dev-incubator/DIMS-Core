using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models.TaskTrack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("task-tracks")]
    public class TaskTrackController : Controller
    {
        private readonly ITaskTrackService taskTracksService;
        private readonly IMapper mapper;

        public TaskTrackController(ITaskTrackService taskTracksService, IMapper mapper)
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
            var model = mapper.Map<IEnumerable<TaskTrackViewModel>>(searchResult);

            return View(model);
        }
    }
}
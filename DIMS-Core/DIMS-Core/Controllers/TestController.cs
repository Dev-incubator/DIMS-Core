using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class TestController : Controller
    {
        //<a asp-action="Index">Action list</a>
        //<a asp-action= "Members" > Members manage grid</a>
        //<a asp-action= "MemberProgress" > MemberProgress manage grid</a>
        //<a asp-action= "MembersTasks" > Memeber's Tasks manage grid</a>
        //<a asp-action= "Tasks" > Tasks manage grid</a>
        //<a asp-action= "TaskTracks" > TaskTracks manage grid</a>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MembersManageGrid()
        {
            return View();
        }

        public IActionResult MemberProgressGrid()
        {
            return View();
        }

        public IActionResult MembersTasksManageGrid()
        {
            return View();
        }

        public IActionResult TasksManageGrid()
        {
            return View();
        }

        public IActionResult TaskTracksManageGrid()
        {
            return View();
        }
    }
}
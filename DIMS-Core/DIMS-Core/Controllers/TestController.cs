using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class TestController : Controller
    {
        private readonly IUserProfileService userProfileService;
        private readonly IVUserProfileService vUserProfileService;
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
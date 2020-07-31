using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class ProgressController : Controller
    {
        public IActionResult MembersTasksManageGrid()
        {
            return View();
        }

        public IActionResult TaskTracksManageGrid()
        {
            return View();
        }

        public async Task<IActionResult> EditNote()
        {
            return PartialView("EditNoteWindow");
        }

        public async Task<IActionResult> DeleteNote()
        {
            return View("TaskTracksManageGrid");
        }

        public async Task<IActionResult> SetSuccess()
        {
            return View("MembersTasksManageGrid");
        }

        public async Task<IActionResult> SetFail()
        {
            return View("MembersTasksManageGrid");
        }
    }
}
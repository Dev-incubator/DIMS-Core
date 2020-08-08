using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models.ProgressModels;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class ProgressController : Controller
    {
        private IVUserTaskService  vUserTaskService {get;set;}
        public ProgressController()
        {

        }

        public async Task<IActionResult> MembersTasksManageGrid(int UserId, string UserName)
        {
            var model = new MembersTasksViewModel();
            var allUserTask = await vUserTaskService.GetAllAsync();
            model.UserName = UserName;
            model.userTaskModels = allUserTask.Where(ut=>ut.UserId==UserId);

            return View(model);
        }

        public IActionResult TaskTracksManageGrid()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateNote(int TaskId)
        {
            return PartialView("CreateNoteWindow");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote()
        {
            return PartialView("CreateNoteWindow");
        }

        [HttpGet]
        public async Task<IActionResult> EditNote(int id)
        {
            return PartialView("EditNoteWindow");
        }

        [HttpPost]
        public async Task<IActionResult> EditNote()
        {
            return PartialView("EditNoteWindow");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteNote(int id)
        {
            return View("TaskTracksManageGrid");
        }

        [HttpPost]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class ProgressController : Controller
    {
        ITaskService taskService;
        IVUserProgressService vUserProgressService;
        IMapper mapper;

        public ProgressController( IMapper mapper, IVUserProgressService vUserProgressService, ITaskService taskService)
        {
            this.mapper = mapper;
            this.vUserProgressService = vUserProgressService;
            this.taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> MemberProgressGrid(int? UserId)
        {
            var userProgress = await vUserProgressService.GetEntityModel(UserId.Value);

            if (UserId is null)
            {
                return Content("User not exists");
            }
            return View(new List<VUserProgressModel> { });
        }

        public async Task<IActionResult> EditTask(int? TaskId)
        {
            if (TaskId != null)
            {
                var model = await taskService.GetEntityModel(TaskId.Value);
                return PartialView("TaskEditWindow", model);
            }

            return PartialView("TaskEditWindow");
        }
    }
}
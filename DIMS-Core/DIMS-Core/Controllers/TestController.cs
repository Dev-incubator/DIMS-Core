using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DIMS_Core.Models;
using AutoMapper;

namespace DIMS_Core.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserProfileService userProfileService;
        private readonly IVUserProfileService vUserProfileService;
        private readonly IVUserProgressService vUserProgressService;
        public TestController(IUserProfileService userProfileService, IVUserProfileService vUserProfileService, IVUserProgressService vUserProgressService,
            IMapper mapper)
        {
            this.userProfileService = userProfileService;
            this.vUserProfileService = vUserProfileService;
            this.vUserProgressService = vUserProgressService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles ="Admin, Mentor")]
        //public async Task<IActionResult> MembersManageGrid()
        //{
        //    MembersGridViewModel model = new MembersGridViewModel();
        //    var vUserProfiles = await vUserProfileService.GetAllUserProfileViews();
        //    model.vUserProfileViewModels = mapper.Map<IEnumerable<vUserProfileViewModel>>(vUserProfiles);
        //    return View(model);
        //}

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
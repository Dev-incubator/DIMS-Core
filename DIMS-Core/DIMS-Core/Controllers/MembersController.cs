using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DIMS_Core.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserProfileService userProfileService;
        private readonly IVUserProfileService vUserProfileService;
        private readonly IVUserProgressService vUserProgressService;
        public MembersController(IUserProfileService userProfileService, IVUserProfileService vUserProfileService, IVUserProgressService vUserProgressService,
            IMapper mapper)
        {
            this.userProfileService = userProfileService;
            this.vUserProfileService = vUserProfileService;
            this.vUserProgressService = vUserProgressService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, Mentor")]
        public async Task<IActionResult> MembersManageGrid()
        {
            MembersGridViewModel model = new MembersGridViewModel();
            var vUserProfiles = await vUserProfileService.GetAllUserProfileViews();
            model.vUserProfileViewModels = mapper.Map<IEnumerable<vUserProfileViewModel>>(vUserProfiles);
            return View(model);
        }
    }
}
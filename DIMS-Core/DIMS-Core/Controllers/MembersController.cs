using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DIMS_Core.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserProfileService userProfileService;
        private readonly IVUserProfileService vUserProfileService;
        private readonly IVUserProgressService vUserProgressService;
        private readonly IDirectionService directionService;
        public MembersController(IUserProfileService userProfileService, IVUserProfileService vUserProfileService, IVUserProgressService vUserProgressService,
            IDirectionService directionService, IMapper mapper)
        {
            this.userProfileService = userProfileService;
            this.vUserProfileService = vUserProfileService;
            this.vUserProgressService = vUserProgressService;
            this.directionService = directionService;
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

        [HttpGet]
        public async Task<IActionResult> EditMember(int UserId)
        {
            var userProfile = await userProfileService.GetEntityModel(UserId);
            var mappedProfile = mapper.Map<UserProfileEditViewModel>(userProfile);
            var directions = await directionService.GetAll();
            ViewBag.directions = new SelectList(directions , "DirectionId", "Name");
            return PartialView("MemberEditWindow", mappedProfile);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChanges(UserProfileEditViewModel model)
        {
            var existingModel = await userProfileService.GetEntityModel(model.UserId);
            var userProfileModel = mapper.Map<UserProfileModel>(model);
            existingModel.Name = userProfileModel.Name;
            existingModel.LastName = userProfileModel.LastName;
            existingModel.DirectionId = userProfileModel.DirectionId;
            existingModel.MobilePhone = userProfileModel.MobilePhone;
            await userProfileService.Update(existingModel);
            return RedirectToAction("MembersManageGrid");
        }

        public async Task<IActionResult> RegistUser()
        {
            return RedirectToAction("MembersManageGrid");

        }
    }
}
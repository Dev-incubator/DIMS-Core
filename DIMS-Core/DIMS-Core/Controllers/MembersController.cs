using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var vUserProfiles = await vUserProfileService.GetAll();
            model.vUserProfileViewModels = mapper.Map<IEnumerable<vUserProfileViewModel>>(vUserProfiles);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditMember(int? UserId)
        {
            var directions = await directionService.GetAll();
            ViewBag.directions = new SelectList(directions, "DirectionId", "Name");

            if (UserId is null)
            {
                return PartialView("MemberEditWindow", new UserProfileEditViewModel());
            }

            var userProfile = await userProfileService.GetEntityModel(UserId.Value);
            var mappedProfile = mapper.Map<UserProfileEditViewModel>(userProfile);
            return PartialView("MemberEditWindow", mappedProfile);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveChanges(UserProfileEditViewModel model)
        {
            if (model.UserId is null)
            {
                await RegistUser(model);
            }
            else
            {
                var existingModel = await userProfileService.GetEntityModel(model.UserId.Value);
                mapper.Map(model, existingModel);
                await userProfileService.Update(existingModel);
            }

            return RedirectToAction("MembersManageGrid");
        }

        [Authorize(Roles = "Admin")]
        private async Task RegistUser(UserProfileEditViewModel model)
        {
            var userProfileModel = mapper.Map<UserProfileModel>(model);
            await userProfileService.Create(userProfileModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int UserId, string FullName)
        {
            DeleteUserViewModel model = new DeleteUserViewModel(UserId, FullName);
            return PartialView("DeleteWindow", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(DeleteUserViewModel model)
        {
            await userProfileService.Delete(model.UserId);
            return RedirectToAction("MembersManageGrid");
        }

        public IActionResult Progress(int UserId)
        {
            return RedirectToAction("MemberProgressGrid", "TaskManager", UserId);
        }
    }
}
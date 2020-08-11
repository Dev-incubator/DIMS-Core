using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Authorize]
    public class MembersManagerController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserProfileService userProfileService;
        private readonly IVUserProfileService vUserProfileService;
        private readonly IDirectionService directionService;

        public MembersManagerController(IUserProfileService userProfileService, IVUserProfileService vUserProfileService, IDirectionService directionService, IMapper mapper)
        {
            this.userProfileService = userProfileService;
            this.vUserProfileService = vUserProfileService;
            this.directionService = directionService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin, Mentor")]
        public async Task<IActionResult> MembersManageGrid()
        {
            MembersGridViewModel model = new MembersGridViewModel();
            var vUserProfiles = await vUserProfileService.GetAllAsync();
            model.vUserProfileViewModels = mapper.ProjectTo<vUserProfileViewModel>(vUserProfiles.AsQueryable());

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(int UserId)
        {
            var directions = await directionService.GetAllAsync();
            ViewBag.directions = new SelectList(directions, "DirectionId", "Name");

            var userProfile = await userProfileService.GetEntityModelAsync(UserId);
            var mappedProfile = mapper.Map<UserProfileEditViewModel>(userProfile);

            return PartialView("MemberEditWindow", mappedProfile);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserProfileEditViewModel model)
        {
            var existingModel = await userProfileService.GetEntityModelAsync(model.UserId.Value);
            mapper.Map(model, existingModel);
            await userProfileService.UpdateAsync(existingModel);

            return RedirectToAction("MembersManageGrid");
        }
    }
}
using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Identity.Configs;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly IDirectionService directionService;
        private readonly IMapper mapper;
        private readonly IUserProfileService userProfileService;

        public AccountController(IUserIdentityService userIdentityService, IUserProfileService userProfileService, IDirectionService directionService, IMapper mapper)
        {
            this.userProfileService = userProfileService;
            this.userIdentityService = userIdentityService;
            this.directionService = directionService;
            this.mapper = mapper;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await userIdentityService.SignInAsync(model);

            if (result.Succeeded)
            {
                var allUserProfiles = await userProfileService.GetAllAsync(); //?????????
                var currentUserProfile = allUserProfiles.FirstOrDefault(up => up.Email == model.Email);

                HttpContext.Session.SetInt32("UserId", currentUserProfile.UserId.Value);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "You entered incorrect email/password.");

                return View(model);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register()
        {
            var model = new UserRegistViewModel();
            var directions = await directionService.GetAllAsync();
            model.Directions = new SelectList(directions, "DirectionId", "Name");
            model.Password = IdentityAdditionalMethods.GenerateRandomPassword();
            model.ConfirmPassword = IdentityAdditionalMethods.GenerateRandomPassword();
            return PartialView("RegistUserWindow", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(UserRegistViewModel model)
        {
            var directions = await directionService.GetAllAsync();
            model.Directions = new SelectList(directions, "DirectionId", "Name");

            if (!ModelState.IsValid)
            {
                return PartialView("RegistUserWindow", model);
            }

            var identityModel = mapper.Map<SignUpModel>(model);
            var result = await userIdentityService.SignUpAsync(identityModel);

            if (result.Succeeded)
            {
                var userProfileModel = mapper.Map<UserProfileModel>(model);
                //userProfileModel.UserId = identityModel.Id; ???? Will work, if both DB are new
                await userProfileService.CreateAsync(userProfileModel);
                return PartialView("RegisterSucceeded");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return PartialView("RegistUserWindow", model);
            }
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
        public async Task<IActionResult> Delete(DeleteUserViewModel model)
        {
            string userEmail = (await userProfileService.GetEntityModelAsync(model.UserId)).Email;

            var result = await userIdentityService.DeleteAsync(userEmail);

            if (result.Succeeded)
            {
                await userProfileService.DeleteAsync(model.UserId);
            }

            return RedirectToAction("MembersManageGrid", "MembersManager");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await userIdentityService.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userIdentityService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
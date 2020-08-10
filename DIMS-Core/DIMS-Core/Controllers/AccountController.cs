using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.BusinessLayer.Services;
using DIMS_Core.Identity.Configs;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var directions = await directionService.GetAllAsync();
            ViewBag.directions = new SelectList(directions, "DirectionId", "Name");
            var model = new UserRegistViewModel();
            model.Password = IdentityAdditionalMethods.GenerateRandomPassword();
            model.ConfirmPassword = IdentityAdditionalMethods.GenerateRandomPassword();
            return PartialView("RegistUserWindow", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(UserRegistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("RegistUserWindow", model);
            }

            var identityModel = mapper.Map<SignUpModel>(model);
            var result = await userIdentityService.SignUpAsync(identityModel);

            if (result.Succeeded)
            {
                var userProfileModel = mapper.Map<UserProfileModel>(model);
                await userProfileService.CreateAsync(userProfileModel);
                return Json("OK");
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
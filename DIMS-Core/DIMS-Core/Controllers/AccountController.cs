using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.BaseModels;
using DIMS_Core.Common.Enums;
using DIMS_Core.Identity.Configs;
using DIMS_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDirectionService directionService;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public AccountController(  IDirectionService directionService,
            IUserService userService , IMapper mapper)
        {
            this.directionService = directionService;
            this.mapper = mapper;
            this.userService = userService;
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

            var result = await userService.SignInAsync(model);

            if (result.Succeeded)
            {
                //var user = await userIdentityService.GetUserAsync(model.Email);
                //HttpContext.Session.SetInt32("UserId", user.Id);
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
            
            var model = new UserRegistViewModel();

            model.Directions = new SelectList(directions, "DirectionId", "Name");
            model.Password = "qwerty";
            model.ConfirmPassword = "qwerty";

            //model.Password = IdentityAdditionalMethods.GenerateRandomPassword();        uncomment, when sender will be workable
            //model.ConfirmPassword = IdentityAdditionalMethods.GenerateRandomPassword();

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

            var userRegistModel = mapper.Map<UserRegistModel>(model);
            var result = await userService.RegistAsync(userRegistModel);

            if (result.Succeeded)
            {
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
            await userService.DeleteAsync(model.UserId);

            return RedirectToAction("MembersManageGrid", "MembersManager");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await userService.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserIdentityService userService;

        public AccountController(IUserIdentityService userService)
        {
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "You entered incorrect email/password.");

                return View(model);
            }
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await userService.SignUpAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(model);
            }
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
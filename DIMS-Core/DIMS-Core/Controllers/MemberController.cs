using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DIMS_Core.Models.Route;

namespace DIMS_Core.Controllers
{
    [Route("members")]
    [Authorize(Roles = "admin, mentor")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;
        private readonly IDirectionService directionService;
        private readonly IUserService userService;
        private readonly ITaskTrackService taskTrackService;

        public MemberController(IMemberService memberService,
                                IDirectionService directionService,
                                IMapper mapper,
                                IUserService userService,
                                ITaskTrackService taskTrackService)
        {
            this.memberService = memberService;
            this.directionService = directionService;
            this.mapper = mapper;
            this.userService = userService;
            this.taskTrackService = taskTrackService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await memberService.GetAll();
            var model = mapper.Map<IEnumerable<MemberViewModel>>(searchResult);

            ViewBag.Route = new Route()
            {
                Controller = "members"
            };
            return View(model);
        }

        [HttpGet("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromQuery] Route route)
        {
            var model = new AddMemberViewModel()
            {
                Role = "member"
            };
            ViewBag.Directions = await directionService.GetAll();
            ViewBag.Roles = userService.GetRoles();
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AddMemberViewModel model, [FromQuery] Route route)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Directions = await directionService.GetAll();
                ViewBag.Roles = userService.GetRoles();
                ViewBag.Route = route;
                return View(model);
            }

            var dto = mapper.Map<UserProfileModel>(model);
            await memberService.Create(dto);

            var signUpModel = mapper.Map<SignUpModel>(model);
            var result = await userService.SignUp(signUpModel);

            if (result.Succeeded)
            {
                var user = userService.GetUser(model.Email);
                await userService.AddUserRole(user, model.Role);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit([FromQuery] Route route)
        {
            if (!route.UserId.HasValue || route.UserId.Value <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(route.UserId.Value);
            var model = mapper.Map<EditMemberViewModel>(dto);

            var user = userService.GetUser(model.Email);
            model.Role = await userService.GetUserRole(user);

            ViewBag.Directions = await directionService.GetAll();
            ViewBag.Roles = userService.GetRoles();
            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditMemberViewModel model, [FromQuery] Route route)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Directions = await directionService.GetAll();
                ViewBag.Roles = userService.GetRoles();
                ViewBag.Route = route;
                return View(model);
            }

            if (model.UserId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");
                ViewBag.Directions = await directionService.GetAll();
                ViewBag.Roles = userService.GetRoles();
                ViewBag.Route = route;
                return View(model);
            }

            var dto = mapper.Map<UserProfileModel>(model);

            await memberService.Update(dto);
            var user = userService.GetUser(model.Email);
            await userService.UpdateUserRole(user, model.Role);

            return RedirectToAction("Index");
        }

        [HttpGet("delete")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromQuery] Route route)
        {
            if (!route.UserId.HasValue || route.UserId.Value <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(route.UserId.Value);
            var model = mapper.Map<MemberViewModel>(dto);

            ViewBag.Route = route;
            return View(model);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost([FromQuery] Route route)
        {
            if (!route.UserId.HasValue || route.UserId.Value <= 0)
            {
                return BadRequest();
            }

            var member = await memberService.GetMember(route.UserId.Value);
            var user = userService.GetUser(member.Email);
            if (user != null)
            {
                await userService.DeleteUser(user);
            }

            await memberService.Delete(route.UserId.Value);

            return RedirectToAction("Index");
        }

        [HttpGet("progress")]
        public async Task<IActionResult> Progress([FromQuery] Route route)
        {
            if (!route.UserId.HasValue || route.UserId.Value <= 0)
            {
                return BadRequest();
            }

            var vTaskTracks = await taskTrackService.GetAllByUserId(route.UserId.Value);
            var progressModels = mapper.Map<IEnumerable<ProgressModel>>(vTaskTracks);

            ViewBag.Member = await memberService.GetMember(route.UserId.Value);
            ViewBag.Route = route;
            return View(progressModels);
        }
    }
}
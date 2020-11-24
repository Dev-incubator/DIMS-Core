using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.Identity.Entities;

namespace DIMS_Core.Controllers
{
    [Route("members")]
    [Authorize(Roles = "admin")]
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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var searchResult = await memberService.GetAll();
            var model = mapper.Map<IEnumerable<MemberViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Directions = await directionService.GetAll();
            ViewBag.Roles = userService.GetRoles();
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]AddMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Directions = await directionService.GetAll();
                return View(model);
            }

            var dto = mapper.Map<UserProfileModel>(model);
            await memberService.Create(dto);

            var signUpModel = mapper.Map<SignUpModel>(model);
            var result = await userService.SignUp(signUpModel);
            if (result.Succeeded)
            {
                var user = userService.GetUser(signUpModel);
                await userService.UpdateRole(user, model.Role);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{userId}")]
        public async Task<IActionResult> Edit(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(userId);
            var model = mapper.Map<EditMemberViewModel>(dto);

            ViewBag.Directions = await directionService.GetAll();
            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]EditMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Directions = await directionService.GetAll();
                return View(model);
            }

            if (model.UserId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");

                return View(model);
            }

            var dto = mapper.Map<UserProfileModel>(model);

            await memberService.Update(dto);

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(userId);
            var model = mapper.Map<MemberViewModel>(dto);

            return View(model);
        }

        [HttpPost("delete/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest();
            }

            await memberService.Delete(userId);

            return RedirectToAction("Index");
        }

        [HttpGet("progress/{userId}")]
        public async Task<IActionResult> Progress(int userId)
        {
            ViewBag.Member = await memberService.GetMember(userId);
            var vTaskTracks = await taskTrackService.GetAllByUserId(userId);
            var progressModels = mapper.Map<IEnumerable<ProgressModel>>(vTaskTracks);

            return View(progressModels);
        }

        [HttpGet("userTasks/{userId}")]
        public async Task<IActionResult> UserTasks(int userId)
        {
            var tasks = await memberService.GetTasksByUserId(userId);
            ViewBag.Member = await memberService.GetMember(userId);
            var userTasksModel = mapper.Map<IEnumerable<UserTasksModel>>(tasks);
            return View(userTasksModel);
        }
    }
}
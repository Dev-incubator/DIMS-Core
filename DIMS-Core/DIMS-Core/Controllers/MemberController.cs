using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Account;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.Models.Member;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("members")]
    [Authorize]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;
        private readonly IDirectionService directionService;
        private readonly IUserService userService;

        public MemberController(IMemberService memberService,
                                IDirectionService directionService,
                                IMapper mapper,
                                IUserService userService)
        {
            this.memberService = memberService;
            this.directionService = directionService;
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await memberService.GetAll();
            var model = mapper.Map<IEnumerable<MemberViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Directions = await directionService.GetAll();
            return View();
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
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

            await userService.SignUp(signUpModel);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(id);
            var model = mapper.Map<EditMemberViewModel>(dto);

            ViewBag.Directions = await directionService.GetAll();
            return View(model);
        }

        [HttpPost("edit")]
        [Authorize(Roles = "admin")]
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

        [HttpGet("delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(id);
            var model = mapper.Map<MemberViewModel>(dto);

            return View(model);
        }

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await memberService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
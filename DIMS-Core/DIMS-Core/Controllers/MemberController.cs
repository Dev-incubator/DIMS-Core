using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Members;
using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.Models.Member;
using DIMS_Core.Models.Sample;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("members")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            this.memberService = memberService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await memberService.Search();
            var model = mapper.Map<IEnumerable<MemberViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]AddMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = mapper.Map<UserProfileModel>(model);

            await memberService.Create(dto);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await memberService.GetMember(id);
            var model = mapper.Map<EditMemberViewModel>(dto);

            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm]EditMemberViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
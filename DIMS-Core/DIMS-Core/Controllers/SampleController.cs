using AutoMapper;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Samples;
using DIMS_Core.Models.Sample;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("samples")]
    public class SampleController : Controller
    {
        private readonly ISampleService sampleService;
        private readonly IMapper mapper;

        public SampleController(ISampleService sampleService, IMapper mapper)
        {
            this.sampleService = sampleService;
            this.mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var searchResult = await sampleService.Search(null);
            var model = mapper.Map<IEnumerable<SampleViewModel>>(searchResult);

            return View(model);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await sampleService.GetSample(id);
            var model = mapper.Map<SampleViewModel>(dto);

            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SampleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = mapper.Map<SampleModel>(model);

            await sampleService.Create(dto);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await sampleService.GetSample(id);
            var model = mapper.Map<SampleViewModel>(dto);

            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] SampleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.SampleId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");

                return View(model);
            }

            var dto = mapper.Map<SampleModel>(model);

            await sampleService.Update(dto);

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var dto = await sampleService.GetSample(id);
            var model = mapper.Map<SampleViewModel>(dto);

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

            await sampleService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
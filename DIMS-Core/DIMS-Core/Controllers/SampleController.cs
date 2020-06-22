using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.BusinessLayer.Models.Samples;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DIMS_Core.Controllers
{
    [Route("samples")]
    public class SampleController : Controller
    {
        private readonly ISampleService sampleService;

        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await sampleService.SearchAsync(null);
            return View(result);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var model = await sampleService.GetSampleAsync(id);

            return View(model);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] SampleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await sampleService.CreateAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var model = await sampleService.GetSampleAsync(id);

            return View(model);
        }

        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] SampleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.SampleId <= 0)
            {
                ModelState.AddModelError("", "Incorrect identifier.");

                return View();
            }

            await sampleService.UpdateAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var model = await sampleService.GetSampleAsync(id);

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

            await sampleService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
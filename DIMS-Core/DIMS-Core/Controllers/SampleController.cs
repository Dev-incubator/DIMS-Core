using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIMS_Core.BusinessLayer.Interfaces;
using DIMS_Core.DataAccessLayer.Filters;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetSamples(SampleFilter filter)
        {
            if (filter is null)
            {
                return BadRequest();
            }

            var result = await sampleService.SearchAsync(filter);

            return Ok(result);
        }
    }
}

namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;
    
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        public RegionController(IRegionsMainService regionsMainService)
        {
            this.RegionsMainService = regionsMainService;
        }

        public IRegionsMainService RegionsMainService { get; }

        
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = this.RegionsMainService.GetRegions();

            return Ok(regions);
        }
    }
}

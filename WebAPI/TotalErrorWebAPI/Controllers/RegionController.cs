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

        [HttpPost]
        [Route("add/{regionName}")]
        public IActionResult AddRegion([FromRoute] string regionName)
        {
            if (regionName is null)
            {
                return BadRequest("Invalid region name!");
            }

            this.RegionsMainService.AddRegion(regionName);

            return Ok(regionName);
        }

        [HttpPost]
        [Route("update/{oldRegionName}-{newRegionName}")]
        public IActionResult UpdateItemType([FromRoute] string oldRegionName, [FromRoute] string newRegionName)
        {
            if (oldRegionName is null || newRegionName is null)
            {
                return BadRequest("Invalid item type name!");
            }

            this.RegionsMainService.UpdateRegion(oldRegionName, newRegionName);

            return Ok(newRegionName);
        }

        [HttpPost]
        [Route("delete/{region}")]
        public IActionResult DeleteItemType([FromRoute] string region)
        {
            if (region is null)
            {
                return BadRequest("Invalid country name!");
            }

            this.RegionsMainService.DeleteRegion(region);

            return Ok($"{region} is deleted!");
        }
    }
}

namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RegionController : ControllerBase
    {
        public RegionController(IRegionsMainService regionsMainService)
        {
            this.RegionsMainService = regionsMainService;
        }

        public IRegionsMainService RegionsMainService { get; }

        
        [HttpGet]
        [Route("All")]
        public IActionResult GetAllRegions()
        {
            var regions = this.RegionsMainService.GetRegions();

            var regionsJson = JsonConvert.SerializeObject(regions);

            return Ok(regionsJson);
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

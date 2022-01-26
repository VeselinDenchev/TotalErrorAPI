namespace TotalErrorWebAPI.Controllers
{
    using Constants.Controllers;

    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route(ControllerConstant.CONTROLLER_ROUTE)]
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
        [Route(RegionControllerConstant.GET_ALL_REGIONS_ROUTE)]
        public IActionResult GetAllRegions()
        {
            var regions = this.RegionsMainService.GetRegions();

            var regionsJson = JsonConvert.SerializeObject(regions);

            return Ok(regionsJson);
        }

        [HttpPost]
        [Route(RegionControllerConstant.ADD_REGION_ROUTE)]
        public IActionResult AddRegion([FromRoute] string regionName)
        {
            if (regionName is null)
            {
                return BadRequest(RegionControllerConstant.INVALID_REGION_NAME_MESSAGE);
            }

            this.RegionsMainService.AddRegion(regionName);

            return Ok(regionName);
        }

        [HttpPost]
        [Route(RegionControllerConstant.UPDATE_REGION_ROUTE)]
        public IActionResult UpdateItemType([FromRoute] string oldRegion, [FromRoute] string newRegion)
        {
            if (oldRegion is null || newRegion is null)
            {
                return BadRequest(RegionControllerConstant.INVALID_REGION_NAME_MESSAGE);
            }

            this.RegionsMainService.UpdateRegion(oldRegion, newRegion);

            return Ok(newRegion);
        }

        [HttpPost]
        [Route(RegionControllerConstant.DELETE_REGION_ROUTE)]
        public IActionResult DeleteItemType([FromRoute] string region)
        {
            if (region is null)
            {
                return BadRequest(RegionControllerConstant.INVALID_REGION_NAME_MESSAGE);
            }

            this.RegionsMainService.DeleteRegion(region);

            return Ok(string.Format(RegionControllerConstant.REGION_DELETED_SUCCESSFULLY_MESSAGE, region));
        }
    }
}

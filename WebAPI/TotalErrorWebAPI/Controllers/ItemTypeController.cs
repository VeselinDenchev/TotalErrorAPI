namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        public ItemTypeController(IItemTypesMainService itemTypesMainService)
        {
            this.ItemTypesMainService = itemTypesMainService;
        }

        public IItemTypesMainService ItemTypesMainService { get; set; }

        [HttpGet]
        public IActionResult GetAllItemTypes()
        {
            var itemTypes = ItemTypesMainService.GetItemTypes();

            return Ok(itemTypes);
        }
    }
}

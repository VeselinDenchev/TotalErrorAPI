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
        [Route("all")]
        public IActionResult GetAllItemTypes()
        {
            var itemTypes = ItemTypesMainService.GetItemTypes();

            return Ok(itemTypes);
        }

        [HttpPost]
        [Route("add/{itemType}")]
        public IActionResult AddItemType([FromRoute] string itemType)
        {
            if (itemType is null)
            {
                return BadRequest("Invalid item type name!");
            }

            this.ItemTypesMainService.AddItemType(itemType);

            return Ok(itemType);
        }

        [HttpPost]
        [Route("update/{oldItemTypeName}-{newItemTypeName}")]
        public IActionResult UpdateItemType([FromRoute] string oldItemTypeName, [FromRoute] string newItemTypeName)
        {
            if (oldItemTypeName is null || newItemTypeName is null)
            {
                return BadRequest("Invalid item type name!");
            }

            this.ItemTypesMainService.UpdateItemType(oldItemTypeName, newItemTypeName);

            return Ok(newItemTypeName);
        }

        [HttpPost]
        [Route("delete/{itemType}")]
        public IActionResult DeleteItemType([FromRoute] string itemType)
        {
            if (itemType is null)
            {
                return BadRequest("Invalid country name!");
            }

            this.ItemTypesMainService.DeleteItemType(itemType);

            return Ok($"{itemType} is deleted!");
        }
    }
}

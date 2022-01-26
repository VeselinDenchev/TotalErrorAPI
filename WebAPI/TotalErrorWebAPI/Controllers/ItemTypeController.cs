namespace TotalErrorWebAPI.Controllers
{
    using Constants.Controllers;

    using Data.Models.Models;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route(ControllerConstant.CONTROLLER_ROUTE)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemTypeController : ControllerBase
    {
        public ItemTypeController(IItemTypesMainService itemTypesMainService, UserManager<User> userManager)
        {
            this.ItemTypesMainService = itemTypesMainService;

        }

        public IItemTypesMainService ItemTypesMainService { get; set; }

        [HttpGet]
        [Route(ItemTypeControllerConstant.GET_ALL_ITEM_TYPES_ROUTE)]
        public IActionResult GetAllItemTypes()
        {
            var itemTypes = ItemTypesMainService.GetItemTypes();

            var itemTypesJson = JsonConvert.SerializeObject(itemTypes);

            return Ok(itemTypesJson);
        }

        [HttpPost]
        [Route(ItemTypeControllerConstant.ADD_ITEM_TYPE_ROUTE)]
        public IActionResult AddItemType([FromRoute] string itemType)
        {
            if (itemType is null)
            {
                return BadRequest(ItemTypeControllerConstant.INVALID_ITEM_TYPE_NAME_MESSAGE);
            }

            this.ItemTypesMainService.AddItemType(itemType);

            return Ok(itemType);
        }

        [HttpPost]
        [Route(ItemTypeControllerConstant.UPDATE_ITEM_TYPE_ROUTE)]
        public IActionResult UpdateItemType([FromRoute] string oldItemType, [FromRoute] string newItemType)
        {
            if (oldItemType is null || newItemType is null)
            {
                return BadRequest(ItemTypeControllerConstant.INVALID_ITEM_TYPE_NAME_MESSAGE);
            }

            this.ItemTypesMainService.UpdateItemType(oldItemType, newItemType);

            return Ok(newItemType);
        }

        [HttpPost]
        [Route(ItemTypeControllerConstant.DELETE_ITEM_TYPE_ROUTE)]
        public IActionResult DeleteItemType([FromRoute] string itemType)
        {
            if (itemType is null)
            {
                return BadRequest(ItemTypeControllerConstant.INVALID_ITEM_TYPE_NAME_MESSAGE);
            }

            this.ItemTypesMainService.DeleteItemType(itemType);

            return Ok(string.Format(ItemTypeControllerConstant.ITEM_TYPE_SUCCESSFULLY_DELETED_MESSAGE, itemType));
        }
    }
}

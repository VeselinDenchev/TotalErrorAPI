namespace TotalErrorWebAPI.Controllers
{
    using Data.Models.Models;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    using System.Security.Claims;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemTypeController : ControllerBase
    {
        public ItemTypeController(IItemTypesMainService itemTypesMainService, UserManager<User> userManager)
        {
            this.ItemTypesMainService = itemTypesMainService;

        }

        public IItemTypesMainService ItemTypesMainService { get; set; }

        //private UserManager<User> UserManager { get; set; }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllItemTypes()
        {
            var itemTypes = ItemTypesMainService.GetItemTypes();

            var itemTypesJson = JsonConvert.SerializeObject(itemTypes);

            return Ok(itemTypesJson);
        }

        [HttpPost]
        [Route("add/{itemType}")]
        public IActionResult AddItemType([FromRoute] string itemType)
        {
            if (itemType is null)
            {
                return BadRequest("Invalid item type name!");
            }

            //ClaimsPrincipal currentUser = this.User;
            //var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var user = this.UserManager.FindByNameAsync(User.Identity.Name).Result;
            //string userId = user.Id;

            //this.ItemTypesMainService.AddItemType(itemType, userId);

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
                return BadRequest("Invalid item type name!");
            }

            this.ItemTypesMainService.DeleteItemType(itemType);

            return Ok($"{itemType} is deleted!");
        }
    }
}

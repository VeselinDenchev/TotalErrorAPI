namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrdersMainService ordersMainService)
        {
            this.OrdersMainService = ordersMainService;
        }

        public IOrdersMainService OrdersMainService { get; }

        public IActionResult GetOrders()
        {
            var result = OrdersMainService.GetOrders();

            return Ok(result);
        }
    }
}

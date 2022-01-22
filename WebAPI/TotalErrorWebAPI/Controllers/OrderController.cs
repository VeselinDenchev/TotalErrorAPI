namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrdersMainService ordersMainService)
        {
            this.OrdersMainService = ordersMainService;
        }

        public IOrdersMainService OrdersMainService { get; }

        [HttpGet]
        [Route("all")]
        public IActionResult GetOrders()
        {
            var orders = OrdersMainService.GetOrders();

            return Ok(orders);
        }

        [HttpGet]
        [Route("orders_grouped_by_region")]
        public IActionResult GetOrdersGroupedByRegion()
        {
            var orders = OrdersMainService.GetOrdersGroupedByRegion(out Dictionary<RegionDto, decimal> countriesTotalCost,
                out Dictionary<RegionDto, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<RegionDto> groupedModel = new GroupedOrdersDtoModel<RegionDto>(orders,
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }

        [HttpGet]
        [Route("orders_grouped_by_country")]
        public IActionResult GetOrdersGroupedByCountry()
        {
            var orders = OrdersMainService.GetOrdersGroupedByCountry(out Dictionary<CountryDto, decimal> countriesTotalCost,
                out Dictionary<CountryDto, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<CountryDto> groupedModel = new GroupedOrdersDtoModel<CountryDto>(orders, 
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);


            return Ok(jsonObject);
        }

        [HttpGet]
        [Route("orders_grouped_by_order_date")]
        public IActionResult GetOrdersGroupedByOrderDate()
        {
            var orders = OrdersMainService.GetOrdersGroupedByOrderDate(out Dictionary<DateTime, decimal> countriesTotalCost,
                out Dictionary<DateTime, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<DateTime> groupedModel = new GroupedOrdersDtoModel<DateTime>(orders,
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }
    }
}

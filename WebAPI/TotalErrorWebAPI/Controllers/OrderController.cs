namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    //[Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrdersMainService ordersMainService)
        {
            this.OrdersMainService = ordersMainService;
        }

        public IOrdersMainService OrdersMainService { get; }

        [Route("api/[controller]/orders")]
        public IActionResult GetOrders()
        {
            var result = OrdersMainService.GetOrders();

            return Ok(result);
        }

        [Route("api/[controller]/orders_grouped_by_region")]
        public IActionResult GetOrdersGroupedByRegion()
        {
            var result = OrdersMainService.GetOrdersGroupedByRegion(out Dictionary<RegionDto, decimal> countriesTotalCost,
                out Dictionary<RegionDto, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<RegionDto> groupedModel = new GroupedOrdersDtoModel<RegionDto>(result,
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }

        [Route("api/[controller]/orders_grouped_by_country")]
        public IActionResult GetOrdersGroupedByCountry()
        {
            var result = OrdersMainService.GetOrdersGroupedByCountry(out Dictionary<CountryDto, decimal> countriesTotalCost,
                out Dictionary<CountryDto, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<CountryDto> groupedModel = new GroupedOrdersDtoModel<CountryDto>(result, 
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);


            return Ok(jsonObject);
        }

        [Route("api/[controller]/orders_grouped_by_order_date")]
        public IActionResult GetOrdersGroupedByOrderDate()
        {
            var result = OrdersMainService.GetOrdersGroupedByOrderDate(out Dictionary<DateTime, decimal> countriesTotalCost,
                out Dictionary<DateTime, decimal> countriesTotalProfit);

            GroupedOrdersDtoModel<DateTime> groupedModel = new GroupedOrdersDtoModel<DateTime>(result,
                countriesTotalCost, countriesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }
    }
}

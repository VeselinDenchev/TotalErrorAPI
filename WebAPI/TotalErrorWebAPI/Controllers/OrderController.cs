namespace TotalErrorWebAPI.Controllers
{
    using Constants.Controllers;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    [Route(ControllerConstant.CONTROLLER_ROUTE)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        public OrderController(IOrdersMainService ordersMainService, TotalErrorDbContext dbContext)
        {
            this.OrdersMainService = ordersMainService;
            this.DbContext = dbContext;
        }

        public IOrdersMainService OrdersMainService { get; }

        public TotalErrorDbContext DbContext { get; }

        [HttpGet]
        [Route(OrderControllerConstant.GET_ALL_ORDERS_ROUTE)]
        public IActionResult GetAllOrders()
        {
            var orders = OrdersMainService.GetOrders();
            
            var ordersJson = JsonConvert.SerializeObject(orders);

            return Ok(ordersJson);
        }

        [HttpGet]
        [Route(OrderControllerConstant.GET_ORDERS_GROUPED_BY_REGION_ROUTE)]
        public IActionResult GetOrdersGroupedByRegion()
        {
            var orders = OrdersMainService.GetOrdersGroupedByRegion(out Dictionary<RegionDto, decimal> regionsTotalCost,
                out Dictionary<RegionDto, decimal> regionsTotalProfit);

            GroupedOrdersDtoModel<RegionDto> groupedModel = new GroupedOrdersDtoModel<RegionDto>(orders,
                regionsTotalCost, regionsTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }

        [HttpGet]
        [Route(OrderControllerConstant.GET_ORDERS_GROUPED_BY_COUNTRY_ROUTE)]
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
        [Route(OrderControllerConstant.GET_ORDERS_GROUPED_BY_ORDER_DATE_ROUTE)]
        public IActionResult GetOrdersGroupedByOrderDate()
        {
            var orders = OrdersMainService.GetOrdersGroupedByOrderDate(out Dictionary<DateTime, decimal> datesTotalCost,
                out Dictionary<DateTime, decimal> datesTotalProfit);

            GroupedOrdersDtoModel<DateTime> groupedModel = new GroupedOrdersDtoModel<DateTime>(orders,
                datesTotalCost, datesTotalProfit);
            var jsonObject = JsonConvert.SerializeObject(groupedModel);

            return Ok(jsonObject);
        }

        [HttpPost]
        [Route(OrderControllerConstant.ADD_ORDER_ROUTE)]
        public IActionResult AddOrder([FromBody] OrderDto order)
        {
            bool isValid = order.SalesChannel is not null && order.Sales is not null && order.OrderPriority is not null 
                && order.Country is not null && order.Country.Region is not null;

            if (!isValid)
            {
                return BadRequest(OrderControllerConstant.INVALID_ORDER_DATA_MESSAGE);
            }

            bool isAlreadyInTheDatabase = false;

            foreach (SaleDto sale in order.Sales)
            {
                isAlreadyInTheDatabase = this.DbContext.Sales.Any(s => s.ShipDate == sale.ShipDate && s.UnitsSold == sale.UnitsSold
                && s.UnitPrice == sale.UnitPrice && s.UnitCost == sale.UnitCost && s.TotalRevenue == sale.TotalRevenue
                && s.TotalCost == sale.TotalCost && s.TotalProfit == sale.TotalProfit && s.ItemType.ItemTypeName == sale.ItemType.ItemTypeName 
                && s.IsDeleted == false);

                if (isAlreadyInTheDatabase)
                {
                    break;
                }
            }

            if (isAlreadyInTheDatabase)
            {
                return BadRequest(OrderControllerConstant.SALE_IS_ALREADY_IN_THE_DATABASE_MESSAGE);
            }

            this.OrdersMainService.AddOrder(order);

            return Ok(order);
        }

        [HttpPost]
        [Route(OrderControllerConstant.UPDATE_ORDER_ROUTE)]
        public IActionResult UpdateOrder(string orderId, [FromBody] OrderDto order)
        {
            Order initialOrder = this.DbContext.Orders.Where(o => o.Id == orderId && o.IsDeleted == false)
                                                        .Include(o => o.Sales.Where(s => s.IsDeleted == false)).FirstOrDefault();
            if (initialOrder is null)
            {
                return BadRequest(OrderControllerConstant.ORDER_WITH_SUCH_ID_DOESNT_EXIST_MESSAGE);
            }

            this.OrdersMainService.UpdateOrder(initialOrder, order);

            return Ok(order);
        }

        [HttpPost]
        [Route(OrderControllerConstant.DELETE_ORDER_ROUTE)]
        public IActionResult DeleteOrder(string orderId)
        {
            Order order = this.DbContext.Orders.Where(o => o.Id == orderId && o.IsDeleted == false)
                                                        .Include(o => o.Sales.Where(s => s.IsDeleted == false)).FirstOrDefault();
            
            if (order is null)
            {
                return BadRequest(OrderControllerConstant.ORDER_WITH_SUCH_ID_DOESNT_EXIST_MESSAGE);
            }

            this.OrdersMainService.DeleteOrder(order);

            return Ok(OrderControllerConstant.ORDER_IS_SUCCSESSFULLY_DELETED_MESSAGE);
        }
    }
}

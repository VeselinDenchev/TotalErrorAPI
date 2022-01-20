namespace Data.Services.MainServices
{
    using AutoMapper;

    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    public class OrdersMainService : IOrdersMainService
    {
        public OrdersMainService(TotalErrorDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public TotalErrorDbContext DbContext { get; set; }

        public IMapper Mapper { get; set; }

        public List<OrderDto> GetOrders()
        {
            /*var orders = this.DbContext.Orders.Select(x => 
            new 
            {
                x.Id,
                x.OrderDate,
                x.OrderPriority,
                x.Sales,
                x.SalesChannel,
                x.Country
            }
            ).Include(c => c.Country).Include(x => x).ThenInclude(s => s.Sales).ToList();*/

            this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.ToList());
            this.Mapper.Map<List<SaleDto>>(this.DbContext.Sales.ToList());
            var result = this.Mapper.Map<List<OrderDto>>(this.DbContext.Orders.ToList());

            return result;
        }
    }
}

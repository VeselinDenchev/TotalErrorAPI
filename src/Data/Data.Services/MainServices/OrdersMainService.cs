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
            this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());
            this.Mapper.Map<List<SaleDto>>(this.DbContext.Sales.Where(s => s.IsDeleted == false).ToList());
            this.Mapper.Map<List<ItemTypeDto>>(this.DbContext.ItemTypes.Where(it => it.IsDeleted == false).ToList());
            var orders = this.Mapper.Map<List<OrderDto>>(this.DbContext.Orders.Where(o => o.IsDeleted == false).ToList());

            return orders;
        }

        public List<IGrouping<RegionDto, OrderDto>> GetOrdersGroupedByRegion(out Dictionary<RegionDto, decimal> countriesTotalCost,
            out Dictionary<RegionDto, decimal> countriesTotalProfit)
        {
            this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());
            this.Mapper.Map<List<RegionDto>>(this.DbContext.Regions.Where(r => r.IsDeleted == false).ToList());
            this.Mapper.Map<List<SaleDto>>(this.DbContext.Sales.Where(s => s.IsDeleted == false).ToList());
            this.Mapper.Map<List<ItemTypeDto>>(this.DbContext.ItemTypes.Where(it => it.IsDeleted == false).ToList());
            var result = this.Mapper.Map<List<OrderDto>>(this.DbContext.Orders.Where(o => o.IsDeleted == false).ToList());
            var groupedByRegionResult = result.GroupBy(r => r.Country.Region).ToList();

            countriesTotalCost = new Dictionary<RegionDto, decimal>();
            countriesTotalProfit = new Dictionary<RegionDto, decimal>();
            foreach (var region in groupedByRegionResult)
            {
                countriesTotalCost[region.Key] = 0;
                countriesTotalProfit[region.Key] = 0;
            }

            foreach (var group in groupedByRegionResult)
            {
                var orders = group.ToList();

                foreach (OrderDto order in orders)
                {
                    foreach (SaleDto sale in order.Sales)
                    {
                        countriesTotalCost[group.Key] += sale.TotalCost;
                        countriesTotalProfit[group.Key] += sale.TotalProfit;
                    }

                }
            }

            return groupedByRegionResult;
        }

        public List<IGrouping<CountryDto, OrderDto>> GetOrdersGroupedByCountry(out Dictionary<CountryDto, decimal> countriesTotalCost,
            out Dictionary<CountryDto, decimal> countriesTotalProfit)
        {
            this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());
            this.Mapper.Map<List<SaleDto>>(this.DbContext.Sales.Where(s => s.IsDeleted == false).ToList());
            this.Mapper.Map<List<ItemTypeDto>>(this.DbContext.ItemTypes.Where(it => it.IsDeleted == false).ToList());
            var result = this.Mapper.Map<List<OrderDto>>(this.DbContext.Orders.Where(o => o.IsDeleted == false).ToList());
            var groupedByCountryResult = result.GroupBy(c => c.Country).ToList();

            countriesTotalCost = new Dictionary<CountryDto, decimal>();
            countriesTotalProfit = new Dictionary<CountryDto, decimal>();
            foreach (var country in groupedByCountryResult)
            {
                countriesTotalCost[country.Key] = 0;
                countriesTotalProfit[country.Key] = 0;
            }

            foreach (var group in groupedByCountryResult)
            {
                var orders = group.ToList();

                foreach (OrderDto order in orders)
                {
                    foreach (SaleDto sale in order.Sales)
                    {
                        countriesTotalCost[group.Key] += sale.TotalCost;
                        countriesTotalProfit[group.Key] += sale.TotalProfit;
                    }
                    
                }
            }

            return groupedByCountryResult;
        }

        public List<IGrouping<DateTime, OrderDto>> GetOrdersGroupedByOrderDate(out Dictionary<DateTime, decimal> countriesTotalCost,
            out Dictionary<DateTime, decimal> countriesTotalProfit)
        {
            this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());
            this.Mapper.Map<List<SaleDto>>(this.DbContext.Sales.Where(s => s.IsDeleted == false).ToList());
            this.Mapper.Map<List<ItemTypeDto>>(this.DbContext.ItemTypes.Where(it => it.IsDeleted == false).ToList());
            var result = this.Mapper.Map<List<OrderDto>>(this.DbContext.Orders.Where(o => o.IsDeleted == false).ToList());
            var groupedByOrderDateResult = result.GroupBy(od => od.OrderDate).ToList();

            countriesTotalCost = new Dictionary<DateTime, decimal>();
            countriesTotalProfit = new Dictionary<DateTime, decimal>();
            foreach (var date in groupedByOrderDateResult)
            {
                countriesTotalCost[date.Key] = 0;
                countriesTotalProfit[date.Key] = 0;
            }

            foreach (var group in groupedByOrderDateResult)
            {
                var orders = group.ToList();

                foreach (OrderDto order in orders)
                {
                    foreach (SaleDto sale in order.Sales)
                    {
                        countriesTotalCost[group.Key] += sale.TotalCost;
                        countriesTotalProfit[group.Key] += sale.TotalProfit;
                    }

                }
            }

            return groupedByOrderDateResult;
        }
    }
}

namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface IOrdersMainService
    {
        public List<OrderDto> GetOrders();

        public List<IGrouping<RegionDto, OrderDto>> GetOrdersGroupedByRegion(out Dictionary<RegionDto, decimal> countriesTotalCost,
            out Dictionary<RegionDto, decimal> countriesTotalProfit);

        public List<IGrouping<CountryDto, OrderDto>> GetOrdersGroupedByCountry(out Dictionary<CountryDto, decimal> countriesTotalCost,
            out Dictionary<CountryDto, decimal> countriesTotalProfit);

        public List<IGrouping<DateTime, OrderDto>> GetOrdersGroupedByOrderDate(out Dictionary<DateTime, decimal> countriesTotalCost,
            out Dictionary<DateTime, decimal> countriesTotalProfit);
    }
}

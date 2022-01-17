namespace Data.Services.MapperProfiles
{
    using AutoMapper;

    using Data.Models.Models;

    using Data.Services.DtoModels;

    public class MapProfile
    {
        public MapProfile()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Country, CountryDto>().ReverseMap();
                cfg.CreateMap<ItemType, ItemTypeDto>().ReverseMap();
                cfg.CreateMap<Order, OrderDto>().ReverseMap();
                cfg.CreateMap<Region, RegionDto>().ReverseMap();
                cfg.CreateMap<Sale, SaleDto>().ReverseMap();
            });

            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            Country country = new Country();
            CountryDto mappedCountry = new CountryDto();

            ItemType itemType = new ItemType();
            ItemTypeDto mappedItemType = new ItemTypeDto();

            Order order = new Order();
            OrderDto mappedOrder = new OrderDto();

            Region region = new Region();
            RegionDto mappedRegion = new RegionDto();

            Sale sale = new Sale();
            SaleDto mappedSale = new SaleDto();
        }
    }
}

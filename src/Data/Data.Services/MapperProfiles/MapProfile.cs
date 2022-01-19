namespace Data.Services.MapperProfiles
{
    using AutoMapper;

    using Data.Models.Models;

    using Data.Services.DtoModels;

    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<ItemType, ItemTypeDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
        }
    }
}

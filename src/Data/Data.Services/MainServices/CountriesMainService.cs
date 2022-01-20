namespace Data.Services.MainServices
{
    using AutoMapper;

    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    public class CountriesMainService : ICountriesMainService
    {
        public CountriesMainService(TotalErrorDbContext dbContext, IMapper mapper)
        {
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }

        public TotalErrorDbContext DbContext { get; }

        public IMapper Mapper { get; }

        public List<CountryDto> GetCountries()
        {
            this.Mapper.Map<List<RegionDto>>(this.DbContext.Regions.ToList());
            var countries = this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());

            return countries;
        }
    }
}

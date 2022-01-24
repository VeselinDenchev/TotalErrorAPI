namespace Data.Services.MainServices
{
    using AutoMapper;

    using Data.Models.Models;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;
    using Data.TotalErrorDbContext;

    using Microsoft.EntityFrameworkCore;

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
            this.Mapper.Map<List<RegionDto>>(this.DbContext.Regions.Where(r => r.IsDeleted == false).ToList());
            var countries = this.Mapper.Map<List<CountryDto>>(this.DbContext.Countries.Where(c => c.IsDeleted == false).ToList());

            return countries;
        }

        public CountryDto GetCountryByName(string name)
        {
            Country country = this.DbContext.Countries.Where(c => c.Name.ToUpper() == name.ToUpper()).Include(c => c.Region).FirstOrDefault();
            Region region = country.Region;

            CountryDto countryDto = this.Mapper.Map<Country, CountryDto>(country);

            return countryDto;
        }

        public void AddCountry(CountryDto countryDto)
        {
            Country country = this.Mapper.Map<CountryDto, Country>(countryDto);

            Region region = this.DbContext.Regions.FirstOrDefault(r => r.Name == countryDto.Region.Name);

            country.Region = region;

            if (region is not null)
            {
                this.DbContext.Regions.Attach(country.Region);
            }

            this.DbContext.Countries.Add(country);
            this.DbContext.SaveChanges();
        }

        public void UpdateCountry(string countryName, CountryDto countryDto)
        {
            Country country = this.DbContext.Countries.Where(c => c.Name == countryName && c.IsDeleted == false).FirstOrDefault();
            Region region = this.DbContext.Regions.Where(r => r.Name == countryDto.Region.Name && r.IsDeleted == false).FirstOrDefault();

            country.Name = countryDto.Name;
            country.Region = region;

            this.DbContext.Regions.Attach(country.Region);
            this.DbContext.Countries.Update(country);
            this.DbContext.SaveChanges();
        }

        public void DeleteCountry(string countryName)
        {
            Country country = this.DbContext.Countries.Where(c => c.Name == countryName && c.IsDeleted == false).FirstOrDefault();

            country.IsDeleted = true;
            country.DeletedAt = DateTime.Now;

            this.DbContext.Countries.Update(country);
            this.DbContext.SaveChanges();
        }
    }
}

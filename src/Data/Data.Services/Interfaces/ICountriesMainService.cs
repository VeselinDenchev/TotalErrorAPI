namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface ICountriesMainService
    {
        public List<CountryDto> GetCountries();

        public CountryDto GetCountryByName(string name);

        public void AddCountry(CountryDto countryDto);

        public void UpdateCountry(string countryName, CountryDto countryDto);

        public void DeleteCountry(string countryName);
    }
}

namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface ICountriesMainService
    {
        public List<CountryDto> GetCountries();
    }
}

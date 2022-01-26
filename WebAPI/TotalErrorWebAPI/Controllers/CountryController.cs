namespace TotalErrorWebAPI.Controllers
{
    using Constants.Controllers;

    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    [Route(ControllerConstant.CONTROLLER_ROUTE)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CountryController : Controller
    {
        public CountryController(ICountriesMainService ordersMainService)
        {
            this.CountriesMainService = ordersMainService;
        }

        public ICountriesMainService CountriesMainService { get; }

        [HttpGet]
        [Route(CountryControllerConstant.GET_ALL_COUNTRIES_ROUTE)]
        public IActionResult GetAllCountries()
        {
            List<CountryDto> countries = this.CountriesMainService.GetCountries();

            var countriesJson = JsonConvert.SerializeObject(countries);

            return Ok(countriesJson);
        }

        [HttpGet]
        [Route(CountryControllerConstant.GET_COUNTRY_BY_NAME_ROUTE)]
        public IActionResult GetCountryByName([FromRoute] string countryName)
        {
            CountryDto country = this.CountriesMainService.GetCountryByName(countryName);

            if (country is null)
            {
                return BadRequest(CountryControllerConstant.INVALID_COUNTRY_DATA_MESSAGE);
            }

            var countryJson = JsonConvert.SerializeObject(country);

            return Ok(countryJson);
        }

        [HttpPost]
        [Route(CountryControllerConstant.ADD_COUNTRY_ROUTE)]
        public IActionResult AddCountry([FromBody] CountryDto country)
        {
            if (country.Name is null || country.Region is null)
            {
                return BadRequest(CountryControllerConstant.INVALID_COUNTRY_DATA_MESSAGE);
            }

            this.CountriesMainService.AddCountry(country);

            return Ok(country);
        }

        [HttpPost]
        [Route(CountryControllerConstant.UPDATE_COUNTRY_ROUTE)]
        public IActionResult UpdateCountry([FromRoute]string countryName, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry.Name is null || updatedCountry.Region is null)
            {
                return BadRequest(CountryControllerConstant.INVALID_COUNTRY_DATA_MESSAGE);
            }

            this.CountriesMainService.UpdateCountry(countryName, updatedCountry);

            return Ok(updatedCountry);
        }

        [HttpPost]
        [Route(CountryControllerConstant.DELETE_COUNTRY_ROUTE)]
        public IActionResult DeleteCountry([FromRoute] string countryName)
        {
            if (countryName is null)
            {
                return BadRequest(CountryControllerConstant.INVALID_COUNTRY_NAME_MESSAGE);
            }

            this.CountriesMainService.DeleteCountry(countryName);

            return Ok(string.Format(CountryControllerConstant.COUNTRY_DELETED_SUCCESSFULLY_MESSAGE, countryName));
        }
    }
}

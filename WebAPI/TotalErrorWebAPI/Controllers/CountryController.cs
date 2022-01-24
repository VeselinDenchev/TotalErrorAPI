namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CountryController : Controller
    {
        public CountryController(ICountriesMainService ordersMainService)
        {
            this.CountriesMainService = ordersMainService;
        }

        public ICountriesMainService CountriesMainService { get; }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllCountries()
        {
            List<CountryDto> countries = this.CountriesMainService.GetCountries();

            return Ok(countries);
        }

        [HttpGet]
        [Route("get_country_by_name/{countryName}")]
        public IActionResult GetCountryByName([FromRoute] string countryName)
        {
            CountryDto country = this.CountriesMainService.GetCountryByName(countryName);

            if (country is null)
            {
                return BadRequest("Invalid country data!");
            }

            return Ok(country);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCountry([FromBody] CountryDto country)
        {
            if (country.Name is null || country.Region is null)
            {
                return BadRequest("Invalid country data!");
            }

            this.CountriesMainService.AddCountry(country);

            return Ok(country);
        }

        [HttpPost]
        [Route("update/{countryName}")]
        public IActionResult UpdateCountry([FromRoute]string countryName, [FromBody] CountryDto updatedCountry)
        {
            if (updatedCountry.Name is null || updatedCountry.Region is null)
            {
                return BadRequest("Invalid country data!");
            }

            this.CountriesMainService.UpdateCountry(countryName, updatedCountry);

            return Ok(updatedCountry);
        }

        [HttpPost]
        [Route("delete/{countryName}")]
        public IActionResult DeleteCountry([FromRoute] string countryName)
        {
            if (countryName is null)
            {
                return BadRequest("Invalid country name!");
            }

            this.CountriesMainService.DeleteCountry(countryName);

            return Ok($"{countryName} is deleted!");
        }
    }
}

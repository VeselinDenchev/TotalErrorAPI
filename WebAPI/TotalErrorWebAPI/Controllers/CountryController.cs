namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CountryController : Controller
    {
        public CountryController(ICountriesMainService ordersMainService)
        {
            this.OrdersMainService = ordersMainService;
        }

        public ICountriesMainService OrdersMainService { get; }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            var countries = this.OrdersMainService.GetCountries();

            return Ok(countries);
        }
    }
}

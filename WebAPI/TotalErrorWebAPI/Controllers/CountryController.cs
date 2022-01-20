namespace TotalErrorWebAPI.Controllers
{
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        public CountryController(ICountriesMainService ordersMainService)
        {
            this.OrdersMainService = ordersMainService;
        }

        public ICountriesMainService OrdersMainService { get; }

        public IActionResult GetAllCountries()
        {
            var result = this.OrdersMainService.GetCountries();

            return Ok(result);
        }
    }
}

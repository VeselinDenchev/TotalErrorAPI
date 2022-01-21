namespace TotalErrorWebAPI.Controllers
{
    using Data.Models.Models;
    using Data.Services.ViewModels;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    //[Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        [HttpPost]
        [Route("api/[controller]/register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel registerModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(registerModel.Email);
            if (checkUser is null)
            {
                User newUser = new User()
                {
                    Email = registerModel.Email,
                    UserName = registerModel.Email
                };

                var result = await UserManager.CreateAsync(newUser, registerModel.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(loginModel.Email);

            var checkPassword = await SignInManager.CheckPasswordSignInAsync(checkUser, loginModel.Password, lockoutOnFailure: false);
            if (checkUser is not null)
            {
                if (checkPassword.Succeeded)
                {
                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }
    }
}

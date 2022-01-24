namespace TotalErrorWebAPI.Controllers
{
    using Data.Models.Models;
    using Data.Services.ViewModels;
    using Data.Services.DtoModels;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Data.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService jwtService)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.JwtService = jwtService;
        }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> SignInManager { get; set; }

        public IJwtTokenService JwtService { get; set; }

        [HttpPost]
        [Route("register")]
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

                var token = JwtService.GenerateUserToken(new RequestTokenModel()
                {
                    Email = newUser.Email,
                    UserName = newUser.UserName,
                });

                var result = await UserManager.CreateAsync(newUser, registerModel.Password);

                if (result.Succeeded && token.Length > 0)
                {
                    return Ok();
                }
                return BadRequest("Register attempt failed! Check email and password!");
            }

            return BadRequest("Invalid register data!");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(loginModel.Email);

            var checkPassword = await SignInManager.CheckPasswordSignInAsync(checkUser, loginModel.Password, lockoutOnFailure: false);
            if (checkUser is not null)
            {
                var token = JwtService.GenerateUserToken(new RequestTokenModel()
                {
                    Email = checkUser.Email,
                    UserName = checkUser.UserName,
                });

                if (checkPassword.Succeeded && token.Length > 0)
                {
                    return Ok();
                }

                return BadRequest();
            }

            return BadRequest();
        }
    }
}

namespace TotalErrorWebAPI.Controllers
{
    using Data.Models.Models;
    using Data.Services.ViewModels;
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Constants.Controllers;

    [Route(ControllerConstant.CONTROLLER_ROUTE)]
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
        [Route(AccountControllerConstant.REGISTER_ROUTE)]
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
                    return Ok(AccountControllerConstant.SUCCESSFULLY_REGISTERED_MESSAGE);
                }
                return BadRequest(AccountControllerConstant.REGISTER_ATTEMPT_FAILED_MESSAGE);
            }

            return BadRequest(AccountControllerConstant.INVALID_REGISTER_DATA_MESSAGE);
        }

        [HttpPost]
        [Route(AccountControllerConstant.LOGIN_ROUTE)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginModel)
        {
            var checkUser = await UserManager.FindByEmailAsync(loginModel.Email);
            
            if (checkUser is not null)
            {
                var checkPassword = await SignInManager.CheckPasswordSignInAsync(checkUser, loginModel.Password, lockoutOnFailure: false);

                var token = JwtService.GenerateUserToken(new RequestTokenModel()
                {
                    Email = checkUser.Email,
                    UserName = checkUser.UserName,
                });

                if (checkPassword.Succeeded && token.Length > 0)
                {
                    return Ok(AccountControllerConstant.SUCCESSFULLY_LOGGED_IN_MESSAGE);
                }

                return BadRequest(AccountControllerConstant.WRONG_PASSWORD_OR_TOKEN_ERROR_MESSAGE);
            }

            return BadRequest(AccountControllerConstant.NO_SUCH_USER_MESSAGE);
        }
    }
}

namespace Data.Services.Implementations
{
    using Data.Services.DtoModels;
    using Data.Services.Interfaces;

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JwtTokenService : IJwtTokenService
    {
        public JwtTokenService(IOptions<TokenModel> options)
        {
            this.TokenModel = options.Value;
        }

        private readonly TokenModel TokenModel;

        public string GenerateUserToken(RequestTokenModel request)
        {
            string token = string.Empty;

            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, request.UserName),
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenModel.TokenSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                TokenModel.ValidateIssuer,
                TokenModel.ValidateAudience,
                claim,
                expires: DateTime.Now.AddMinutes(TokenModel.AccessExpiration),
                signingCredentials: credentials
            );

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}

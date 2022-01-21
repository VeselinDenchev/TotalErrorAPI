namespace Data.Services.Interfaces
{
    using Data.Services.DtoModels;

    public interface IJwtTokenService
    {
        public string GenerateUserToken(RequestTokenModel request);
    }
}

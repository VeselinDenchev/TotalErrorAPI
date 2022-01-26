namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;
    using Constants;

    [JsonObject(TokenModelConstant.JWT)]
    public class TokenModel
    {
        [JsonProperty(TokenModelConstant.TOKEN_SECRET)]
        public string TokenSecret { get; set; }

        [JsonProperty(TokenModelConstant.VALIDATE_ISSUER)]
        public string ValidateIssuer { get; set; }

        [JsonProperty(TokenModelConstant.VALIDATE_AUDIENCE)]
        public string ValidateAudience { get; set; }

        [JsonProperty(TokenModelConstant.ACCESS_EXPIRATION)]
        public int AccessExpiration { get; set; }

        [JsonProperty(TokenModelConstant.REFRESH_EXPIRATION)]
        public int RefreshExpiration { get; set; }
    }
}

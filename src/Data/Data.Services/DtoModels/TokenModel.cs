namespace Data.Services.DtoModels
{
    using Newtonsoft.Json;

    [JsonObject("JWT")]
    public class TokenModel
    {
        [JsonProperty("TokenSecret")]
        public string TokenSecret { get; set; }

        [JsonProperty("ValidateIssuer")]
        public string ValidateIssuer { get; set; }

        [JsonProperty("ValidateAudience")]
        public string ValidateAudience { get; set; }

        [JsonProperty("AccessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("RefreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}

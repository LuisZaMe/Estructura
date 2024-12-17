using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public class APIConfig
    {
        [JsonProperty("Logging")]
        public Logging Logging { get; set; }

        [JsonProperty("AllowedHosts")]
        public string AllowedHosts { get; set; }

        [JsonProperty("Environments")]
        public Environment Environment { get; set; }

        [JsonProperty("TokenizationCredentials")]
        public TokenizationCredentials TokenizationCredentials { get; set; }

        [JsonProperty("CompanyInformation")]
        public CompanyInformation CompanyInformation { get; set; }
    }
}

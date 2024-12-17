using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public partial class TokenizationCredentials
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("IV")]
        public string Iv { get; set; }

        [JsonProperty("MobileApiHashKey")]
        public string ApiHashKey { get; set; }

        [JsonProperty("MinutesToLive")]
        public long MinutesToLive { get; set; }

        [JsonProperty("DaysToLive")]
        public long DaysToLive { get; set; }

        [JsonProperty("Issuer")]
        public string Issuer { get; set; }

        [JsonProperty("Audience")]
        public string Audience { get; set; }

        [JsonProperty("RequireHttpsMetadata")]
        public bool RequireHttpsMetadata { get; set; }
    }
}

using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public partial class LogLevel
    {
        [JsonProperty("Default")]
        public string Default { get; set; }

        [JsonProperty("Microsoft")]
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }
}

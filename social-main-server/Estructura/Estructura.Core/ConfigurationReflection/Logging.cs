using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public partial class Logging
    {
        [JsonProperty("LogLevel")]
        public LogLevel LogLevel { get; set; }
    }
}

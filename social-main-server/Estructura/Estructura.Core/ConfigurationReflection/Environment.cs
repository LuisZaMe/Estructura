using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public partial class Environment
    {
        [JsonProperty("Connection")]
        public string Connection { get; set; }

        [JsonProperty("APIUrl")]
        public string APIUrl { get; set; }

        [JsonProperty("EmailFrom")]
        public string EmailFrom { get; set; }

        [JsonProperty("EmailHost")]
        public string EmailHost { get; set; }

        [JsonProperty("EmailPass")]
        public string EmailPass { get; set; }

        [JsonProperty("EmailPort")]
        public int EmailPort { get; set; }

        [JsonProperty("WebUrl")]
        public string WebUrl { get; set; }
    }
}

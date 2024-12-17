using Newtonsoft.Json;

namespace Estructura.Core.ConfigurationReflection
{
    public partial class CompanyInformation
    {
        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("CompanyID")]
        public long CompanyId { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }
    }
}

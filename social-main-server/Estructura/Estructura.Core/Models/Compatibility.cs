using System;

namespace Estructura.Core.Models
{
    public partial class Compatibility
    {
        public int AppVersion { get; set; }
        public DateTime ApiVersion { get; set; }
        public bool KillSwitch { get; set; }
    }
}

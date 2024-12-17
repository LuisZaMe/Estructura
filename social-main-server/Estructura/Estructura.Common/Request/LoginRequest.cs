using Estructura.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Request
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int AppBuildVersion { get; set; }
    }
}

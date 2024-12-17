using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Request
{
    public class CompleteRecoverPasswordRequest
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}

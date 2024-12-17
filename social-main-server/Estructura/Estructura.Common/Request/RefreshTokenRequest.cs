using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Request
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }//Refresh token, not just the slide jwt token
        public int AppBuildVersion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Response
{
    public class LoginResponse : ResponseBaseModel
    {
        public string Token { get; set; }
        public DateTime SlidingExpiration { get; set; }
        public Models.Identity Identity { get; set; }
        public string RefreshToken { get; set; }
    }
}

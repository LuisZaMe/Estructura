using Estructura.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Response
{
    public class RefreshTokenResponse : ResponseBaseModel
    {
        public Identity Identity { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime SlidingExpiration { get; set; }
    }
}

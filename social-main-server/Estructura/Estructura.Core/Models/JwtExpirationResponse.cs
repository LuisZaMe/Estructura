using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Core.Models
{
    public class JwtExpirationResponse
    {
        public string Token { get; set; }
        public DateTime SlidingExpiration { get; set; }
    }
}

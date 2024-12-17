using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public string ChallengeHash { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public DateTime IssuedServerDate { get; set; }
        public int DaysToLive { get; set; }
        public bool IsValid { get; set; }
    }
}

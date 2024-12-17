using Estructura.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Core.Utilities
{
    public interface ITokenUtil
    {
        Dictionary<string, string> Decode(string token);
        List<System.Security.Claims.Claim> GetClaims(Common.Models.Identity input);
        JwtExpirationResponse GetAndStoreJWTToken(List<System.Security.Claims.Claim> claims);
        public string CreateRefreshToken(Common.Models.Identity input);
        public byte[] Hash(string input);
        public string GenericEncode(object data);
        public T GenericDecode<T>(string encoded);
    }
}

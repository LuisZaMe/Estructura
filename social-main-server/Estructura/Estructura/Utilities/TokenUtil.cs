using Jose;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Estructura.Common.Models;
using Estructura.Core.Models;
using Estructura.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Estructura.Common.Enums;

namespace Estructura.API.Utilities
{
    public class TokenUtil : ITokenUtil
    {
        private static byte[] _hash;
        MemoryCacheEntryOptions cacheOptions;
        Estructura.Core.ConfigurationReflection.APIConfig options;
        IMemoryCache cache;
        public TokenUtil(IOptions<Estructura.Core.ConfigurationReflection.APIConfig> options, IMemoryCache cache)
        {
            this.options = options.Value;
            this.cache = cache;
            cacheOptions = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = new TimeSpan(0, (int)this.options.TokenizationCredentials.MinutesToLive, 0), //removed on one hour if it is inactive
                AbsoluteExpiration = DateTime.Now.AddDays(this.options.TokenizationCredentials.DaysToLive),//removed on one year for sure
            };
        }


        #region Private Methods
        private string GenerateChallenge()
        {
            string challenge = "";
            Random _random = new Random();
            for (int i = 0; i < 32; i++)
            {
                int num = _random.Next(0, 26); // Zero to 25
                char let = (char)('a' + num);
                challenge += let;
            }
            return challenge;
        }

        private byte[] GetHash()
        {
            if (_hash != null) return _hash;

            var shaManaged = new SHA256Managed();

            _hash = shaManaged.ComputeHash(Encoding.UTF8.GetBytes(options.TokenizationCredentials.ApiHashKey));

            return _hash;
        }

        private string Encode(ClaimsIdentity id)
        {
            var token = Jose.JWT.Encode(ClaimsToDictionary(id), GetHash(), JweAlgorithm.DIR, JweEncryption.A128CBC_HS256,
                JweCompression.DEF);

            return token;
        }
        private string Encode(Dictionary<string, string> input)
        {
            var token = Jose.JWT.Encode(input, GetHash(), JweAlgorithm.DIR, JweEncryption.A128CBC_HS256,
                JweCompression.DEF);

            return token;
        }
        private Dictionary<string, string> ClaimsToDictionary(ClaimsIdentity id)
        {
            var di = new Dictionary<string, string>();
            foreach (var claim in id.Claims)
            {
                string keyName;

                switch (claim.Type)
                {
                    case ClaimTypes.NameIdentifier:
                        keyName = "sub";
                        break;
                    case ClaimTypes.Name:
                        keyName = "name";
                        break;
                    case "role":
                        keyName = ClaimTypes.Role;
                        break;
                    default:
                        keyName = claim.Type;
                        break;
                }

                di.Add(keyName, claim.Value);
            }
            string hasExpire;
            di.TryGetValue("ExpiresOn", out hasExpire);

            if (string.IsNullOrEmpty(hasExpire))
            {
                di.Add("ExpiresOn", DateTime.UtcNow.AddMinutes(double.Parse(options.TokenizationCredentials.MinutesToLive.ToString())).ToString(CultureInfo.InvariantCulture));
            }
            string hasCreatedOn;
            di.TryGetValue("CreatedOn", out hasCreatedOn);

            if (string.IsNullOrEmpty(hasCreatedOn))
            {
                di.Add("CreatedOn", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
            }

            return di;
        }
        private ClaimsIdentity DictionaryToClaims(Dictionary<string, string> di, string authenticationType)
        {
            var user = new ClaimsIdentity(authenticationType);

            foreach (var kv in di)
            {
                user.AddClaim(new Claim(kv.Key, kv.Value));
            }
            return user;
        }
        #endregion


        public Dictionary<string, string> Decode(string token)
        {
            var decoded = Jose.JWT.Decode(token, GetHash());
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(decoded);
        }

        public List<Claim> GetClaims(Identity input)
        {
            Claim permissions = null;
            switch (input.Role)
            {
                case Common.Enums.Role.ADMINISTRADOR:
                    permissions = new Claim(ClaimTypes.Role, Estructura.Core.Policies.Policies.Administrador);
                    break;
                case Common.Enums.Role.CLIENTES:
                    permissions = new Claim(ClaimTypes.Role, Estructura.Core.Policies.Policies.Clientes);
                    break;
                case Common.Enums.Role.INTERNO_ANALISTA:
                    permissions = new Claim(ClaimTypes.Role, Estructura.Core.Policies.Policies.InternoAnalista);
                    break;
                case Common.Enums.Role.INTERNO_ENTREVISTADOR:
                    permissions = new Claim(ClaimTypes.Role, Estructura.Core.Policies.Policies.InternoEntrevistador);
                    break;
            }

            var claims = new List<Claim>
            {
                //to add claims just add a new claim and be sure to add the property in the appuser model
                new Claim(JwtRegisteredClaimNames.Sub, input.Id.ToString()),
                permissions,
                new Claim(AppUser.CLAIM_USER, input.Id.ToString()),
                new Claim(AppUser.CLAIM_USER_ID, input.Id.ToString()),
                new Claim(AppUser.CLAIM_EMAIL, input.Email),
                new Claim(AppUser.CLAIM_USER_ROLE, ((int)input.Role).ToString())
            };
            return claims;
        }

        public JwtExpirationResponse GetAndStoreJWTToken(List<Claim> claims)
        {
            try
            {
                var securityKeyBytes = Encoding.ASCII.GetBytes(options.TokenizationCredentials.ApiHashKey);
                var securityKey = new SymmetricSecurityKey(securityKeyBytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.UtcNow.AddMinutes(double.Parse(options.TokenizationCredentials.MinutesToLive.ToString()));
                var jwtToken = new JwtSecurityToken(
                    issuer: options.TokenizationCredentials.Issuer.ToString(),
                    audience: options.TokenizationCredentials.Audience,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: credentials);

                //return updated expiration dates and new token
                return new JwtExpirationResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    SlidingExpiration = expiration
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CreateRefreshToken(Identity input)
        {
            var tokenValues = new Dictionary<string, string>
            {
                {AppUser.CLAIM_CHALLENGE, GenerateChallenge() },
                {AppUser.CLAIM_EMAIL, input.Email },
                {AppUser.CLAIM_ISSUED_SERVER_DATE, DateTime.UtcNow.ToString() },
                {AppUser.CLAIM_DAYS_TO_LIVE, this.options.TokenizationCredentials.DaysToLive.ToString() }
            };
            var token = Encode(tokenValues);//token to be used by the client
            return token;
        }

        public byte[] Hash(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, input);
            return md5.ComputeHash(mStream.ToArray());
        }

        public string GenericEncode(object data)
        {
            var serialized = JsonConvert.SerializeObject(data);
            var dicc = new Dictionary<string, string>();
            dicc.Add("data", serialized);            
            return System.Web.HttpUtility.UrlEncode(Encode(dicc));
        }

        public T GenericDecode<T>(string encoded)
        {
            var dicc = Decode(System.Web.HttpUtility.UrlDecode(encoded));
            string output = "";
            if (dicc.TryGetValue("data", out output))
                return JsonConvert.DeserializeObject<T>(output);
            return default(T);
        }
    }
}

using Estructura.Common.Request;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IAuthRepository
    {
        public Task<LoginResponse> LoginAsync(LoginRequest input);
        public Task<Common.Models.RefreshToken> GetRefreshTokenFromDB(long customerID);
        public Task<Common.Models.RefreshToken> GetRefreshTokenFromDB(string token);
        public Task<Common.Models.Identity> GetIdentity(long customerId);
        public Task<int> DeleteAllOldTokens(Common.Models.RefreshToken tokenToMaintain);
        public Task<bool> DeleteRefreshToken(string token);
        public Task<bool> StoreRefreshToken(System.Collections.Generic.Dictionary<string, string> decryptedRefreshToken, string token, byte[] hash, Common.Models.Identity customer);
    }
}

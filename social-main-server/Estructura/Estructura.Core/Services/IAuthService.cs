using Estructura.Common.Request;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Services
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<RefreshTokenResponse> RefreshAsync(RefreshTokenRequest request);
    }
}

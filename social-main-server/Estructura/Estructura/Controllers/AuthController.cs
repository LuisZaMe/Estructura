using Estructura.Common.Request;
using Estructura.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estructura.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly Core.Services.IAuthService _login;
        public AuthController(Core.Services.IAuthService login, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            _login = login;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<Common.Response.LoginResponse>> LoginAsync(LoginRequest request)
        {
            return await GetActionResult(_login.LoginAsync(request));
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<Common.Response.RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequest request)
        {
            return await GetActionResult(_login.RefreshAsync(request));
        }
    }
}

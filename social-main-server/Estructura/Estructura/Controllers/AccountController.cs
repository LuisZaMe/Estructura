using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
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
    public class AccountController : BaseController
    {
        private readonly Core.Services.IAccountService _account;
        public AccountController(Core.Services.IAccountService account, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            _account = account;
        }

        [HttpPost("Superadmin")]
        //[Authorize(Policy = Core.Policies.Policies.SuperAdministrador)]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<Identity>>> CreateSuperAdmin(Identity request)
        {
            var user = AppUser;
            return await GetActionResult(_account.CreateSuperAdmin(request));
        }


        [HttpPost("")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<Identity>>> CreateAccount(Identity request)
        {
            return await GetActionResult(_account.CreateAccount(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.Administrador)]
        public async Task<ActionResult<GenericResponse<Identity>>> DeleteUserAccount(long Id)
        {
            return await GetActionResult(_account.DeleteUserAccount(Id));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Identity>>>> GetActiveUsers([FromQuery] List<long> Id, [FromQuery] int currentPage, [FromQuery] int offset)
        {
            return await GetActionResult(_account.GetActiveUsers(Id, currentPage, offset));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Identity>>> UpdateUserInformation(Identity request)
        {
            return await GetActionResult(_account.UpdateUserInformation(request));
        }

        [HttpGet("Pending")]
        [Authorize(Policy = Core.Policies.Policies.Administrador)]
        public async Task<ActionResult<GenericResponse<List<Identity>>>> GetPendingApprovalUsers(int currentPage, int offset)
        {
            return await GetActionResult(_account.GetPendingApprovalUsers(currentPage, offset));
        }

        [HttpPut("{Id}/Approve")]
        [Authorize(Policy = Core.Policies.Policies.Administrador)]
        public async Task<ActionResult<GenericResponse<Identity>>> ApproveAccount(long Id)
        {
            return await GetActionResult(_account.ApproveAccount(new Identity() { Id = Id}));
        }

        [HttpPut("{Id}/Reject")]
        [Authorize(Policy = Core.Policies.Policies.Administrador)]
        public async Task<ActionResult<GenericResponse<Identity>>> UnapproveAccount(long Id)
        {
            return await GetActionResult(_account.UnapproveAccount(new Identity() { Id = Id }));
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<List<Identity>>>> SearchUsers(string key, bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE, int currentPage = 0, int offset = 10)
        {
            return await GetActionResult(_account.SearchUsers(key, showSuperAdmin, role, currentPage, offset));
        }

        [HttpGet("Role")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Identity>>>> GetUsersByRole(Common.Enums.Role role, int currentPage, int offset)
        {
            return await GetActionResult(_account.GetUsersByRole(role, currentPage, offset));
        }

        [HttpPost("SendRecoverPasswordMail")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<bool>>> SendRecoverPasswordMail(RecoverPasswordRequest request)
        {
            return await GetActionResult(_account.SendRecoverPasswordMail(request));
        }

        [HttpPut("ChangePassword")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<Identity>>> UpdateAccountPassword(CompleteRegistration request)
        {
            return await GetActionResult(_account.UpdateAccountPassword(request));
        }

        [HttpPost("CompleteRecoverPassword")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<Identity>>> CompleteRecoverPassword(CompleteRecoverPasswordRequest request)
        {
            return await GetActionResult(_account.RecoverPasswordByMail(request));
        }

        [HttpPut("CompleteRegistration")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<Identity>>> CompleteAccountRegistration(CompleteRegistration request)
        {
            return await GetActionResult(_account.CompleteAccountRegistration(request));
        }

        [HttpGet("Pagination")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<int>>> Pagination(int splitBy = 10, string key = "", bool showSuperAdmin = false, Common.Enums.Role role = Common.Enums.Role.NONE)
        {
            return await GetActionResult(_account.Pagination(splitBy, key, showSuperAdmin, role));
        }

    }
}

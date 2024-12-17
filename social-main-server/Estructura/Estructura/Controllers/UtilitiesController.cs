using Estructura.Common.Models;
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
    public class UtilitiesController: BaseController
    {
        private readonly Core.Services.IUtilitiesService _utilitiesService;
        public UtilitiesController(Core.Services.IUtilitiesService _utilitiesService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._utilitiesService=_utilitiesService;
        }


        [HttpGet("GetCurrentVersion")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<string>>> GetCurrentVersion()
        {
            return await GetActionResult(_utilitiesService.GetCurrentVersion());
        }

        [HttpGet("IsAppCompatible")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<bool>>> IsAppCompatible(int appBuildVersion)
        {
            return await GetActionResult(_utilitiesService.IsAppCompatible(appBuildVersion));
        }

        [HttpGet("VerifyEmailExist")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<bool>>> VerifyEmailExist(string email)
        {
            return await GetActionResult(_utilitiesService.VerifyEmailExist(email));
        }

        [HttpGet("GetCities")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<List<City>>>> GetCities(int stateId)
        {
            return await GetActionResult(_utilitiesService.GetCities(stateId));
        }

        [HttpGet("GetStates")]
        [AllowAnonymous]
        public async Task<ActionResult<GenericResponse<List<State>>>> GetStates()
        {
            return await GetActionResult(_utilitiesService.GetStates());
        }
    }
}

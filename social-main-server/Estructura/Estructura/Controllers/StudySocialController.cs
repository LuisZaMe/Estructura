using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Services;
using Estructura.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estructura.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudySocialController:BaseController
    {
        private readonly IStudySocialService _studySocialService;
        public StudySocialController(IStudySocialService _studySocialService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studySocialService=_studySocialService;
        }


        // Study Social
        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudySocial>>> CreateStudySocial(StudySocial request)
        {
            return await GetActionResult(_studySocialService.CreateStudySocial(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudySocial>>>> GetStudySocial([FromQuery] List<long> Id)
        {
            return await GetActionResult(_studySocialService.GetStudySocial(Id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudySocial>>> UpdateStudySocial(StudySocial request)
        {
            return await GetActionResult(_studySocialService.UpdateStudySocial(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudySocial>>> DeleteStudySocial(long Id)
        {
            return await GetActionResult(_studySocialService.DeleteStudySocial(Id));
        }



        // Social Goals
        [HttpPost("SocialGoals")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<SocialGoals>>>> CreateSocialGoals(List<SocialGoals> request)
        {
            return await GetActionResult(_studySocialService.CreateSocialGoals(request));
        }

        [HttpGet("SocialGoals")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<SocialGoals>>>> GetSocialGoals([FromQuery] List<long> Id)
        {
            return await GetActionResult(_studySocialService.GetSocialGoals(Id));
        }

        [HttpPut("SocialGoals")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<SocialGoals>>> UpdateSocialGoals(SocialGoals request)
        {
            return await GetActionResult(_studySocialService.UpdateSocialGoals(request));
        }

        [HttpDelete("SocialGoals")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<SocialGoals>>> DeleteSocialGoals(long Id)
        {
            return await GetActionResult(_studySocialService.DeleteSocialGoals(Id));
        }
    }
}

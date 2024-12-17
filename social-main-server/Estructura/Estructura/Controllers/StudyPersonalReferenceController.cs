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
    public class StudyPersonalReferenceController:BaseController
    {
        private readonly IStudyPersonalReferenceService _studyPersonalReferenceService;
        public StudyPersonalReferenceController(IStudyPersonalReferenceService _studyPersonalReferenceService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyPersonalReferenceService=_studyPersonalReferenceService;
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyPersonalReference>>>> GetStudyPersonalReference([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyPersonalReferenceService.GetStudyPersonalReference(id));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyPersonalReference>>> DeleteStudyPersonalReference(long id)
        {
            return await GetActionResult(_studyPersonalReferenceService.DeleteStudyPersonalReference(id));
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<StudyPersonalReference>>>> CreateStudyPersonalReference(List<StudyPersonalReference> request)
        {
            return await GetActionResult(_studyPersonalReferenceService.CreateStudyPersonalReference(request));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyPersonalReference>>> UpdateStudyPersonalReference(StudyPersonalReference request)
        {
            return await GetActionResult(_studyPersonalReferenceService.UpdateStudyPersonalReference(request));
        }
    }
}

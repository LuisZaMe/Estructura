using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estructura.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudyFinalResultController:BaseController
    {
        private readonly Core.Services.IStudyFinalResultService _studyfinalResultService;
        public StudyFinalResultController(Core.Services.IStudyFinalResultService _studyfinalResultService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyfinalResultService = _studyfinalResultService;
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<StudyFinalResult>>>> GetStudyFinalResult([FromQuery] List<long> id, int currentPage, int offset)
        {
            return await GetActionResult(_studyfinalResultService.GetStudyFinalResult(id, currentPage, offset));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyFinalResult>>> GetStudyFinalResult(long id)
        {
            return await GetActionResult(_studyfinalResultService.DeleteStudyFinalResult(id));
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyFinalResult>>> CreateStudyFinalResult(StudyFinalResult request)
        {
            return await GetActionResult(_studyfinalResultService.CreateStudyFinalResult(request));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyFinalResult>>> UpdateStudyFinalResult(StudyFinalResult request)
        {
            return await GetActionResult(_studyfinalResultService.UpdateStudyFinalResult(request));
        }
    }
}

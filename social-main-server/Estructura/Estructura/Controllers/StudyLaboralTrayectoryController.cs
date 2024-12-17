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
    public class StudyLaboralTrayectoryController:BaseController
    {
        private readonly IStudyLaboralTrayectoryService _studyLaboralTrayectoryService;
        public StudyLaboralTrayectoryController(IStudyLaboralTrayectoryService _studyLaboralTrayectoryService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyLaboralTrayectoryService=_studyLaboralTrayectoryService;
        }


        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<StudyLaboralTrayectory>>>> GetStudyLaboralTrayectory([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyLaboralTrayectoryService.GetStudyLaboralTrayectory(id));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyLaboralTrayectory>>> DeleteStudyLaboralTrayectory(long id)
        {
            return await GetActionResult(_studyLaboralTrayectoryService.DeleteStudyLaboralTrayectory(id));
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyLaboralTrayectory>>>> CreateStudyLaboralTrayectory(List<StudyLaboralTrayectory> request)
        {
            return await GetActionResult(_studyLaboralTrayectoryService.CreateStudyLaboralTrayectory(request));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyLaboralTrayectory>>> UpdateStudyLaboralTrayectory(StudyLaboralTrayectory request)
        {
            return await GetActionResult(_studyLaboralTrayectoryService.UpdateStudyLaboralTrayectory(request));
        }
    }
}

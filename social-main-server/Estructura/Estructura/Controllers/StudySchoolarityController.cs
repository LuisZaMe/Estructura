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
    public class StudySchoolarityController:BaseController
    {
        private readonly IStudySchoolarityService _studySchoolarityService;
        public StudySchoolarityController(IStudySchoolarityService _studySchoolarityService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studySchoolarityService=_studySchoolarityService;
        }

        // Study Schoolarity
        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudySchoolarity>>> CreateStudySchoolarity(StudySchoolarity request)
        {
            return await GetActionResult(_studySchoolarityService.CreateStudySchoolarity(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudySchoolarity>>>> GetStudySchoolarity([FromQuery] List<long> id)
        {
            return await GetActionResult(_studySchoolarityService.GetStudySchoolarity(id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudySchoolarity>>> UpdateStudySchoolarity(StudySchoolarity request)
        {
            return await GetActionResult(_studySchoolarityService.UpdateStudySchoolarity(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudySchoolarity>>> DeleteStudySchoolarity(long id)
        {
            return await GetActionResult(_studySchoolarityService.DeleteStudySchoolarity(id));
        }



        // Schoolarity
        [HttpPost("Schoolarity")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Scholarity>>>> CreateSchoolarity(List<Scholarity> request)
        {
            return await GetActionResult(_studySchoolarityService.CreateSchoolarity(request));
        }

        [HttpGet("Schoolarity")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Scholarity>>>> GetSchoolarity([FromQuery] List<long> id)
        {
            return await GetActionResult(_studySchoolarityService.GetSchoolarity(id));
        }

        [HttpPut("Schoolarity")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Scholarity>>> UpdateSchoolarity(Scholarity request)
        {
            return await GetActionResult(_studySchoolarityService.UpdateSchoolarity(request));
        }

        [HttpDelete("Schoolarity")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Scholarity>>> DeleteSchoolarity(long id)
        {
            return await GetActionResult(_studySchoolarityService.DeleteSchoolarity(id));
        }



        // Extracurricular Activities
        [HttpPost("ExtracurricularActivities")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<ExtracurricularActivities>>>> CreateExtracurricularActivities(List<ExtracurricularActivities> request)
        {
            return await GetActionResult(_studySchoolarityService.CreateExtracurricularActivities(request));
        }

        [HttpGet("ExtracurricularActivities")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<ExtracurricularActivities>>>> GetExtracurricularActivities([FromQuery] List<long> id)
        {
            return await GetActionResult(_studySchoolarityService.GetExtracurricularActivities(id));
        }

        [HttpPut("ExtracurricularActivities")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<ExtracurricularActivities>>> UpdateExtracurricularActivities(ExtracurricularActivities request)
        {
            return await GetActionResult(_studySchoolarityService.UpdateExtracurricularActivities(request));
        }

        [HttpDelete("ExtracurricularActivities")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<ExtracurricularActivities>>> DeleteExtracurricularActivities(long id)
        {
            return await GetActionResult(_studySchoolarityService.DeleteExtracurricularActivities(id));
        }


    }
}

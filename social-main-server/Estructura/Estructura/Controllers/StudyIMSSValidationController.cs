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
    public class StudyIMSSValidationController:BaseController
    {
        private readonly IStudyIMSSValidationService _studyIMSSValidationService;
        public StudyIMSSValidationController(IStudyIMSSValidationService _studyIMSSValidationService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyIMSSValidationService=_studyIMSSValidationService;
        }


        //StudyImssValidation
        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyIMSSValidation>>> CreateStudyIMSSValidation(StudyIMSSValidation request)
        {
            return await GetActionResult(_studyIMSSValidationService.CreateStudyIMSSValidation(request));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyIMSSValidation>>> UpdateStudyIMSSValidation(StudyIMSSValidation request)
        {
            return await GetActionResult(_studyIMSSValidationService.UpdateStudyIMSSValidation(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyIMSSValidation>>>> GetStudyIMSSValidation([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyIMSSValidationService.GetStudyIMSSValidation(id));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyIMSSValidation>>> DeleteStudyIMSSValidation(long id)
        {
            return await GetActionResult(_studyIMSSValidationService.DeleteStudyIMSSValidation(id));
        }



        //Imss Validation
        [HttpPost("IMSSValidation")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<IMSSValidation>>>> CreateIMSSValidation(List<IMSSValidation> request)
        {
            return await GetActionResult(_studyIMSSValidationService.CreateIMSSValidation(request));
        }

        [HttpPut("IMSSValidation")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<IMSSValidation>>> UpdateIMSSValidation(IMSSValidation request)
        {
            return await GetActionResult(_studyIMSSValidationService.UpdateIMSSValidation(request));
        }

        [HttpGet("IMSSValidation")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<IMSSValidation>>>> GetIMSSValidation([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyIMSSValidationService.GetIMSSValidation(id));
        }

        [HttpDelete("IMSSValidation")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<IMSSValidation>>> DeleteIMSSValidation(long id)
        {
            return await GetActionResult(_studyIMSSValidationService.DeleteIMSSValidation(id));
        }
    }
}

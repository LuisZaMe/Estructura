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
    public class StudyFamilyController:BaseController
    {
        private readonly IStudyFamilyService _studyFamilyService;
        public StudyFamilyController(IStudyFamilyService _studyFamilyService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyFamilyService=_studyFamilyService;
        }

        // StudyFamily
        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyFamily>>> CreateStudyFamily(StudyFamily request)
        {
            return await GetActionResult(_studyFamilyService.CreateStudyFamily(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyFamily>>>> GetStudyFamily([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyFamilyService.GetStudyFamily(id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyFamily>>> UpdateStudyFamily(StudyFamily request)
        {
            return await GetActionResult(_studyFamilyService.UpdateStudyFamily(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyFamily>>> DeleteStudyFamily(long id)
        {
            return await GetActionResult(_studyFamilyService.DeleteStudyFamily(id));
        }



        //LivingFamily
        [HttpPost("LivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<LivingFamily>>>> CreateLivingFamily(List<LivingFamily> request)
        {
            return await GetActionResult(_studyFamilyService.CreateLivingFamily(request));
        }

        [HttpGet("LivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<LivingFamily>>>> GetLivingFamily([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyFamilyService.GetLivingFamily(id));
        }

        [HttpPut("LivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<LivingFamily>>> UpdateLivingFamily(LivingFamily request)
        {
            return await GetActionResult(_studyFamilyService.UpdateLivingFamily(request));
        }

        [HttpDelete("LivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<LivingFamily>>> DeleteLivingFamily(long id)
        {
            return await GetActionResult(_studyFamilyService.DeleteLivingFamily(id));
        }



        //NoLivingFamily
        [HttpPost("NoLivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<NoLivingFamily>>>> CreateNoLivingFamily(List<NoLivingFamily> request)
        {
            return await GetActionResult(_studyFamilyService.CreateNoLivingFamily(request));
        }

        [HttpGet("NoLivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<NoLivingFamily>>>> GetNoLivingFamily([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyFamilyService.GetNoLivingFamily(id));
        }

        [HttpPut("NoLivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<NoLivingFamily>>> UpdateNoLivingFamily(NoLivingFamily request)
        {
            return await GetActionResult(_studyFamilyService.UpdateNoLivingFamily(request));
        }

        [HttpDelete("NoLivingFamily")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<NoLivingFamily>>> DeleteNoLivingFamily(long id)
        {
            return await GetActionResult(_studyFamilyService.DeleteNoLivingFamily(id));
        }
    }
}

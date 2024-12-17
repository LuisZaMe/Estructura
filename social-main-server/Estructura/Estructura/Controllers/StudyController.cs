using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Request;
using Estructura.Common.Response;
using Estructura.Core.Services;
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
    public class StudyController:BaseController
    {
        private readonly IStudyService _studyService;
        public StudyController(IStudyService _studyService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyService=_studyService;
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Study>>> CreateStudy(Study request)
        {
            return await GetActionResult(_studyService.CreateStudy(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Study>>>> GetStudy(
            [FromQuery] List<long> Id,
            long AnalystId,
            DateTime? startDate,
            DateTime? endDate,
            Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE,
            long interviewerId = 0,
            Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE,
            long clientId = 0,
            long candidateId = 0,
            StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE,
            int currentPage = 0,
            int offset = 10,
            bool bringStudiesOnlyOwn = true,
            int bringStudiesOnly = 0 
          )
        {
            return await GetActionResult(
                _studyService.GetStudy(
                    Id,
                    AnalystId,
                    startDate,
                    endDate,
                    serviceType,
                    interviewerId,
                    studyStatus,
                    clientId,
                    candidateId,
                    studyProgressStatus,
                    currentPage,
                    offset,
                    bringStudiesOnlyOwn,
                    bringStudiesOnly
                 )
            );
        }

        [HttpGet("Pagination")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<int>>> Pagination([FromQuery] List<long> Id, DateTime? startDate, DateTime? endDate, Common.Enums.ServiceTypes serviceType = Common.Enums.ServiceTypes.NONE, long interviewerId = 0, Common.Enums.StudyStatus studyStatus = Common.Enums.StudyStatus.NONE, long clientId = 0, long candidateId = 0, StudyProgressStatus studyProgressStatus = StudyProgressStatus.NONE, int splitBy = 10)
        {
            return await GetActionResult(_studyService.Pagination(Id, startDate, endDate, serviceType, interviewerId, studyStatus, clientId, candidateId, studyProgressStatus, splitBy ));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Study>>> DeleteStudy(long Id)
        {
            return await GetActionResult(_studyService.DeleteStudy(Id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Study>>> UpdateStudy(Study request)
        {
            return await GetActionResult(_studyService.UpdateStudy(request));
        }


        //Note
        [HttpPost("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyNote>>> CreateNote(StudyNote request)
        {
            return await GetActionResult(_studyService.CreateNote(request));
        }

        [HttpPut("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyNote>>> UpdateNote(StudyNote request)
        {
            return await GetActionResult(_studyService.UpdateNote(request));
        }

        [HttpDelete("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyNote>>> DeleteNote(long Id)
        {
            return await GetActionResult(_studyService.DeleteNote(Id));
        }

        [HttpGet("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyNote>>>> GetNotes([FromQuery] List<long> Id, string key = "", long studyId = 0, int currentPage = 0, int offset = 10)
        {
            return await GetActionResult(_studyService.GetNotes(Id, key, studyId, currentPage, offset));
        }
    } 
}

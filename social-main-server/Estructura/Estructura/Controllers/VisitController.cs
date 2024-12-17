using Estructura.Common.Enums;
using Estructura.Common.Models;
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
    public class VisitController: BaseController
    {
        private readonly IVisitService _visitService;
        public VisitController(IVisitService _visitService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._visitService=_visitService;
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Visit>>> CreateVisit(Visit request)
        {
            return await GetActionResult(_visitService.CreateVisit(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Visit>>>> GetVisit([FromQuery] List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int currentPage = 0, int offset = 10)
        {
            return await GetActionResult(_visitService.GetVisit(Id, interviewerId, candidateId, clientId, studyId, startDate,endDate,cityId,stateId,visitStatus,currentPage,offset));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Visit>>> UpdateVisit(Visit request)
        {
            return await GetActionResult(_visitService.UpdateVisit(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Visit>>> DeleteVisit(long Id)
        {
            return await GetActionResult(_visitService.DeleteVisit(Id));
        }

        [HttpGet("Pagination")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<int>>> Pagination([FromQuery] List<long> Id, long? interviewerId, long? candidateId, long? clientId, long? studyId, DateTime? startDate, DateTime? endDate, int? cityId, int? stateId, VisitStatus visitStatus = VisitStatus.NONE, int splitBy = 10)
        {
            return await GetActionResult(_visitService.Pagination(Id, interviewerId, candidateId, clientId, studyId, startDate, endDate, cityId, stateId, visitStatus, splitBy));
        }

    }
}

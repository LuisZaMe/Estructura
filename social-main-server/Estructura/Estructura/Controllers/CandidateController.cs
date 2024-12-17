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
    public class CandidateController:BaseController
    {
        private readonly Core.Services.ICandidateService _candidate;
        public CandidateController(Core.Services.ICandidateService _candidate, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._candidate=_candidate;
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Candidate>>> CreateCandidate(Candidate request)
        {
            return await GetActionResult(_candidate.CreateCandidate(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Candidate>>>> GetCandidate([FromQuery] List<long> Id, int currentPage, int offset)
        {
            return await GetActionResult(_candidate.GetCandidate(Id,currentPage,offset));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Candidate>>> UpdateCandidate(Candidate request)
        {
            return await GetActionResult(_candidate.UpdateCandidate(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Candidate>>> DeleteCandidate(long Id)
        {
            return await GetActionResult(_candidate.DeleteCandidate(Id));
        }

        [HttpGet("Pagination")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<int>>> Pagination(string key = "", long clientId = 0, int splitBy = 10)
        {
            return await GetActionResult(_candidate.Pagination(key, clientId, splitBy));
        }

        [HttpGet("Search")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Candidate>>>> GetCandidate(string key, long clientId = 0, int currentPage = 0, int offset = 10)
        {
            return await GetActionResult(_candidate.SearchCandidate(key, clientId, currentPage, offset));
        }



        //Notes
        [HttpPost("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<CandidateNote>>> CreateNote(CandidateNote request)
        {
            return await GetActionResult(_candidate.CreateNote(request));
        }

        [HttpPut("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<CandidateNote>>> UpdateNote(CandidateNote request)
        {
            return await GetActionResult(_candidate.UpdateNote(request));
        }

        [HttpDelete("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<CandidateNote>>> DeleteNote(long Id)
        {
            return await GetActionResult(_candidate.DeleteNote(Id));
        }

        [HttpGet("Note")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<CandidateNote>>>> GetNotes([FromQuery] List<long> Id, string key = "", long candidateId = 0, int currentPage = 0, int offset = 10)
        {
            return await GetActionResult(_candidate.GetNotes(Id, key, candidateId, currentPage, offset));
        }
    }
}

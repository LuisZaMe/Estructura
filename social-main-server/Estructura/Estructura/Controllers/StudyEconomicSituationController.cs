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
    public class StudyEconomicSituationController:BaseController
    {
        private readonly IStudyEconomicSituationService _studyEconomicSituationService;
        public StudyEconomicSituationController(IStudyEconomicSituationService _studyEconomicSituationService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyEconomicSituationService=_studyEconomicSituationService;
        }



        //Study Economic Situation
        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyEconomicSituation>>> CreateStudyEconomicSituation(StudyEconomicSituation request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateStudyEconomicSituation(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyEconomicSituation>>>> GetStudyEconomicSituation([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetStudyEconomicSituation(id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyEconomicSituation>>> UpdateStudyEconomicSituation(StudyEconomicSituation request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateStudyEconomicSituation(request));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyEconomicSituation>>> DeleteStudyEconomicSituation(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteStudyEconomicSituation(id));
        }



        //Incoming
        [HttpPost("Incoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Incoming>>>> CreateIncoming(List<Incoming> request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateIncoming(request));
        }

        [HttpGet("Incoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Incoming>>>> GetIncoming([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetIncoming(id));
        }

        [HttpPut("Incoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Incoming>>> UpdateIncoming(Incoming request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateIncoming(request));
        }

        [HttpDelete("Incoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Incoming>>> DeleteIncoming(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteIncoming(id));
        }



        //AdditionalIncoming
        [HttpPost("AdditionalIncoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<AdditionalIncoming>>>> CreateAdditionalIncoming(List<AdditionalIncoming> request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateAdditionalIncoming(request));
        }

        [HttpGet("AdditionalIncoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<AdditionalIncoming>>>> GetAdditionalIncoming([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetAdditionalIncoming(id));
        }

        [HttpPut("AdditionalIncoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<AdditionalIncoming>>> UpdateAdditionalIncoming(AdditionalIncoming request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateAdditionalIncoming(request));
        }

        [HttpDelete("AdditionalIncoming")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<AdditionalIncoming>>> DeleteAdditionalIncoming(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteAdditionalIncoming(id));
        }



        //Credit
        [HttpPost("Credit")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Credit>>>> CreateCredit(List<Credit> request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateCredit(request));
        }

        [HttpGet("Credit")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Credit>>>> GetCredit([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetCredit(id));
        }

        [HttpPut("Credit")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Credit>>> UpdateCredit(Credit request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateCredit(request));
        }

        [HttpDelete("Credit")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Credit>>> DeleteCredit(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteCredit(id));
        }



        //Estate
        [HttpPost("Estate")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Estate>>>> CreateEstate(List<Estate> request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateEstate(request));
        }

        [HttpGet("Estate")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Estate>>>> GetEstate([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetEstate(id));
        }

        [HttpPut("Estate")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Estate>>> UpdateEstate(Estate request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateEstate(request));
        }

        [HttpDelete("Estate")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Estate>>> DeleteEstate(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteEstate(id));
        }



        //Estate
        [HttpPost("Vehicle")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<Vehicle>>>> CreateVehicle(List<Vehicle> request)
        {
            return await GetActionResult(_studyEconomicSituationService.CreateVehicle(request));
        }

        [HttpGet("Vehicle")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<Vehicle>>>> GetVehicle([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyEconomicSituationService.GetVehicle(id));
        }

        [HttpPut("Vehicle")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Vehicle>>> UpdateVehicle(Vehicle request)
        {
            return await GetActionResult(_studyEconomicSituationService.UpdateVehicle(request));
        }

        [HttpDelete("Vehicle")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<Vehicle>>> DeleteVehicle(long id)
        {
            return await GetActionResult(_studyEconomicSituationService.DeleteVehicle(id));
        }
    }
}

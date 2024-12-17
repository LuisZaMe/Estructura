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
    public class StudyGeneralInformationController:BaseController
    {
        private readonly IStudyGeneralInformationService _studyGeneralInformationController;
        public StudyGeneralInformationController(IStudyGeneralInformationService _studyGeneralInformationController, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyGeneralInformationController=_studyGeneralInformationController;
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyGeneralInformation>>> CreateStudyGeneralInformation(StudyGeneralInformation request)
        {
            return await GetActionResult(_studyGeneralInformationController.CreateStudyGeneralInformation(request));
        }

        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<StudyGeneralInformation>>>> GetStudyGeneralInformation([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyGeneralInformationController.GetStudyGeneralInformation(id));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyGeneralInformation>>> DeleteStudyGeneralInformation(long id)
        {
            return await GetActionResult(_studyGeneralInformationController.DeleteStudyGeneralInformation(id));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyGeneralInformation>>> UpdateStudyGeneralInformation(StudyGeneralInformation request)
        {
            return await GetActionResult(_studyGeneralInformationController.UpdateStudyGeneralInformation(request));
        }




        // Recommendation cards
        [HttpPost("RecommendationCard")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<RecommendationCard>>>> CreateRecommendationCard(List<RecommendationCard> request)
        {
            return await GetActionResult(_studyGeneralInformationController.CreateRecommendationCard(request));
        }

        [HttpPut("RecommendationCard")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<RecommendationCard>>> UpdateRecommendationCard(RecommendationCard request)
        {
            return await GetActionResult(_studyGeneralInformationController.UpdateRecommendationCard(request));
        }

        [HttpGet("RecommendationCard")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<List<RecommendationCard>>>> GetRecommendationCard([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyGeneralInformationController.GetRecommendationCard(id));
        }

        [HttpDelete("RecommendationCard")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<RecommendationCard>>> DeleteRecommendationCard(long id)
        {
            return await GetActionResult(_studyGeneralInformationController.DeleteRecommendationCard(id));
        }
    }
}

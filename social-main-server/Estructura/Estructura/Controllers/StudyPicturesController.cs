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
    public class StudyPicturesController:BaseController
    {
        private readonly IStudyPicturesService _studyPicturesService;
        public StudyPicturesController(IStudyPicturesService _studyPicturesService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._studyPicturesService=_studyPicturesService;
        }


        [HttpGet("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<List<StudyPictures>>>> GetStudyPictures([FromQuery] List<long> id)
        {
            return await GetActionResult(_studyPicturesService.GetStudyPictures(id));
        }

        [HttpDelete("")]
        [Authorize(Policy = Core.Policies.Policies.InternoGlobal)]
        public async Task<ActionResult<GenericResponse<StudyPictures>>> DeleteStudyPictures(long id)
        {
            return await GetActionResult(_studyPicturesService.DeleteStudyPictures(id));
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyPictures>>> CreateStudyPictures(StudyPictures request)
        {
            return await GetActionResult(_studyPicturesService.CreateStudyPictures(request));
        }

        [HttpPut("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<StudyPictures>>> UpdateStudyPictures(StudyPictures request)
        {
            return await GetActionResult(_studyPicturesService.UpdateStudyPictures(request));
        }
    }
}

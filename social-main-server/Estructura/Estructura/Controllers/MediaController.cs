using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Estructura.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : BaseController
    {
        private readonly Core.Services.IMediaService _mediaService;
        public MediaController(Core.Services.IMediaService _mediaService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._mediaService=_mediaService;
        }

        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Media>>> CreateMedia(Media request)
        {
            return await GetActionResult(_mediaService.CreateMedia(request));
        }
    }
}

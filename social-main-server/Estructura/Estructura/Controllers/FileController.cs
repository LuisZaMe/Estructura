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
    public class FileController:BaseController
    {
        private readonly Core.Services.IFileService _fileService;
        public FileController(Core.Services.IFileService _fileService, ITokenUtil tokenUtil) : base(tokenUtil)
        {
            this._fileService = _fileService;   
        }


        [HttpPost("")]
        [Authorize(Policy = Core.Policies.Policies.InternoPlataforma)]
        public async Task<ActionResult<GenericResponse<Doccument>>> CreateMedia(Doccument request)
        {
            return await GetActionResult(_fileService.CreateFile(request));
        }
    }
}

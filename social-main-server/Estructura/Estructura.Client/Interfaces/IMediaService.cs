using Estructura.Common.Models;
using Estructura.Common.Response;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IMediaService
    {
        Task<GenericResponse<Media>> CreateMedia(Media request);
    }
}

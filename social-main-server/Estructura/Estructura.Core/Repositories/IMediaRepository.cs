using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IMediaRepository
    {
        Task<GenericResponse<Media>> CreateMedia(Media request);
        Task<GenericResponse<List<Media>>> GetMedia(List<long> Id);
        Task<GenericResponse<Media>> DeleteMedia(long Id);
        Task<GenericResponse<Media>> StoreMedia(StoreMediaType place, Media media);
    }
}

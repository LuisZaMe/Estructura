using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using Estructura.Core.Repositories;
using Estructura.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.BLL.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        public MediaService(IMediaRepository _mediaRepository)
        {
            this._mediaRepository=_mediaRepository;
        }


        public async Task<GenericResponse<Media>> CreateMedia(Media request)
        {
            var error = new GenericResponse<Media>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
            var stored = await StoreMedia(request.StoreMediaType, request);

            if (stored!=null&&stored.Sucess && stored.Response!=null)
            {
                return await _mediaRepository.CreateMedia(stored.Response); ;
            }
            else
            {
                error.ErrorMessage = stored!=null&&!string.IsNullOrWhiteSpace(stored.ErrorMessage) ? stored.ErrorMessage : "Internal server error";
                return error;
            }
        }

        public async Task<GenericResponse<Media>> DeleteMedia(long Id)
        {
            return await _mediaRepository.DeleteMedia(Id);
        }

        public async Task<GenericResponse<List<Media>>> GetMedia(List<long> Id)
        {
            return await _mediaRepository.GetMedia(Id);
        }

        public async Task<GenericResponse<Media>> StoreMedia(StoreMediaType place, Media media)
        {
            return await _mediaRepository.StoreMedia(place, media);
        }
    }
}

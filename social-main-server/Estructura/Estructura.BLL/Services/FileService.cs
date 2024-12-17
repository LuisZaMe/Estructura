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
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        public FileService(IFileRepository _fileRepository)
        {
            this._fileRepository=_fileRepository;
        }

        public async Task<GenericResponse<Doccument>> CreateFile(Doccument request, bool returnBase64Source = false)
        {
            var error = new GenericResponse<Doccument>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            };
            var stored = await StoreFile(request.StoreMediaType, request);

            if (stored!=null&&stored.Sucess && stored.Response!=null)
            {
                return await _fileRepository.CreateFile(stored.Response, returnBase64Source);
            }
            else
            {
                error.ErrorMessage = stored!=null&&!string.IsNullOrWhiteSpace(stored.ErrorMessage) ? stored.ErrorMessage : "Internal server error";
                return error;
            }
        }

        public async Task<GenericResponse<Doccument>> DeleteFile(long Id)
        {
            return await _fileRepository.DeleteFile(Id);
        }

        public async Task<GenericResponse<List<Doccument>>> GetFile(List<long> Id)
        {
            return await _fileRepository.GetFile(Id);
        }

        public async Task<GenericResponse<Doccument>> StoreFile(StoreMediaType place, Doccument media)
        {
            return await _fileRepository.StoreFile(place, media);
        }
    }
}

using Estructura.Common.Enums;
using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Repositories
{
    public interface IFileRepository
    {
        Task<GenericResponse<Doccument>> CreateFile(Doccument request, bool returnBase64Source = false);
        Task<GenericResponse<List<Doccument>>> GetFile(List<long> Id);
        Task<GenericResponse<Doccument>> DeleteFile(long Id);
        Task<GenericResponse<Doccument>> StoreFile(StoreMediaType place, Doccument media);
    }
}

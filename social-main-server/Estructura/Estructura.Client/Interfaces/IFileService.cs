using Estructura.Common.Models;
using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Client.Interfaces
{
    public interface IFileService
    {
        Task<GenericResponse<Doccument>> CreateFile(Doccument request);
    }
}

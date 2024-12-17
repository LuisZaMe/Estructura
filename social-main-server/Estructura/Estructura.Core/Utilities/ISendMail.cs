using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Core.Utilities
{
    public interface ISendMail
    {
        Task<GenericResponse<bool>> SendSingle(string to, string body, string subject);
    }
}

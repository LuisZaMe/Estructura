using Estructura.Common.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Estructura.Client.Utilities
{
    public static class HttpErrorParser
    {
        public static async System.Threading.Tasks.Task<GenericResponse<T>> ErrorParser<T>(HttpResponseMessage error)
        {
            try
            {
                return await error.Content.ReadAsObjectAsync<GenericResponse<T>>();
            }
            catch(Exception exc)
            {
                return new GenericResponse<T>() { StatusCode = error.StatusCode, ErrorMessage = exc.Message };
            }
        }
    }
}

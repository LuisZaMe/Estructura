using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Response
{
    public class ResponseBaseModel
    {
        public bool Sucess { get; set; } = false;
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

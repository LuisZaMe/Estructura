using Estructura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class ServiceType
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public ServiceTypes Service { get; set; }
    }
}

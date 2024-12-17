using System;
using System.Collections.Generic;
using System.Text;

namespace Estructura.Common.Models
{
    public class PaymentMethod
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Common.Enums.PaymentMethods Method { get; set; }
    }
}
